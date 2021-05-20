﻿using HarvestValley.GameObjects.Tools;
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
            Add(new Hoe("Tools/spr_hoe", false, 0, 1));
            Add(new Axe("Tools/spr_axe", false, 0, 1));
            Add(new Pickaxe("Tools/pickaxe", false, 0, 1));
            Add(new WateringCan("Items/spr_watering_can", false, 0, 1));
            Add(new Seed("Environment/spr_seed1_stage1", true, 10, 2));
            Add(new TreeSeed("Items/spr_treeseed", true, 5, 1));
            Add(new Sprinkler("Environment/sprinkler", true, 3, 1.33f));
            Add(new Wood("Items/spr_wood", true, 0, 1));
            Add(new Rock("Items/rock", true, 0, 1));
            Add(new Wheat("Items/Wheat", true, 0, 1));
            #endregion
        }
    }
}
