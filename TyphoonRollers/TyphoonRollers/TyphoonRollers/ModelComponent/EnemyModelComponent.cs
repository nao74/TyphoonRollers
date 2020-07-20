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
    public class EnemyModelComponent : ModelGameComponent
    {
        #region フィールド

        public BoundingSphere Sphere
        {
            get { return sphere; }
            set { sphere = value; }
        }
        private BoundingSphere sphere;

        public Vector3 SphereCenter
        {
            get { return sphere.Center; }
            set { sphere.Center = value; }
        }

        public float SphereX
        {
            get { return sphere.Center.X; }
            set { sphere.Center.X = value; }
        }

        public float SphereZ
        {
            get { return sphere.Center.Z; }
            set { sphere.Center.Z = value; }
        }


        private bool hit;
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

        private const float trackingPlayerArea = 150.0f;
        public float TrackingPlayerArea
        {
            get { return trackingPlayerArea; }
        }

        private const int enemyNum = 10;
        public int EnemuNum
        {
            get { return enemyNum; }
        }

        private const float damage = 1.0f;
        public float Damage
        {
            get { return damage; }
        }

        #endregion
        public EnemyModelComponent(Game game)
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
            sphere = new BoundingSphere((Position), 3);

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
