using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HarvestValley
{
    class GlobalTime : GameObject
    {
        public GlobalTime() : base() { }
        public int counter = 1;
        float countDuration = 2f;
        float currentTime = 1f;

        public override void Update(GameTime gameTime)
        {
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds; // time passed since last update
            if (currentTime >= countDuration)
            {
                counter++;
                currentTime -= countDuration;
            }
        }

        public override void Reset()
        {
            counter = 0;
        }
    }
}
