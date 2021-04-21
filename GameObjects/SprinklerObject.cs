using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace HarvestValley.GameObjects
{
    class SprinklerObject : GameObjectList
    {
        public int sprinklerSprite = 0;
        public SpriteGameObject empty, sprinkler1;
        private float _scale;
        public SprinklerObject(Vector2 _position, float scale) : base()
        {
            position = _position;
            _scale = scale;

            empty = new SpriteGameObject("spr_empty", 0, "0");
            sprinkler1 = new SpriteGameObject("Sprinkler", 0, "1");
            Add(empty);
            Add(sprinkler1);

            for (int i = 0; i < children.Count; i++)
            {
                (children[i] as SpriteGameObject).Scale = _scale;
                (children[i] as SpriteGameObject).PerPixelCollisionDetection = false;
                (children[i] as SpriteGameObject).Visible = false;
            }

            sprinkler1.scale = 1.3f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (SpriteGameObject SGO in Children)
            {
                SGO.Visible = false;
                if (SGO.Id == sprinklerSprite.ToString())
                {
                    SGO.Visible = true;
                }
            }
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
