using HarvestValley.GameObjects.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley.GameObjects
{
    class ShopItems : GameObjectList
    {
        public string itemSelected = "HOE";

        public ShopItems()
        {
            #region Adding Items
            Add(new Seed("spr_seed1_stage1", true, 10));
            Add(new Wood("spr_wood", true, 0));
            Add(new TreeSeed("spr_treeseed", true, 5));
            Add(new Rock("rock", true, 0));
            Add(new Sprinkler("sprinkler", true, 3));
            #endregion
        }
    }
}
