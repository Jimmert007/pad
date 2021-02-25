using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BaseProject
{
    class Cell : GameObject
    {
        List<Texture2D> sprites = new List<Texture2D>();
        int tileAmount = 4;
        public int id;
        public Cell(string _assetName, Vector2 _position, Vector2 _size, int _id) : base(_assetName)
        {
            position = _position;
            size = _size;
            id = _id;
            for (int i = 0; i < tileAmount; i++)
            {
                sprites.Add(GameEnvironment.ContentManager.Load<Texture2D>("tiles/Grassland 2 Color 2@128x128"));
                sprites.Add(GameEnvironment.ContentManager.Load<Texture2D>("tiles/Grassland 2 Color 3@128x128"));
                sprites.Add(GameEnvironment.ContentManager.Load<Texture2D>("tiles/Grassland 2 Fall@128x128"));
                sprites.Add(GameEnvironment.ContentManager.Load<Texture2D>("tiles/Grassland 2 Spring@128x128"));
                sprites.Add(GameEnvironment.ContentManager.Load<Texture2D>("tiles/Grassland 2 Winter@128x128"));
                sprites.Add(GameEnvironment.ContentManager.Load<Texture2D>("tiles/Grassland 2@128x128"));
            }
            if (position.Y == 0 || position.Y + size.Y == GameEnvironment.Screen.Y)
            {
                texture = sprites[5];
            }
            else if (position.X == 0 || position.X + size.X == GameEnvironment.Screen.X)
            {
                texture = sprites[2];
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, 50, 50), Color.Red);
        }
    }
}
