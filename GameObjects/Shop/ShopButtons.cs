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
            for (int i = 0; i < 8; i++){
                shopButtons[i] = new Button();
            }

            Add(confirm = new Button("checkmark"));
            confirm.Position = new Vector2(GameEnvironment.Screen.X * .3f, GameEnvironment.Screen.Y * .6f);
            //yes.Scale = .5f;
            Add(cancel = new Button("cross"));
            cancel.Position = new Vector2(GameEnvironment.Screen.X * .6f, GameEnvironment.Screen.Y * .6f);
            //no.Scale = .5f;
            Add(addItem = new Button("cross"));
            //addItem.Position = new Vector2(GameEnvironment.Screen.X * .6f, GameEnvironment.Screen.Y * .6f);
            Add(reduceItem = new Button("cross"));
            //reduceItem.Position = new Vector2(GameEnvironment.Screen.X * .6f, GameEnvironment.Screen.Y * .6f);
            Add(add10Items = new Button("cross"));
            //add10Items.Position = new Vector2(GameEnvironment.Screen.X * .6f, GameEnvironment.Screen.Y * .6f);
            Add(reduce10Items = new Button("cross"));
            //reduce10Items.Position = new Vector2(GameEnvironment.Screen.X * .6f, GameEnvironment.Screen.Y * .6f);
            Add(buy = new Button("checkmark"));
            //buy.Position = new Vector2(GameEnvironment.Screen.X * .6f, GameEnvironment.Screen.Y * .6f);
            Add(sell = new Button("cross"));
            //sell.Position = new Vector2(GameEnvironment.Screen.X * .6f, GameEnvironment.Screen.Y * .6f);

            shopButtons[0] = confirm;
            shopButtons[1] = cancel;
            shopButtons[2] = addItem;
            shopButtons[3] = reduceItem;
            shopButtons[4] = add10Items;
            shopButtons[5] = reduce10Items;
            shopButtons[6] = buy;
            shopButtons[7] = sell;

        }

    }
}
