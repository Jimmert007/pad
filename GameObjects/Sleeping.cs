using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace HarvestValley
{
    class Sleeping : SpriteGameObject
    {
        public float fadeAmount = 0;
        public bool useOnce = true;
        public bool fade = false;
        public bool fadeIn, fadeOut;
        Color color1, color2, finalColor;
        public SpriteSheet fadeSprite;
        public Sleeping(string _assetName) : base(_assetName)
        {
            color1 = new Color(0, 0, 0, 0);
            color2 = new Color(0, 0, 0, 255);
            fadeSprite = new SpriteSheet("EnergyBarBackground");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && useOnce/* && insert cords check*/)
            {
                Sleep(gameTime);
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

        public void Sleep(GameTime time)
        {
            fade = true;
            fadeIn = true;
        }
    }
}
