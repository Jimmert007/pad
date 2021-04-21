using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley.GameObjects
{
    class CraftingMenu : GameObjectList
    {
        SpriteGameObject CraftingBackground;
        public CraftingMenu() : base()
        {
            CraftingBackground = new SpriteGameObject("EnergyBarBackground");
            //Add(new TextGameObject("GameFont"));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(CraftingBackground.Sprite.Sprite, new Rectangle(GameEnvironment.Screen.X / 6, GameEnvironment.Screen.Y / 6, GameEnvironment.Screen.X / 3 * 2, GameEnvironment.Screen.Y / 3 * 2), Color.White);
        }
    }
}
