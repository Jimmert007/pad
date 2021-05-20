using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace HarvestValley.GameObjects.UI
{
    class UIBox : SpriteGameObject
    {
        public UIBox(string _assetName) : base(_assetName)
        {
            Scale = 2;
        }
    }
}
