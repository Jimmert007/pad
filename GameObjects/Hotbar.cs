using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BaseProject
{
    class Hotbar : SpriteGameObject
    {
        public List<GameObject> hotbarItemList;
        public int ScreenWidth;
        public int ScreenHeight;

        public Hotbar(string _assetName) : base(_assetName)
        {
            hotbarItemList = new List<GameObject>();

            ScreenWidth = GameEnvironment.Screen.X;
            ScreenHeight = GameEnvironment.Screen.Y;
            position.X = ScreenWidth / 2 - sprite.Width / 2;
           /* position.Y = ScreenHeight - texture.Height;*/

            /* velocity = _velocity;*/

        }

      
    }
}
