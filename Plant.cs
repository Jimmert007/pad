﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    class Plant : SpriteGameObject
    {
        public int growthStage = 0;
        public SpriteSheet empty, seed1stage1, seed1stage2, seed1stage3, seed1stage4;

        public Plant(string assetName, int _x, int _y, int _w, int _h) : base(assetName)
        {
            position.X = _x;
            position.Y = _y;
            size.X = _w;
            size.Y = _h;
            empty = new SpriteSheet("spr_empty");
            seed1stage1 = new SpriteSheet("spr_seed1_stage1");
            seed1stage2 = new SpriteSheet("spr_seed1_stage2");
            seed1stage3 = new SpriteSheet("spr_seed1_stage3");
            seed1stage4 = new SpriteSheet("spr_seed1_stage4");
        }

        public override void Update(GameTime gameTime)
        {
            //seed growth stages update
            if (growthStage == 0)
            {
                sprite = empty;
            }
            else if (growthStage == 1)
            {
                sprite = seed1stage1;
            }
            else if (growthStage == 2)
            {
                sprite = seed1stage2;
            }
            else if (growthStage == 3)
            {
                sprite = seed1stage3;
            }
            else if (growthStage >= 4)
            {
                sprite = seed1stage4;
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
