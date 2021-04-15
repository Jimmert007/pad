using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HarvestValley.GameObjects
{
    class UIButton : UI
    {
        public UIButton(string _assetName, int _x, int _y, float _scale) : base(_assetName)
        {
            position.X = _x;
            position.Y = _y;
            scale = _scale;
        }
    }
}
