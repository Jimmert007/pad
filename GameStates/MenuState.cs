using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley.GameStates
{
    class MenuState : GameObjectList
    {
        TextGameObject temp = new TextGameObject("GameFont");

        public MenuState()
        {
            temp.Text = "druk op een toets om naar het spel te gaan";
            temp.Position = new Vector2(GameEnvironment.Screen.X * .5f - temp.Size.X * .5f, GameEnvironment.Screen.Y * .5f - temp.Size.Y * .5f);
            Add(temp);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.AnyKeyPressed)
            {
                GameEnvironment.GameStateManager.SwitchTo("playingState");
            }

        }
    }
}
