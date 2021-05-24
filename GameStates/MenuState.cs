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
        SpriteGameObject button1 = new SpriteGameObject("UI/spr_target_ui_bar") { scale = 1.5f}, button2 = new SpriteGameObject("UI/spr_target_ui_bar") { scale = 1.5f}, button3 = new SpriteGameObject("UI/spr_target_ui_bar") { scale = 1.5f};
        GameObjectList bgs;
        SpriteGameObject bg;
        MouseGameObject mouseGO = new MouseGameObject();

        public MenuState()
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

            Add(mouseGO);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (mouseGO.CollidesWith(button1) && inputHelper.MouseLeftButtonPressed())
            {
                GameEnvironment.GameStateManager.SwitchTo("playingState");
            }
            if (mouseGO.CollidesWith(button2) && inputHelper.MouseLeftButtonPressed())
            {
                GameEnvironment.GameStateManager.SwitchTo("creditsState");
            }
            if (mouseGO.CollidesWith(button3) && inputHelper.MouseLeftButtonPressed())
            {
                System.Environment.Exit(1);
            }
            bgs.Position = inputHelper.MousePosition * .01f + bg.Position;
        }
    }
}
