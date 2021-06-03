using HarvestValley.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley
{
    class Seed : Item
    {
        public Seed(string _assetName, bool stackable, int startItemAmount, float scale = 1) : base(_assetName, stackable, startItemAmount, scale)
        {

        }
    }
}
