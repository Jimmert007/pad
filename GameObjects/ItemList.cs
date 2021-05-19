using HarvestValley.GameObjects.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley.GameObjects
{
    class ItemList : GameObjectList
    {
        public string itemSelected = "HOE";

        public ItemList()
        {
            #region Adding Items
            Add(new Hoe("Tools/spr_hoe", false, 1));
            Add(new Axe("Tools/spr_axe", false, 1));
            Add(new Pickaxe("Tools/pickaxe", false, 1));
            Add(new WateringCan("Items/spr_watering_can", false, 1));
            Add(new Seed("Environment/spr_seed1_stage1", true, 10));
            Add(new Wood("Items/spr_wood", true, 0));
            Add(new TreeSeed("Items/spr_treeseed", true, 5));
            Add(new Rock("Items/rock", true, 0));
            Add(new Sprinkler("Environment/sprinkler", true, 3));
            #endregion
        }
    }
}
