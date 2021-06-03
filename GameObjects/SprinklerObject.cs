using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace HarvestValley.GameObjects
{
    /// <summary>
    /// Jim van de Burgwal
    /// This script contains the sprinkler
    /// </summary>
    class SprinklerObject : GameObjectList
    {
        public int sprinklerSprite = 1;
        public SpriteGameObject sprinkler1;
        private float _scale;
        public SprinklerObject(Vector2 _position, float scale) : base()
        {
            position = _position;
            _scale = scale;
            sprinkler1 = new SpriteGameObject("Environment/Sprinkler", 0, "1");
            Add(sprinkler1);

            for (int i = 0; i < children.Count; i++)
            {
                (children[i] as SpriteGameObject).Scale = _scale;
                (children[i] as SpriteGameObject).PerPixelCollisionDetection = false;
                (children[i] as SpriteGameObject).Visible = true;
            }

            sprinkler1.scale = 1.3f;
        }

        public bool CollidesWith(SpriteGameObject obj)
        {
            foreach (SpriteGameObject SGO in Children)
            {
                if (SGO.CollidesWith(obj))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
