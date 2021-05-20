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
        public int mapSizeX = GameEnvironment.Screen.X, mapSizeY = GameEnvironment.Screen.Y, cellSize = 64,
            outerringRandomTree = 4, outerringRandomStone = 2, middleringRandomTree = 4, middleringRandomStone = 6, innerringRandomTree = 20, innerringRandomStone = 30;


        int mapWidth = GameEnvironment.Screen.X * 3, mapHeight = GameEnvironment.Screen.Y * 3;
        public int rows, cols;
        public Map() : base()
        {
            cols = mapWidth / 64;
            rows = mapHeight / 64;

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

