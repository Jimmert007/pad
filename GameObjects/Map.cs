using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Niels Duivenvoorden, 500847100
//purpose: Generate a random map for every player

namespace HarvestValley
{
    class Map : GameObject
    {
        int mapWidth = GameEnvironment.Screen.X * 4, mapHeight = GameEnvironment.Screen.Y * 4;
        public int rows, cols;
        public Map() : base()
        {
            cols = mapWidth / 128;
            rows = mapHeight / 128;

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

