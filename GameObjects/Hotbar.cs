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
        public int ScreenWidth;
        public int ScreenHeight;

        public Hotbar(string _assetName, Vector2 _velocity) : base(_assetName)
        {
            
            ScreenWidth = GameEnvironment.screen.X;
            ScreenHeight = GameEnvironment.screen.Y;
            position.X = ScreenWidth/2 - texture.Width/2;
            position.Y = ScreenHeight - texture.Height;
           

            velocity = _velocity;
        }
    }
}
