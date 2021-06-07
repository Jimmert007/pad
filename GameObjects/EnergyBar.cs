using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HarvestValley.GameObjects
{
    /// <summary>
    /// Jim van de Burgwal
    /// This script contains the sprites and functionality for the energy bar
    /// </summary>
    class EnergyBar : GameObject
    {
        SpriteSheet energyBarBackground;
        SpriteSheet energyBarPercentage;
        SpriteSheet energyBarLogo;
        Vector2 logoPosition;
        Vector2 logoSize;
        Vector2 percentagePosition;
        Vector2 percentageSize;
        public float energyLost = 0;
        public float oneUse;
        public bool passOut;
        Vector2 size;
        public EnergyBar(int _x, int _y, int _w, int _h)
        {
            position.X = _x;
            position.Y = _y;
            energyBarBackground = new SpriteSheet("UI/EnergyBarBackground");
            energyBarPercentage = new SpriteSheet("UI/EnergyBarPercentage");
            energyBarLogo = new SpriteSheet("UI/EnergyLogo");
            oneUse = (_h - 10) / 100;   //onePercent gets calculated
            percentagePosition.X = position.X + 5;
            percentageSize.X = _w - 10;
            size = new Vector2(_w, _h);
            logoSize = new Vector2(size.X / 2, size.Y / 4);
            logoPosition = new Vector2(position.X + size.X / 2 - logoSize.X / 2, position.Y + size.Y / 2 - logoSize.Y / 2);
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            percentagePosition.Y = position.Y + 5 + energyLost; //Because the energy lost gets updated after player actions, the new position needs to be calculated each frame
            percentageSize.Y = size.Y - 10 - energyLost;   //The height of the remaining energy also gets calculated every frame
            if (percentageSize.Y <= 0)  //If the player runs out of energy
            {
                passOut = true;         //The player passes out
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            //Because the size of de energy bar needs to change we use a draw function instead of the regular SpriteGameObject, with the draw we can manipulate height and width seperately
            spriteBatch.Draw(energyBarBackground.Sprite, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y), Color.White);
            spriteBatch.Draw(energyBarPercentage.Sprite, new Rectangle((int)percentagePosition.X, (int)percentagePosition.Y, (int)percentageSize.X, (int)percentageSize.Y), Color.White);
            spriteBatch.Draw(energyBarLogo.Sprite, new Rectangle((int)logoPosition.X, (int)logoPosition.Y, (int)logoSize.X, (int)logoSize.Y), Color.White);
        }

        public override void Reset()
        {
            //When the Reset is called, the energy lost is set to 0
            energyLost = 0;
            if (passOut)                //If the player passed out
            {
                energyLost = 100;    //The energy lost is set to 100
            }
            passOut = false;
        }
    }
}
