using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

namespace BaseProject
{
    class Sleeping
    {
 
        float energy = 50;
        bool useOnce = true;


        public void Update(GlobalTime globalTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && useOnce/* && insert cords check*/)
            {
                Sleep(globalTime);
                useOnce = false;
              
            }
        }

        void Sleep(GlobalTime time)
        {
            energy += 50;
            time.Reset();
            //plant grow
        }

    }
}
