using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace TyphoonRollers
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class PlayerModelComponent : ModelGameComponent
    {
        #region フィールド

        enum rayDistance
        {
            forward,right,backward,left
        };

        public Vector3 TrackingAreaMin
        {
            get { return trackingArea.Min; }
            set { trackingArea.Min = value; }
        }
        public Vector3 TrackingAreaMax
        {
            get { return trackingArea.Max; }
            set { trackingArea.Max = value; }
        }
        public BoundingBox TrackingArea
        {
            get { return trackingArea; }
            set { trackingArea = value; }
        }
        private BoundingBox trackingArea;

        public Ray[] IntersectRay
        {
            get { return intersectRay; }
            set { intersectRay = value; }
        }
        private Ray[] intersectRay;

        public Vector3[] IntersectRayDirection
        {
            get { return intersectRayDirection; }
            set { intersectRayDirection = value; }
        }
        private Vector3[] intersectRayDirection;

        public Vector3 RightRotationPosition
        {
            get { return rightRotationPosition; }
            set { rightRotationPosition = value; }
        }
        private Vector3 rightRotationPosition;

        public Vector3 LeftRotationPosition
        {
            get { return leftRotationPosition; }
            set { leftRotationPosition = value; }
        }
        private Vector3 leftRotationPosition;

        public Vector3 RightAnnulusRotation
        {
            get { return rightAnnulusRotation; }
            set { rightAnnulusRotation = value; }
        }
        private Vector3 rightAnnulusRotation;

        public Vector3 LeftAnnulusRotation
        {
            get { return leftAnnulusRotation; }
            set { leftAnnulusRotation = value; }
        }
        private Vector3 leftAnnulusRotation;

        public Vector3 RightBallRotation
        {
            get { return rightBallRotation; }
            set { rightBallRotation = value; }
        }
        private Vector3 rightBallRotation;

        public Vector3 LeftBallRotation
        {
            get { return leftBallRotation; }
            set { leftBallRotation = value; }
        }
        private Vector3 leftBallRotation;

        public bool Annulus
        {
            get { return annulus; }
            set { annulus = value; }
        }
        private bool annulus;

        public bool Ball
        {
            get { return ball; }
            set { ball = value; }
        }
        private bool ball;

        public bool RotationMode
        {
            get { return rotationMode; }
            set { rotationMode = value; }
        }
        private bool rotationMode;

        public bool ForwardRotation
        {
            get { return forwardRotation; }
            set { forwardRotation = value; }
        }
        private bool forwardRotation;

        public bool BackwardRotation
        {
            get { return backwardRotation;}
            set { backwardRotation = value; }
        }
        private bool backwardRotation;

        public bool BackwardHit
        {
            get { return backwardHit; }
            set { backwardHit = value; }
        }
        private bool backwardHit;

        public bool ForwardHit
        {
            get { return forwardHit; }
            set { forwardHit = value; }
        }
        private bool forwardHit;

        /// <summary>
        /// 回転量
        /// </summary>
        private float rotationSpeed;
        public float RotationSpeed
        {
            get { return rotationSpeed; }
            set { rotationSpeed = value; }
        }


        /// <summary>
        /// 現在の回転角度
        /// </summary>
        private float nowRadian;

        public float NowRadian
        {
            get { return nowRadian; }
            set { nowRadian = value; }
        }

        /// <summary>
        /// 当たり判定線本数
        /// </summary>
        private const int rayNum = 4;
        public int RayNum
        {
            get { return rayNum; }
        }

        /// <summary>
        /// アニュラス回転
        /// </summary>
        private const float annulusCenterPoint = 5.0f;

        /// <summary>
        /// 球中心回転
        /// </summary>
        private const float ballCenterPoint = 1.6f;

        /// <summary>
        /// 移動スピード
        /// </summary> 
        private const float moveSpeed = 5.0f;
        public float MoveSpeed
        {
            get { return moveSpeed; }
        }

        /// <summary>
        /// 攻撃状態の入力しきい値
        /// </summary>
        private const float attackThreshold = 0.5f;

        /// <summary>
        /// スケール定数値
        /// </summary>
        private const float scaleVec = 30.0f;

        /// <summary>
        /// 追尾範囲
        /// </summary>
        private const float tracking = 8.3f;

        private Vector3 bossScenePlayerPosition = new Vector3(0, 0, 500.0f);

        public Vector3 BossScenePlayerPosition
        {
            get { return bossScenePlayerPosition; }
        }

        /// <summary>
        /// 回転速度：速い
        /// </summary>
        private const float rotationSpeedIsFast = 3.5f;
        public float RotationSpeedIsFast
        {
            get { return rotationSpeedIsFast; }
        }

        /// <summary>
        /// 回転速度：通常
        /// </summary>
        private const float rotationSpeedIsNormal = 2.0f;
        public float RotationSpeedIsNormal
        {
            get { return rotationSpeedIsNormal; }
        }

        private const float maxRadian = 6.283f;
        public float MaxRadian
        {
            get { return maxRadian; }
        }

        #endregion

        public PlayerModelComponent(Game game)
            : base(game)
        {
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            rightAnnulusRotation = new Vector3(-annulusCenterPoint * Scale.X, 0.0f, 0.0f) + Position;
            leftAnnulusRotation = new Vector3(annulusCenterPoint * Scale.X, 0.0f, 0.0f) + Position;
            rightBallRotation = new Vector3(-ballCenterPoint * Scale.X, 0.0f, 0.0f) + Position;
            leftBallRotation = new Vector3(ballCenterPoint * Scale.X, 0.0f, 0.0f) + Position;

            // 右回転時の中心座標初期位置
            rightRotationPosition = rightAnnulusRotation;
            // 左回転時の中心座標初期位置
            leftRotationPosition = leftAnnulusRotation;

            InitializeRay();

            base.Initialize();
        }

        private void InitializeRay()
        {
            for (int i = 0; i < rayNum; i++)
            {
                intersectRay = new Ray[rayNum];
                intersectRayDirection = new Vector3[rayNum];
            }

            trackingArea.Min = new Vector3(-tracking, 0.0f, -tracking) * Scale + Position;
            trackingArea.Max = new Vector3(tracking, tracking, tracking) * Scale + Position;

            for (int i = 0; i < rayNum; i++)
            {
                switch (i)
                {
                    case (int)rayDistance.forward:
                        intersectRay[i] = new Ray(new Vector3(-2.0f, 0.0f, -0.4f) * Scale + Position, Vector3.Right);
                        break;
                    case (int)rayDistance.right:
                        intersectRay[i] = new Ray(new Vector3(2.0f, 0.0f, -0.4f) * Scale + Position, Vector3.Backward);
                        break;
                    case (int)rayDistance.backward:
                        intersectRay[i] = new Ray(new Vector3(2.0f, 0.0f, 0.4f) * Scale + Position, Vector3.Right);
                        break;
                    case (int)rayDistance.left:
                        intersectRay[i] = new Ray(new Vector3(-2.0f, 0.0f, 0.4f) * Scale + Position, Vector3.Backward);
                        break;
                }
            }

            DistanceUpdate();  
        }

        public void DistanceUpdate()
        {
            for (int i = 0; i < rayNum; i++)
            {
                switch (i)
                {
                    case (int)rayDistance.forward:
                        intersectRayDirection[(int)rayDistance.forward] = Vector3.Normalize(intersectRay[(int)rayDistance.right].Position - intersectRay[(int)rayDistance.forward].Position);
                        intersectRay[(int)rayDistance.forward].Direction = intersectRayDirection[(int)rayDistance.forward];
                        break;
                    case (int)rayDistance.right:
                        intersectRayDirection[(int)rayDistance.right] = Vector3.Normalize(intersectRay[(int)rayDistance.backward].Position - intersectRay[(int)rayDistance.right].Position);
                        intersectRay[(int)rayDistance.right].Direction = intersectRayDirection[(int)rayDistance.right];
                        break;
                    case (int)rayDistance.backward:
                        intersectRayDirection[(int)rayDistance.backward] = Vector3.Normalize(intersectRay[(int)rayDistance.left].Position - intersectRay[(int)rayDistance.backward].Position);
                        intersectRay[(int)rayDistance.backward].Direction = intersectRayDirection[(int)rayDistance.backward];
                        break;
                    case (int)rayDistance.left:
                        intersectRayDirection[(int)rayDistance.left] = Vector3.Normalize(intersectRay[(int)rayDistance.forward].Position - intersectRay[(int)rayDistance.left].Position);
                        intersectRay[(int)rayDistance.left].Direction = intersectRayDirection[(int)rayDistance.left];
                        break;
                }
            }
        }
    }
}
