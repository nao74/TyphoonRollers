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
        #region �t�B�[���h

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
        /// �{�X�̑O�������o�E���f�B���O�{�b�N�X
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
        /// �{�X�̍��E������{�b�N�X
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
        /// �{�X�̌���
        /// </summary>
        private float angle;

        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        /// <summary>
        /// �v���C���[�̈ʒu�ۑ��p
        /// </summary>
        private Vector3 playerPos;

        public Vector3 PlayerPos
        {
            get { return playerPos; }
            set { playerPos = value; }
        }

        /// <summary>
        /// �v���C���[�Ƃ̂����蔻��
        /// </summary>
        private bool hit;

        public bool Hit
        {
            get { return hit; }
            set { hit = value; }
        }

        /// <summary>
        /// �O�ːi����
        /// </summary>
        private bool frontRush;

        public bool FrontRush
        {
            get { return frontRush; }
            set { frontRush = value; }
        }

        /// <summary>
        /// ���E�ːi����
        /// </summary>
        private bool leftAndRightRush;

        public bool LeftAndRightRush
        {
            get { return leftAndRightRush; }
            set { leftAndRightRush = value; }
        }

        /// <summary>
        /// �{�X�̑O�i�s����
        /// </summary>
        private bool reverse;

        public bool Reverse
        {
            get { return reverse; }
            set { reverse = value; }
        }

        /// <summary>
        /// ���������E������
        /// </summary>
        private bool leftOrRight;

        public bool LeftOrRight
        {
            get { return leftOrRight; }
            set { leftOrRight = value; }
        }

        /// <summary>
        /// �ǂɓ����胏�[�v������
        /// </summary>
        private bool respawn;

        public bool Respawn
        {
            get { return respawn; }
            set { respawn = value; }
        }

        /// <summary>
        /// ���G����
        /// </summary>
        private bool invincible;

        public bool Invincible
        {
            get { return invincible; }
            set { invincible = value; }
        }

        /// <summary>
        /// �̗�
        /// </summary>
        private static float hitPoint;

        public float HitPoint
        {
            get { return hitPoint; }
            set { hitPoint = value; }
        }

        /// <summary>
        /// �{�X�̊�{�X�s�[�h
        /// </summary>
        private const float speed = 4.0f;
        public float Speed
        {
            get { return speed; }
        }

        /// <summary>
        /// �ːi���̃X�s�[�h
        /// </summary>
        private const float rushSpeed = 12.0f;
        public float RushSpeed
        {
            get { return rushSpeed; }
        }

        /// <summary>
        /// �O�ːi�܂ł̎���(�[���X�^�[�g)
        /// </summary>
        private float rushFrontTime = 0.0f;
        public float RushFrontTime
        {
            get { return rushFrontTime; }
            set { rushFrontTime = value; }
        }

        /// <summary>
        /// ���E�ːi�܂ł̎���(�[���X�^�[�g)
        /// </summary>
        float rushLfetAndRightTime = 0.0f;
        public float RushLfetAndRightTime
        {
            get { return rushLfetAndRightTime; }
            set { rushLfetAndRightTime = value; }
        }

        /// <summary>
        /// ���b��������ːi���邩
        /// </summary>
        private const float rushStartTime = 2.0f;
        public float RushStartTime
        {
            get { return rushStartTime; }
        }

        /// <summary>
        /// �������Ă���̎���
        /// </summary>
        private float hitTime = 5.0f;
        public float HitTime
        {
            get { return hitTime; }
            set { hitTime = value; }
        }

        /// <summary>
        /// ���G���̎���
        /// </summary>
        private float invincibleTime = 5.0f;
        public float InvincibleTime
        {
            get { return invincibleTime; }
            set { invincibleTime = value; }
        }

        /// <summary>
        /// ���[�v�㖳�h������
        /// </summary>
        private float defenselessTime = 5.0f;
        public float DefenselessTime
        {
            get { return defenselessTime; }
            set { defenselessTime = value; }
        }

        /// <summary>
        /// �t�B�[���h�̍L��
        /// </summary>
        private const int wall = 1600;
        public int Wall
        {
            get { return wall; }
        }

        /// <summary>
        /// �ːi��v���C���[�̏ꏊ����ǂꂭ�炢�܂ōs����
        /// </summary>
        private const int overPosition = 300;
        public int OverPosition
        {
            get { return overPosition; }
        }

        /// <summary>
        /// �{�X�̍ő�HP
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
