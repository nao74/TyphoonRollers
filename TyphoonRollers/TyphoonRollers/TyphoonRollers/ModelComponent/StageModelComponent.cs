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
    public class StageModelComponent : ModelGameComponent
    {
        #region フィールド

        public BoundingBox[] Box
        {
            get { return box; }
            set { box = value; }
        }
        private BoundingBox[] box;

        private const int boxNum = 4;
        public int BoxNum
        {
            get { return boxNum; }
        }

        #endregion

        #region コンストラクタ

        public StageModelComponent(Game game)
            : base(game)
        {
            box = new BoundingBox[boxNum];

            box[0] = new BoundingBox(new Vector3(-800.0f, 0.0f, -800.0f), new Vector3(800.0f, 800.0f, -800.0f));
            box[1] = new BoundingBox(new Vector3(800.0f, 0.0f, -800.0f), new Vector3(800.0f, 800.0f, 800.0f));
            box[2] = new BoundingBox(new Vector3(-800.0f, 0.0f, 800.0f), new Vector3(800.0f, 800.0f, 800.0f));
            box[3] = new BoundingBox(new Vector3(-800.0f, 0.0f, -800.0f), new Vector3(-800.0f, 800.0f, 800.0f));
        }

        #endregion
    }
}