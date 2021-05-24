using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HarvestValley.GameObjects
{
    class Cliff : GameObjectList
    {
        int type;
        RotatingSpriteGameObject cliff, cliffCorner;
        public Cliff(Vector2 position, float degrees, int type = 1)
        {
            this.type = type;

            cliff = new RotatingSpriteGameObject("tiles/Cliff");
            Add(cliff);

            cliffCorner = new RotatingSpriteGameObject("tiles/CliffCorner");
            Add(cliffCorner);

            foreach (RotatingSpriteGameObject r in children)
            {
                r.PerPixelCollisionDetection = false;
                r.Origin = r.Sprite.Center;
                r.Position = position + new Vector2(r.Sprite.Width / 2, r.Sprite.Height / 2);
                r.Degrees = degrees;
                r.Visible = false;
            }
            if (type == 1)
            {
                cliff.Visible = true;
            }
            else if (type == 2)
            {
                cliffCorner.Visible = true;
            }
        }
    }
}
