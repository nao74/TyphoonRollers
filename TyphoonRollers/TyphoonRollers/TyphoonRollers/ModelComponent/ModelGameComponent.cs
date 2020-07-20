#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace TyphoonRollers
{
    /// <summary>
    /// ���f���̓ǂݍ��݂ƕ`��
    /// �����̃J�������瓯���ɕ`����s�����߁A
    /// Draw�̓I�[�o�[���C�h�����A�Ǝ��̕`�惁�\�b�h���Ăяo���悤�ɂ���
    /// </summary>
    public class ModelGameComponent : DrawableGameComponent
    {
        #region �t�B�[���h
        /// <summary>
        ///  �ǂݍ��ރ��f���̃p�X
        /// </summary>
        protected string path;

        /// <summary>
        /// ���f��
        /// </summary>
        public Model ModelData
        {
            get { return model; }
        }
		private Model model;
        
        /// <summary>
        /// �{�[��
        /// </summary>
        public Matrix[] BonesData
        {
            get { return bones; }
        }
		private Matrix[] bones;

        /// <summary>
        /// �|�W�V����
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
        /// ��]�p
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
        /// �X�P�[��
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
        /// ���[���h�ϊ��s��
        /// </summary>
        public Matrix World
        {
            set { world = value; }
            get { return world; }
        }
        private Matrix world;

        /// <summary>
        ///  �r���[�s��
        /// </summary>
        public Matrix View
        {
            set { view = value; }
            get { return view; }
        }        
        private Matrix view;

        /// <summary>
        ///  �ˉe�ϊ��s��
        /// </summary>
        public Matrix Projection
        {
            set { projection = value; }
            get { return projection; }
        }        
        private Matrix projection;
        
        /// <summary>
        /// �A���t�@�l
        /// </summary>
        public float Alpha
        {
            set { alpha = value; }
            get { return alpha; }
        }
        private float alpha = 1.0f;
        #endregion

        #region �R���X�g���N�^
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="game">�Q�[���N���X</param>
        public ModelGameComponent(Game game)
            : base(game)
        {
        }
        #endregion

        #region ������
        /// <summary>
        /// �Ώۃ��f���̓ǂݍ��ݐݒ�
        /// </summary>
        /// <param name="path">�ǂݍ��ރ��f���̃p�X</param>
        public void Initialize(string path)
        {
            Initialize(path, Vector3.Zero, Matrix.Identity, Vector3.One);
        }

        /// <summary>
        /// �Ώۃ��f���̓ǂݍ��ݐݒ�
        /// </summary>
        /// <param name="path">�ǂݍ��ރ��f���̃p�X</param>
        /// <param name="position">�|�W�V����</param>
        public void Initialize(string path, Vector3 position)
        {
            Initialize(path, position, Matrix.Identity, Vector3.One);
        }

        /// <summary>
        /// �Ώۃ��f���̓ǂݍ��ݐݒ�
        /// </summary>
        /// <param name="path">�ǂݍ��ރ��f���̃p�X</param>
        /// <param name="position">�|�W�V����</param>
        /// <param name="rotate">����</param>
        public void Initialize(string path, Vector3 position, Matrix rotate)
        {
            Initialize(path, position, rotate, Vector3.One);
        }

        /// <summary>
        /// �Ώۃ��f���̓ǂݍ��ݐݒ�
        /// </summary>
        /// <param name="path">�ǂݍ��ރ��f���̃p�X</param>
        /// <param name="position">�|�W�V����</param>
        /// <param name="scale">�X�P�[��</param>
        public void Initialize(string path, Vector3 position, Vector3 scale)
        {
            Initialize(path, position, Matrix.Identity, scale);
        }

        /// <summary>
        /// �Ώۃ��f���̓ǂݍ��ݐݒ�
        /// </summary>
        /// <param name="path">�ǂݍ��ރ��f���̃p�X</param>
        /// <param name="position">�|�W�V����</param>
        /// <param name="rotate">����</param>
        /// <param name="scale">�X�P�[��</param>
        public void Initialize(string path, Vector3 position, Matrix rotate, Vector3 scale)
        {
            this.path = path;

            Position = position;
            Rotate = rotate;
            Scale = scale;
        }
        #endregion

        #region ���\�[�X�̓ǂݍ��݂Ɖ��
        /// <summary>
        /// �R���e���c�ǂݍ��݂̃^�C�~���O�Ƀt���[�����[�N����Ăяo����܂�
        /// </summary>
        protected override void LoadContent()
        {
            model = Game.Content.Load<Model>(path);

            // ���f������{�[�����擾����
            bones = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(bones);

            base.LoadContent();
        }

        /// <summary>
        /// �R���e���c����̃^�C�~���O�Ƀt���[�����[�N����Ăяo����܂�
        /// </summary>
        protected override void UnloadContent()
        {
            model = null;
            bones = null;

            base.UnloadContent();
        }
        #endregion

        #region ���\�b�h
        /// <summary>
        /// �J�����̍s���ݒ�
        /// </summary>
        /// <param name="view">�r���[�s��</param>
        /// <param name="projection">�ˉe�s��</param>
        public void SetCamera(Matrix view, Matrix projection)
        {
            View = view;
            Projection = projection;
        }

        /// <summary>
        /// �ړ�
        /// </summary>
        /// <param name="move">���Z����l</param>
        public void Move(Vector3 move)
        {
            Position += move;
        }

        /// <summary>
        /// �ړ�
        /// </summary>
        /// <param name="x">X�ړ���</param>
        /// <param name="y">Y�ړ���</param>
        /// <param name="z">Z�ړ���</param>
        public void Move(float x, float y, float z)
        {
            position.X += x;
            position.Y += y;
            position.Z += z;
        }
        #endregion

        #region �I�[�o�[���C�h
        /// <summary>
        /// GraphicDevice��ύX�������ꍇ�ɃI�[�o�[���C�h����
        /// </summary>
        protected virtual void PreModifyGraphicsDevice() {}

        /// <summary>
        /// �G�t�F�N�g�̕ύX
        /// </summary>
        /// <param name="gameTime">�Q�[���^�C��</param>
        /// <param name="effect">�G�t�F�N�g</param>
        protected virtual void ModifyEffect(GameTime gameTime, BasicEffect effect){}

        /// <summary>
        /// GraphicDevice�̃p�����[�^��߂����߂ɃI�[�o�[���C�h����
        /// </summary>
        protected virtual void AfterModifyGraphicsDevice() {}
        #endregion

        #region �`��
        /// <summary>
        /// �`�悵�����^�C�~���O�ŌĂяo���ĉ�����
        /// </summary>
        /// <param name="gameTime">�Q�[���^�C��</param>
        public void UserDraw(GameTime gameTime)
        {
            if (Visible && model != null)
            {
                // �`��O�ɉ��z�֐����Ăяo��
                PreModifyGraphicsDevice();

                // BasicEffect���g�p���ă��f����`�悷��
                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        // �f�t�H���g�̃��C�e�B���O�ݒ�ɂ���
                        effect.EnableDefaultLighting();

                        // ���[���h���W���v�Z����
                        effect.World = bones[mesh.ParentBone.Index] * matScale * rotate * matPosition;
                        effect.View = view;
                        effect.Projection = projection;
                        effect.Alpha = alpha;

                        // �G�t�F�N�g��ύX���鉼�z�֐����Ăяo��
                        ModifyEffect(gameTime, effect);
                    }

                    mesh.Draw();
                }

                // �`���ɉ��z�֐����Ăяo��
                AfterModifyGraphicsDevice();
            }
        }
        #endregion
    }
}
