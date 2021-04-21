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
            Add(new Axe("spr_empty", false, 1));
            Add(new WateringCan("spr_empty", false, 1));
            Add(new Seed("spr_seed1_stage1", true, 10));
            
            #endregion
        }
    }
}
