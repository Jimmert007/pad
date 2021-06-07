using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HarvestValley
{
    /// <summary>
    /// Wessel Vink
    /// This script contains the fade in and fade out for the sleeping action
    /// </summary>
    class Sleeping : GameObject
    {
        public float fadeAmount = 0;
        public bool useOnce = true, sleepHitboxHit;
        public bool fade = false;
        public bool fadeIn, fadeOut;
        Color color1, color2, finalColor;
        public SpriteSheet fadeSprite;
        public Sleeping()
        {
            color1 = new Color(0, 0, 0, 0);
            color2 = new Color(0, 0, 0, 255);
            fadeSprite = new SpriteSheet("UI/EnergyBarBackground");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (sleepHitboxHit && useOnce)
            {
                Sleep();
                useOnce = false;
            }
            if (fade)
            {
                FadeScreen();
            }

            if (fadeAmount >= 1)
            {
                fadeIn = false;
                fadeOut = true;
            }
            if (fadeAmount <= 0)
            {
                fadeOut = false;
                useOnce = true;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            if (fade)
            {
                spriteBatch.Draw(fadeSprite.Sprite, new Rectangle(0, 0, GameEnvironment.Screen.X, GameEnvironment.Screen.Y), finalColor);
            }
        }

        public void FadeScreen()
        {
            if (fadeIn)
            {
                fadeAmount += .01f;
            }
            else if (fadeOut)
            {
                fadeAmount -= .01f;
            }
            finalColor = Color.Lerp(color1, color2, fadeAmount);
        }

        public void Sleep()
        {
            fade = true;
            fadeIn = true;
        }
    }
}
