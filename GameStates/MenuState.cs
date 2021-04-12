using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley.GameStates
{
    class MenuState : GameObjectList
    {
        public MenuState()
        {
            TextGameObject temp = new TextGameObject("GameFont");
            temp.Text = "druk op een toets om naar het spel te gaan";
            temp.Position = new Vector2(GameEnvironment.Screen.X * .5f - temp.Size.X * .5f, GameEnvironment.Screen.Y * .5f);
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
