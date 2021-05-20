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
            Add(new Seed("Environment/spr_seed1_stage1", true, 10));
            Add(new Wood("Items/spr_wood", true, 0));
            Add(new TreeSeed("Items/spr_treeseed", true, 5));
            Add(new Rock("Items/rock", true, 0));
            Add(new Sprinkler("Environment/sprinkler", true, 3));
            #endregion
        }
    }
}
