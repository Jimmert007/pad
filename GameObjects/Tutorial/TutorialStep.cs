using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HarvestValley.GameObjects.Tutorial
{
    /// <summary>
    /// Jim van de Burgwal
    /// This class makes it easier to add TutorialSteps to the TutorialStepList by using all the important information in de constructor to fit the TutorialStep nicely into its box
    /// </summary>
    class TutorialStep : GameObjectList
    {
        public int step;                    //This int keeps track of what step this will be
        public bool mouseCollides;          //Uses this bool to keep track of when the mouse collides
        public string tutorialText;         //This string is the text that will be shown as the TutorialStep
        public Vector2 tutorialPosition;    //This vector sets the position for the TutorialStep
        TextGameObject text;                //The string tutorialText needs to be used inside a TextGameObject, which this is
        SpriteSheet backgroundSprite;       //To make the text readable a backgroundSprite is declared
        SpriteFont textFont;                //The TextGameObjects needs a Font to be used, which this is
        public TutorialStep(int step, string tutorialText)
        {
            this.step = step;
            this.tutorialText = tutorialText;
            backgroundSprite = new SpriteSheet("UI/TutorialBackground");
            textFont = GameEnvironment.AssetManager.Content.Load<SpriteFont>("Fonts/JimFont");
            text = new TextGameObject("Fonts/JimFont");
            text.Text = tutorialText; 
            foreach (GameObject GO in children)
            {
                GO.Visible = false; //Everything is made invisible
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            tutorialPosition = inputHelper.MousePosition; //The tutorialPosition is placed on the MousePosition
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            if (mouseCollides) //Only when the mouse collides with the questionmark box
            {
                //The background and text are drawn, this is done in the draw function because the size of the background needs to be adjusted to the text size
                spriteBatch.Draw(backgroundSprite.Sprite, new Rectangle((int)tutorialPosition.X, (int)tutorialPosition.Y, (int)text.Size.X + 10, (int)text.Size.Y + 10), Color.White);
                spriteBatch.DrawString(textFont, tutorialText, new Vector2((int)tutorialPosition.X + 5, (int)tutorialPosition.Y + 5), Color.Black);
            }
        }

    }
}
