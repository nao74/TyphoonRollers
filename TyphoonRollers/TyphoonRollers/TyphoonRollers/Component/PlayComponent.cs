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
    public class PlayComponent : DrawableGameComponent
    {
        #region �t�B�[���h

        /// <summary>
        /// �R���e���c�}�l�[�W��
        /// </summary>
        private ContentManager content;

        /// <summary>
        /// �f�o�C�X�}�l�[�W��
        /// </summary>
        private GraphicsDevice device;

        /// <summary>
        /// �X�v���C�g�o�b�`
        /// </summary>
        private SpriteBatch spriteBatch;

        private Random random;

        /// <summary>
        /// �J����
        /// </summary>
        private Camera camera;
        private Vector3 cameraRot;

        /// <summary>
        /// ���f��
        /// </summary>
        private PlayerModelComponent player;
        private EnemyModelComponent[] enemy;
        private StageModelComponent stage;
        private BossModelComponent boss;

        /// <summary>
        /// �e�N�X�`��
        /// </summary>
        private Texture2D timeTexture;
        private Texture2D scoreTexture;
        private Texture2D hpPlateTexture;
        private Texture2D hpBaseTexture;
        private Texture2D[] hpTexture;
        private Texture2D gameOverTexture;
        private Texture2D gameClearTexture;
        private Texture2D pushTexture;
        private Texture2D resultTexture;
        private Texture2D stageTexture;
        private Texture2D aRankTexture;
        private Texture2D bRankTexture;
        private Texture2D fRankTexture;
        private Texture2D sRankTexture;

        /// <summary>
        /// �e�N�X�`���\���ʒu
        /// </summary>
        private Vector2 timeTexturePosition;
        private Vector2 scoreTexturePosition;
        private Vector2 hpTexturePosition;

        /// <summary>
        /// �T�E���h�G�t�F�N�g
        /// </summary>
        private SoundEffect[] comboSound;

        /// <summary>
        /// BGM
        /// </summary>
        private Song bgm;

        /// <summary>
        /// �p�[�e�B�N��
        /// </summary>
        private ParticleSystem rotationParticle;
        private ParticleSystem notRotationParticle;
        private ParticleSystem[] enemyDeadParticle;
        private ParticleSystem[] enemyAttackParticle;

        private float[] particleRadius;

        private bool[] hitEffect;
        private bool[] hitPlayerIsRotation;
        private Vector3[] effectPos;
        private Vector3[] bossEffectPos;

        /// <summary>
        /// �̗�
        /// </summary>
        private float hp;
        private const float maxHp = 5.0f;

        /// <summary>
        /// �R���{��
        /// </summary>
        private int combo;

        /// <summary>
        /// �X�R�A
        /// </summary>
        private float score;

        /// <summary>
        /// �g�[�^���X�R�A
        /// </summary>
        private float totalScore;
        private float recoveryTime;

        /// <summary>
        /// ���@�������Ă��邩�ǂ���
        /// </summary>
        private bool move;

        /// <summary>
        /// ��������
        /// </summary>
        private TimeSpan time;
        private float timeLeft;
        private DateTime tempTime;

        /// <summary>
        /// �G�l�~�[�����N���ő吔
        /// </summary>
        private const int enemyNum = 10;

        /// <summary>
        /// ���f���e��X�P�[��
        /// </summary>
        private const float playerScale = 30.0f;
        private const float enemyScale = 5.0f;
        private const float bossScale = 1.0f;

        /// <summary>
        /// �G�l�~�[�X�|�[���͈�
        /// </summary>
        private const int spawnArea = 800;

        /// <summary>
        /// �R���{���̃^�C���񕜗�
        /// </summary>
        private const float comboRecoveryTime = 1.2f;

        /// <summary>
        /// ��������
        /// </summary>
        private const int timeLimit = 120;

        /// <summary>
        /// �R���{�ő吔
        /// </summary>
        private const int maxCombo = 7;

        /// <summary>
        /// bgm����
        /// </summary>
        private const float bgmVolume = 0.5f;

        /// <summary>
        /// �X�e�[�W�Ǎ��W
        /// </summary>
        private const float wall = 800.0f;

        /// <summary>
        /// �Q�[���I�[�o�[
        /// </summary>
        private bool gameOver;
        private bool gameClear;

        /// <summary>
        /// �t�F�[�h�A�E�g�p
        /// </summary>
        private Texture2D fadeTexture;
        private float fadeAlpha;
        private bool fadeMaxReace;
        private const float pushTimeAlpha = 127.0f;
        private const float maxAlpha = 255.0f;
        private float fadeClearPosition = -1280.0f;
        private float fadePushPosition = 1280.0f;

        /// <summary>
        /// �Q�[���V�[��
        /// </summary>
        private bool normalScene;
        private bool bossScene;
        private bool push;
        private bool onceUpdate;

        /// <summary>
        /// ���@���C�̔��苗��
        /// </summary>
        private const int playerFrontRayReach = 120;
        private const int playerSideRayReach = 24;

        /// <summary>
        /// �N���A�^�C��
        /// </summary>
        private float clearTime;

        /// <summary>
        /// �N���A���_�ł̃X�R�A
        /// </summary>
        private float clearScore;

        /// <summary>
        /// �g�[�^���X�R�A
        /// </summary>
        private float clearTotalScore;

        /// <summary>
        /// ���U���g��ʂւ̃t���O
        /// </summary>
        private bool result;

        /// <summary>
        /// ���U���g��ʕ`�掞��
        /// </summary>
        private float resultTime;
        private const float resultShowTime = 3.5f;

        /// <summary>
        /// �G�l�~�[���j��
        /// </summary>
        private float enemyCount;

        /// <summary>
        /// �{�X�ڍs�K�v���j��
        /// </summary>
        private const float enemyCountMax = 35;

        /// <summary>
        /// �{�X���j�{�[�i�X�X�R�A
        /// </summary>
        private float bossBonus = 3000;

        /// <summary>
        /// �����N�ʃX�R�A
        /// </summary>
        private const float sRankScore = 30000;

        private const float aRankScore = 25000;

        private const float bRankScore = 20000;

        /// <summary>
        ///  ��ǂݒn�_
        /// </summary>
        public Vector3 playerPos;
        public BoundingSphere playerPosSphere;
        private bool PlayerPosHit;

        /// <summary>
        ///  ��ǂ݃|�C���g�܂ł̋���
        /// </summary>
        public float flontDistance = 240;
        Vector3[] flontDirection = new Vector3[enemyNum];

        /// <summary>
        /// �����_���|�C���g
        /// </summary>
        public Vector3[] randomPos = new Vector3[enemyNum];
        public BoundingSphere[] randomSphere = new BoundingSphere[enemyNum];

        /// <summary>
        ///  �|�C���g���Z�b�g����Ă��邩�ǂ���
        /// </summary>
        private bool[] randomPosSet = new bool[enemyNum];

        /// <summary>
        /// �����_���|�C���g�����͈� 200
        /// </summary>
        public const int randomArea = 200;
        Vector3[] randDirection = new Vector3[enemyNum];

        /// <summary>
        /// ���u��
        /// </summary>
        private enum rayDistance
        {
            forward, right, backward, left
        }

        /// <summary>
        /// ���u��
        /// </summary>
        private static bool exit;

        private bool enemyTime;

        private int bossEffectNum;

        private const float resetRadianNum = 10.0f;

        private float addTime;

        private float tempLeftVecZ;

        private float tempRightVecZ;

        int ab;

        private const float moveSpeed0 = 3.0f;
        private const float moveSpeed1 = 3.0f;
        private const float moveSpeed2 = 3.0f;
        private const float moveSpeed3 = 3.0f;
        private const float moveSpeed4 = 3.0f;
        private const float moveSpeed5 = 3.0f;
        private const float moveSpeed6 = 3.0f;
        private const float moveSpeed7 = 3.0f; // case7�ɂē�{
        private const float moveSpeed8 = 3.0f; // case8�ɂē�{
        private const float moveSpeed9 = 3.0f; // case9�ɂĎ��{

        private const float moveLimit4 = 200.0f;
        private const float moveLimit5 = 200.0f;
        private const float moveLimit6 = 400.0f;
        private const float moveLimit7 = 800.0f;


        #endregion

        #region �R���X�g���N�^

        public PlayComponent(Game1 game)
            : base(game)
        {
            rotationParticle = new RotationParticleSystem(game, game.Content);
            notRotationParticle = new NotRotationParticleSystem(game, game.Content);

            enemyDeadParticle = new EnemyDeadParticleSystem[enemyNum];
            enemyAttackParticle = new EnemyAttackParticleSystem[enemyNum];

            for (int i = 0; i < enemyNum; i++)
            {
                enemyDeadParticle[i] = new EnemyDeadParticleSystem(game, game.Content);
                enemyAttackParticle[i] = new EnemyAttackParticleSystem(game, game.Content);
            }
        }

        #endregion

        #region ������

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            hp = maxHp;

            random = new Random();

            hitEffect = new bool[enemyNum];
            hitPlayerIsRotation = new bool[enemyNum];
            effectPos = new Vector3[enemyNum];
            bossEffectPos = new Vector3[enemyNum];
            particleRadius = new float[enemyNum];

            for(int i = 0; i < enemyNum; i++)
            {
                hitEffect[i] = new bool();
                hitPlayerIsRotation[i] = new bool();
                effectPos[i] = new Vector3();
                bossEffectPos[i] = new Vector3();
                particleRadius[i] = new float();
            }
            
            timeLeft = timeLimit;

            tempTime = DateTime.Now;

            addTime = 0;

            enemyCount = 0;

            MediaPlayer.IsRepeating = true;

            normalScene = true;
            bossScene = false;

            base.Initialize();
        }

        #endregion

        #region �ǂݍ���

        protected override void LoadContent()
        {
            // �R���e���c�}�l�[�W���̏�����
            content = new ContentManager(Game.Services, "Content");
            // �f�o�C�X�}�l�[�W���̏�����
            device = GraphicsDevice;
            // �X�v���C�g�o�b�`�̏�����
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // ���f���ǂݍ���
            player = new PlayerModelComponent(Game);
            stage = new StageModelComponent(Game);
            boss = new BossModelComponent(Game);
            enemy = new EnemyModelComponent[enemyNum];
            for (int i = 0; i < enemyNum; i++)
                enemy[i] = new EnemyModelComponent(Game);

            player.Initialize("Model/Player", Vector3.Zero, new Vector3(playerScale, playerScale, playerScale));
            stage.Initialize("Model/Stage");
            boss.Initialize("Model/1012", new Vector3(0,40,-700), new Vector3(bossScale, bossScale, bossScale));
            for (int i = 0; i < enemyNum; i++)
            {
                Vector3 rnd = Vector3.Zero;

                if (random.Next(2) == 0)
                    rnd.X = random.Next(spawnArea / 2, spawnArea);
                else
                    rnd.X = random.Next(-spawnArea, -spawnArea / 2);

                if(random.Next(2) == 0)
                    rnd.Z = random.Next(spawnArea / 2, spawnArea);
                else
                    rnd.Z = random.Next(-spawnArea, -spawnArea / 2);

                enemy[i].Initialize("Model/Enemy", rnd, new Vector3(enemyScale, enemyScale, enemyScale));
            }

            // �R���|�[�l���g�Ƀ��f����ǉ�
            Game.Components.Add(player);
            Game.Components.Add(stage);
            Game.Components.Add(boss);
            for (int i = 0; i < enemyNum; i++)
                Game.Components.Add(enemy[i]);

            boss.HitPoint = boss.MaxHp;

            // �\���|�C���g�p�̃X�t�B�A
            playerPosSphere = new BoundingSphere((playerPos), 3);

            // �����_���p�̃X�t�B�A
            for (int i = 0; i < enemyNum; i++)
            {
                randomSphere[i] = new BoundingSphere((randomPos[i]), 3);
            }

            // �e�N�X�`���ǂݍ���
            fadeTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);

            fadeTexture.SetData<Color>(new Color[] { Color.White });
            scoreTexture = content.Load <Texture2D>("Texture/score");
            timeTexture = content.Load < Texture2D>("Texture/time");
            hpPlateTexture = content.Load<Texture2D>("Texture/hpPlate");
            hpBaseTexture = content.Load<Texture2D>("Texture/hpBase");
            gameClearTexture = content.Load<Texture2D>("Texture/clear");
            gameOverTexture = content.Load<Texture2D>("Texture/over");
            pushTexture = content.Load<Texture2D>("Texture/push");
            resultTexture = content.Load<Texture2D>("Texture/result");
            stageTexture = content.Load<Texture2D>("Texture/stage");
            aRankTexture = content.Load<Texture2D>("Texture/rankA");
            bRankTexture = content.Load<Texture2D>("Texture/rankB");
            fRankTexture = content.Load<Texture2D>("Texture/rankF");
            sRankTexture = content.Load<Texture2D>("Texture/rankS");

            hpTexture = new Texture2D[(int)maxHp];

            hpTexture[0] = content.Load<Texture2D>("Texture/HP1");
            hpTexture[1] = content.Load<Texture2D>("Texture/HP3");
            hpTexture[2] = content.Load<Texture2D>("Texture/HP4");
            hpTexture[3] = content.Load<Texture2D>("Texture/HP5");
            hpTexture[4] = content.Load<Texture2D>("Texture/HP2");

            scoreTexturePosition = new Vector2(515, 0);
            timeTexturePosition = new Vector2(0, 0);
            hpTexturePosition = new Vector2(1040, 480);

            // �T�E���h�ǂݍ���
            comboSound = new SoundEffect[maxCombo];

            comboSound[0] = content.Load<SoundEffect>("Sound/combo1");
            comboSound[1] = content.Load<SoundEffect>("Sound/combo2");
            comboSound[2] = content.Load<SoundEffect>("Sound/combo3");
            comboSound[3] = content.Load<SoundEffect>("Sound/combo4");
            comboSound[4] = content.Load<SoundEffect>("Sound/combo5");
            comboSound[5] = content.Load<SoundEffect>("Sound/combo6");
            comboSound[6] = content.Load<SoundEffect>("Sound/combo7");

            bgm = content.Load<Song>("Sound/normalScene");

            // �R���|�[�l���g�ɃG�t�F�N�g��ǉ�
            Game.Components.Add(rotationParticle);
            Game.Components.Add(notRotationParticle);

            for (int i = 0; i < enemyNum; i++)
            {
                Game.Components.Add(enemyDeadParticle[i]);
                Game.Components.Add(enemyAttackParticle[i]);
            }

            // �J�����𐶐�����
            camera = new Camera(Game);

            // �p�����[�^��ݒ�
            camera.FieldOfView = MathHelper.ToRadians(70.0f);
            camera.AspectRatio = (float)GraphicsDevice.Viewport.Width / (float)GraphicsDevice.Viewport.Height;
            camera.NearPlaneDistance = 1.0f;
            camera.FarPlaneDistance = 10000.0f;
            camera.ReferenceTranslate = new Vector3(0.0f, 200.0f, 200.0f);
            camera.Target = Vector3.Zero;

            // �R���|�[�l���g�ɃJ������ǉ�����
            Game.Components.Add(camera);
        }

        #endregion

        #region �A�b�v�f�[�g

        /// <summary>
        /// �v���C���[�A�b�v�f�[�g
        /// </summary>
        /// <param name="gameTime"></param>
        private void PlayerUpdate(GameTime gameTime)
        {
            // �ړ���
            Vector3 trans = Vector3.Zero;
            Vector3 leftAnnulusTrans = Vector3.Zero;
            Vector3 rightAnnulusTrans = Vector3.Zero;
            Vector3 leftBallTrans = Vector3.Zero;
            Vector3 rightBallTrans = Vector3.Zero;
            // ��]��
            cameraRot = Vector3.Zero;
            // �v�Z�p�̃x�N�g��(���X�e�B�b�N)
            Vector3 leftVec = Vector3.Zero;
            // �v�Z�p�̃x�N�g��(�E�X�e�B�b�N)
            Vector3 rightVec = Vector3.Zero;
            // �p�x
            Vector3 radian = Vector3.Zero;
            // ��]���̌v�Z�ꎞ�ۑ�
            Vector3 temp = Vector3.Zero;
            Vector3 rotTemp = Vector3.Zero;

            // ���E�X�e�B�b�N�̓��͂��擾����
            Vector2 leftStick = Vector2.Zero;
            Vector2 rightStick = Vector2.Zero;

            leftStick = InputManager.GetThumbSticksLeft(PlayerIndex.One);
            rightStick = InputManager.GetThumbSticksRight(PlayerIndex.One);

            // ���͂����ۈړ���������֑������
            leftVec.Z = leftStick.Y;

            rightVec.Z = rightStick.Y;

            // �L�[����
            if (InputManager.IsKeyDown(Keys.W))
                leftVec.Z = 1.0f;
            if (InputManager.IsKeyDown(Keys.Up))
                rightVec.Z = 1.0f;
            if (InputManager.IsKeyDown(Keys.Down))
                rightVec.Z = -1.0f;
            if (InputManager.IsKeyDown(Keys.S))
                leftVec.Z = -1.0f;
            
            if (!InputManager.IsButtonDown(PlayerIndex.One, Buttons.LeftStick) && !InputManager.IsButtonDown(PlayerIndex.One, Buttons.RightStick))
            {
                // �ʏ��]
                player.Annulus = true;
                player.Ball = false;

                player.RightRotationPosition = player.RightAnnulusRotation;
                player.LeftRotationPosition = player.LeftAnnulusRotation;

                player.RotationSpeed = player.RotationSpeedIsNormal;

            }
            else
            {
                // �{�[�����S��]
                player.Ball = true;
                player.Annulus = false;

                player.RightRotationPosition = player.RightBallRotation;
                player.LeftRotationPosition = player.LeftBallRotation;

                player.RotationSpeed = player.RotationSpeedIsFast;
                                             
            }

            if (leftVec.Length() > 0.0f || rightVec.Length() > 0.0f)
            {
                move = true;

                float tv = leftVec.Z - rightVec.Z;
                float tv2 = rightVec.Z - leftVec.Z;

                // �O��i
                if (Math.Abs(tv) < 0.1f && Math.Abs(tv2) < 0.1f)
                {
                    Vector3 forward = Vector3.Normalize(player.World.Forward);

                    trans += forward * leftVec.Z * player.MoveSpeed;;

                    UpdateCollision(trans);

                    if (StageCollision())
                    {
                        UpdateCollision(-trans);
                        trans = Vector3.Zero;
                        return;
                    }

                    player.RightRotationPosition += trans;
                    player.LeftRotationPosition += trans;

                    player.RightAnnulusRotation += trans;
                    player.LeftAnnulusRotation += trans;

                    player.RightBallRotation += trans;
                    player.LeftBallRotation += trans;

                    player.RotationMode = false;
                    player.ForwardRotation = false;
                    player.BackwardRotation = false;

                    tempLeftVecZ += leftVec.Z;
                    tempRightVecZ += rightVec.Z;

                    Console.WriteLine(tempLeftVecZ);

                    if (tempRightVecZ >= 15.0f && tempRightVecZ >= 15.0f)
                        player.NowRadian = 0;
                }
                // ���v���
                else if (leftVec.Length() > rightVec.Length())
                {
                    player.RotationMode = true;

                    tempLeftVecZ = 0;
                    tempRightVecZ = 0;

                    if (leftVec.Z > 0.0f)
                    {
                        player.ForwardRotation = true;
                        player.BackwardRotation = false;
                    }
                    else
                    {
                        player.BackwardRotation = true;
                        player.ForwardRotation = false;
                    }

                    radian.Y = MathHelper.ToRadians(leftVec.Z * player.RotationSpeed);

                    trans = RotationTrans(player.Position, player.LeftRotationPosition, radian) - player.Position;

                    UpdateCollision(trans, player.LeftRotationPosition, radian);

                    if (StageCollision())
                    {
                        UpdateCollision(-trans, player.LeftRotationPosition, -radian);
                        radian = Vector3.Zero;
                        trans = Vector3.Zero;
                        return;
                    }

                    // �v���C���[�̊p�x�����Z
                    player.Rotation += new Vector3(0, -leftVec.Z * player.RotationSpeed, 0);

                    if (player.Annulus)
                    {
                        player.RightAnnulusRotation = RotationTrans(player.RightAnnulusRotation, player.LeftAnnulusRotation, radian);

                        player.LeftBallRotation = RotationTrans(player.LeftBallRotation, player.LeftAnnulusRotation, radian);
                        player.RightBallRotation = RotationTrans(player.RightBallRotation, player.LeftAnnulusRotation, radian);

                        player.RightRotationPosition = player.RightAnnulusRotation;
                    }
                    else if (player.Ball)
                    {
                        player.LeftAnnulusRotation = RotationTrans(player.LeftAnnulusRotation, player.LeftBallRotation, radian);
                        player.RightAnnulusRotation = RotationTrans(player.RightAnnulusRotation, player.LeftBallRotation, radian);

                        player.RightBallRotation = RotationTrans(player.RightBallRotation, player.LeftBallRotation, radian);

                        player.RightRotationPosition = player.RightBallRotation;
                    }
                }
                // �����v���
                else if (rightVec.Length() > leftVec.Length())
                {
                    player.RotationMode = true;

                    tempLeftVecZ = 0;
                    tempRightVecZ = 0;

                    if (rightVec.Z > 0.0f)
                    {
                        player.ForwardRotation = true;
                        player.BackwardRotation = false;
                    }
                    else
                    {
                        player.BackwardRotation = true;
                        player.ForwardRotation = false;
                    }

                    radian.Y = MathHelper.ToRadians(-rightVec.Z * player.RotationSpeed);

                    trans = RotationTrans(player.Position, player.RightRotationPosition, radian) - player.Position;

                    UpdateCollision(trans, player.RightRotationPosition, radian);

                    if (StageCollision())
                    {
                        UpdateCollision(-trans, player.RightRotationPosition, -radian);
                        radian = Vector3.Zero;
                        trans = Vector3.Zero;
                        return;
                    }

                    // �v���C���[�̊p�x�����Z
                    player.Rotation += new Vector3(0, rightVec.Z * player.RotationSpeed, 0);

                    if (player.Annulus)
                    {
                        player.LeftAnnulusRotation = RotationTrans(player.LeftAnnulusRotation, player.RightAnnulusRotation, radian);

                        player.RightBallRotation = RotationTrans(player.RightBallRotation, player.RightAnnulusRotation, radian);
                        player.LeftBallRotation = RotationTrans(player.LeftBallRotation, player.RightAnnulusRotation, radian);

                        player.LeftRotationPosition = player.LeftAnnulusRotation;
                    }
                    else if (player.Ball)
                    {
                        player.RightAnnulusRotation = RotationTrans(player.RightAnnulusRotation, player.RightBallRotation, radian);
                        player.LeftAnnulusRotation = RotationTrans(player.LeftAnnulusRotation, player.RightBallRotation, radian);

                        player.LeftBallRotation = RotationTrans(player.LeftBallRotation, player.RightBallRotation, radian);

                        player.LeftRotationPosition = player.LeftBallRotation;
                    }
                }
            }
            else
            {
                player.NowRadian = 0;
                player.RotationMode = false;
                player.ForwardRotation = false;
                player.BackwardRotation = false;
                move = false;
            }

            // �J�����̊p�x�����Z
            cameraRot = -radian;

            player.NowRadian += Math.Abs(radian.Y);

            Console.WriteLine(player.NowRadian);

            if (player.NowRadian >= player.MaxRadian)
            {
                player.RotationMode = false;
            }

            player.Position += trans;

            if (onceUpdate)
            {
                player.Position = player.BossScenePlayerPosition;
                radian.Y = MathHelper.ToRadians(player.Rotation.Y);
                player.Rotation = Vector3.Zero;
                cameraRot = -radian;
                player.Initialize();
            }

            player.World = UpdateMatrix(player.Position, player.Scale, player.Rotation);
        }

        private void EnemyUpdate(GameTime gameTime)
        {
            Vector3 trans = Vector3.Zero;
            enemyTime = true;

            float[] forword = new float[enemyNum];
            Vector3[] direction = new Vector3[enemyNum];

            for (int i = 0; i < enemyNum; i++)
            {
                if (enemy[i].Hit)
                {
                    Vector3 rnd = Vector3.Zero;
                    bool br = false;

                    while (true)
                    {
                        rnd.X = random.Next(-spawnArea, spawnArea);
                        rnd.Z = random.Next(-spawnArea, spawnArea);

                        enemy[i].Position = rnd;
                        enemy[i].Initialize();

                        for (int j = 0; j < enemyNum; j++)
                        {
                            if (!enemy[i].Sphere.Intersects(enemy[j].Sphere))
                                br = true;
                        }

                        if (br)
                            break;
                    }
                    enemy[i].Hit = false;

                    randomPosSet[i] = false;
                }


                if (Intersects(enemy[i].Sphere))
                {
                    enemy[i].Hit = true;

                    hitEffect[i] = true;

                    effectPos[i] = enemy[i].Position;

                    if (player.RotationMode)
                        hitPlayerIsRotation[i] = true;
                    else
                        hitPlayerIsRotation[i] = false;

                    if (!player.RotationMode || player.ForwardHit || player.BackwardHit)
                        hp -= enemy[i].Damage;

                    if (combo < maxCombo)
                        comboSound[combo].Play();
                    else
                        comboSound[maxCombo - 1].Play();

                    ab++;

                    if (player.RotationMode)
                        enemyCount++;
                }
                else
                {
                    direction[i] = player.Position - enemy[i].Position;

                    direction[i].Normalize();

                    Prediction();

                    flontDirection[i] = playerPos - enemy[i].Position;

                    flontDirection[i].Normalize();

                    RandomPoint(i);

                    randDirection[i] = randomPos[i] - enemy[i].Position;

                    randDirection[i].Normalize();

                    switch (i)
                    {
                        case 0:
                            if (randomSphere[i].Intersects(enemy[i].Sphere))
                            {
                                randomPosSet[i] = false;
                            }
                            else
                            {
                                trans = randDirection[i] * moveSpeed0;

                                enemy[i].SphereCenter += trans;
                                enemy[i].Position += trans;
                            }

                            EnemyHitCheck(i, trans);
                            break;
                        case 1:
                            if (randomSphere[i].Intersects(enemy[i].Sphere))
                            {
                                randomPosSet[i] = false;
                            }
                            else
                            {
                                trans = randDirection[i] * moveSpeed1;

                                enemy[i].SphereCenter += trans;
                                enemy[i].Position += trans;
                            }

                            EnemyHitCheck(i, trans);
                            break;
                        case 2:
                            // �X�e�[�W�O�܂��͗\���|�C���g�ɂ�����v���C���[�Ɍ�����
                            if ((playerPos.Z < -spawnArea || playerPos.Z > spawnArea || playerPos.X < -spawnArea || playerPos.X > spawnArea) ||
                                PlayerPosHit == true)
                            {
                                trans = direction[i] * moveSpeed2;

                                enemy[i].Position += trans;
                                enemy[i].SphereCenter += trans;
                            }
                            else
                            {
                                // �\���|�C���g�ɂ�����
                                if (enemy[i].Sphere.Intersects(playerPosSphere))
                                {
                                    PlayerPosHit = true;
                                }
                                else // �\���|�C���g�ɂ��Ȃ��Ȃ�
                                {
                                    trans = flontDirection[i] * moveSpeed2;

                                    enemy[i].Position += trans;
                                    enemy[i].SphereCenter += trans;
                                }
                            }

                            EnemyHitCheck(i, trans);
                            break;
                        case 3:
                            trans = direction[i] * moveSpeed3;

                            enemy[i].Position += trans;
                            enemy[i].SphereCenter += trans;

                            EnemyHitCheck(i, trans);
                            break;
                        case 4:
                        case 5:
                            if (enemy[i].Sphere.Intersects(player.TrackingArea))
                            {
                                trans = direction[i] * moveSpeed4;

                                enemy[i].Position += trans;
                                enemy[i].SphereCenter += trans;

                                EnemyHitCheck(i, trans);
                            }
                            else
                            {
                                if (enemy[i].Position.X >= moveLimit4)
                                {
                                    enemy[i].PositionX = moveLimit4;
                                    enemy[i].SphereX = moveLimit4;

                                    enemy[i].Reverse = true;
                                }
                                else if (enemy[i].Position.X <= -moveLimit4)
                                {
                                    enemy[i].PositionX = -moveLimit4;
                                    enemy[i].SphereX = -moveLimit4;

                                    enemy[i].Reverse = false;
                                }

                                if (enemy[i].Reverse)
                                {
                                    enemy[i].PositionX -= moveSpeed4;
                                    enemy[i].SphereX -= moveSpeed4;
                                }
                                else
                                {
                                    enemy[i].PositionX += moveSpeed4;
                                    enemy[i].SphereX += moveSpeed4;
                                }
                            }
                            break;
                        case 6:
                            if (enemy[i].Sphere.Intersects(player.TrackingArea))
                            {
                                trans = direction[i] * moveSpeed6;

                                enemy[i].Position += trans;
                                enemy[i].SphereCenter += trans;

                                EnemyHitCheck(i, trans);
                            }
                            else
                            {
                                if (enemy[i].Position.X >= moveLimit6)
                                {
                                    enemy[i].PositionX = moveLimit6;
                                    enemy[i].SphereX = moveLimit6;

                                    enemy[i].Reverse = true;
                                }
                                else if (enemy[i].Position.X <= -moveLimit6)
                                {
                                    enemy[i].PositionX = -moveLimit6;
                                    enemy[i].SphereX = -moveLimit6;

                                    enemy[i].Reverse = false;
                                }

                                if (enemy[i].Reverse)
                                {
                                    enemy[i].PositionX -= moveSpeed6;
                                    enemy[i].SphereX -= moveLimit6;
                                }
                                else
                                {
                                    enemy[i].PositionX += moveSpeed6;
                                    enemy[i].SphereX += moveSpeed6;
                                }
                            }
                            break;
                        case 7:
                            if (enemy[i].Sphere.Intersects(player.TrackingArea))
                            {
                                trans = direction[i] * moveSpeed7;

                                enemy[i].Position += trans;
                                enemy[i].SphereCenter += trans;

                                EnemyHitCheck(i, trans);
                            }
                            else
                            {
                                if (enemy[i].Position.Z >= moveLimit7)
                                {
                                    enemy[i].PositionZ = moveLimit7;
                                    enemy[i].Initialize();

                                    enemy[i].Reverse = true;
                                }
                                else if (enemy[i].Position.Z <= -moveLimit7)
                                {
                                    enemy[i].PositionZ = -moveLimit7;
                                    enemy[i].Initialize();

                                    enemy[i].Reverse = false;
                                }

                                if (enemy[i].Reverse)
                                {
                                    enemy[i].PositionZ -= moveSpeed7;
                                    enemy[i].SphereZ -= moveSpeed7;
                                }
                                else
                                {
                                    enemy[i].PositionZ += moveSpeed7;
                                    enemy[i].SphereZ += moveSpeed7;
                                }
                            }
                            break;
                        case 8:
                            if (enemy[i].Sphere.Intersects(player.TrackingArea))
                            {
                                trans = direction[i] * moveSpeed8;

                                enemy[i].Position += trans;
                                enemy[i].SphereCenter += trans;

                                EnemyHitCheck(i, trans);
                            }
                            else
                            {
                                trans = direction[i] * moveSpeed8;

                                enemy[i].Position += trans;
                                enemy[i].SphereCenter += trans;
                            }
                            break;
                        case 9:
                            trans = direction[i] * moveSpeed9;

                            enemy[i].Position += trans;
                            enemy[i].SphereCenter += trans;

                            EnemyHitCheck(i, trans);
                            break;
                        default:
                            trans = direction[i] * moveSpeed0;

                            enemy[i].Position += trans;
                            enemy[i].SphereCenter += trans;

                            EnemyHitCheck(i, trans);
                            break;
                    }
                }
                enemy[i].World = UpdateMatrix(enemy[i].Position, enemy[i].Scale, new Vector3(0, (float)Math.Atan2(direction[i].X, direction[i].Z), 0));
            }
            enemyTime = false;
        }


        private void BossUpdate(GameTime gameTime)
        {
            // �{�X1�̓���
            if (boss.HitPoint > 0)
            {
                enemyTime = true;

                Vector3 direction;

                BossHit(gameTime);

                direction = player.Position - boss.Position;
                direction.Normalize();

                boss.World = UpdateMatrix(boss.Position, boss.Scale, new Vector3(0, (float)Math.Atan2(direction.X, direction.Z), 0));

                if (boss.Respawn == false)
                {
                    BossMovement(gameTime);
                    FrontRush(gameTime);
                    LeftorRightRush(gameTime);

                    boss.DefenselessTime = 5;//defenselessTime�̒l�ƂƓ����ɂ���
                }

                for (int i = 0; i < enemyNum; i++)
                {
                    float x = random.Next((int)boss.BoundingBoxMinX, (int)boss.BoundingBoxMaxX);
                    float y = random.Next(100);
                    float z = random.Next((int)boss.BoundingBoxMinZ, (int)boss.BoundingBoxMaxZ);

                    bossEffectPos[i] = new Vector3(x, y, z);
                }
            }
            enemyTime = false;

        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (!gameOver && !gameClear || onceUpdate)
            {
                if (exit)
                    exit = false;

                if (InputManager.IsKeyDown(Keys.Escape) || InputManager.IsButtonDown(PlayerIndex.One, Buttons.Back))
                    exit = true;

                if (result == false)
                {
                    time = DateTime.Now - tempTime;
                    timeLeft = timeLimit - (float)Math.Floor(time.TotalSeconds) + addTime;
                }

                if (timeLeft <= 0 || hp <= 0)
                    gameOver = true;

                if (enemyCount == enemyCountMax || bossEffectNum > 20)
                    gameClear = true;
                
                PlayerUpdate(gameTime);

                if (normalScene)
                    EnemyUpdate(gameTime);
                else if (bossScene)
                    BossUpdate(gameTime);

                camera.Update(gameTime);
                CameraMotion.ThirdPersonMotion(ref camera, player.Position, cameraRot);

                // �X�R�A�v�Z
                if (player.RotationMode)
                {
                    for (int i = 0; i < enemyNum; i++)
                    {
                        if (enemy[i].Hit)
                            combo++;
                    }
                }
                else
                {
                    score += combo * combo;
                    addTime += (float)Math.Round(combo * 1.2f);
                    totalScore = score * 100;
                    recoveryTime = comboRecoveryTime * combo;
                    combo = 0;
                }

                AddParticle();

                if (onceUpdate)
                {
                    addTime += 30.0f;

                    for (int i = 0; i < enemyNum; i++)
                        hitEffect[i] = false;

                    onceUpdate = false;
                }
            }
            else if(push)
            {
                normalScene = false;
                bossScene = true;

                if (maxAlpha > fadeAlpha && !fadeMaxReace)
                {
                    fadeAlpha += 2.0f;
                    fadeClearPosition += 20.157f;
                    fadePushPosition -= 20.157f;
                }

                if (fadeAlpha >= maxAlpha)
                    onceUpdate = true;

                if(fadeMaxReace || fadeAlpha >= maxAlpha)
                {
                    fadeMaxReace = true;
                    fadeAlpha -= 4.0f;
                }

                if (fadeAlpha <= 0)
                {
                    if (gameOver)
                        result = true;
                    else
                    {
                        hp = maxHp;
                        enemyCount = 0;

                        fadeAlpha = 0.0f;

                        fadeClearPosition = -1280.0f;
                        fadePushPosition = 1280.0f;

                        gameClear = false;
                        push = false;
                    }
                }
            }
            else
            {
                if (pushTimeAlpha > fadeAlpha)
                {
                    fadeAlpha += 2.0f;
                    fadeClearPosition += 20.157f;
                    fadePushPosition -= 20.157f;
                }

                if (InputManager.AnyButtonDown(PlayerIndex.One))
                {
                    push = true;
                    if (bossScene)
                        result = true;
                }
            }

            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(bgm);
                MediaPlayer.Volume = bgmVolume;
            }
            else if (result || exit)
                MediaPlayer.Stop();

            //���U���g��ʂ̏���
            if (result == true)
            {
                gameClear = false;

                float z = timeLeft * 100;

                clearTime = z;

                clearScore = totalScore;

                if (gameOver)
                    bossBonus = 0;

                clearTotalScore = clearScore + clearTime + bossBonus;

                if (resultTime <= resultShowTime)
                {
                    resultTime += 1.0f / 60.0f;
                }
                else
                {
                    if (InputManager.AnyButtonDown(PlayerIndex.One))
                    {
                        exit = true;
                    }
                }
            }


            //�f�o�b�N�p
            if (InputManager.IsKeyDown(Keys.P))
            {
                normalScene = false;
                bossScene = true;
            }

            base.Update(gameTime);
        }

        #endregion

        #region ���\�b�h

        private void SetCameraEffect(Camera camera)
        {
            rotationParticle.SetCamera(camera.View, camera.Projection);
            notRotationParticle.SetCamera(camera.View, camera.Projection);

            for (int i = 0; i < enemyNum; i++)
            {
                enemyDeadParticle[i].SetCamera(camera.View, camera.Projection);
                enemyAttackParticle[i].SetCamera(camera.View, camera.Projection);
            }
        }

        private void BossMovement(GameTime gameTime)
        {
            #region �v���C���[�̈ʒu���{�X�̐i�s�����ω�  
            if (boss.PositionZ < player.Position.Z && boss.FrontRush == false)
            {
                boss.Reverse = false;
            }
            else if (boss.PositionZ > player.Position.Z && boss.FrontRush == false)
            {
                boss.Reverse = true;
            }

            if (boss.PositionX < player.Position.X && boss.LeftAndRightRush == false)
            {
                boss.LeftOrRight = false;
            }
            else if (boss.PositionX > player.Position.X && boss.LeftAndRightRush == false)
            {
                boss.LeftOrRight = true;
            }
            #endregion

            #region �ːi�\�͈͂ɂ��邩

            // �O�ːi�\�͔͈͂ɂ��邩
            if (Intersects(boss.FrontAndBackBox) && boss.LeftAndRightRush == false)
            {
                boss.FrontRush = true;

                boss.LeftAndRightRush = false;
            }
            // ���E�ːi�\�͔͈͂ɂ��邩
            else if (Intersects(boss.LeftAndRightBox) && boss.FrontRush == false)
            {
                boss.LeftAndRightRush = true;

                boss.FrontRush = false;
            }

            #endregion

            #region ��{�s���i���E�ړ�)
            if (boss.FrontRush == false && boss.LeftAndRightRush == false)
            {
                // �����v���C���[X�ƃ{�XX���Ⴄ�Ȃ�playerPos�̈ʒu�܂�X�ړ�
                if (player.Position.X < boss.PositionX)
                {
                    boss.PositionX -= boss.Speed;
                    boss.BoundingBoxMinX -= boss.Speed;
                    boss.BoundingBoxMaxX -= boss.Speed;
                    boss.FrontAndBackBoxMaxX -= boss.Speed;
                    boss.FrontAndBackBoxMinX -= boss.Speed;

                    if (player.Position.X > boss.PositionX)
                    {
                        boss.PositionX = player.Position.X;
                        boss.BoundingBoxMinX = player.Position.X;
                        boss.BoundingBoxMaxX = player.Position.X;
                        boss.FrontAndBackBoxMaxX = player.Position.X;
                        boss.FrontAndBackBoxMinX = player.Position.X;
                    }
                }

                if (player.Position.X > boss.PositionX)
                {
                    boss.PositionX += boss.Speed;
                    boss.BoundingBoxMinX += boss.Speed;
                    boss.BoundingBoxMaxX += boss.Speed;
                    boss.FrontAndBackBoxMaxX += boss.Speed;
                    boss.FrontAndBackBoxMinX += boss.Speed;

                    if (player.Position.X < boss.PositionX)
                    {
                        boss.PositionX = player.Position.X;
                        boss.BoundingBoxMinX = player.Position.X;
                        boss.BoundingBoxMaxX = player.Position.X;
                        boss.FrontAndBackBoxMaxX = player.Position.X;
                        boss.FrontAndBackBoxMinX = player.Position.X;
                    }
                }
            }
            #endregion
        }

        // �O�ːi����
        private void FrontRush(GameTime gameTime)
        {
            // �O�ɓːi
            if (boss.FrontRush == true)
            {
                boss.RushFrontTime += 1.0f / 60.0f;

                // �v���C���[�̈ʒu��ۑ�
                boss.PlayerPos = player.Position;

                if (boss.RushFrontTime > boss.RushStartTime)
                {
                    if (boss.Reverse == false)
                    {   // �v���C���[�̃|�W�V�����{overPosition�܂ňړ�
                        if (boss.PlayerPos.Z + boss.OverPosition > boss.PositionZ)
                        {
                            boss.PositionZ += boss.RushSpeed;
                            boss.BoundingBoxMinZ += boss.RushSpeed;
                            boss.BoundingBoxMaxZ += boss.RushSpeed;
                            boss.LeftAndRightBoxMinZ += boss.RushSpeed;
                            boss.LeftAndRightBoxMaxZ += boss.RushSpeed;
                        }
                        else
                        {
                            boss.FrontRush = false;
                            boss.RushFrontTime = 0;
                        }
                    }
                    else
                    {   // �v���C���[�̃|�W�V�����[overPosition�܂ňړ�
                        if (boss.PlayerPos.Z - boss.OverPosition < boss.PositionZ)
                        {
                            boss.PositionZ -= boss.RushSpeed;
                            boss.BoundingBoxMinZ -= boss.RushSpeed;
                            boss.BoundingBoxMaxZ -= boss.RushSpeed;
                            boss.LeftAndRightBoxMinZ -= boss.RushSpeed;
                            boss.LeftAndRightBoxMaxZ -= boss.RushSpeed;
                        }
                        else
                        {
                            boss.FrontRush = false;
                            boss.RushFrontTime = 0;
                        }
                    }
                }
            }
        }

        // ���E�ːi����
        private void LeftorRightRush(GameTime gameTime)
        {
            // ���E�ɓːi
            if (boss.LeftAndRightRush == true)
            {
                boss.RushLfetAndRightTime += 1.0f / 60.0f;

                // �v���C���[�̈ʒu��ۑ�
                boss.PlayerPos = player.Position;

                if (boss.RushLfetAndRightTime > boss.RushStartTime)
                {
                    if (boss.LeftOrRight == false)
                    {   // �v���C���[�̃|�W�V�����{overPosition�܂ňړ�
                        if (boss.PlayerPos.X + boss.OverPosition > boss.PositionX)
                        {
                            boss.PositionX += boss.RushSpeed;
                            boss.BoundingBoxMinX += boss.RushSpeed;
                            boss.BoundingBoxMaxX += boss.RushSpeed;
                            boss.FrontAndBackBoxMaxX += boss.RushSpeed;
                            boss.FrontAndBackBoxMinX += boss.RushSpeed;
                        }
                        else
                        {
                            boss.LeftAndRightRush = false;
                            boss.RushLfetAndRightTime = 0;
                        }
                    }
                    else
                    {   // �v���C���[�̃|�W�V�����[overPosition�܂ňړ�
                        if (boss.PlayerPos.X - boss.OverPosition < boss.PositionX)
                        {
                            boss.PositionX -= boss.RushSpeed;
                            boss.BoundingBoxMinX -= boss.RushSpeed;
                            boss.BoundingBoxMaxX -= boss.RushSpeed;
                            boss.FrontAndBackBoxMaxX -= boss.RushSpeed;
                            boss.FrontAndBackBoxMinX -= boss.RushSpeed;
                        }
                        else
                        {
                            boss.LeftAndRightRush = false;
                            boss.RushLfetAndRightTime = 0;
                        }
                    }
                }
            }
        }

        // �v���C���[�ƃ{�X�̓����蔻�聕���[�v��̏���
        private void BossHit(GameTime gameTime)
        {
            if (boss.HitPoint > 0)
            {
                // �������Ƀv���C���[����]���Ă�����(�v���C���[���U�����Ȃ�)�̏���������
                // �ːi���̓_���[�W�󂯂Ȃ�
                if (Intersects(boss.BoundingBox))
                {
                    boss.Hit = true;

                    hitEffect[0] = true;
                    effectPos[0] = boss.Position;
                    if (!player.RotationMode)
                        hitPlayerIsRotation[0] = false;
                    else
                        hitPlayerIsRotation[0] = true;

                    if (player.RotationMode == true && boss.FrontRush == false && boss.LeftAndRightRush == false)
                    {
                        boss.Invincible = true;

                        if (boss.HitTime == boss.InvincibleTime)
                        {
                            if (boss.Hit == true)
                            {
                                boss.HitPoint -= 1;//�v���C���[�Ƃ�����ƃ}�C�i�X
                            }
                        }
                    }

                    // ���G���Ԓ���
                    if (boss.Invincible == true)
                    {
                        boss.HitTime -= 1.0f / 60.0f;

                        if (boss.HitTime <= 0)
                        {
                            boss.HitTime = boss.InvincibleTime;
                            boss.Invincible = false;
                        }
                    }
                }
                else
                {
                    if (boss.Hit && boss.FrontRush && !boss.Invincible|| boss.Hit && boss.LeftAndRightRush && !boss.Invincible)
                    {
                        hp -= boss.Damage;
                        hitPlayerIsRotation[0] = false;
                        boss.Hit = false;
                    }
                }
            }

            // �X�e�[�W�O�ɂ��z������{�X�������Ƀ��[�v
            if (boss.PositionZ >= wall || boss.PositionZ <= -wall
                || boss.PositionX >= wall || boss.PositionX <= -wall)
            {
                boss.Position = new Vector3(0.0f, 40.0f, 0.0f);
                boss.Initialize();
                boss.RushFrontTime = 0;
                boss.RushLfetAndRightTime = 0;
                boss.HitTime = 5.0f;
                boss.HitTime = boss.InvincibleTime;
                boss.LeftAndRightRush = false;
                boss.FrontRush = false;
                boss.Respawn = true;
            }

            // ���X�|�[��
            if (boss.Respawn == true)
            {
                boss.DefenselessTime -= 1.0f / 60.0f;

                if (boss.DefenselessTime < 0)
                {
                    boss.Respawn = false;
                }
            }
        }

        // �\���|�C���g�ݒu�̏���
        public void Prediction()
        {
            Vector3 forWard = Vector3.Normalize(player.World.Forward);

            // ��] or playerPosition��playerPos����v������\���|�C���g�X�V
            if (player.RotationMode == true || player.Position.X == playerPos.X ||
                player.Position.Z == playerPos.Z)
            {
                playerPos = player.Position;
                playerPos += forWard * flontDistance;
                playerPosSphere.Center = playerPos;
            }
        }

        // �v���C���[�̈��͈͂Ƀ����_���ňړ��|�C���g�ݒu�̏���
        public void RandomPoint(int i)
        {
            if (randomPosSet[i] == false)
            {
                if (random.Next(2) == 0)
                    // �ŏ��A�ő�
                    randomPos[i].X = random.Next((int)player.Position.X, (int)player.Position.X + randomArea);
                else
                    randomPos[i].X = random.Next((int)player.Position.X - randomArea, (int)player.Position.X);

                if (random.Next(2) == 0)
                    randomPos[i].Z = random.Next((int)player.Position.Z, (int)player.Position.Z + randomArea);
                else
                    randomPos[i].Z = random.Next((int)player.Position.Z - randomArea, (int)player.Position.Z);

                randomSphere[i].Center = randomPos[i];
            }

            if (randomPos[i].X < spawnArea && randomPos[i].X > -spawnArea)
            {
                if (randomPos[i].Z < spawnArea && randomPos[i].Z > -spawnArea)
                    randomPosSet[i] = true;
            }

        }


        // �R���W�����O���
        private void UpdateCollision(Vector3 t)
        {
            for (int i = 0; i < player.RayNum; i++)
                player.IntersectRay[i].Position += t;

            player.DistanceUpdate();

            player.TrackingAreaMin += t;
            player.TrackingAreaMax += t;
        }

        // �R���W������]�ړ�
        private void UpdateCollision(Vector3 t, Vector3 center, Vector3 radian)
        {
            for (int i = 0; i < player.RayNum; i++)
                player.IntersectRay[i].Position = RotationTrans(player.IntersectRay[i].Position, center, radian);

            player.DistanceUpdate();

            player. TrackingAreaMin += t;
            player.TrackingAreaMax += t;
        }

        // ��]�v�Z
        private Vector3 RotationTrans(Vector3 posA, Vector3 posB, Vector3 radian)
        {
            Vector3 ans = Vector3.Zero;

            ans.X = (posA.X - posB.X) * (float)Math.Cos(radian.Y) - (posA.Z - posB.Z) * (float)Math.Sin(radian.Y) + posB.X;
            ans.Z = (posA.X - posB.X) * (float)Math.Sin(radian.Y) + (posA.Z - posB.Z) * (float)Math.Cos(radian.Y) + posB.Z;

            return ans;
        }

        // �v���C���[�����蔻��
        private bool Intersects(BoundingBox box)
        {
            player.ForwardHit = false;
            player.BackwardHit = false;

            for (int i = 0; i < player.RayNum; i++)
            {
                switch (i)
                {
                    case (int)rayDistance.forward:
                        if (player.IntersectRay[i].Intersects(box) < playerFrontRayReach)
                        {
                            if (player.BackwardRotation)
                                player.ForwardHit = false;

                            return true;
                        }
                        break;
                    case (int)rayDistance.backward:
                        if (player.IntersectRay[i].Intersects(box) < playerFrontRayReach)
                        {
                            if (player.ForwardRotation)
                                player.BackwardHit = false;

                            return true;
                        }
                        break;
                    case (int)rayDistance.right:
                    case (int)rayDistance.left:
                        if (player.IntersectRay[i].Intersects(box) < playerSideRayReach)
                        {
                            return true;
                        }
                        break;
                }
            }
            return false;
        }

        // �v���C���[�����蔻��(�X�t�B�A��)
        private bool Intersects(BoundingSphere sphere)
        {
            player.ForwardHit = false;
            player.BackwardHit = false;

            for (int i = 0; i < player.RayNum; i++)
            {
                switch (i)
                {
                    case (int)rayDistance.forward:
                        if (player.IntersectRay[i].Intersects(sphere) < playerFrontRayReach)
                        {
                            if (player.BackwardRotation)
                                player.ForwardHit = false;

                            return true;
                        }
                        break;
                    case (int)rayDistance.backward:
                        if (player.IntersectRay[i].Intersects(sphere) < playerFrontRayReach)
                        {
                            if (player.ForwardRotation)
                                player.BackwardHit = false;

                            return true;
                        }
                        break;
                    case (int)rayDistance.right:
                    case (int)rayDistance.left:
                        if (player.IntersectRay[i].Intersects(sphere) < playerSideRayReach)
                        {
                            return true;
                        }
                        break;
                }
            }
            return false;
        }

        // �X�e�[�W�ǔ���
        private bool StageCollision()
        {
            for (int i = 0; i < stage.BoxNum; i++)
            {
                for (int j = 0; j < player.RayNum; j++)
                {
                    switch (j)
                    {
                        case (int)rayDistance.forward:
                        case (int)rayDistance.backward:
                            if (stage.Box[i].Intersects(player.IntersectRay[j]) < playerFrontRayReach)
                                return true;
                            break;
                        case (int)rayDistance.right:
                        case (int)rayDistance.left:
                            if (stage.Box[i].Intersects(player.IntersectRay[j]) < playerSideRayReach)
                                return true;
                            break;
                        default:
                            break;
                    }
                }
            }
            return false;
        }

        private Matrix UpdateMatrix(Vector3 p,Vector3 s,Vector3 r)
        {
            Matrix translationMatrix, scalingMatrix, rotationMatrix, rotationX, rotationY, rotationZ, world;

            // ���s�ړ��s��̌v�Z
            translationMatrix = Matrix.CreateTranslation(p);

            // �g��k���s��̌v�Z
            scalingMatrix = Matrix.CreateScale(s);

            // X�����S�̉�]�s����v�Z����
            rotationX = Matrix.CreateRotationX(MathHelper.ToRadians(r.X));

            if (!enemyTime)
                // Y�����S�̉�]�s����v�Z����
                rotationY = Matrix.CreateRotationY(MathHelper.ToRadians(r.Y));
            else
                rotationY = Matrix.CreateRotationY(r.Y);

            // Z�����S�̉�]�s����v�Z����
            rotationZ = Matrix.CreateRotationZ(MathHelper.ToRadians(r.Z));

            // ��]�s��̍���
            rotationMatrix = rotationX * rotationY * rotationZ;

            // ���[���h�ϊ��s����v�Z����
            // ���f�����g��k�����A��]���������ƈړ�������
            world = scalingMatrix * rotationMatrix * translationMatrix;

            return world;
        }

        //���X�t�B�A�ɕύX
        private void EnemyHitCheck(int i, Vector3 t)
        {
            for (int j = 0; j < enemyNum; j++)
            {
                if (i == j)
                    continue;

                if (enemy[i].Sphere.Intersects(enemy[j].Sphere))
                {
                    enemy[i].PositionX -= t.X;
                    enemy[i].SphereX -= t.X;

                    if (enemy[i].Sphere.Intersects(enemy[j].Sphere))
                    {
                        enemy[i].PositionZ -= t.Z;
                        enemy[i].SphereZ -= t.Z;

                        enemy[i].PositionX += t.X;
                        enemy[i].SphereX += t.X;

                        if (enemy[i].Sphere.Intersects(enemy[j].Sphere))
                        {
                            enemy[i].PositionX -= t.X;
                            enemy[i].SphereX -= t.X;
                        }
                    }
                }
            }
        }

        private void AddParticle()
        {
            if (move)
            {

                for (int i = 0; i < 12; i++)
                {
                    if (player.RotationMode)
                    {
                        rotationParticle.AddParticle(new Vector3(player.LeftBallRotation.X, player.LeftBallRotation.Y + 15.0f, player.LeftBallRotation.Z), Vector3.Zero);
                        rotationParticle.AddParticle(new Vector3(player.RightBallRotation.X, player.RightBallRotation.Y + 15.0f, player.RightBallRotation.Z), Vector3.Zero);
                    }
                    else
                    {
                        notRotationParticle.AddParticle(new Vector3(player.LeftBallRotation.X, player.LeftBallRotation.Y + 15.0f, player.LeftBallRotation.Z), Vector3.Zero);
                        notRotationParticle.AddParticle(new Vector3(player.RightBallRotation.X, player.RightBallRotation.Y + 15.0f, player.RightBallRotation.Z), Vector3.Zero);
                    }
                }

            }

            for (int i = 0; i < enemyNum; i++)
            {
                if (hitEffect[i])
                {
                    if (particleRadius[i] < 100)
                    {
                        for (int j = 0; j < 100; j++)
                        {
                            if (hitPlayerIsRotation[i])
                                enemyDeadParticle[i].AddParticle(RandomPointOnSphere(i) + new Vector3(0, 10.0f, 0) + effectPos[i], Vector3.Zero);
                            else
                                enemyAttackParticle[i].AddParticle(RandomPointOnSphere(i) + new Vector3(0, 10.0f, 0) + effectPos[i], Vector3.Zero);

                        }
                        particleRadius[i] += 4;
                    }
                    else
                    {
                        particleRadius[i] = 0;
                        hitEffect[i] = false;
                    }
                }
            }
            if (boss.HitPoint <= 0)
            {
                for (int i = 0; i < enemyNum / 2; i++)
                {
                    if (particleRadius[i] < 100)
                    {
                        for (int j = 0; j < 1000; j++)
                        {
                            enemyDeadParticle[i].AddParticle(RandomPointOnSphere(i) + bossEffectPos[i], Vector3.Zero);
                        }
                        particleRadius[i] += 4;
                    }
                    else
                    {
                        particleRadius[i] = 0;
                        bossEffectNum++;
                    }
                }
            }
        }

        Vector3 RandomPointOnSphere(int i)
        {
            double angle = random.NextDouble() * Math.PI * 2;
            double angle2 = random.NextDouble() * Math.PI * 2;

            float x = (float)Math.Cos(angle) * (float)Math.Cos(angle2) * particleRadius[i];
            float y = (float)Math.Cos(angle) * (float)Math.Sin(angle2) * particleRadius[i];
            float z = (float)Math.Sin(angle) * particleRadius[i];

            return new Vector3(x, y, z);
        }

        #endregion

        #region �`��

        private void DrawEffect(GameTime gameTime)
        {

            rotationParticle.UserDraw(gameTime);
            notRotationParticle.UserDraw(gameTime);

            for (int i = 0; i < enemyNum; i++)
            {
                enemyDeadParticle[i].UserDraw(gameTime);
                enemyAttackParticle[i].UserDraw(gameTime);
            }

            device.BlendState = BlendState.Opaque;
        }

        private void DrawModel(Model model, Matrix[] transform, Matrix world)
        {
            // ���f�����̃��b�V�������ׂĕ`�悷��
            foreach (ModelMesh mesh in model.Meshes)
            {
                // �f�t�H���g���C�e�B���O��L���ɂ���
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();

                    // �K�v�ȍs���ݒ肷��
                    effect.View = camera.View;                              // �r���[�ϊ��s��
                    effect.Projection = camera.Projection;                  // �ˉe�ϊ��s��
                    effect.World = transform[mesh.ParentBone.Index] * world;  // ���f���̃��[���h�ϊ��s��
                }
                // ���b�V���̕`��
                mesh.Draw();
            }
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            device.DepthStencilState = DepthStencilState.Default;

            DrawModel(stage.ModelData, stage.BonesData, Matrix.Identity);
            
            DrawModel(player.ModelData, player.BonesData, player.World);

            if (normalScene)
            {
                for (int i = 0; i < enemyNum; i++)
                {
                    if (!enemy[i].Hit)
                        DrawModel(enemy[i].ModelData, enemy[i].BonesData, enemy[i].World);
                }
            }
            else if (bossScene && bossEffectNum < 20)
                DrawModel(boss.ModelData, boss.BonesData, boss.World);

            SetCameraEffect(camera);
            DrawEffect(gameTime);

            spriteBatch.Begin();

            spriteBatch.Draw(timeTexture, timeTexturePosition, Color.White);
            spriteBatch.Draw(scoreTexture, scoreTexturePosition, Color.White);
            spriteBatch.Draw(hpPlateTexture, hpTexturePosition, Color.White);

            for (int i = 0; i < hp; i++)
                spriteBatch.Draw(hpTexture[i], hpTexturePosition, Color.White);

            spriteBatch.Draw(hpBaseTexture, hpTexturePosition, Color.White);

            if (gameOver || gameClear)
            {
                spriteBatch.Draw(fadeTexture,
        new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
        new Color(Color.Black.R, Color.Black.G, Color.Black.B, (byte)fadeAlpha));

                if (gameOver)
                    spriteBatch.Draw(gameOverTexture, new Vector2(fadeClearPosition, 0), Color.White);
                else
                    spriteBatch.Draw(gameClearTexture, new Vector2(fadeClearPosition, 0), Color.White);

                spriteBatch.Draw(pushTexture, new Vector2(fadePushPosition, 0), Color.White);
            }

            // ���U���g
            if (result)
            {
                spriteBatch.Draw(resultTexture, new Vector2(0, 0), Color.White);
                spriteBatch.Draw(stageTexture, new Vector2(0, 0), Color.White);

                if (resultTime >= 2.5f)
                {
                    // ���Q�[���I�[�o�[��������F�����N
                    if (gameOver || clearTotalScore < bRankScore)
                        spriteBatch.Draw(fRankTexture, new Vector2(1060, 470), Color.White);
                    else
                    {
                        // �������N�̔���
                        if (clearTotalScore > aRankScore && clearTotalScore < sRankScore)
                            spriteBatch.Draw(aRankTexture, new Vector2(1060, 470), Color.White);
                        if (clearTotalScore < aRankScore && clearTotalScore > bRankScore)
                            spriteBatch.Draw(bRankTexture, new Vector2(1060, 470), Color.White);
                        if (clearTotalScore > sRankScore)
                            spriteBatch.Draw(sRankTexture, new Vector2(1060, 470), Color.White);
                    }

                }


                if (resultTime >= resultShowTime)
                    spriteBatch.Draw(pushTexture, new Vector2(0, 0), Color.White);
            }

            spriteBatch.End();

            #region �f�o�b�O�p
            float line = 15.0f;
            float s = totalScore;
            float n = combo;
            string a = "score:" + s;
            string b = "conbo:" + n;
            string c = "TIME:" + (timeLeft);
            string e = "boss :" + boss.HitPoint;
            string f = "break:" + ab;

            string g = "" + clearTime;
            string h = "" + clearScore;
            string j = "" + bossBonus;
            string k = "" + clearTotalScore;
            
            Game1.spriteBatch.Begin();

            if (result == false)
            {
                Game1.spriteBatch.DrawString(Game1.font, a, new Vector2(570, 12), Color.Black);
                Game1.spriteBatch.DrawString(Game1.font2, c, new Vector2(67, 23), Color.Black);
            }
            else //���U���g�֘A
            {
                if (resultTime > 0.5f)
                    Game1.spriteBatch.DrawString(Game1.font2, h, new Vector2(640, 215), Color.Black);
                if (resultTime > 1.0f)
                    Game1.spriteBatch.DrawString(Game1.font2, g, new Vector2(640, 320), Color.Black);
                if (resultTime > 1.5f)
                    Game1.spriteBatch.DrawString(Game1.font2, j, new Vector2(640, 425), Color.Black);
                if (resultTime > 2.0f)
                    Game1.spriteBatch.DrawString(Game1.font2, k, new Vector2(640, 535), Color.Black);

            }
            
            Game1.spriteBatch.End();
            #endregion
        }

        #endregion

        #region ���\�b�h

        public bool IsExit()
        {
            return exit;
        }

        #endregion
    }
}
