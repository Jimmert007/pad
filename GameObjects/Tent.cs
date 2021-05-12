using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace HarvestValley.GameObjects
{
    class Tent : GameObjectList
    {
        public SpriteGameObject tent, tentCollision;
        public Tent()
        {
            Add(tent = new SpriteGameObject("Tent"));
            tent.Position = new Vector2(128, 128);
            tent.Origin = new Vector2(0, 55);
            Add(tentCollision = new SpriteGameObject("TentCollision"));
            tentCollision.Position = new Vector2(128, 162);
        }

        public bool CollidesWith(SpriteGameObject obj)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if ((children[1] as SpriteGameObject).CollidesWith(obj))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
