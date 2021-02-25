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
        Rectangle sourceRect;
        public Cell(string _assetName, Vector2 _position, Vector2 _size, int _id) : base(_assetName)
        {
            position = _position;
            size = _size;
            id = _id;
            for (int i = 0; i < tileAmount; i++)
            {
                sprites.Add(GameEnvironment.ContentManager.Load<Texture2D>("tiles/Grassland 2 Color 2@128x128"));
                sprites.Add(GameEnvironment.ContentManager.Load<Texture2D>("tiles/Grassland Color 2@128x128"));
            }
            if (position.Y == 0 || position.Y == GameEnvironment.Screen.Y - size.Y){
                texture = sprites[0];
                sourceRect = new Rectangle(64, 64, 1024, 1024);
            }
            else
            {
                texture = sprites[1];
                sourceRect = new Rectangle(900, 0, 380, 380);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y), sourceRect, Color.White);
        }
    }
}
