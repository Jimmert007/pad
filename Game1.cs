using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseProject
{
    class Game1 : GameEnvironment
    {

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
            screen = new Point(600, 500);
            ApplyResolutionSettings();


            gameStateManager.AddGameState("playingState", new PlayingState());
            gameStateManager.SwitchTo("playingState");

            IsMouseVisible = true;
        }
    }
}
