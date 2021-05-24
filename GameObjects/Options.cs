using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace HarvestValley.GameObjects
{
    class Options : GameObjectList
    {
        SpriteGameObject options, optionBackground, closeButton, muteButton, unmuteButton, plusButton, minusButton, exitButton, exitConfirmedButton, stayButton;
        TextGameObject optionText, soundOptionText, volume, exitGame, exitGameConfirmation;
        MouseGameObject mouseGO;
        public bool optionsVisible, exitConfirmation;
        public Options(MouseGameObject mouseGO)
        {
            #region open/close options menu
            options = new SpriteGameObject("UI/Menu");
            options.Position = new Vector2(0, GameEnvironment.Screen.Y - options.Height);
            Add(options);

            optionBackground = new SpriteGameObject("UI/spr_target_bg");
            optionBackground.Position = GameEnvironment.Screen.ToVector2() * .5f - new Vector2(optionBackground.Width * .5f, optionBackground.Height * .5f);
            Add(optionBackground);

            optionText = new TextGameObject("Fonts/JimFont");
            optionText.Text = "Options";
            optionText.Color = Color.Black;
            optionText.Position = new Vector2(GameEnvironment.Screen.X * .5f - optionText.Size.X * .5f, optionBackground.Position.Y + 25);
            Add(optionText);

            closeButton = new SpriteGameObject("UI/spr_yes_button");
            closeButton.Position = optionBackground.Position + new Vector2(optionBackground.Width - closeButton.Sprite.Width, optionBackground.Height - closeButton.Sprite.Height);
            Add(closeButton);
            #endregion

            #region sound options
            soundOptionText = new TextGameObject("Fonts/JimFont");
            soundOptionText.Color = Color.Black;
            soundOptionText.Text = "Volume:";
            soundOptionText.Position = optionBackground.Position + new Vector2(70, 140);
            Add(soundOptionText);

            muteButton = new SpriteGameObject("UI/SoundOFF");
            muteButton.Position = soundOptionText.Position + new Vector2(100, soundOptionText.Size.Y * .5f - muteButton.Height * .5f);
            Add(muteButton);

            minusButton = new SpriteGameObject("UI/Minus");
            minusButton.Position = muteButton.Position + new Vector2(50, 0);
            Add(minusButton);

            volume = new TextGameObject("Fonts/JimFont");
            volume.Color = Color.Black;
            Add(volume);


            plusButton = new SpriteGameObject("UI/Plus");
            plusButton.Position = minusButton.Position + new Vector2(100, 0);
            Add(plusButton);

            unmuteButton = new SpriteGameObject("UI/SoundON");
            unmuteButton.Position = plusButton.Position + new Vector2(50, 0);
            Add(unmuteButton);
            #endregion

            #region exit game
            exitButton = new SpriteGameObject("UI/spr_target_ui_bar");
            exitButton.Position = new Vector2(GameEnvironment.Screen.X * .5f - exitButton.Width * .5f, optionBackground.Position.Y + 300 - exitButton.Height * .5f);
            Add(exitButton);

            exitGame = new TextGameObject("Fonts/JimFont");
            exitGame.Color = Color.Black;
            exitGame.Text = "Exit game";
            exitGame.Position = new Vector2(GameEnvironment.Screen.X * .5f - exitGame.Size.X * .5f, optionBackground.Position.Y + 300 - exitGame.Size.Y * .5f);
            Add(exitGame);
            #endregion

            #region exit confirmation
            exitGameConfirmation = new TextGameObject("Fonts/JimFont");
            exitGameConfirmation.Color = Color.Black;
            exitGameConfirmation.Text = "Are you sure you want to exit,\n" +
                                        "all progress wil be lost!";
            exitGameConfirmation.Position = optionBackground.Position + new Vector2(optionBackground.Width * .5f - exitGameConfirmation.Size.X * .5f, optionBackground.Height * .5f - exitGameConfirmation.Size.Y * .5f);
            Add(exitGameConfirmation);

            exitConfirmedButton = new SpriteGameObject("UI/spr_yes_button");
            exitConfirmedButton.Position = closeButton.Position;
            Add(exitConfirmedButton);

            stayButton = new SpriteGameObject("UI/spr_no_button");
            stayButton.Position = new Vector2(optionBackground.Position.X, closeButton.Position.Y);
            Add(stayButton);
            #endregion

            this.mouseGO = mouseGO;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            volume.Text = (Math.Round(GameEnvironment.AssetManager.volume, 1) * 100).ToString();
            volume.Position = minusButton.Position + new Vector2(50 - volume.Size.X * .5f + minusButton.Width * .5f, minusButton.Height * .5f - volume.Size.Y * .5f);

            if (!optionsVisible)
            {
                foreach (GameObject g in children)
                {
                    g.Visible = false;
                }
            }
            else if (optionsVisible)
            {
                foreach (GameObject g in children)
                {
                    g.Visible = true;
                }
            }

            if (exitConfirmation)
            {
                optionBackground.Visible = true;
                exitGameConfirmation.Visible = true;
                exitConfirmedButton.Visible = true;
                stayButton.Visible = true;
            }
            else if (!exitConfirmation)
            {
                exitGameConfirmation.Visible = false;
                exitConfirmedButton.Visible = false;
                stayButton.Visible = false;
            }
            children[0].Visible = true;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.MouseLeftButtonPressed())
            {
                if (mouseGO.CollidesWith(options))
                {
                    optionsVisible = true;
                }
                if (mouseGO.CollidesWith(closeButton))
                {
                    optionsVisible = false;
                }
                if (mouseGO.CollidesWith(muteButton))
                {
                    GameEnvironment.AssetManager.volume = 0;
                }
                if (mouseGO.CollidesWith(unmuteButton))
                {
                    GameEnvironment.AssetManager.volume = 1;
                }
                if (mouseGO.CollidesWith(plusButton))
                {
                    GameEnvironment.AssetManager.volume += .1f;
                    if (GameEnvironment.AssetManager.volume > 1)
                    {
                        GameEnvironment.AssetManager.volume = 1;
                    }
                }
                if (mouseGO.CollidesWith(minusButton))
                {
                    GameEnvironment.AssetManager.volume -= .1f;
                    if (GameEnvironment.AssetManager.volume < 0)
                    {
                        GameEnvironment.AssetManager.volume = 0;
                    }
                }
                if (mouseGO.CollidesWith(exitButton))
                {
                    exitConfirmation = true;
                    optionsVisible = false;
                }
                if (mouseGO.CollidesWith(stayButton))
                {
                    exitConfirmation = false;
                    optionsVisible = true;
                }
                if (mouseGO.CollidesWith(exitConfirmedButton))
                {
                    System.Environment.Exit(1);
                }
            }
        }
    }
}
