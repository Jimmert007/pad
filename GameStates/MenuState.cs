using HarvestValley.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley.GameStates
{
    class MenuState : GameObjectList
    {
        TextGameObject start = new TextGameObject("Fonts/JimFont");
        TextGameObject credits = new TextGameObject("Fonts/JimFont");
        TextGameObject close = new TextGameObject("Fonts/JimFont");
        TextGameObject title = new TextGameObject("Fonts/GameFont");
        SpriteGameObject button1 = new SpriteGameObject("UI/spr_target_ui_bar") { scale = 1.5f}, button2 = new SpriteGameObject("UI/spr_target_ui_bar") { scale = 1.5f}, button3 = new SpriteGameObject("UI/spr_target_ui_bar") { scale = 1.5f}, button1credits = new SpriteGameObject("UI/spr_target_ui_bar") { scale = 1.5f };
        GameObjectList bgs;
        SpriteGameObject bg;
        MouseGameObject mouseGO = new MouseGameObject();
        TextGameObject creditsTitle, niels, luke, mo, jim, back;
        bool creditsScreen;
        Sounds sounds;

        public MenuState()
        {
            sounds = new Sounds();
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
            button1.Position = new Vector2(GameEnvironment.Screen.X * .5f - button1.Width * .5f, GameEnvironment.Screen.Y * .5f - button1.Height * .5f);
            Add(button1);

            button2.Position = new Vector2(GameEnvironment.Screen.X * .5f - button2.Width * .5f, button1.Position.Y + 75);
            Add(button2);

            button3.Position = new Vector2(GameEnvironment.Screen.X * .5f - button3.Width * .5f, button1.Position.Y + 150);
            Add(button3);

            title.Text = "Harvest Valley";
            title.Position = new Vector2(GameEnvironment.Screen.X * .5f - title.Size.X * .5f, GameEnvironment.Screen.Y * .3f - title.Size.Y * .5f);
            Add(title);

            start.Text = "Start game";
            start.Position = new Vector2(GameEnvironment.Screen.X * .5f - start.Size.X * .5f, GameEnvironment.Screen.Y * .5f - start.Size.Y * .5f);
            Add(start);

            credits.Text = "Credits";
            credits.Position = new Vector2(GameEnvironment.Screen.X * .5f - credits.Size.X * .5f, start.Position.Y + 75);
            Add(credits);

            close.Text = "Close game";
            close.Position = new Vector2(GameEnvironment.Screen.X * .5f - close.Size.X * .5f, start.Position.Y + 150);
            Add(close);

            creditsTitle = new TextGameObject("Fonts/GameFont");
            creditsTitle.Text = "Credits";
            creditsTitle.Position = new Vector2(GameEnvironment.Screen.X * .5f - creditsTitle.Size.X * .5f, GameEnvironment.Screen.Y * .3f - creditsTitle.Size.Y * .5f);
            Add(creditsTitle);

            niels = new TextGameObject("Fonts/CreditFont");
            niels.Text = "Niels Duivenvoorden";
            niels.Position = new Vector2(GameEnvironment.Screen.X * .5f - niels.Size.X * .5f, GameEnvironment.Screen.Y * .5f - niels.Size.Y * .5f);
            Add(niels);

            luke = new TextGameObject("Fonts/CreditFont");
            luke.Text = "Luke Sikma";
            luke.Position = new Vector2(GameEnvironment.Screen.X * .5f - luke.Size.X * .5f, niels.Position.Y + 75);
            Add(luke);

            mo = new TextGameObject("Fonts/CreditFont");
            mo.Text = "Mohammad Al Hadiansyah Suwandhy";
            mo.Position = new Vector2(GameEnvironment.Screen.X * .5f - mo.Size.X * .5f, niels.Position.Y + 150);
            Add(mo);

            jim = new TextGameObject("Fonts/CreditFont");
            jim.Text = "Jim van de Burgwal";
            jim.Position = new Vector2(GameEnvironment.Screen.X * .5f - jim.Size.X * .5f, niels.Position.Y + 225);
            Add(jim);

            button1credits.Position = new Vector2(GameEnvironment.Screen.X * .5f - button1credits.Width * .5f, niels.Position.Y + 300 - button1credits.Height * .5f);
            Add(button1credits);

            back = new TextGameObject("Fonts/JimFont");
            back.Text = "Back to main menu";
            back.Position = new Vector2(GameEnvironment.Screen.X * .5f - back.Size.X * .5f, niels.Position.Y + 300 - back.Size.Y * .5f);
            Add(back);

            Add(mouseGO);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (GameObject g in children)
            {
                g.Visible = false;
            }
            if (!creditsScreen)
            {
                for (int i = 0; i < 8; i++)
                {
                    children[i].Visible = true;
                }
            }
            else if (creditsScreen)
            {
                for (int i = 8; i < 15; i++)
                {
                    children[i].Visible = true;
                }
            }
            children[0].Visible = true;
            children[children.Count - 1].Visible = true;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.MouseLeftButtonPressed())
            {
                if (mouseGO.CollidesWith(button1))
                {
                    GameEnvironment.GameStateManager.SwitchTo("playingState");
                    // Play ButtonClick
                    GameEnvironment.AssetManager.PlayOnce(sounds.SEIs[9]);
                }
                if (mouseGO.CollidesWith(button2))
                {
                    creditsScreen = true;
                    // Play ButtonClick
                    GameEnvironment.AssetManager.PlayOnce(sounds.SEIs[9]);
                }
                if (mouseGO.CollidesWith(button3))
                {
                    System.Environment.Exit(1);
                    // Play ButtonClick
                    GameEnvironment.AssetManager.PlayOnce(sounds.SEIs[9]);
                }
                if (mouseGO.CollidesWith(button1credits))
                {
                    creditsScreen = false;
                    // Play ButtonClick
                    GameEnvironment.AssetManager.PlayOnce(sounds.SEIs[9]);
                }
            }
            bgs.Position = inputHelper.MousePosition * .01f + bg.Position;
        }
    }
}
