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
            Add(new Hoe("spr_hoe", false, 1));
            Add(new Axe("spr_axe", false, 1));
            Add(new WateringCan("spr_watering_can", false, 1));
            Add(new Seed("spr_seed1_stage1", true, 10));
            Add(new Wood("spr_wood", true, 0));
            Add(new TreeSeed("spr_treeseed", true, 0));
            Add(new Sprinkler("sprinkler", true, 3));
            #endregion
        }
    }
}
