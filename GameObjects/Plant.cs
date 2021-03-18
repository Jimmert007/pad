using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley
{
    class Plant : GameObjectList
    {
        public int growthStage = 0;
        public SpriteGameObject empty, seed1stage1, seed1stage2, seed1stage3, seed1stage4;
        private float _scale;
        public bool soilHasPlant;

        public Plant(Vector2 _postition, float scale) : base()
        {
            position = _postition;
            _scale = scale;
            empty = new SpriteGameObject("spr_empty", 0, "0");
            seed1stage1 = new SpriteGameObject("spr_seed1_stage1", 0, "1");
            seed1stage2 = new SpriteGameObject("spr_seed1_stage2", 0, "2");
            seed1stage3 = new SpriteGameObject("spr_seed1_stage3", 0, "3");
            seed1stage4 = new SpriteGameObject("spr_seed1_stage4", 0, "4");
            Add(empty);
            Add(seed1stage1);
            Add(seed1stage2);
            Add(seed1stage3);
            Add(seed1stage4);
            soilHasPlant = false;
            for (int i = 0; i < children.Count; i++)
            {
                (children[i] as SpriteGameObject).Scale = _scale;
                (children[i] as SpriteGameObject).PerPixelCollisionDetection = false;
                (children[i] as SpriteGameObject).Visible = false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (SpriteGameObject SGO in Children)
            {
                SGO.Visible = false;
                if (SGO.Id == growthStage.ToString())
                {
                    SGO.Visible = true;
                }
            }
        }

        /*public override void Draw(SpriteBatch spriteBatch)
        {
            if (growthStage != 0)
            {
                spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y), Color.White);
            }
        }*/
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
