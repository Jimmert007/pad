
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley
{
    class Hoe : Tools
    {

        public Hoe(string _assetName, Vector2 _position, float _scale) : base(_assetName)
        {
            position = _position;
            scale = _scale;
        }
    }
}
