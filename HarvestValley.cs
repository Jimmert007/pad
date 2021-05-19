using HarvestValley.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace HarvestValley
{
    
    class HarvestValley : GameEnvironment
    {
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 
    

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screen = new Point(1280, 720);
            ApplyResolutionSettings();

            

            gameStateManager.AddGameState("menuState", new MenuState());
            gameStateManager.AddGameState("playingState", new PlayingState());
            gameStateManager.SwitchTo("menuState");

            IsMouseVisible = true;
        }
    }
}
