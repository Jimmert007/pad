using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HarvestValley.GameObjects.Tutorial
{
    class TutorialStep : GameObjectList
    {
        public int step;
        public bool mouseCollides;
        public string tutorialText;
        public Vector2 tutorialPosition;
        SpriteGameObject background;
        TextGameObject text;
        SpriteSheet backgroundSprite;
        SpriteFont textSprite;
        public TutorialStep(int step, string tutorialText)
        {
            this.step = step;
            this.tutorialText = tutorialText;
            backgroundSprite = new SpriteSheet("TutorialBackground");
            textSprite = GameEnvironment.AssetManager.Content.Load<SpriteFont>("JimFont");
            foreach (GameObject GO in children)
            {
                GO.Visible = false;
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            tutorialPosition = inputHelper.MousePosition;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            if (mouseCollides)
            {
                spriteBatch.Draw(backgroundSprite.Sprite, new Rectangle((int)tutorialPosition.X, (int)tutorialPosition.Y, (int)530, (int)50), Color.White);
                spriteBatch.DrawString(textSprite, tutorialText, new Vector2((int)tutorialPosition.X + 5, (int)tutorialPosition.Y + 5), Color.Black);
            }
        }

    }
}
