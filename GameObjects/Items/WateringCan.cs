using HarvestValley.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley
{
    class WateringCan : Item
    {
        public WateringCan(string _assetName, bool stackable, int startItemAmount, float scale) : base(_assetName, stackable, startItemAmount, scale)
        {
        }
    }
}
