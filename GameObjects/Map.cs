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
        public int rows, cols;
        public GameObjectList cells = new GameObjectList();
        public Map(Vector2 _size) : base()
        {
            cols = (int)MathF.Floor(GameEnvironment.Screen.X / _size.X * 2);
            rows = (int)MathF.Floor(GameEnvironment.Screen.Y / _size.Y * 2);

            //Debug.WriteLine(cols.ToString() + " x " + rows.ToString() + " totaal " + rows * cols);
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

