using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    class Cell
    {
        Vector2 position;
        Texture2D texture;
        Game1 game1;

        public Cell(int _x, int _y, Game1 _game1) {
            position.X = _x;
            position.Y = _y;
            game1 = _game1;
            texture = game1.Content.Load<Texture2D>("test");
        }

        public void Display() {
            game1.spriteBatch.Draw(texture, new Rectangle(0, 0, 50, 50), Color.Yellow);  
        }
    }
}
