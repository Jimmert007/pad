using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley.GameStates
{
    class MenuState : GameObjectList
    {
        TextGameObject start = new TextGameObject("JimFont");
        TextGameObject title = new TextGameObject("GameFont");
        GameObjectList bgs;
        SpriteGameObject bg;

        public MenuState()
        {
            bgs = new GameObjectList();
            Add(bgs);
            for (int i = 0; i < 2; i++)
            {
                for (int x = 0; x < 2; x++)
                {
                    bg = new SpriteGameObject("spr_menu_bg");
                    bg.Position = new Vector2(GameEnvironment.Screen.X * .5f - x * bg.Width, GameEnvironment.Screen.Y * .5f - i * bg.Height);
                    bgs.Add(bg);
                }
            }
            start.Text = "Druk op de spatietoets om naar het spel te gaan";
            start.Position = new Vector2(GameEnvironment.Screen.X * .5f - start.Size.X * .5f, GameEnvironment.Screen.Y * .5f - start.Size.Y * .5f);
            Add(start);
            title.Text = "Harvest Valley";
            title.Position = new Vector2(GameEnvironment.Screen.X * .5f - title.Size.X * .5f, GameEnvironment.Screen.Y * .3f - title.Size.Y * .5f);
            Add(title);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.KeyPressed(Keys.Space))
            {
                GameEnvironment.GameStateManager.SwitchTo("playingState");
            }
            bgs.Position = inputHelper.MousePosition * .01f + bg.Position;
        }
    }
}
