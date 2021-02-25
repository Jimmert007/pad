using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    class Plant : GameObject
    {
        public int growthStage = 0;
        public Texture2D seed1stage1;
        public Texture2D seed1stage2;
        public Texture2D seed1stage3;
        public Texture2D seed1stage4;

        public Plant(string assetName, int _x, int _y, int _w, int _h) : base(assetName)
        {
            position.X = _x;
            position.Y = _y;
            size.X = _w;
            size.Y = _h;
            seed1stage1 = GameEnvironment.ContentManager.Load<Texture2D>("spr_seed1_stage1");
            seed1stage2 = GameEnvironment.ContentManager.Load<Texture2D>("spr_seed1_stage2");
            seed1stage3 = GameEnvironment.ContentManager.Load<Texture2D>("spr_seed1_stage3");
            seed1stage4 = GameEnvironment.ContentManager.Load<Texture2D>("spr_seed1_stage4");
        }

        public override void Update()
        {
            //seed growth stages update
            if (growthStage == 1)
            {
                texture = seed1stage1;
            }
            else if (growthStage == 2)
            {
                texture = seed1stage2;
            }
            else if (growthStage == 3)
            {
                texture = seed1stage3;
            }
            else if (growthStage >= 4)
            {
                texture = seed1stage4;
            }
        }

        /*public override void Draw(SpriteBatch spriteBatch)
        {
            if (growthStage != 0)
            {
                spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y), Color.White);
            }
        }*/
    }
}
