using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley.GameObjects.Tools
{
    class Rock : Item
    {
        public Rock(string _assetName, bool stackable, int startItemAmount) : base(_assetName, stackable, startItemAmount)
        {

        }
    }
}
