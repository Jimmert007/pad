using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace BaseProject
{
    class Game1 : GameEnvironment
    {
        protected override void LoadContent()
        {
            base.LoadContent();
            screen = new Point(960, 540);
            ApplyResolutionSettings();

            gameStateList.Add(new PlayingState());
            GameEnvironment.SwitchTo(0);
            IsMouseVisible = true;
        }
    }
}
