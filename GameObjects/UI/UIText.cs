using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HarvestValley.GameObjects
{
    class UIText : TextGameObject
    {
        public string[] dialogueLines = { "Dit is een test", "Hallo", "Het werkt", "INSERT TEXT", "Proleet" };
        int current = 0;
        Vector2 startPosition;

        public UIText() : base("GameFont")
        {
            startPosition = new Vector2(GameEnvironment.Screen.X * .5f, GameEnvironment.Screen.Y * .5f);
            text = "";
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.KeyPressed(Keys.P))
            {
                if (current < dialogueLines.Length - 1)
                {
                    current++;
                }
                else
                {
                    current = 0;
                }
                text = dialogueLines[current];
            }
        }
    }
}
