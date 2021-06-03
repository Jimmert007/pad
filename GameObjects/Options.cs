using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace HarvestValley.GameObjects
{
    /// <summary>
    /// Jim van de Burgwal & Niels Duivenvoorden
    /// This script contains all the GameObject surrounding the options
    /// </summary>
    class Options : GameObjectList
    {
        SpriteGameObject options, optionBackground, closeButton, muteButton, unmuteButton, plusButton, minusButton, exitButton, exitConfirmedButton, stayButton; //SpriteGameObjects for the options menu button, the background and all the other buttons
        TextGameObject optionText, soundOptionText, volume, exitGame, exitGameConfirmation, fullscreenText; //TextGameObjects for the options menu
        MouseGameObject mouseGO;    //Options uses the MouseGameObject so it's added here
        public bool optionsVisible, exitConfirmation;   //Booleans for the different menu's
        Sounds sounds;  //Also needs sounds for the button clicks
        public Options(MouseGameObject mouseGO)
        {
            sounds = new Sounds();

            //This region contains the 'basic' SpriteGameObjects and TextGameObjects for the options menu
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

            //This region contains the SpriteGameObjects and TextGameObjects for the sound option
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

            //This region contains the TextGameObject for the fullscreen toggle (Niels Duivenvoorden)
            #region fullscreen toggle
            fullscreenText = new TextGameObject("Fonts/JimFont");
            fullscreenText.Color = Color.Black;
            fullscreenText.Text = "Press F11 to toggle fullscreen";
            fullscreenText.Position = new Vector2(GameEnvironment.Screen.X * .5f - fullscreenText.Size.X * .5f, optionBackground.Position.Y + 225 - fullscreenText.Size.Y * .5f);
            Add(fullscreenText);
            #endregion

            //This region contains the SpriteGameObject and TextGameObject for the exit game option
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

            //This region contains the SpriteGameObjects and TextGameObject for the confirm exit game screen
            #region exit confirmation
            exitGameConfirmation = new TextGameObject("Fonts/JimFont");
            exitGameConfirmation.Color = Color.Black;
            exitGameConfirmation.Text = "Are you sure you want to exit,\n" +
                                        "all progress will be lost!";
            exitGameConfirmation.Position = optionBackground.Position + new Vector2(optionBackground.Width * .5f - exitGameConfirmation.Size.X * .5f, optionBackground.Height * .5f - exitGameConfirmation.Size.Y * .5f);
            Add(exitGameConfirmation);

            exitConfirmedButton = new SpriteGameObject("UI/spr_yes_button");
            exitConfirmedButton.Position = closeButton.Position;
            Add(exitConfirmedButton);

            stayButton = new SpriteGameObject("UI/spr_no_button");
            stayButton.Position = new Vector2(optionBackground.Position.X, closeButton.Position.Y);
            Add(stayButton);
            #endregion

            this.mouseGO = mouseGO; //Uses the same MouseGameObjects as the playing state
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            volume.Text = (Math.Round(GameEnvironment.AssetManager.volume, 1) * 100).ToString(); //Updates volume text and multiplies by 100, because it's normally between 0 and 1
            volume.Position = minusButton.Position + new Vector2(50 - volume.Size.X * .5f + minusButton.Width * .5f, minusButton.Height * .5f - volume.Size.Y * .5f); //Updates volume text position to always be in the middle of the plus and minus buttons

            if (!optionsVisible) //If options are not visible, all GameObjects are invisible
            {
                foreach (GameObject g in children)
                {
                    g.Visible = false;
                }
            }
            else if (optionsVisible) //If options are visible, all GameObjects are visible
            {
                foreach (GameObject g in children)
                {
                    g.Visible = true;
                }
            }

            //The exit confirmation is a different menu from the normal options, so certain GameObjects are true or false when it is active/inactive
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

            //The options button in the bottom left corner is always visible
            children[0].Visible = true;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.MouseLeftButtonPressed()) //If the left button is pressed check for all the buttons if they are pressed
            {
                if (mouseGO.CollidesWith(options))
                {
                    optionsVisible = true;
                    // Play ButtonClick
                    GameEnvironment.AssetManager.PlayOnce(sounds.SEIs[10]);
                }
                if (mouseGO.CollidesWith(closeButton))
                {
                    optionsVisible = false;
                    // Play ButtonClick
                    GameEnvironment.AssetManager.PlayOnce(sounds.SEIs[10]);
                }
                if (mouseGO.CollidesWith(muteButton))
                {
                    GameEnvironment.AssetManager.volume = 0;
                    // Play ButtonClick
                    GameEnvironment.AssetManager.PlayOnce(sounds.SEIs[10]);
                }
                if (mouseGO.CollidesWith(unmuteButton))
                {
                    GameEnvironment.AssetManager.volume = 1;
                    // Play ButtonClick
                    GameEnvironment.AssetManager.PlayOnce(sounds.SEIs[10]);
                }
                if (mouseGO.CollidesWith(plusButton))
                {
                    // Play ButtonClick
                    GameEnvironment.AssetManager.PlayOnce(sounds.SEIs[10]);

                    GameEnvironment.AssetManager.volume += .1f;
                    if (GameEnvironment.AssetManager.volume > 1)    //Keeps the volume from going over 100%
                    {

                        GameEnvironment.AssetManager.volume = 1;
                    }
                }
                if (mouseGO.CollidesWith(minusButton))
                {
                    // Play ButtonClick
                    GameEnvironment.AssetManager.PlayOnce(sounds.SEIs[10]);

                    GameEnvironment.AssetManager.volume -= .1f;
                    if (GameEnvironment.AssetManager.volume < 0)    //Keeps the volume from going below 0%
                    {
                        GameEnvironment.AssetManager.volume = 0;
                    }
                }
                if (mouseGO.CollidesWith(exitButton))
                {
                    // Play ButtonClick
                    GameEnvironment.AssetManager.PlayOnce(sounds.SEIs[10]);

                    exitConfirmation = true;
                    optionsVisible = false;
                }
                if (mouseGO.CollidesWith(stayButton))
                {
                    // Play ButtonClick
                    GameEnvironment.AssetManager.PlayOnce(sounds.SEIs[10]);

                    exitConfirmation = false;
                    optionsVisible = true;
                }
                if (mouseGO.CollidesWith(exitConfirmedButton))
                {
                    // Play ButtonClick
                    GameEnvironment.AssetManager.PlayOnce(sounds.SEIs[10]);

                    System.Environment.Exit(1);
                }
            }
        }

        /// <summary>
        /// Checks both booleans for the options to see if options are active or not and combines them to one boolean
        /// </summary>
        public bool IsActive
        {
            get { return optionsVisible || exitConfirmation; }
        }
    }
}
