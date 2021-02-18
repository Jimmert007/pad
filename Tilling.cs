using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Linq;

namespace BaseProject
{
    class Tilling
    {
        //Jim van de Burgwal

        //creating variables
        String tool = "HOE";
        Boolean soilIsTilled;
        MouseState state = Mouse.GetState();
        int rectSize = 100;
        int width;
        int height;

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        public Tilling()
        {
 
        }

        public void Update()
        {
            Debug.Print("x" + state.X.ToString() + " y " + state.Y.ToString());
        }
        
        public void Draw()
        {
            new System.Drawing.Rectangle(rectSize, rectSize, rectSize, rectSize);
        }

        void Till()
        {
            //tilling the soil
            if (tool == "HOE")
            {
                //if (state.X > )
            }
        }
    }
}
