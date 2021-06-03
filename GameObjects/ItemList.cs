using HarvestValley.GameObjects.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley.GameObjects
{
    /// <summary>
    /// Jim van de Burgwal
    /// This GameObjectList contains all the items in the game and sets their specific details like their sprite, if they are stackable, with how many items you start and the sprite scale
    /// </summary>
    class ItemList : GameObjectList
    {
        public string itemSelected = "HOE"; //The hoe is always selected at first

        public ItemList()
        {
            #region Adding Items
            Add(new Hoe("Tools/spr_hoe", false, 0, 1));
            Add(new Axe("Tools/spr_axe", false, 0, 1));
            Add(new Pickaxe("Tools/pickaxe", false, 0, 1));
            Add(new WateringCan("Items/spr_watering_can", false, 0, 1));
            Add(new Seed("Environment/spr_seed1_stage1", true, 10, 2));
            Add(new TreeSeed("Items/spr_treeseed", true, 0, 1));
            Add(new Sprinkler("Environment/sprinkler", true, 0, 1.33f));
            Add(new Wood("Items/spr_wood", true, 0, 1));
            Add(new Rock("Items/rock", true, 0, 1));
            Add(new Wheat("Items/Wheat", true, 0, 1));
            #endregion
        }
    }
}
