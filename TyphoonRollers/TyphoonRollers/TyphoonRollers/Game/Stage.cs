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
    class Stage
    {
        #region フィールド

        public Model StageModel
        {
            get;set;
        }

        public Matrix[] Transform
        {
            get;set;
        }

        public static BoundingBox[] box;

        public const int boxNum = 4;

        #endregion

        public Stage() { }

        public void Initialize()
        {
            Transform = new Matrix[StageModel.Bones.Count];
            StageModel.CopyAbsoluteBoneTransformsTo(Transform);

            for (int i = 0; i < boxNum; i++)
                box = new BoundingBox[boxNum];

            box[0] = new BoundingBox(new Vector3(-800.0f, 0.0f, -800.0f), new Vector3(800.0f, 800.0f, -800.0f));
            box[1] = new BoundingBox(new Vector3(800.0f, 0.0f, -800.0f), new Vector3(800.0f, 800.0f, 800.0f));
            box[2] = new BoundingBox(new Vector3(-800.0f, 0.0f, 800.0f), new Vector3(800.0f, 800.0f, 800.0f));
            box[3] = new BoundingBox(new Vector3(-800.0f, 0.0f, -800.0f), new Vector3(-800.0f, 800.0f, 800.0f));
        }
    }
}
