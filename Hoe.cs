
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    class Hoe : Tools
    {

        public Hoe(string _assetName, int _x, int _y, int _w, int _h) : base(_assetName)
        {
            position.X = _x;
            position.Y = _y;
            size.X = _w;
            size.Y = _h;
            texture = GameEnvironment.ContentManager.Load<Texture2D>("spr_hoe");
        }
    }
}
