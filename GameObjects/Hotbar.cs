using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BaseProject
{
    class Hotbar : GameObject
    {
        public List<GameObject> hotbarItemList;
        public int ScreenWidth;
        public int ScreenHeight;

        public Hotbar(string _assetName) : base(_assetName)
        {
            hotbarItemList = new List<GameObject>();

            ScreenWidth = GameEnvironment.screen.X;
            ScreenHeight = GameEnvironment.screen.Y;
            position.X = ScreenWidth / 2 - texture.Width / 2;
           /* position.Y = ScreenHeight - texture.Height;*/

            /* velocity = _velocity;*/

        }

      
    }
}
