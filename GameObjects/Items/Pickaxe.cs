using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley.GameObjects.Tools
{
    class Pickaxe : Item
    {
        public Pickaxe(string _assetName, bool stackable, int startItemAmount, float scale) : base(_assetName, stackable, startItemAmount, scale)
        {

        }
    }
}
