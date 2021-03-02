using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseProject
{
    class GlobalTime : GameObject
    {
        public GlobalTime(string _assetName) : base(_assetName) { }
        public int counter = 1;
        float countDuration = 2f;
        float currentTime = 1f;

        public void Update(GameTime gameTime)
        {
            
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds; // time passed since last update
            if (currentTime >= countDuration)
            {
                counter++;
                currentTime -= countDuration;
            }
            Debug.WriteLine(counter);
        }

        public void Reset()
        {
            counter = 0;
        }
    }
}
