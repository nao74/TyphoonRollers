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
    public enum playerState
    {
        longMode, shortMode
    };

    class Player
    {
        #region フィールド

        private static playerState mode;

        public playerState Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        private enum rayDistance
        {
            forword,right,backward,left
        };

        /// <summary>
        /// 通常時プレイヤーモデル
        /// </summary>
        private static Model[] playerModel;

        public Model PlayerModel
        {
            get { return playerModel[(int)playerState.longMode]; }
            set { playerModel[(int)playerState.longMode] = value; }
        }

        public Model PlayerMiniModel
        {
            get { return playerModel[(int)playerState.shortMode]; }
            set { playerModel[(int)playerState.shortMode] = value; }
        }

        /// <summary>
        /// プレイヤーモデルに格納されているボーンの行列
        /// </summary>
        private static Matrix[] transform;

        public Matrix[] Transform
        {
            get { return transform; }
            set { transform = value; }
        }

        private static Matrix[] miniTransform;

        public Matrix[] MiniTransform
        {
            get { return miniTransform; }
            set { miniTransform = value; }
        }

        /// <summary>
        /// ワールド変換行列
        /// </summary>
        private static Matrix world;

        public Matrix World
        {
            get { return world; }
            set { world = value; }
        }

        /// <summary>
        /// プレイヤーモデルのバウンディングボックス
        /// </summary>
        /// 
        private static BoundingBox trackingArea;

        public BoundingBox TrackingArea
        {
            get { return trackingArea; }
            set { trackingArea = value; }
        }

        private BoundingBox stageOut;

        private static Ray[] intersectRay;

        public Ray[] IntersectRay
        {
            get { return intersectRay; }
            set { intersectRay = value; }
        }

        private static Vector3[] intersectRayDirection;

        public Vector3[] IntersectRayDirection
        {
            get { return intersectRayDirection; }
            set { intersectRayDirection = value; }
        }

        /// <summary>
        /// プレイヤー位置
        /// </summary>
        private static Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// プレイヤー回転量
        /// </summary>
        private Vector3 rotation;

        /// <summary>
        /// プレイヤー拡大縮小
        /// </summary>
        private Vector3 scale;

        /// <summary>
        /// 行列
        /// </summary>
        private Matrix translationMatrix, scalingMatrix, rotationMatrix;

        /// <summary>
        /// 回転行列
        /// </summary>
        private Matrix rotationX, rotationY, rotationZ;

        /// <summary>
        /// カメラ回転角度
        /// </summary>
        private static Vector3 rot;

        public Vector3 Rot
        {
            get { return rot; }
            set { rot = value; }
        }

        private double elapsedTime;

        /// <summary>
        /// 右回転時の中心座標
        /// </summary>
        private Vector3 rightRotationPosition;

        /// <summary>
        /// 左回転時の中心座標
        /// </summary>
        private Vector3 leftRotationPosition;


        private Vector3 rightAnnulusRotation;

        private Vector3 leftAnnulusRotation;

        private static Vector3 rightBallRotation;

        public Vector3 RightBallRotation
        {
            get { return rightBallRotation; }
        }

        private static Vector3 leftBallRotation;

        public Vector3 LeftBallRotation
        {
            get { return leftBallRotation; }
        }

        /// <summary>
        /// 体力
        /// </summary>
        private static float hitPoint;

        public float HitPoint
        {
            get { return hitPoint; }
            set { hitPoint = value; }
        }

        /// <summary>
        /// プレイヤー形態
        /// </summary>
        private bool shortMode;

        public bool ShortMode
        {
            get { return shortMode; }
        }

        private bool longMode;

        public bool LongMode
        {
            get { return longMode; }
        }

        /// <summary>
        /// 無敵状態
        /// </summary>
        private static bool invincibleMode;

        public bool InvincibleMode
        {
            get { return invincibleMode; }
            set { invincibleMode = value; }
        }

        /// <summary>
        /// 攻撃状態
        /// </summary>
        private static bool attackMode;

        public bool AttackMode
        {
            get { return attackMode; }
        }

        /// <summary>
        /// 通常状態
        /// </summary>
        private static bool normalMode;

        public bool NormalMode
        {
            get { return normalMode; }
        }

        private bool annulus;

        private bool ball;

        private static bool rotationMode;

        public bool RotationMode
        {
            get { return rotationMode; }
        }

        private static bool rightRotation;

        public bool RightRotation
        {
            get { return rightRotation; }
        }

        private static bool leftRotation;

        public bool LeftRotation
        {
            get { return leftRotation; }
        }

        private float nowRadian;

        /// <summary>
        /// 当たり判定線本数
        /// </summary>
        private const int rayNum = 4;

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

        /// <summary>
        /// 回転量
        /// </summary>
        private const float rotationSpeed = 2.0f;

        /// <summary>
        /// 最大HP
        /// </summary>
        private const float maxHp = 5.0f;

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


        private const float maxRadian = 6.283f;

        /// <summary>
        /// デバッグ用
        /// </summary>
        public bool front;
        public bool side;
        public int kabe;


        #endregion

        #region コンストラクタ

        public Player()
        {
            playerModel = new Model[(int)playerState.shortMode + 1];
        }

        #endregion

        #region 初期化

        public void Initialize()
        {
            InitializeModel();

            hitPoint = maxHp;

            normalMode = true;

            rightAnnulusRotation = new Vector3(-annulusCenterPoint * scale.X, 0.0f, 0.0f);

            leftAnnulusRotation = new Vector3(annulusCenterPoint * scale.X, 0.0f, 0.0f);

            rightBallRotation = new Vector3(-ballCenterPoint * scale.X, 0.0f, 0.0f);

            leftBallRotation = new Vector3(ballCenterPoint * scale.X, 0.0f, 0.0f);

            // 右回転時の中心座標初期位置
            rightRotationPosition = rightAnnulusRotation;

            // 左回転時の中心座標初期位置
            leftRotationPosition = leftAnnulusRotation;
           
        }

        public void InitializeModel()
        {
            // ボーンの数だけコピーする
            Transform = new Matrix[PlayerModel.Bones.Count];
            PlayerModel.CopyAbsoluteBoneTransformsTo(Transform);

            miniTransform = new Matrix[PlayerMiniModel.Bones.Count];
            PlayerMiniModel.CopyAbsoluteBoneTransformsTo(miniTransform);

            // 位置、回転、スケールの初期化
            Position = Vector3.Zero;
            rotation = Vector3.Zero;
            scale = new Vector3(scaleVec, scaleVec, scaleVec);

            // ワールド変換行列の初期化
            World = Matrix.Identity;

            longMode = true;
            shortMode = false;

            stageOut.Min = new Vector3(-2.0f, 0.0f, -2.0f) * scale;
            stageOut.Max = new Vector3(2.0f, 0.0f, 2.0f) * scale;

            InitializeRay();
        }

        private void InitializeRay()
        {
            for (int i = 0; i < rayNum; i++)
            {
                intersectRay = new Ray[rayNum];
                intersectRayDirection = new Vector3[rayNum];
            }

            trackingArea.Min = new Vector3(-tracking, 0.0f, -tracking) * scale + position;
            trackingArea.Max = new Vector3(tracking, tracking, tracking) * scale + position;

            if (longMode)
            {
                for(int i = 0; i < rayNum; i++)
                {
                    switch(i)
                    {
                        case (int)rayDistance.forword:
                            intersectRay[i] = new Ray(new Vector3(-2.0f, 0.0f, -0.4f) * scale, Vector3.Right);
                            break;
                        case (int)rayDistance.right:
                            intersectRay[i] = new Ray(new Vector3(2.0f, 0.0f, -0.4f) * scale, Vector3.Backward);
                            break;
                        case (int)rayDistance.backward:
                            intersectRay[i] = new Ray(new Vector3(2.0f, 0.0f, 0.4f) * scale, Vector3.Right);
                            break;
                        case (int)rayDistance.left:
                            intersectRay[i] = new Ray(new Vector3(-2.0f, 0.0f, 0.4f) * scale, Vector3.Backward);
                            break;
                    }
                }

                DistanceUpdate();
                
            }
        }

        private void DistanceUpdate()
        {
            for (int i = 0; i < rayNum; i++)
            {
                switch (i)
                {
                    case (int)rayDistance.forword:
                        intersectRayDirection[(int)rayDistance.forword] = Vector3.Normalize(intersectRay[(int)rayDistance.right].Position - intersectRay[(int)rayDistance.forword].Position);
                        intersectRay[(int)rayDistance.forword].Direction = intersectRayDirection[(int)rayDistance.forword];
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
                        intersectRayDirection[(int)rayDistance.left] = Vector3.Normalize(intersectRay[(int)rayDistance.forword].Position - intersectRay[(int)rayDistance.left].Position);
                        intersectRay[(int)rayDistance.left].Direction = intersectRayDirection[(int)rayDistance.left];
                        break;
                }
            }
        }

        #endregion

        #region 更新処理

        public void Update(GameTime gameTime)
        {
            UpdateInput();

            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            if (invincibleMode)
            {
                if (elapsedTime > 1000)
                    normalMode = true;
            }


            UpdateMatrix();
        }

        private void UpdateInput()
        {
            // 移動量
            Vector3 trans = Vector3.Zero;

            Vector3 leftAnnulusTrans = Vector3.Zero;
            Vector3 rightAnnulusTrans = Vector3.Zero;
            Vector3 leftBallTrans = Vector3.Zero;
            Vector3 rightBallTrans = Vector3.Zero;

            // 回転量
            Rot = Vector3.Zero;
            // 計算用のベクトル(左スティック)
            Vector3 leftVec = Vector3.Zero;
            // 計算用のベクトル(右スティック)
            Vector3 rightVec = Vector3.Zero;
            // 角度
            Vector3 radian = Vector3.Zero;
            // 回転時の計算一時保存
            Vector3 temp = Vector3.Zero;

            Vector3 rotTemp = Vector3.Zero;

            // 左右スティックの入力を取得する
            Vector2 leftStick = Vector2.Zero;
            Vector2 rightStick = Vector2.Zero;

            leftStick = InputManager.GetThumbSticksLeft(PlayerIndex.One);
            rightStick = InputManager.GetThumbSticksRight(PlayerIndex.One);

            // 入力を実際移動する方向へ代入する
            leftVec.Z = leftStick.Y;

            rightVec.Z = rightStick.Y;

            // キー操作
            if (InputManager.IsKeyDown(Keys.W))
                leftVec.Z = 1.0f;

            if (InputManager.IsKeyDown(Keys.Up))
                rightVec.Z = 1.0f;

            

            //// Lトリガーを入力すると棒が短くなる
            //if (InputManager.IsJustButtonDown(PlayerIndex.One, Buttons.LeftShoulder) || InputManager.IsKeyDown(Keys.LeftShift))
            //{
            //    if (shortMode == false)
            //    {
            //        shortMode = true;

            //        longMode = false;

            //        InitializeBox();
            //    }
            //}

            //// Rトリガーを入力すると棒が長くなる
            //if (InputManager.IsJustButtonDown(PlayerIndex.One, Buttons.RightShoulder) || InputManager.IsKeyDown(Keys.RightShift))
            //{
            //    if (longMode == false)
            //    {
            //        longMode = true;

            //        shortMode = false;

            //        InitializeBox();
            //    }
            //}

            if (!InputManager.IsButtonDown(PlayerIndex.One, Buttons.LeftStick) && !InputManager.IsButtonDown(PlayerIndex.One, Buttons.RightStick))
            {
                annulus = true;
                ball = false;

                rightRotationPosition = rightAnnulusRotation;
                leftRotationPosition = leftAnnulusRotation;
            }
            else
            {
                ball = true;
                annulus = false;

                rightRotationPosition = rightBallRotation;
                leftRotationPosition = leftBallRotation;
            }

            if (!invincibleMode)
                normalMode = true;

            if (leftVec.Length() > 0.0f || rightVec.Length() > 0.0f)
            {
                // 前後進
                if (leftVec.Z == rightVec.Z)
                {
                    Vector3 forward = Vector3.Normalize(World.Forward);

                    trans += forward * leftVec.Z * moveSpeed;

                    UpdateCollision(trans);

                    if (StageCollision())
                    {
                        UpdateCollision(-trans);
                        trans = Vector3.Zero;
                        return;
                    }

                    rightRotationPosition += trans;
                    leftRotationPosition += trans;

                    rightAnnulusRotation += trans;
                    leftAnnulusRotation += trans;

                    rightBallRotation += trans;
                    leftBallRotation += trans;

                    rotationMode = false;
                }
                // 時計回り
                else if (leftVec.Length() > rightVec.Length())
                {
                    rotationMode = true;

                    radian.Y = MathHelper.ToRadians(leftVec.Z * rotationSpeed);

                    trans = RotationTrans(position, leftRotationPosition, radian) - position;

                    UpdateCollision(trans, leftRotationPosition, radian);

                    if (StageCollision())
                    {
                        UpdateCollision(-trans, leftRotationPosition, -radian);
                        radian = Vector3.Zero;
                        trans = Vector3.Zero;
                        return;
                    }

                    // プレイヤーの角度を加算
                    rotation.Y += -leftVec.Z * rotationSpeed;

                    if (annulus)
                    {
                        rightAnnulusRotation = RotationTrans(rightAnnulusRotation, leftAnnulusRotation, radian);

                        leftBallRotation = RotationTrans(leftBallRotation, leftAnnulusRotation, radian);
                        rightBallRotation = RotationTrans(rightBallRotation, leftAnnulusRotation, radian);

                        rightRotationPosition = rightAnnulusRotation;
                    }
                    else if (ball)
                    {
                        leftAnnulusRotation = RotationTrans(leftAnnulusRotation, leftBallRotation, radian);
                        rightAnnulusRotation = RotationTrans(rightAnnulusRotation, leftBallRotation, radian);

                        rightBallRotation = RotationTrans(rightBallRotation, leftBallRotation, radian);

                        rightRotationPosition = rightBallRotation;
                    }
                }
                // 反時計回り
                else if (rightVec.Length() > leftVec.Length())
                {
                    rotationMode = true;

                    radian.Y = MathHelper.ToRadians(-rightVec.Z * rotationSpeed);

                    trans = RotationTrans(position, rightRotationPosition, radian) - position;

                    UpdateCollision(trans, rightRotationPosition, radian);

                    if (StageCollision())
                    {
                        UpdateCollision(-trans, rightRotationPosition, -radian);
                        radian = Vector3.Zero;
                        trans = Vector3.Zero;
                        return;
                    }

                    // プレイヤーの角度を加算
                    rotation.Y += rightVec.Z * rotationSpeed;

                    if (annulus)
                    {
                        leftAnnulusRotation = RotationTrans(leftAnnulusRotation, rightAnnulusRotation, radian);

                        rightBallRotation = RotationTrans(rightBallRotation, rightAnnulusRotation, radian);
                        leftBallRotation = RotationTrans(leftBallRotation, rightAnnulusRotation, radian);

                        leftRotationPosition = leftAnnulusRotation;
                    }
                    else if (ball)
                    {
                        rightAnnulusRotation = RotationTrans(rightAnnulusRotation, rightBallRotation, radian);
                        leftAnnulusRotation = RotationTrans(leftAnnulusRotation, rightBallRotation, radian);

                        leftBallRotation = RotationTrans(leftBallRotation, rightBallRotation, radian);

                        leftRotationPosition = leftBallRotation;
                    }
                }
            }
            else
            {
                nowRadian = 0;
                rotationMode = false;
            }

            // カメラの角度を加算
            Rot = -radian;

            nowRadian += Math.Abs(radian.Y);

            if (nowRadian >= maxRadian)
            {
                rotationMode = false;
            }

            position += trans;
        }

        // コリジョン回転移動
        private void UpdateCollision(Vector3 t,Vector3 center,Vector3 radian)
        {
            for (int i = 0; i < rayNum; i++)
                intersectRay[i].Position = RotationTrans(intersectRay[i].Position, center, radian);

            DistanceUpdate();
            
            trackingArea.Min += t;
            trackingArea.Max += t;

            stageOut.Min += t;
            stageOut.Max += t;
        }

        // コリジョン前後退
        private void UpdateCollision(Vector3 t)
        {
            for (int i = 0; i < rayNum; i++)
                intersectRay[i].Position += t;

            DistanceUpdate();

            trackingArea.Min += t;
            trackingArea.Max += t;

            stageOut.Min += t;
            stageOut.Max += t;
        }

        // 回転計算
        private Vector3 RotationTrans(Vector3 posA, Vector3 posB, Vector3 radian)
        {
            Vector3 ans = Vector3.Zero;

            ans.X = (posA.X - posB.X) * (float)Math.Cos(radian.Y) - (posA.Z - posB.Z) * (float)Math.Sin(radian.Y) + posB.X;
            ans.Z = (posA.X - posB.X) * (float)Math.Sin(radian.Y) + (posA.Z - posB.Z) * (float)Math.Cos(radian.Y) + posB.Z;

            return ans;
        }

        private bool StageCollision()
        {
            //if (Stage.box.Contains(stageOut) != ContainmentType.Contains)
            //    return true;

            for(int i = 0; i < Stage.boxNum; i++)
            {
                for(int j = 0; j < rayNum; j++)
                {
                    switch (j)
                    {
                        case (int)rayDistance.forword:
                        case (int)rayDistance.backward:
                            if (Stage.box[i].Intersects(intersectRay[j]) < 120)
                            {
                                front = true;
                                kabe = i;
                                return true;
                            }
                            else
                            {
                                kabe = 0;
                                front = false;
                            }
                            break;
                        case (int)rayDistance.right:
                        case (int)rayDistance.left:
                            if (Stage.box[i].Intersects(intersectRay[j]) < 24)
                            {
                                side = true;
                                kabe = i;
                                return true;
                            }
                            else
                            {
                                kabe = 0;
                                side = false;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            return false;
        }

        private void UpdateMatrix()
        {
            // 平行移動行列の計算
            translationMatrix = Matrix.CreateTranslation(Position);

            // 拡大縮小行列の計算
            scalingMatrix = Matrix.CreateScale(scale);

            // X軸中心の回転行列を計算する
            rotationX = Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X));

            // Y軸中心の回転行列を計算する
            rotationY = Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y));

            // Z軸中心の回転行列を計算する
            rotationZ = Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z));

            // 回転行列の合成
            rotationMatrix = rotationX * rotationY * rotationZ;

            // ワールド変換行列を計算する
            // モデルを拡大縮小し、回転させたあと移動させる
            World = scalingMatrix * rotationMatrix * translationMatrix;
        }

        // Ray当たり判定
        public bool Intersects(BoundingBox box)
        {
            for(int i = 0; i < rayNum; i++)
            {
                switch(i)
                {
                    case (int)rayDistance.forword:
                    case (int)rayDistance.backward:
                        if (intersectRay[i].Intersects(box) < 120)
                        {
                            return true;
                        }
                        break;
                    case (int)rayDistance.right:
                    case (int)rayDistance.left:
                        if (intersectRay[i].Intersects(box) < 24)
                        {
                            return true;
                        }
                        break;
                }
            }
            return false;
        }

        #endregion
    }
}
