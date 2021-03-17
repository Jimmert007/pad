using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley
{
    class Tools : SpriteGameObject
    {
        public String toolSelected = "HOE";
        public Tools(string _assetName) : base(_assetName)
        {
            sprite = new SpriteSheet("spr_empty");
        }
    }
}
