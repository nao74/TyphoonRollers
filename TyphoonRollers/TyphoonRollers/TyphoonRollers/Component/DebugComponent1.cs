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
    public class DebugComponent1 : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private GraphicsDevice device;

        ParticleSystem enemyDeadParticle;

        Camera camera;

        Random random = new Random();

        Vector3 pos = new Vector3(0.0f, 10.0f, 0.0f);

        Vector3 poss = new Vector3(1000.0f, 500, 200);

        Vector3 po;

        int j = 0;

        private bool exit;

        public bool Exit
        {
            get { return exit; }
            set { exit = value; }
        }

        float radius;
        float height = 0;

        private Model model;
        private Matrix[] transform;

        public DebugComponent1(Game1 game)
            : base(game)
        {
            // TODO: Construct any child components here
            enemyDeadParticle = new EnemyDeadParticleSystem(game, game.Content);
            enemyDeadParticle.DrawOrder = 500;
            game.Components.Add(enemyDeadParticle);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            po = poss / 1000;

            base.Initialize();
        }

        private void InitializeCamera()
        {
            // カメラを生成する
            camera = new Camera(Game);

            // パラメータを設定
            camera.FieldOfView = MathHelper.ToRadians(70.0f);
            camera.AspectRatio = (float)GraphicsDevice.Viewport.Width / (float)GraphicsDevice.Viewport.Height;
            camera.NearPlaneDistance = 1.0f;
            camera.FarPlaneDistance = 2000.0f;
            camera.ReferenceTranslate = new Vector3(0.0f, 200.0f, 200.0f);
            camera.Target = Vector3.Zero;
        }

        protected override void LoadContent()
        {
            InitializeCamera();

            model = Game.Content.Load<Model>("Model/Enemy");
            transform = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transform);

            device = GraphicsDevice;

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (j < 1000)
                j++;
            // TODO: Add your update code here

            if (InputManager.IsKeyDown(Keys.Escape) || InputManager.IsButtonDown(PlayerIndex.One, Buttons.Back))
                exit = true;

            camera.Update(gameTime);

            //for(int j = 1000; j < 0; j--)
            //{
            //    for (int k = 0; k < 1000; k++)
            //        enemyDeadParticle.AddParticle(new Vector3(100,100,100), Vector3.Zero);
            //}

            //for (int k = 0; k < j; k++)
            //{
            //    for (int i = 0; i < 1; i++)
            //        enemyDeadParticle.AddParticle(po * k, Vector3.Zero);
            //}


            for (int i = 0; i < 1000; i++)
                enemyDeadParticle.AddParticle(po + po * i, Vector3.Zero);

                //if (radius < 100)
                //{

                //        for (int i = 0; i < 1000; i++)
                //            enemyDeadParticle.AddParticle(RandomPointOnSphere(), Vector3.Up);

                //    radius += 4;
                //    height++;
                //}
                //else
                //    radius = 0;

                base.Update(gameTime);
        }

        Vector3 RandomPointOnSphere()
        {
            const float maxRadius = 30;
            const float maxHeight = 40;

            double angle = random.NextDouble() * Math.PI * 2;
            double angle2 = random.NextDouble() * Math.PI * 2;

            float x = (float)Math.Cos(angle) * (float)Math.Cos(angle2) * radius;
            float y = (float)Math.Cos(angle) * (float)Math.Sin(angle2) * radius;
            float z = (float)Math.Sin(angle) * radius;
            
            return new Vector3(x, y, z);
        }

        private void DrawEffect(GameTime gameTime)
        {
            enemyDeadParticle.UserDraw(gameTime);

            device.BlendState = BlendState.Opaque;
        }

        public override void Draw(GameTime gameTime)
        {


            DrawModel(model, transform, Matrix.CreateScale(5.0f));


            enemyDeadParticle.SetCamera(camera.View, camera.Projection);
            DrawEffect(gameTime);

            

            base.Draw(gameTime);
        }

        private void DrawModel(Model model, Matrix[] transform, Matrix world)
        {
            // モデル内のメッシュをすべて描画する
            foreach (ModelMesh mesh in model.Meshes)
            {
                // デフォルトライティングを有効にする
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();

                    // 必要な行列を設定する
                    effect.View = camera.View;                              // ビュー変換行列
                    effect.Projection = camera.Projection;                  // 射影変換行列
                    effect.World = transform[mesh.ParentBone.Index] * world;  // モデルのワールド変換行列
                }
                // メッシュの描画
                mesh.Draw();
            }
        }
    }
}
