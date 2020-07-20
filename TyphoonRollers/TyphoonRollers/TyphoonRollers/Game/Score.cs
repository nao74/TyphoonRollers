using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TyphoonRollers
{
    public class Score
    {
        private int combo = 0;

        private float s = 0.0f;

        public void Scoring(bool rot , bool hit)
        {
            if (rot)
            {
                if(hit)
                    combo++;
            }
            else
            {
                s += combo * combo;
                combo = 0;
            }
        }

        public float IsScore()
        {
            return s;
        }

        public int IsCombo()
        {
            return combo;
        }
    }
}
