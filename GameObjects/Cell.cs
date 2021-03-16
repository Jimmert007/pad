using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BaseProject
{
    class Cell : SpriteGameObject
    {
        public int cellID;
        public bool soilHasPlant = false;
        private int _sheetIndex;

        public Cell(SpriteSheet _sprite, Vector2 _position, float _scale, int _id) : base(_sprite)
        {
            scale = _scale;
            position = _position;
            cellID = _id;
            if (cellID > 0 && cellID < 11)
            {
                _sheetIndex = 7;
            }
            else if (cellID > 72 && cellID < 83)
            {
                _sheetIndex = 1;
            }
            else if (cellID % 12 == 0 && cellID > 0 && cellID < 72)
            {
                _sheetIndex = 5;
            }
            else if (cellID % 12 == 11 && cellID < 83 && cellID > 12)
            {
                _sheetIndex = 3;
            }
            else if (cellID == 72)
            {
                _sheetIndex = 10;
            }
            else if (cellID == 11)
            {
                _sheetIndex = 11;
            }
            else if (cellID == 0)
            {
                _sheetIndex = 11;
            }
            else if (cellID == 83)
            {
                _sheetIndex = 10;
            }
            else
            {
                _sheetIndex = 9;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sprite.SheetIndex = _sheetIndex;
            sprite.Draw(spriteBatch, Position, origin, scale);
        }
    }
}