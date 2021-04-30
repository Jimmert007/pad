using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;

namespace HarvestValley.GameObjects
{
    class Stone : GameObjectList
    {
        public int hitTimer, hitTimerReset = 60, health = 6, _sprite = 0;
        public bool stoneHit;
        SpriteGameObject stone1, stoneMine;
        public Stone(Vector2 _position, float _scale) : base()
        {
            position = _position;
            stone1 = new SpriteGameObject("Stone", 0, "1");
            stoneMine = new SpriteGameObject("Stone", 0, "2");
            Add(stone1);
            Add(stoneMine);

            stoneMine.Origin = new Vector2(5, 5);

            for (int i = 0; i < children.Count; i++)
            {
                (children[i] as SpriteGameObject).Scale = _scale;
                (children[i] as SpriteGameObject).PerPixelCollisionDetection = false;
                (children[i] as SpriteGameObject).Visible = false;
            }

            _sprite = 1;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_sprite > 1)
            {
                _sprite = 1;
            }
            foreach (SpriteGameObject SGO in Children)
            {
                SGO.Visible = false;

                if (SGO.Id == _sprite.ToString())
                {
                    SGO.Visible = true;
                }
                if (stoneHit)
                {
                    _sprite = 2;
                    hitTimer -= 1;
                    if (hitTimer <= 0)
                    {
                        stoneHit = false;
                    }
                }
            }

        }

        public bool CollidesWith(SpriteGameObject obj)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if ((children[0] as SpriteGameObject).CollidesWith(obj))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
