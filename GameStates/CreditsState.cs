using HarvestValley.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley.GameStates
{
    class CreditsState : GameObjectList
    {
        GameObjectList bgs;
        SpriteGameObject bg, button1 = new SpriteGameObject("UI/spr_target_ui_bar") { scale = 1.5f};
        MouseGameObject mouseGO = new MouseGameObject();
        TextGameObject credits, niels, luke, mo, jim, back;

        public CreditsState()
        {
            bgs = new GameObjectList();
            Add(bgs);
            for (int i = 0; i < 2; i++)
            {
                for (int x = 0; x < 2; x++)
                {
                    bg = new SpriteGameObject("UI/spr_menu_bg");
                    bg.Position = new Vector2(GameEnvironment.Screen.X * .5f - x * bg.Width, GameEnvironment.Screen.Y * .5f - i * bg.Height);
                    bgs.Add(bg);
                }
            }
            credits = new TextGameObject("Fonts/GameFont");
            credits.Text = "Credits";
            credits.Position = new Vector2(GameEnvironment.Screen.X * .5f - credits.Size.X * .5f, GameEnvironment.Screen.Y * .3f - credits.Size.Y * .5f);
            Add(credits);

            niels = new TextGameObject("Fonts/CreditFont");
            niels.Text = "Niels Duivenvoorden: Coole gast zijn";
            niels.Position = new Vector2(GameEnvironment.Screen.X * .5f - niels.Size.X * .5f, GameEnvironment.Screen.Y * .5f - niels.Size.Y * .5f);
            Add(niels);

            luke = new TextGameObject("Fonts/CreditFont");
            luke.Text = "Luke Sikma: Coole gast zijn";
            luke.Position = new Vector2(GameEnvironment.Screen.X * .5f - luke.Size.X * .5f, niels.Position.Y + 75);
            Add(luke);

            mo = new TextGameObject("Fonts/CreditFont");
            mo.Text = "Mohammad Al Hadiansyah Suwandhy: Coole gast zijn";
            mo.Position = new Vector2(GameEnvironment.Screen.X * .5f - mo.Size.X * .5f, niels.Position.Y + 150);
            Add(mo);

            jim = new TextGameObject("Fonts/CreditFont");
            jim.Text = "Jim van de Burgwal: Coolste gast zijn";
            jim.Position = new Vector2(GameEnvironment.Screen.X * .5f - jim.Size.X * .5f, niels.Position.Y + 225);
            Add(jim);

            button1.Position = new Vector2(GameEnvironment.Screen.X * .5f - button1.Width * .5f, niels.Position.Y + 300 - button1.Height * .5f);
            Add(button1);

            back = new TextGameObject("Fonts/JimFont");
            back.Text = "Back to main menu";
            back.Position = new Vector2(GameEnvironment.Screen.X * .5f - back.Size.X * .5f, niels.Position.Y + 300 - back.Size.Y * .5f);
            Add(back);

            

            Add(mouseGO);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (mouseGO.CollidesWith(button1) && inputHelper.MouseLeftButtonPressed())
            {
                GameEnvironment.GameStateManager.SwitchTo("menuState");
            }
            bgs.Position = inputHelper.MousePosition * .01f + bg.Position;
        }
    }
}
