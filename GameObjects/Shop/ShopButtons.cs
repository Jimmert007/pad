using HarvestValley.GameObjects.UI;

namespace HarvestValley.GameObjects.Shop
{
    class ShopButtons : GameObjectList
    {
        public Button confirm, cancel, addItem, reduceItem, add10Items, reduce10Items, buy, sell;
        public ShopButtons()
        {
            //Add all the different shop buttons
            Add(confirm = new Button("UI/spr_yes_button"));
            Add(cancel = new Button("UI/spr_no_button"));
            Add(addItem = new Button("UI/Plus"));
            Add(reduceItem = new Button("UI/Minus"));
            Add(add10Items = new Button("UI/Plus"));
            Add(reduce10Items = new Button("UI/Minus"));
            Add(buy = new Button("UI/spr_shop_buy"));
            Add(sell = new Button("UI/spr_shop_sell"));
        }
    }
}
