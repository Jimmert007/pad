using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley.GameObjects.Tools
{
    class Wheat : Item
    {
        public Wheat(string _assetName, bool stackable, int startItemAmount, float scale) : base(_assetName, stackable, startItemAmount, scale)
        {

        }
    }
}
