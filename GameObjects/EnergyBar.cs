using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley.GameObjects
{
    class EnergyBar : SpriteGameObject
    {
        public SpriteSheet energyBarBackground;
        public SpriteSheet energyBarPercentage;
        public Vector2 percentagePosition;
        public Vector2 percentageSize;
        public float percentageLost;
        public float oneUse;
        public bool passOut;
        public EnergyBar(string assetName, int _x, int _y, int _w, int _h) : base(assetName)
        {
            position.X = _x;
            position.Y = _y;
            //size.X = _w;
            //size.Y = _h;
            energyBarBackground = new SpriteSheet("EnergyBarBackground");
            energyBarPercentage = new SpriteSheet("EnergyBarPercentage");
            oneUse = (sprite.Height - 10) / 100;
            percentagePosition.X = position.X + 5;
            percentageSize.X = sprite.Width - 10;
        }


        public override void Update(GameTime gameTime)
        {
            percentagePosition.Y = position.Y + 5 + percentageLost;
            percentageSize.Y = sprite.Height - 10 - percentageLost;
            if (percentageSize.Y <= 0)
            {
                passOut = true;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(energyBarBackground.Sprite, new Rectangle((int)position.X, (int)position.Y, (int)sprite.Width, (int)sprite.Height), Color.White);
            spriteBatch.Draw(energyBarPercentage.Sprite, new Rectangle((int)percentagePosition.X, (int)percentagePosition.Y, (int)percentageSize.X, (int)percentageSize.Y), Color.White);

        }

        public override void Reset()
        {
            percentageLost = 0;
            passOut = false;
        }
    }
}
