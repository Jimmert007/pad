using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject
{
    class Sleeping : GameObject
    {
        public float fadeAmount = 0;
        public bool useOnce = true;
        public bool fade = false;
        public bool fadeIn, fadeOut;
        Color color1, color2, finalColor;
        public Texture2D fadeSprite;
        public Sleeping(string _assetName) : base(_assetName)
        {
            color1 = new Color(0, 0, 0, 0);
            color2 = new Color(0, 0, 0, 255);
            fadeSprite = GameEnvironment.ContentManager.Load<Texture2D>("EnergyBarBackground");
            texture = fadeSprite;
        }

        public void Update(GlobalTime globalTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && useOnce/* && insert cords check*/)
            {
                Sleep(globalTime);
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (fade)
            {
                spriteBatch.Draw(texture, new Rectangle(0, 0, GameEnvironment.Screen.X, GameEnvironment.Screen.Y), finalColor);
            }
        }
        public void FadeScreen()
        {
            if (fadeIn)
            {
                fadeAmount += .01f;
            } else if (fadeOut) 
            {
                fadeAmount -= 0.01f; 
            }
            finalColor = Color.Lerp(color1, color2, fadeAmount);
        }

        public void Sleep(GlobalTime time)
        {
            fade = true;
            fadeIn = true;
            time.Reset();
        }
    }
}
