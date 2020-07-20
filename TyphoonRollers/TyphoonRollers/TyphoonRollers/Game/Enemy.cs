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

    class EnemyState
    {
        /// <summary>
        /// エネミーモデル
        /// </summary>
        private static Model enemyModel;

        public Model EnemyModel
        {
            get { return enemyModel; }
            set { enemyModel = value; }
        }

        /// <summary>
        /// エネミーモデルに格納されているボーンの行列
        /// </summary>
        private static Matrix[] transform;

        public Matrix[] Transform
        {
            get { return transform; }
            set { transform = value; }
        }

        /// <summary>
        /// ワールド変換行列
        /// </summary>
        private Matrix world;

        public Matrix World
        {
            get { return world; }
            set { world = value; }
        }

        /// <summary>
        /// エネミーモデルのバウンディングボックス
        /// </summary>
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
        /// エネミー位置
        /// </summary>
        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float PositionX
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public float PositionY
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public float PositionZ
        {
            get { return position.Z; }
            set { position.Z = value; }
        }

        private  bool hit;

        public bool Hit
        {
            get { return hit; }
            set { hit = value; }
        }

        private bool reverse;

        public bool Reverse
        {
            get { return reverse; }
            set { reverse = value; }
        }

        //追加
        private float trackingPlayerArea;

        public float TrackingPlayerArea
        {
            get { return trackingPlayerArea; }
            set { trackingPlayerArea = value; }
        }

        // コンストラクタ
        public EnemyState() { }

    }

    class EnemyPop
    {
        private static Vector3[] position;

        public Vector3[] Position
        {
            get { return position; }
        }

        private const int enemyNum = 10;

        public EnemyPop()
        {
            position = new Vector3[enemyNum];

            position[0] = new Vector3(-200.0f, 0.0f, -200.0f);

            position[1] = new Vector3(200.0f, 0.0f, 400.0f);

            position[2] = new Vector3(-200.0f, 0.0f, -300.0f);

            position[3] = new Vector3(300.0f, 0.0f, 300.0f);

            position[4] = new Vector3(-600.0f, 0.0f, -400.0f);

            position[5] = new Vector3(700.0f, 0.0f, 400.0f);

            position[6] = new Vector3(-500.0f, 0.0f, -500.0f);

            position[7] = new Vector3(100.0f, 0.0f, 700.0f);

            position[8] = new Vector3(-700.0f, 0.0f, -100.0f);

            position[9] = new Vector3(400.0f, 0.0f, 700.0f);
        }
    }

    class Enemy
    {
        #region フィールド

        private static int enemyNumber = 10;

        public int EnemyNumber
        {
            get { return enemyNumber; }
        }

        private int enemyMax = 10;//敵残りカウント

        private const int spawnLimitationNum = 9;

        private int[] spawnLimitation;

        private Random rand = new System.Random();

        private Vector3[] direction;

        private Vector3 trans;

        private Vector3 scale;

        private Player player;

        private EnemyPop enemyPop;

        public EnemyState[] enemyState;

        public bool[] hitEffect;

        public Vector3[] effectPos;

        private const float damage = 1.0f;

        //敵のスピード
        //private const float moveSpeed0 = 3.0f;
        //private const float moveSpeed1 = 3.0f;
        //private const float moveSpeed2 = 4.0f;
        //private const float moveSpeed3 = 4.0f;
        //private const float moveSpeed4 = 4.0f;
        //private const float moveSpeed5 = 2.5f;
        //private const float moveSpeed6 = 2.5f;
        //private const float moveSpeed7 = 2.5f; // case7にて二倍
        //private const float moveSpeed8 = 2.5f; // case8にて二倍
        //private const float moveSpeed9 = 3.0f; // case9にて七倍

        private const float moveSpeed0 = 3.0f;
        private const float moveSpeed1 = 3.0f;
        private const float moveSpeed2 = 3.0f;
        private const float moveSpeed3 = 3.0f;
        private const float moveSpeed4 = 3.0f;
        private const float moveSpeed5 = 3.0f;
        private const float moveSpeed6 = 3.0f;
        private const float moveSpeed7 = 3.0f; // case7にて二倍
        private const float moveSpeed8 = 3.0f; // case8にて二倍
        private const float moveSpeed9 = 3.0f; // case9にて七倍

        private const float moveLimit4 = 200.0f;
        private const float moveLimit5 = 200.0f;
        private const float moveLimit6 = 400.0f;
        private const float moveLimit7 = 800.0f;

        private const int spawnArea = 800;

        float a;

        #endregion

        public Enemy() { }

        public void Initialize()
        {
            player = new Player();

            enemyPop = new EnemyPop();

            enemyState = new EnemyState[EnemyNumber];

            for (int i = 0; i < EnemyNumber; i++)
            {
                enemyState[i] = new EnemyState();
                hitEffect = new bool[enemyNumber];
                effectPos = new Vector3[enemyNumber];
            }

            direction = new Vector3[10];

            spawnLimitation = new int[spawnLimitationNum];

            for (int i = 0; i < spawnLimitationNum; i++)
                spawnLimitation[i] = 0;
        }

        public void InitializeModel()
        {
            scale = new Vector3(5.0f, 5.0f, 5.0f);

            for (int i = 0; i < enemyNumber; i++)
            {
                enemyState[i].Position = enemyPop.Position[i];
                enemyState[i].Transform = new Matrix[enemyState[i].EnemyModel.Bones.Count];
                enemyState[i].EnemyModel.CopyAbsoluteBoneTransformsTo(enemyState[i].Transform);
                enemyState[i].World = Matrix.Identity;
                InitializeBox(i);
                //敵の追尾範囲
                enemyState[i].TrackingPlayerArea = 150.0f;
            }
        }

        private void InitializeBox(int i)
        {
            enemyState[i].BoundingBoxMax = new Vector3(3.577f, 11.655f, 1.744f) * scale + enemyState[i].Position;
            enemyState[i].BoundingBoxMin = new Vector3(-3.277f, 0.0f, -1.571f) * scale + enemyState[i].Position;
        }

        public void Movement(float delta)
        {
            float[] forword = new float[enemyNumber];

            for (int i = 0; i < enemyNumber; i++)
            {
                if (enemyState[i].Hit)
                {
                    Vector3 rnd = Vector3.Zero;
                    bool br = false;

                    while (true)
                    {
                        rnd.X = rand.Next(-spawnArea, spawnArea);
                        rnd.Z = rand.Next(-spawnArea, spawnArea);

                        enemyState[i].Position = rnd;
                        InitializeBox(i);

                        for (int j = 0; j < enemyNumber; j++)
                        {
                            if (!enemyState[i].BoundingBox.Intersects(enemyState[j].BoundingBox))
                                br = true;
                        }

                        if (br)
                            break;
                    }
                    enemyState[i].Hit = false;
                }


                if (player.Intersects(enemyState[i].BoundingBox))
                {
                    if (!enemyState[i].Hit)
                        enemyMax--;

                    enemyState[i].Hit = true;

                    hitEffect[i] = true;

                    effectPos[i] = enemyState[i].Position;

                    player.InvincibleMode = true;

                    if (!player.RotationMode)
                        player.HitPoint -= damage;
                }
                else
                {
                    direction[i] = player.Position - enemyState[i].Position;

                    direction[i].Normalize();

                    switch (i)
                    {
                        case 0:
                            trans = direction[i] * moveSpeed0;

                            enemyState[i].Position += trans;
                            enemyState[i].BoundingBoxMax += trans;
                            enemyState[i].BoundingBoxMin += trans;

                            EnemyHitCheck(i, trans);
                            break;
                        case 1:
                            trans = direction[i] * moveSpeed1;

                            enemyState[i].Position += trans;
                            enemyState[i].BoundingBoxMax += trans;
                            enemyState[i].BoundingBoxMin += trans;

                            EnemyHitCheck(i, trans);
                            break;
                        case 2:
                            trans = direction[i] * moveSpeed2;

                            enemyState[i].Position += trans;
                            enemyState[i].BoundingBoxMax += trans;
                            enemyState[i].BoundingBoxMin += trans;

                            EnemyHitCheck(i, trans);
                            break;
                        case 3:
                            trans = direction[i] * moveSpeed3;

                            enemyState[i].Position += trans;
                            enemyState[i].BoundingBoxMax += trans;
                            enemyState[i].BoundingBoxMin += trans;

                            EnemyHitCheck(i, trans);
                            break;
                        case 4:
                            if (enemyState[i].BoundingBox.Intersects(player.TrackingArea))
                            {
                                trans = direction[i] * moveSpeed4;

                                enemyState[i].Position += trans;
                                enemyState[i].BoundingBoxMax += trans;
                                enemyState[i].BoundingBoxMin += trans;

                                EnemyHitCheck(i, trans);
                            }
                            else
                            {
                                if (enemyState[i].Position.X >= moveLimit4)
                                {
                                    enemyState[i].PositionX = moveLimit4;
                                    enemyState[i].BoundingBoxMaxX = moveLimit4;
                                    enemyState[i].BoundingBoxMinX = moveLimit4;

                                    enemyState[i].Reverse = true;
                                }
                                else if (enemyState[i].Position.X <= -moveLimit4)
                                {
                                    enemyState[i].PositionX = -moveLimit4;
                                    enemyState[i].BoundingBoxMaxX = -moveLimit4;
                                    enemyState[i].BoundingBoxMinX = -moveLimit4;

                                    enemyState[i].Reverse = false;
                                }

                                if (enemyState[i].Reverse)
                                {
                                    enemyState[i].PositionX -= moveSpeed4;
                                    enemyState[i].BoundingBoxMaxX -= moveSpeed4;
                                    enemyState[i].BoundingBoxMinX -= moveSpeed4;
                                }
                                else
                                {
                                    enemyState[i].PositionX += moveSpeed4;
                                    enemyState[i].BoundingBoxMaxX += moveSpeed4;
                                    enemyState[i].BoundingBoxMinX += moveSpeed4;
                                }
                            }
                            break;
                        case 5:
                            if (enemyState[i].BoundingBox.Intersects(player.TrackingArea))
                            {
                                trans = direction[i] * moveSpeed5;

                                enemyState[i].Position += trans;
                                enemyState[i].BoundingBoxMax += trans;
                                enemyState[i].BoundingBoxMin += trans;

                                EnemyHitCheck(i, trans);
                            }
                            else
                            {
                                if (enemyState[i].Position.Z >= moveLimit5)
                                {
                                    enemyState[i].PositionZ = moveLimit5;
                                    enemyState[i].BoundingBoxMaxZ = moveLimit5;
                                    enemyState[i].BoundingBoxMinZ = moveLimit5;

                                    enemyState[i].Reverse = true;
                                }
                                else if (enemyState[i].Position.Z <= -moveLimit5)
                                {
                                    enemyState[i].PositionZ = -moveLimit5;
                                    enemyState[i].BoundingBoxMaxZ = -moveLimit5;
                                    enemyState[i].BoundingBoxMinZ = -moveLimit5;

                                    enemyState[i].Reverse = false;
                                }

                                if (enemyState[i].Reverse)
                                {
                                    enemyState[i].PositionZ -= moveSpeed5;
                                    enemyState[i].BoundingBoxMaxZ -= moveSpeed5;
                                    enemyState[i].BoundingBoxMinZ -= moveSpeed5;
                                }
                                else
                                {
                                    enemyState[i].PositionZ += moveSpeed5;
                                    enemyState[i].BoundingBoxMaxZ += moveSpeed5;
                                    enemyState[i].BoundingBoxMinZ += moveSpeed5;
                                }
                            }
                            break;
                        case 6:
                            if (enemyState[i].BoundingBox.Intersects(player.TrackingArea))
                            {
                                trans = direction[i] * moveSpeed6;

                                enemyState[i].Position += trans;
                                enemyState[i].BoundingBoxMax += trans;
                                enemyState[i].BoundingBoxMin += trans;

                                EnemyHitCheck(i, trans);
                            }
                            else
                            {
                                if (enemyState[i].Position.X >= moveLimit6)
                                {
                                    enemyState[i].PositionX = moveLimit6;
                                    enemyState[i].BoundingBoxMaxX = moveLimit6;
                                    enemyState[i].BoundingBoxMinX = moveLimit6;

                                    enemyState[i].Reverse = true;
                                }
                                else if (enemyState[i].Position.X <= -moveLimit6)
                                {
                                    enemyState[i].PositionX = -moveLimit6;
                                    enemyState[i].BoundingBoxMaxX = -moveLimit6;
                                    enemyState[i].BoundingBoxMinX = -moveLimit6;

                                    enemyState[i].Reverse = false;
                                }

                                if (enemyState[i].Reverse)
                                {
                                    enemyState[i].PositionX -= moveSpeed6;
                                    enemyState[i].BoundingBoxMaxX -= moveSpeed6;
                                    enemyState[i].BoundingBoxMinX -= moveSpeed6;
                                }
                                else
                                {
                                    enemyState[i].PositionX += moveSpeed6;
                                    enemyState[i].BoundingBoxMaxX += moveSpeed6;
                                    enemyState[i].BoundingBoxMinX += moveSpeed6;
                                }
                            }
                            break;
                        case 7:
                            if (enemyState[i].BoundingBox.Intersects(player.TrackingArea))
                            {
                                trans = direction[i] * moveSpeed7;

                                enemyState[i].Position += trans;
                                enemyState[i].BoundingBoxMax += trans;
                                enemyState[i].BoundingBoxMin += trans;

                                EnemyHitCheck(i, trans);
                            }
                            else
                            {
                                if (enemyState[i].Position.Z >= moveLimit7)
                                {
                                    enemyState[i].PositionZ = moveLimit7;
                                    InitializeBox(i);

                                    enemyState[i].Reverse = true;
                                }
                                else if (enemyState[i].Position.Z <= -moveLimit7)
                                {
                                    enemyState[i].PositionZ = -moveLimit7;
                                    InitializeBox(i);

                                    enemyState[i].Reverse = false;
                                }

                                if (enemyState[i].Reverse)
                                {
                                    enemyState[i].PositionZ -= moveSpeed7;
                                    enemyState[i].BoundingBoxMaxZ -= moveSpeed7;
                                    enemyState[i].BoundingBoxMinZ -= moveSpeed7;
                                }
                                else
                                {
                                    enemyState[i].PositionZ += moveSpeed7;
                                    enemyState[i].BoundingBoxMaxZ += moveSpeed7;
                                    enemyState[i].BoundingBoxMinZ += moveSpeed7;
                                }
                            }
                            break;
                        case 8:
                            if (enemyState[i].BoundingBox.Intersects(player.TrackingArea))
                            {
                                trans = direction[i] * moveSpeed8;

                                enemyState[i].Position += trans;
                                enemyState[i].BoundingBoxMax += trans;
                                enemyState[i].BoundingBoxMin += trans;

                                EnemyHitCheck(i, trans);
                            }
                            else
                            {
                                trans = direction[i] * moveSpeed8;

                                enemyState[i].Position += trans;
                                enemyState[i].BoundingBoxMax += trans;
                                enemyState[i].BoundingBoxMin += trans;
                            }
                            break;
                        case 9:
                            trans = direction[i] * moveSpeed9;

                            enemyState[i].Position += trans;
                            enemyState[i].BoundingBoxMax += trans;
                            enemyState[i].BoundingBoxMin += trans;

                            EnemyHitCheck(i, trans);
                            break;
                        default:
                            break;
                    }
                }

                Score.Scoring(player.RotationMode, enemyState[i].Hit);

                enemyState[i].World = Matrix.CreateRotationY((float)Math.Atan2(direction[i].X, direction[i].Z)) * Matrix.CreateScale(scale) * Matrix.CreateTranslation(enemyState[i].Position);


                // ランダム位置リスポーン


                // 指定位置リスポーン
                //if (enemyState[i].Hit)
                //{
                //    bool br = false;

                //    while (true)
                //    {
                //        r = rand.Next(9);

                //        for(int j = 0;j < currentSpawnLimitation; j++)
                //        {
                //            if (spawnLimitation[j] != r)
                //                br = true;
                //        }

                //        if (br)
                //            break;
                //    }

                //    if(currentSpawnLimitation < spawnLimitationNum)
                //    {
                //        spawnLimitation[currentSpawnLimitation] = r;
                //        currentSpawnLimitation++;
                //    }
                //    else
                //    {
                //        for (int j = 0; j < spawnLimitationNum - 1; j++)
                //            spawnLimitation[j] = spawnLimitation[j + 1];

                //        spawnLimitation[spawnLimitationNum - 1] = r;
                //    }

                //    enemyState[i].Position = enemyPop.Position[r];
                //    InitializeBox(i);

                //    enemyState[i].Hit = false;
                //}
            }
        }

        private void EnemyHitCheck(int i , Vector3 t)
        {
            for(int j =0;j<enemyNumber;j++)
            {
                if (i == j)
                    continue;

                if(enemyState[i].BoundingBox.Intersects(enemyState[j].BoundingBox))
                {
                    enemyState[i].PositionX -= t.X;
                    enemyState[i].BoundingBoxMaxX -= t.X;
                    enemyState[i].BoundingBoxMinX -= t.X;

                    if(enemyState[i].BoundingBox.Intersects(enemyState[j].BoundingBox))
                    {
                        enemyState[i].PositionZ -= t.Z;
                        enemyState[i].BoundingBoxMaxZ -= t.Z;
                        enemyState[i].BoundingBoxMinZ -= t.Z;

                        enemyState[i].PositionX += t.X;
                        enemyState[i].BoundingBoxMaxX += t.X;
                        enemyState[i].BoundingBoxMinX += t.X;

                        if(enemyState[i].BoundingBox.Intersects(enemyState[j].BoundingBox))
                        {
                            enemyState[i].PositionX -= t.X;
                            enemyState[i].BoundingBoxMaxX -= t.X;
                            enemyState[i].BoundingBoxMinX -= t.X;
                        }
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            
            UpdateModelCoordinates(gameTime);
        }

        private void UpdateModelCoordinates(GameTime gameTime)
        {
            ////敵の移動
            if (enemyState[0].Hit == false || enemyState[1].Hit == false || enemyState[2].Hit == false || enemyState[3].Hit == false || enemyState[4].Hit == false ||
                enemyState[5].Hit == false || enemyState[6].Hit == false || enemyState[7].Hit == false || enemyState[8].Hit == false || enemyState[9].Hit == false)
            {
                ////前のフレームから経過した時間を取得
                float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

                ////敵の移動計算を呼ぶ
                Movement(delta);
            }
        }
    }
}
