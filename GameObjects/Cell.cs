using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    class Cell : GameObject
    {
        List<Texture2D> sprites = new List<Texture2D>();
        int tileAmount = 4;
        public Cell(string _assetName, Vector2 _position, Vector2 _size) : base(_assetName)
        {
            position = _position;
            size = _size;
            for (int i = 0; i < tileAmount; i++)
            {
                sprites.Add(GameEnvironment.ContentManager.Load<Texture2D>("tiles/Grassland 2 Color 2@128x128"));
                sprites.Add(GameEnvironment.ContentManager.Load<Texture2D>("tiles/Grassland 2 Fall@128x128"));
            }
        }
    }
}
