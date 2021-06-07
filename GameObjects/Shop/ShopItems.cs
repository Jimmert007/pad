using HarvestValley.GameObjects.Tools;

namespace HarvestValley.GameObjects
{
    class ShopItems : GameObjectList
    {
        public string itemSelected = "HOE";

        public ShopItems()
        {
            #region Adding Items for the shop
            Add(new Seed("Environment/spr_seed1_stage1", true, 15) { scale = 2 });
            Add(new Wood("Items/spr_wood", true, 0));
            Add(new TreeSeed("Items/spr_treeseed", true, 5));
            Add(new Rock("Items/rock", true, 0));
            Add(new Sprinkler("Environment/sprinkler", true, 3));
            Add(new Wheat("Items/Wheat", true, 0, 1));
            #endregion
        }
    }
}
