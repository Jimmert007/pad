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
        public SpriteSheet energyBarLogo;
        public Vector2 LogoPosition;
        public Vector2 LogoSize;
        public Vector2 percentagePosition;
        public Vector2 percentageSize;
        public float percentageLost = 0;
        public float oneUse;
        public bool passOut;
        Vector2 _size;
        public EnergyBar(string assetName, int _x, int _y, int _w, int _h) : base(assetName)
        {
            position.X = _x;
            position.Y = _y;
            energyBarBackground = new SpriteSheet("UI/EnergyBarBackground");
            energyBarPercentage = new SpriteSheet("UI/EnergyBarPercentage");
            energyBarLogo = new SpriteSheet("UI/EnergyLogo");
            oneUse = (_h - 10) / 100;
            percentagePosition.X = position.X + 5;
            percentageSize.X = _w - 10;
            _size = new Vector2(_w, _h);
        }


        public override void Update(GameTime gameTime)
        {
            percentagePosition.Y = position.Y + 5 + percentageLost;
            percentageSize.Y = _size.Y - 10 - percentageLost;
            if (percentageSize.Y <= 0)
            {
                passOut = true;
            }

            LogoPosition.Y = position.Y + _size.Y / 2 - LogoSize.Y/2;
            LogoPosition.X = position.X + _size.X / 2 - LogoSize.X/2;
            LogoSize.X = _size.X/2;
            LogoSize.Y = _size.Y/4;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(energyBarBackground.Sprite, new Rectangle((int)position.X, (int)position.Y, (int)_size.X, (int)_size.Y), Color.White);
            spriteBatch.Draw(energyBarPercentage.Sprite, new Rectangle((int)percentagePosition.X, (int)percentagePosition.Y, (int)percentageSize.X, (int)percentageSize.Y), Color.White);
            spriteBatch.Draw(energyBarLogo.Sprite, new Rectangle((int)LogoPosition.X, (int)LogoPosition.Y, (int)LogoSize.X, (int)LogoSize.Y), Color.White);


        }

        public override void Reset()
        {
            percentageLost = 0;
            if (passOut)
            {
                percentageLost = 100;
            }
            passOut = false;
        }
    }
}
