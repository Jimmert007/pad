using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    class Hotbar
    {
        int x;
        int y;

        Vector2 position;
        Texture2D texture;
        Game1 game1;

        public Hotbar(int _x, int _y, Game1 _game1)
        {
            position.X = _x;
            position.Y = _y;
            game1 = _game1;
            texture = game1.Content.Load<Texture2D>("test");
        }

        public void Display()
        {
            game1.spriteBatch.Draw(texture, new Rectangle(x, y, 200, 50), Color.Gray);
          
        }
    }
}
