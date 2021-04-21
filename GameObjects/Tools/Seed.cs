using HarvestValley.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley
{
    class Seed : Item
    {
        public Seed(string _assetName, bool stackable, int startItemAmount) : base(_assetName, stackable, startItemAmount)
        {
            
        }
    }
}
