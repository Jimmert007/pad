using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HarvestValley
{
    /// <summary>
    /// Niels Duivenvoorden, 500847100
    /// purpose: Generate a random map for every player
    /// </summary>
    class Map : GameObject
    {
        //ints to place the trees and rocks randomly
        public int mapSizeX = GameEnvironment.Screen.X, mapSizeY = GameEnvironment.Screen.Y, cellSize = Cell.CELL_SIZE,
            outerringRandomTree = 4, outerringRandomStone = 2, middleringRandomTree = 4, middleringRandomStone = 6, innerringRandomTree = 20, innerringRandomStone = 30;

        int mapWidth, mapHeight, multiplyMap = 3;
        public int rows, cols;
        public Map()
        {
            mapWidth = GameEnvironment.Screen.X * multiplyMap;  //make the screen 3 times its width
            mapHeight = GameEnvironment.Screen.Y * multiplyMap; //make the screen 3 times its height
            cols = mapWidth / cellSize;                         //set the colum amount
            rows = mapHeight / cellSize;                        //set the row amount
        }
    }
}