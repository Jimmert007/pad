using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseProject
{
    class GlobalTime
    {
       public int counter = 1;
        int limit = 10;
        float countDuration = 2f;
        float currentTime = 1f;
        


       public void Update(GameTime gameTime)
        {
            Debug.WriteLine(counter);

            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds; // time passed since last update
            if (currentTime >= countDuration)
            {
                counter++;
                currentTime -= countDuration;
            }
            
            
        }

        public void Reset()
        {
            Debug.WriteLine("sleeping now");
            counter = 0;
        }
    }
}
