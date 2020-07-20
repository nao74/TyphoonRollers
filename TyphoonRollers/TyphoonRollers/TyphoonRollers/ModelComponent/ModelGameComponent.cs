#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace TyphoonRollers
{
    /// <summary>
    /// モデルの読み込みと描画
    /// 複数のカメラから同時に描画を行うため、
    /// Drawはオーバーライドせず、独自の描画メソッドを呼び出すようにする
    /// </summary>
    public class ModelGameComponent : DrawableGameComponent
    {
        #region フィールド
        /// <summary>
        ///  読み込むモデルのパス
        /// </summary>
        protected string path;

        /// <summary>
        /// モデル
        /// </summary>
        public Model ModelData
        {
            get { return model; }
        }
		private Model model;
        
        /// <summary>
        /// ボーン
        /// </summary>
        public Matrix[] BonesData
        {
            get { return bones; }
        }
		private Matrix[] bones;

        /// <summary>
        /// ポジション
        /// </summary>
        public Vector3 Position
        {
            set { position = value;
                  matPosition = Matrix.CreateTranslation(position);
            }
            get { return position; }
        }        
        private Vector3 position;
        private Matrix matPosition;

        public float PositionX
        {
            set { position.X = value; }
            get { return position.X; }
        }

        public float PositionZ
        {
            set { position.Z = value; }
            get { return position.Z; }
        }

        /// <summary>
        /// 回転角
        /// </summary>
        public Matrix Rotate
        {
            set { rotate = value; }
            get { return rotate; }
        }
		private Matrix rotate;

        public Vector3 Rotation
        {
            set { rotation = value; }
            get { return rotation; }
        }
        private Vector3 rotation;

        /// <summary>
        /// スケール
        /// </summary>
        public Vector3 Scale
        {
            set { scale = value;
                  matScale = Matrix.CreateScale(scale);
            }
            get { return scale; }
        }
        private Vector3 scale;
        private Matrix matScale;

        /// <summary>
        /// ワールド変換行列
        /// </summary>
        public Matrix World
        {
            set { world = value; }
            get { return world; }
        }
        private Matrix world;

        /// <summary>
        ///  ビュー行列
        /// </summary>
        public Matrix View
        {
            set { view = value; }
            get { return view; }
        }        
        private Matrix view;

        /// <summary>
        ///  射影変換行列
        /// </summary>
        public Matrix Projection
        {
            set { projection = value; }
            get { return projection; }
        }        
        private Matrix projection;
        
        /// <summary>
        /// アルファ値
        /// </summary>
        public float Alpha
        {
            set { alpha = value; }
            get { return alpha; }
        }
        private float alpha = 1.0f;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="game">ゲームクラス</param>
        public ModelGameComponent(Game game)
            : base(game)
        {
        }
        #endregion

        #region 初期化
        /// <summary>
        /// 対象モデルの読み込み設定
        /// </summary>
        /// <param name="path">読み込むモデルのパス</param>
        public void Initialize(string path)
        {
            Initialize(path, Vector3.Zero, Matrix.Identity, Vector3.One);
        }

        /// <summary>
        /// 対象モデルの読み込み設定
        /// </summary>
        /// <param name="path">読み込むモデルのパス</param>
        /// <param name="position">ポジション</param>
        public void Initialize(string path, Vector3 position)
        {
            Initialize(path, position, Matrix.Identity, Vector3.One);
        }

        /// <summary>
        /// 対象モデルの読み込み設定
        /// </summary>
        /// <param name="path">読み込むモデルのパス</param>
        /// <param name="position">ポジション</param>
        /// <param name="rotate">向き</param>
        public void Initialize(string path, Vector3 position, Matrix rotate)
        {
            Initialize(path, position, rotate, Vector3.One);
        }

        /// <summary>
        /// 対象モデルの読み込み設定
        /// </summary>
        /// <param name="path">読み込むモデルのパス</param>
        /// <param name="position">ポジション</param>
        /// <param name="scale">スケール</param>
        public void Initialize(string path, Vector3 position, Vector3 scale)
        {
            Initialize(path, position, Matrix.Identity, scale);
        }

        /// <summary>
        /// 対象モデルの読み込み設定
        /// </summary>
        /// <param name="path">読み込むモデルのパス</param>
        /// <param name="position">ポジション</param>
        /// <param name="rotate">向き</param>
        /// <param name="scale">スケール</param>
        public void Initialize(string path, Vector3 position, Matrix rotate, Vector3 scale)
        {
            this.path = path;

            Position = position;
            Rotate = rotate;
            Scale = scale;
        }
        #endregion

        #region リソースの読み込みと解放
        /// <summary>
        /// コンテンツ読み込みのタイミングにフレームワークから呼び出されます
        /// </summary>
        protected override void LoadContent()
        {
            model = Game.Content.Load<Model>(path);

            // モデルからボーンを取得する
            bones = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(bones);

            base.LoadContent();
        }

        /// <summary>
        /// コンテンツ解放のタイミングにフレームワークから呼び出されます
        /// </summary>
        protected override void UnloadContent()
        {
            model = null;
            bones = null;

            base.UnloadContent();
        }
        #endregion

        #region メソッド
        /// <summary>
        /// カメラの行列を設定
        /// </summary>
        /// <param name="view">ビュー行列</param>
        /// <param name="projection">射影行列</param>
        public void SetCamera(Matrix view, Matrix projection)
        {
            View = view;
            Projection = projection;
        }

        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="move">加算する値</param>
        public void Move(Vector3 move)
        {
            Position += move;
        }

        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="x">X移動量</param>
        /// <param name="y">Y移動量</param>
        /// <param name="z">Z移動量</param>
        public void Move(float x, float y, float z)
        {
            position.X += x;
            position.Y += y;
            position.Z += z;
        }
        #endregion

        #region オーバーライド
        /// <summary>
        /// GraphicDeviceを変更したい場合にオーバーライドする
        /// </summary>
        protected virtual void PreModifyGraphicsDevice() {}

        /// <summary>
        /// エフェクトの変更
        /// </summary>
        /// <param name="gameTime">ゲームタイム</param>
        /// <param name="effect">エフェクト</param>
        protected virtual void ModifyEffect(GameTime gameTime, BasicEffect effect){}

        /// <summary>
        /// GraphicDeviceのパラメータを戻すためにオーバーライドする
        /// </summary>
        protected virtual void AfterModifyGraphicsDevice() {}
        #endregion

        #region 描画
        /// <summary>
        /// 描画したいタイミングで呼び出して下さい
        /// </summary>
        /// <param name="gameTime">ゲームタイム</param>
        public void UserDraw(GameTime gameTime)
        {
            if (Visible && model != null)
            {
                // 描画前に仮想関数を呼び出す
                PreModifyGraphicsDevice();

                // BasicEffectを使用してモデルを描画する
                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        // デフォルトのライティング設定にする
                        effect.EnableDefaultLighting();

                        // ワールド座標を計算する
                        effect.World = bones[mesh.ParentBone.Index] * matScale * rotate * matPosition;
                        effect.View = view;
                        effect.Projection = projection;
                        effect.Alpha = alpha;

                        // エフェクトを変更する仮想関数を呼び出す
                        ModifyEffect(gameTime, effect);
                    }

                    mesh.Draw();
                }

                // 描画後に仮想関数を呼び出す
                AfterModifyGraphicsDevice();
            }
        }
        #endregion
    }
}
