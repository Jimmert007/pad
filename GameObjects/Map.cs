using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Niels Duivenvoorden, 500847100
//purpose: Generate a random map for every player

namespace BaseProject
{
    class Map
    {
        int cols, rows;
        List<Cell> grid = new List<Cell>();

        Map() { 
            
        }

        int index(int x, int y)
        {
            if (x < 0 || y < 0 || x > cols - 1 || y > rows - 1)
            { //check of het zich in het speelveld bevindt
                return 0;
            }
            return x + y * cols;
        }
    }
}
