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
        GameObjectList sprites = new GameObjectList();
        int tileAmount = 4;
        public int id;
        public bool soilHasPlant = false;
        public Rectangle sourceRect;
        public Cell(string _assetName, Vector2 _position, Vector2 _size, int _id) : base(_assetName)
        {
            position = _position;
            size = _size;
            id = _id;
            for (int i = 0; i < tileAmount; i++)
            {
                sprites.Add(new SpriteGameObject("tiles/Grassland 2 Color 2@128x128"));
                sprites.Add(new SpriteGameObject("tiles/Grassland Color 2@128x128"));
            }
            if (position.Y == 0 || position.Y == GameEnvironment.Screen.Y - size.Y || position.X == 0){
                sprite = new SpriteSheet("tiles/Grassland 2 Color 2@128x128");
                sourceRect = new Rectangle(64, 64, 1024, 1024);
            }
            else
            {
                sprite = new SpriteSheet("tiles/Grassland Color 2@128x128");
                sourceRect = new Rectangle(900, 0, 380, 380);
            }
        }
    }
}
