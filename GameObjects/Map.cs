﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Niels Duivenvoorden, 500847100
//purpose: Generate a random map for every player

namespace BaseProject
{
    class Map : GameObject
    {
        public int rows, cols;

        public Map(Vector2 _position, Vector2 _size) : base()
        {
            position = _position;
            size = _size;
            cols = (int)MathF.Round(GameEnvironment.Screen.X / size.X);
            rows = (int)MathF.Round(GameEnvironment.Screen.Y / size.Y);

            Debug.WriteLine(cols.ToString() + " x " + rows.ToString() + " totaal " + rows * cols);
        }

        public int index(int x, int y)
        {
            if (x < 0 || y < 0 || x > cols - 1 || y > rows - 1)
            {
                return 0;
            }
            return x + y * cols;
        }
    }
}

