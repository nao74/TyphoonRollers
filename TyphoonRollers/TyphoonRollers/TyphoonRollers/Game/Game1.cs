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
    /// �I�u�W�F�N�g�R���g���[��
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region �t�B�[���h

        /// <summary>
        /// �O���t�B�b�N�f�o�C�X�}�l�[�W���[
        /// </summary>
        GraphicsDeviceManager graphics;

        GraphicsDevice device;

        /// <summary>
        /// �X�v���C�g�o�b�`
        /// </summary>
        public static SpriteBatch spriteBatch;

        /// <summary>
        /// �t�H���g
        /// </summary>
        public static SpriteFont font;
        public static SpriteFont font2;
        public static SpriteFont font3;

        /// <summary>
        /// �v���C��ʃR���|�[�l���g
        /// </summary>
        PlayComponent playCompo;

        TitleComponent titleCompo;

        DebugComponent1 debugCompo;

        ManualComponent manualCompo;

        /// <summary>
        /// �Q�[�����[�h�񋓑�
        /// </summary>
        enum GameMode
        {
            Title,
            Manual,
            Play
        }

        /// <summary>
        /// �Q�[�����[�h
        /// </summary>
        GameMode mode;

        #endregion

        #region �R���X�g���N�^

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public Game1()
        {
            // �f�o�C�X�}�l�[�W���[�𐶐�
            graphics = new GraphicsDeviceManager(this);

            // �𑜓x�̐ݒ�
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;

            // �R���e���g�̃f�B���N�g����"Content"�ɐݒ肷��
            Content.RootDirectory = "Content";
        }

        #endregion

        #region ������

        /// <summary>
        /// ������
        /// </summary>
        protected override void Initialize()
        {
            // �C���v�b�g�}�l�[�W���̏�����
            InputManager.Initialize();

            // �R���|�[�l���g�̃C���X�^���X��
            titleCompo = new TitleComponent(this);
            playCompo = new PlayComponent(this);
            debugCompo = new DebugComponent1(this);
            manualCompo = new ManualComponent(this);

            // ���[�h������
            mode = GameMode.Title;

            if (!Components.Contains(titleCompo))
                Components.Add(titleCompo);

            base.Initialize();
        }

        #endregion

        #region �R���e���c�̓ǂݍ��ݏ���
        /// <summary>
        /// �R���e���c�̓ǂݍ��ݏ���
        /// </summary>
        protected override void LoadContent()
        {
            // �X�v���C�g�o�b�`�̐���
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // �t�H���g�̓ǂݍ���
            font = Content.Load<SpriteFont>(@"SpriteFont1");
            font2 = Content.Load<SpriteFont>(@"SpriteFont2");
            font3 = Content.Load<SpriteFont>(@"SpriteFont3");
        }

        #endregion

        #region �R���e���c�̉������
        /// <summary>
        /// �R���e���c�̉������
        /// </summary>
        protected override void UnloadContent()
        {
        }

        #endregion

        #region �X�V����
        /// <summary>
        /// �Q�[���̍X�V����
        /// </summary>
        /// <param name="gameTime">���݂̎���.</param>
        protected override void Update(GameTime gameTime)
        {
            //�C���v�b�g�}�l�[�W���̍X�V
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

        #region �`�揈��

        /// <summary>
        /// �Q�[���̕`�揈��
        /// </summary>
        /// <param name="gameTime">���݂̎���</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

        #endregion
    }
}
