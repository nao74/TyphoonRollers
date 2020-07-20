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
    /// オブジェクトコントロール
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region フィールド

        /// <summary>
        /// グラフィックデバイスマネージャー
        /// </summary>
        GraphicsDeviceManager graphics;

        GraphicsDevice device;

        /// <summary>
        /// スプライトバッチ
        /// </summary>
        public static SpriteBatch spriteBatch;

        /// <summary>
        /// フォント
        /// </summary>
        public static SpriteFont font;
        public static SpriteFont font2;
        public static SpriteFont font3;

        /// <summary>
        /// プレイ画面コンポーネント
        /// </summary>
        PlayComponent playCompo;

        TitleComponent titleCompo;

        DebugComponent1 debugCompo;

        ManualComponent manualCompo;

        /// <summary>
        /// ゲームモード列挙体
        /// </summary>
        enum GameMode
        {
            Title,
            Manual,
            Play
        }

        /// <summary>
        /// ゲームモード
        /// </summary>
        GameMode mode;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Game1()
        {
            // デバイスマネージャーを生成
            graphics = new GraphicsDeviceManager(this);

            // 解像度の設定
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;

            // コンテントのディレクトリを"Content"に設定する
            Content.RootDirectory = "Content";
        }

        #endregion

        #region 初期化

        /// <summary>
        /// 初期化
        /// </summary>
        protected override void Initialize()
        {
            // インプットマネージャの初期化
            InputManager.Initialize();

            // コンポーネントのインスタンス化
            titleCompo = new TitleComponent(this);
            playCompo = new PlayComponent(this);
            debugCompo = new DebugComponent1(this);
            manualCompo = new ManualComponent(this);

            // モード初期化
            mode = GameMode.Title;

            if (!Components.Contains(titleCompo))
                Components.Add(titleCompo);

            base.Initialize();
        }

        #endregion

        #region コンテンツの読み込み処理
        /// <summary>
        /// コンテンツの読み込み処理
        /// </summary>
        protected override void LoadContent()
        {
            // スプライトバッチの生成
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // フォントの読み込み
            font = Content.Load<SpriteFont>(@"SpriteFont1");
            font2 = Content.Load<SpriteFont>(@"SpriteFont2");
            font3 = Content.Load<SpriteFont>(@"SpriteFont3");
        }

        #endregion

        #region コンテンツの解放処理
        /// <summary>
        /// コンテンツの解放処理
        /// </summary>
        protected override void UnloadContent()
        {
        }

        #endregion

        #region 更新処理
        /// <summary>
        /// ゲームの更新処理
        /// </summary>
        /// <param name="gameTime">現在の時間.</param>
        protected override void Update(GameTime gameTime)
        {
            //インプットマネージャの更新
            InputManager.Update();

            
            switch (mode)
            {
                case GameMode.Title:
                    if (titleCompo.IsSelected())
                    {
                        switch (titleCompo.selectedMenu)
                        {
                            case TitleComponent.Menu.Start:
                                Components.Remove(titleCompo);
                                Components.Add(playCompo);

                                titleCompo = new TitleComponent(this);

                                mode = GameMode.Play;
                                break;
                            case TitleComponent.Menu.Manual:
                                Components.Remove(titleCompo);
                                Components.Add(manualCompo);

                                titleCompo = new TitleComponent(this);

                                mode = GameMode.Manual;
                                break;

                            case TitleComponent.Menu.Exit:
                                Exit();
                                break;
                        }
                    }
                    break;
                case GameMode.Manual:
                    if(manualCompo.Exit)
                    {
                        Components.Remove(manualCompo);
                        Components.Add(titleCompo);

                        mode = GameMode.Title;

                        manualCompo = new ManualComponent(this);

                        //manualCompo.Exit = false;
                    }
                    break;
                case GameMode.Play:
                    if(playCompo.IsExit())
                    {
                        Components.Remove(playCompo);
                        Components.Add(titleCompo);

                        playCompo = new PlayComponent(this);

                        mode = GameMode.Title;
                    }
                    break;

            }
           

            base.Update(gameTime);
        }

        #endregion

        #region 描画処理

        /// <summary>
        /// ゲームの描画処理
        /// </summary>
        /// <param name="gameTime">現在の時間</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

        #endregion
    }
}
