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
    public class BossModelComponent : ModelGameComponent
    {
        #region フィールド

        private BoundingBox boundingBox;

        public BoundingBox BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        public Vector3 BoundingBoxMin
        {
            get { return boundingBox.Min; }
            set { boundingBox.Min = value; }
        }

        public Vector3 BoundingBoxMax
        {
            get { return boundingBox.Max; }
            set { boundingBox.Max = value; }
        }

        public float BoundingBoxMinX
        {
            get { return boundingBox.Min.X; }
            set { boundingBox.Min.X = value; }
        }

        public float BoundingBoxMinZ
        {
            get { return boundingBox.Min.Z; }
            set { boundingBox.Min.Z = value; }
        }

        public float BoundingBoxMaxX
        {
            get { return boundingBox.Max.X; }
            set { boundingBox.Max.X = value; }
        }

        public float BoundingBoxMaxZ
        {
            get { return boundingBox.Max.Z; }
            set { boundingBox.Max.Z = value; }
        }

        /// <summary>
        /// ボスの前後を見るバウンディングボックス
        /// </summary>
        private BoundingBox frontAndBackBox;

        public BoundingBox FrontAndBackBox
        {
            get { return frontAndBackBox; }
            set { frontAndBackBox = value; }
        }

        public Vector3 FrontAndBackBoxMin
        {
            get { return frontAndBackBox.Min; }
            set { frontAndBackBox.Min = value; }
        }

        public Vector3 FrontAndBackBoxMax
        {
            get { return frontAndBackBox.Max; }
            set { frontAndBackBox.Max = value; }
        }

        public float FrontAndBackBoxMinX
        {
            get { return frontAndBackBox.Min.X; }
            set { frontAndBackBox.Min.X = value; }
        }

        public float FrontAndBackBoxMaxX
        {
            get { return frontAndBackBox.Max.X; }
            set { frontAndBackBox.Max.X = value; }
        }

        /// <summary>
        /// ボスの左右を見るボックス
        /// </summary>
        private BoundingBox leftAndRightBox;

        public BoundingBox LeftAndRightBox
        {
            get { return leftAndRightBox; }
            set { leftAndRightBox = value; }
        }

        public Vector3 LeftAndRightBoxMin
        {
            get { return leftAndRightBox.Min; }
            set { leftAndRightBox.Min = value; }
        }

        public Vector3 LeftAndRightBoxMax
        {
            get { return leftAndRightBox.Max; }
            set { leftAndRightBox.Max = value; }
        }

        public float LeftAndRightBoxMinZ
        {
            get { return leftAndRightBox.Min.Z; }
            set { leftAndRightBox.Min.Z = value; }
        }

        public float LeftAndRightBoxMaxZ
        {
            get { return leftAndRightBox.Max.Z; }
            set { leftAndRightBox.Max.Z = value; }
        }

        /// <summary>
        /// ボスの向き
        /// </summary>
        private float angle;

        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        /// <summary>
        /// プレイヤーの位置保存用
        /// </summary>
        private Vector3 playerPos;

        public Vector3 PlayerPos
        {
            get { return playerPos; }
            set { playerPos = value; }
        }

        /// <summary>
        /// プレイヤーとのあたり判定
        /// </summary>
        private bool hit;

        public bool Hit
        {
            get { return hit; }
            set { hit = value; }
        }

        /// <summary>
        /// 前突進中か
        /// </summary>
        private bool frontRush;

        public bool FrontRush
        {
            get { return frontRush; }
            set { frontRush = value; }
        }

        /// <summary>
        /// 左右突進中か
        /// </summary>
        private bool leftAndRightRush;

        public bool LeftAndRightRush
        {
            get { return leftAndRightRush; }
            set { leftAndRightRush = value; }
        }

        /// <summary>
        /// ボスの前進行方向
        /// </summary>
        private bool reverse;

        public bool Reverse
        {
            get { return reverse; }
            set { reverse = value; }
        }

        /// <summary>
        /// 左方向か右方向か
        /// </summary>
        private bool leftOrRight;

        public bool LeftOrRight
        {
            get { return leftOrRight; }
            set { leftOrRight = value; }
        }

        /// <summary>
        /// 壁に当たりワープしたか
        /// </summary>
        private bool respawn;

        public bool Respawn
        {
            get { return respawn; }
            set { respawn = value; }
        }

        /// <summary>
        /// 無敵中か
        /// </summary>
        private bool invincible;

        public bool Invincible
        {
            get { return invincible; }
            set { invincible = value; }
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
        /// ボスの基本スピード
        /// </summary>
        private const float speed = 4.0f;
        public float Speed
        {
            get { return speed; }
        }

        /// <summary>
        /// 突進時のスピード
        /// </summary>
        private const float rushSpeed = 12.0f;
        public float RushSpeed
        {
            get { return rushSpeed; }
        }

        /// <summary>
        /// 前突進までの時間(ゼロスタート)
        /// </summary>
        private float rushFrontTime = 0.0f;
        public float RushFrontTime
        {
            get { return rushFrontTime; }
            set { rushFrontTime = value; }
        }

        /// <summary>
        /// 左右突進までの時間(ゼロスタート)
        /// </summary>
        float rushLfetAndRightTime = 0.0f;
        public float RushLfetAndRightTime
        {
            get { return rushLfetAndRightTime; }
            set { rushLfetAndRightTime = value; }
        }

        /// <summary>
        /// 何秒たったら突進するか
        /// </summary>
        private const float rushStartTime = 2.0f;
        public float RushStartTime
        {
            get { return rushStartTime; }
        }

        /// <summary>
        /// 当たってからの時間
        /// </summary>
        private float hitTime = 5.0f;
        public float HitTime
        {
            get { return hitTime; }
            set { hitTime = value; }
        }

        /// <summary>
        /// 無敵中の時間
        /// </summary>
        private float invincibleTime = 5.0f;
        public float InvincibleTime
        {
            get { return invincibleTime; }
            set { invincibleTime = value; }
        }

        /// <summary>
        /// ワープ後無防備時間
        /// </summary>
        private float defenselessTime = 5.0f;
        public float DefenselessTime
        {
            get { return defenselessTime; }
            set { defenselessTime = value; }
        }

        /// <summary>
        /// フィールドの広さ
        /// </summary>
        private const int wall = 1600;
        public int Wall
        {
            get { return wall; }
        }

        /// <summary>
        /// 突進後プレイヤーの場所からどれくらいまで行くか
        /// </summary>
        private const int overPosition = 300;
        public int OverPosition
        {
            get { return overPosition; }
        }

        /// <summary>
        /// ボスの最大HP
        /// </summary>
        private const float maxHp = 3.0f;
        public float MaxHp
        {
            get { return maxHp; }
        }

        private const float damage = 1.0f;

        public float Damage
        {
            get { return damage; }
        }


        #endregion

        public BossModelComponent(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            boundingBox.Max = new Vector3(30.0f, 30.0f, 30.0f) * Scale + Position;
            boundingBox.Min = new Vector3(-30.0f, -40.0f, -30.0f) * Scale + Position;

            frontAndBackBox.Max = new Vector3(30.0f, 30.0f, wall) * Scale + Position;
            frontAndBackBox.Min = new Vector3(-30.0f, -40.0f, -wall) * Scale + Position;

            leftAndRightBox.Max = new Vector3(wall, 30.0f, 30.0f) * Scale + Position;
            leftAndRightBox.Min = new Vector3(-wall, -40.0f, -30.0f) * Scale + Position;

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }
    }
}
