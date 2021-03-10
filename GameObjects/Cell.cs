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
        SpriteSheet sprites;
        int tileAmount = 4;
        public int cellID;
        public bool soilHasPlant = false;
        public Rectangle sourceRect;
        public Cell(string _assetName, Vector2 _position, Vector2 _size, int _id) : base(_assetName)
        {
            position = _position;
            size = _size;
            cellID = _id;

            if (position.Y == 0 || position.Y == GameEnvironment.Screen.Y - size.Y || position.X == 0){
                sourceRect = new Rectangle(64, 64, 1024, 1024);
            }
            else
            {
                sourceRect = new Rectangle(900, 0, 380, 380);
            }
        }
    }
}
