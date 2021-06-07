namespace HarvestValley.GameObjects
{
    class CraftingMenu : GameObjectList
    {
        SpriteGameObject CraftingBackground;
        TextGameObject crafting, sprinklerCost;
        public CraftingMenu() : base()
        {
            CraftingBackground = new SpriteGameObject("UI/EnergyBarBackground");
            //Add(CraftingBackground);
        }
    }
}
