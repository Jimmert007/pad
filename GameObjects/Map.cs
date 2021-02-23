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
    class Map : GameObject
    {
        public int rows, cols;
        public List<Cell> cells = new List<Cell>();

        public Map(string _assetName, Vector2 _position, Vector2 _size) : base(_assetName)
        {
            position = _position;
            size = _size;
            Debug.WriteLine(size.X + ", " +  size.Y);
            rows = GameEnvironment.Screen.X / texture.Width;
            cols = GameEnvironment.Screen.Y / texture.Height;

            Debug.Print(rows.ToString() + " x " + cols.ToString());
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
