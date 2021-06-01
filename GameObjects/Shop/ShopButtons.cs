using System;
using System.Collections.Generic;
using System.Text;
using HarvestValley.GameObjects.UI;
using Microsoft.Xna.Framework;

namespace HarvestValley.GameObjects.Shop
{
    class ShopButtons : GameObjectList
    {
        public Button confirm, cancel, addItem, reduceItem, add10Items, reduce10Items, buy, sell;
        public Button[] shopButtons = new Button[8];
        public ShopButtons()
        {
            for (int i = 0; i < 8; i++)
            {
                shopButtons[i] = new Button();
            }

            Add(confirm = new Button("UI/spr_yes_button"));
            Add(cancel = new Button("UI/spr_no_button"));
            Add(addItem = new Button("UI/Plus"));
            Add(reduceItem = new Button("UI/Minus"));
            Add(add10Items = new Button("UI/Plus"));
            Add(reduce10Items = new Button("UI/Minus"));
            Add(buy = new Button("UI/spr_shop_buy"));
            Add(sell = new Button("UI/spr_shop_sell"));

            //shopButtons[0] = confirm;
            //shopButtons[1] = cancel;
            //shopButtons[2] = addItem;
            //shopButtons[3] = reduceItem;
            //shopButtons[4] = add10Items;
            //shopButtons[5] = reduce10Items;
            //shopButtons[6] = buy;
            //shopButtons[7] = sell;

        }

    }
}
