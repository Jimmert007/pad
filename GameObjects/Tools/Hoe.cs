using HarvestValley.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley
{
    class Hoe : Item
    {
        public Hoe(string _assetName, bool stackable, int startItemAmount) : base(_assetName, stackable, startItemAmount)
        {
            visible = false;
        }
    }
}
