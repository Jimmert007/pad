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
        void Start()
        {
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && useOnce/* && insert cords check*/)
            {
                Sleep();
                useOnce = false;
            }
        }

        void Sleep()
        {
            energy += 50;
            //time = day;
            //plant grow
        }

    }
}
