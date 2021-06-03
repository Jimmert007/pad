using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HarvestValley.GameObjects
{
    /// <summary>
    /// Jim van de Burgwal
    /// This class contains te sprites for the cliff
    /// </summary>
    class Cliff : GameObjectList
    {
        RotatingSpriteGameObject cliff, cliffCorner;
        public Cliff(Vector2 position, float degrees, int type = 1)
        {
            cliff = new RotatingSpriteGameObject("tiles/Cliff");
            Add(cliff);

            cliffCorner = new RotatingSpriteGameObject("tiles/CliffCorner");
            Add(cliffCorner);

            foreach (RotatingSpriteGameObject r in children)
            {
                r.PerPixelCollisionDetection = false;
                //The sprites are placed in the middle of the cell with its origin also in the center, to use rotation easier
                r.Origin = r.Sprite.Center;
                r.Position = position + new Vector2(r.Sprite.Width / 2, r.Sprite.Height / 2);
                r.Degrees = degrees;
                r.Visible = false;
            }
            //Default type = 1, which is the normal cliff
            if (type == 1)
            {
                cliff.Visible = true;
            }
            //To get the corners you need to change the type to 2
            else if (type == 2)
            {
                cliffCorner.Visible = true;
            }
        }
    }
}
