using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using HarvestValley.GameObjects.Shop;
using HarvestValley.GameObjects;
using HarvestValley.GameObjects.UI;
using HarvestValley.GameObjects.Tools;
using System.Diagnostics;

namespace HarvestValley.GameObjects.Shop
{
    class ShopMenuUIList : GameObjectList
    {
        SpriteGameObject mouseGO;
        ShopItems shopItems;
        //Button confirm, cancel, addItem, reduceItem, add10Items, reduce10Items, buy, sell;
        TextGameObject topLine, bottomLine, buyLine, sellLine, cancelLine;
        UIBox uIBox;
        ShopButtons shopButtons;
        public bool shopActive, buyActive, buyAmount, sellActive, sellAmount = false;
        private int shopItemAmount = 0, offset = 128;
        public string[] shopDialogueLines = { "Welcome to the shop", "What do you want to do?", "What do you want to buy?", "What do you want to sell?", "How many do you want to buy?", "How many do you want to sell?", "Buy", "Sell", "Cancel" };
        public int[] itemAmount = { 0, 0, 1, -1, 10, -10, 0, 0 };
        ItemList itemList;
        Item selectedShopItem;
        SpriteGameObject shopPC;

        public ShopMenuUIList(ItemList _itemList)
        {
            Add((shopPC = new SpriteGameObject("UI/spr_selected_square")));
            shopPC.Position = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);

            //Add all the instances of the Shop elements
            Add(uIBox = new UIBox("UI/ui_bar"));
            Add(shopButtons = new ShopButtons());
            Add(shopItems = new ShopItems());
            Add(topLine = new TextGameObject("Fonts/JimFont"));
            Add(bottomLine = new TextGameObject("Fonts/JimFont"));
            Add(buyLine = new TextGameObject("Fonts/JimFont"));
            Add(sellLine = new TextGameObject("Fonts/JimFont"));
            Add(cancelLine = new TextGameObject("Fonts/JimFont"));
            Add(mouseGO = new SpriteGameObject("Player/1px"));


            itemList = _itemList;

            ResetShop();
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            mouseGO.Position = inputHelper.MousePosition;

            //Activate UI bools
            if (inputHelper.MouseLeftButtonPressed() && shopPC.CollidesWith(mouseGO))
            {
                InitShopWelcomePage();
            }
            //if (inputHelper.KeyPressed(Keys.B)) { ResetShop(); }
            if (shopActive)
            {
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[6].collidesWithMouse(inputHelper.MousePosition) && shopButtons.shopButtons[6].Visible) { InitBuyPage(); }
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[7].collidesWithMouse(inputHelper.MousePosition) && shopButtons.shopButtons[7].Visible) { InitSellPage(); }
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[1].collidesWithMouse(inputHelper.MousePosition) && shopButtons.shopButtons[1].Visible) { ResetShop();  shopActive = false; }
            }
            if (buyActive)
            {
                foreach (Item item in shopItems.Children) { if (inputHelper.MouseLeftButtonPressed() && item.CollidesWith(mouseGO) && item.Visible) { selectedShopItem = item; selectedShopItem.selectedItem = true; InitConfirmBuy(); } }
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[1].collidesWithMouse(inputHelper.MousePosition) && shopButtons.shopButtons[1].Visible) { bottomLine.Text = shopDialogueLines[1]; InitShopWelcomePage(); }
            }
            if (sellActive)
            {
                foreach (Item item in shopItems.Children) { if (inputHelper.MouseLeftButtonPressed() && item.CollidesWith(mouseGO) && item.Visible) { selectedShopItem = item; selectedShopItem.selectedItem = true; InitConfirmSell(); } }
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[1].collidesWithMouse(inputHelper.MousePosition) && shopButtons.shopButtons[1].Visible) { bottomLine.Text = shopDialogueLines[1]; InitShopWelcomePage(); }
            }
            if (buyAmount)
            {
                //Make item amount, buy and sell buttons visible
                for (int i = 2; i < 6; i++)
                {
                    if (shopButtons.shopButtons[i].collidesWithMouse(inputHelper.MousePosition) && inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[i].Visible)
                    {
                        shopItemAmount += itemAmount[i];
                    }
                }
                //Inputs for the confirm button
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[0].collidesWithMouse(inputHelper.MousePosition) && shopButtons.shopButtons[0].Visible)
                {
                    //Add item amount on the hotbar
                    foreach (Item x in itemList.Children)
                    {
                        if (x.GetType() == selectedShopItem.GetType())
                        {
                            x.itemAmount += shopItemAmount;
                            selectedShopItem.selectedItem = false;
                            //Reduce money for specified item here
                            ResetShop();
                        }
                    }
                }
                //Inputs for the cancel button
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[1].collidesWithMouse(inputHelper.MousePosition) && shopButtons.shopButtons[1].Visible)
                {
                    selectedShopItem.selectedItem = false;
                    InitBuyPage();
                }
            }
            if (sellAmount)
            {
                //Make item amount, buy and sell buttons visible
                for (int i = 2; i < 6; i++)
                {
                    if (shopButtons.shopButtons[i].collidesWithMouse(inputHelper.MousePosition) && inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[i].Visible)
                    {
                        shopItemAmount += itemAmount[i];
                    }

                }
                //Inputs for the confirm button
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[0].collidesWithMouse(inputHelper.MousePosition) && shopButtons.shopButtons[0].Visible)
                {
                    //Add item amount on the hotbar
                    foreach (Item x in itemList.Children)
                    {
                        if (x.GetType() == selectedShopItem.GetType())
                        {
                            x.itemAmount -= shopItemAmount;
                            selectedShopItem.selectedItem = false;
                            //Add money here for specifed item here
                            ResetShop();
                        }
                    }
                }
                //Inputs for the cancel button
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[1].collidesWithMouse(inputHelper.MousePosition) && shopButtons.shopButtons[1].Visible)
                {
                    selectedShopItem.selectedItem = false;
                    InitSellPage();
                }
            }
        }
        public void ResetShop()  //Resets all the shop elements
        {
            //Set the text lines for the shop
            topLine.Text = shopDialogueLines[0];
            bottomLine.Text = shopDialogueLines[1];
            buyLine.Text = shopDialogueLines[6];
            sellLine.Text = shopDialogueLines[7];
            cancelLine.Text = shopDialogueLines[8];

            //Set the position of Shop elements
            uIBox.Position = new Vector2(GameEnvironment.Screen.X * .5f - uIBox.Sprite.Width * .5f * uIBox.Scale, GameEnvironment.Screen.Y * .35f);
            topLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - topLine.Size.X * .5f, GameEnvironment.Screen.Y * .1f);
            bottomLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - bottomLine.Size.X * .5f, GameEnvironment.Screen.Y * .2f);

            buyLine.Position = new Vector2(GameEnvironment.Screen.X * 1 / 3 - buyLine.Size.X * .5f, GameEnvironment.Screen.Y * .7f);
            sellLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - sellLine.Size.X * .5f, GameEnvironment.Screen.Y * .7f);
            cancelLine.Position = new Vector2(GameEnvironment.Screen.X * 2 / 3 - cancelLine.Size.X * .5f, GameEnvironment.Screen.Y * .7f);

            shopButtons.shopButtons[1].Position = new Vector2(GameEnvironment.Screen.X * 2 / 3 - shopButtons.shopButtons[1].Sprite.Width / 2, GameEnvironment.Screen.Y * .6f);
            //shopButtons.shopButtons[6].Position = new Vector2(GameEnvironment.Screen.X * 2 / 3 - shopButtons.shopButtons[1].Sprite.Width / 2, GameEnvironment.Screen.Y * .6f);
            //shopButtons.shopButtons[7].Position = new Vector2(GameEnvironment.Screen.X * 2 / 3 - shopButtons.shopButtons[1].Sprite.Width / 2, GameEnvironment.Screen.Y * .6f);

            //Make Shop Items invisible
            foreach (Item x in shopItems.Children)
            {
                x.Visible = false;
            }
            //Set the ui lines invisible
            uIBox.Visible = false;
            topLine.Visible = false;
            bottomLine.Visible = false;
            buyLine.Visible = false;
            cancelLine.Visible = false;
            sellLine.Visible = false;

            //Resets the shop buttons
            for (int i = 0; i < shopButtons.shopButtons.Length; i++)
            {
                shopButtons.shopButtons[i].Visible = false;
            }

            //Set positions of the 3 main shop buttons
            shopButtons.shopButtons[7].Position = new Vector2(GameEnvironment.Screen.X * 1 / 2 - shopButtons.shopButtons[7].Sprite.Width / 2, GameEnvironment.Screen.Y * .6f);
            shopButtons.shopButtons[1].Position = new Vector2(GameEnvironment.Screen.X * 2 / 3 - shopButtons.shopButtons[1].Sprite.Width / 2, GameEnvironment.Screen.Y * .6f);
            shopButtons.shopButtons[6].Position = new Vector2(GameEnvironment.Screen.X * 1 / 3 - shopButtons.shopButtons[6].Sprite.Width / 2, GameEnvironment.Screen.Y * .6f);

            //Set positions of the itam amount buttons
            shopButtons.shopButtons[2].Position = new Vector2(GameEnvironment.Screen.X * 4 / 7, GameEnvironment.Screen.Y / 2);      //Add item button position
            shopButtons.shopButtons[3].Position = new Vector2(GameEnvironment.Screen.X * 3 / 7, GameEnvironment.Screen.Y / 2);      //Reduce item button position
            shopButtons.shopButtons[4].Position = new Vector2(GameEnvironment.Screen.X * .5f, GameEnvironment.Screen.Y * 2 / 7);    //Add 10 itams button position
            shopButtons.shopButtons[5].Position = new Vector2(GameEnvironment.Screen.X * .5f, GameEnvironment.Screen.Y * 5 / 7);    //Reduce 10 items position

            //Resets the bools
            shopActive = false;
            buyActive = false;
            sellActive = false;
            buyAmount = false;
            sellAmount = false;

            //Reset shopAmount
            shopItemAmount = 0;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //Prevents shopItemAmmount from going below 0
            if (shopItemAmount < 0) { shopItemAmount = 0; }

            //Continuesly updates the text when buying/selling
            if (buyAmount || sellAmount)
            {
                bottomLine.Text = shopItemAmount.ToString();
            }
        }
        public void InitShopWelcomePage()
        {//Set the welcome page elements of the shop on true
            shopActive = true;
            topLine.Text = shopDialogueLines[0];
            uIBox.Visible = true;
            topLine.Visible = true;
            bottomLine.Visible = true;
            buyLine.Visible = true;
            cancelLine.Visible = true;
            sellLine.Visible = true;
            shopButtons.shopButtons[6].Visible = true;
            shopButtons.shopButtons[7].Visible = true;
            shopButtons.shopButtons[1].Visible = true;
            shopButtons.shopButtons[7].Position = new Vector2(GameEnvironment.Screen.X * 1 / 2 - shopButtons.shopButtons[7].Sprite.Width / 2, GameEnvironment.Screen.Y * .6f);
            shopButtons.shopButtons[1].Position = new Vector2(GameEnvironment.Screen.X * 2 / 3 - shopButtons.shopButtons[1].Sprite.Width / 2, GameEnvironment.Screen.Y * .6f);
            shopButtons.shopButtons[6].Position = new Vector2(GameEnvironment.Screen.X * 1 / 3 - shopButtons.shopButtons[6].Sprite.Width / 2, GameEnvironment.Screen.Y * .6f);
            
            //Deactivate elements from other pages
            sellActive = false;
            buyActive = false;
            buyAmount = false;
            sellAmount = false;
            foreach(Item x in shopItems.Children) { x.Visible = false; }
        }
        public void InitBuyPage()
        {
            //Turn off elements from Welcome page
            buyActive = true;
            shopActive = false;

            buyLine.Visible = false;
            sellLine.Visible = false;
            cancelLine.Visible = false;

            shopButtons.shopButtons[6].Visible = false;
            shopButtons.shopButtons[7].Visible = false;


            //Turn on the buy page elements of the shop true
            bottomLine.Visible = false;
            topLine.Text = shopDialogueLines[2];
            shopButtons.shopButtons[1].Position = new Vector2(GameEnvironment.Screen.X * 1 / 2 - shopButtons.shopButtons[1].Sprite.Width / 2, GameEnvironment.Screen.Y * .7f);

            //Draw the items in a row
            for (int i = 0; i < shopItems.Children.Count; i++)
            {
                Item item = (shopItems.Children[i] as Item);

                shopItems.Children[i].Visible = true;
                shopItems.Children[i].Position = new Vector2(GameEnvironment.Screen.X / 4 + (offset * i), GameEnvironment.Screen.Y / 2);
            }
            //Make item amount, buy and sell buttons invisible
            for (int i = 2; i < 6; i++)
            {
                shopButtons.shopButtons[i].Visible = false;
                shopButtons.shopButtons[0].Visible = false;

            }
        }
        public void InitConfirmBuy()
        {
            //Turn off elements from Buy page
            buyAmount = true;
            buyActive = false;
            bottomLine.Visible = true;
            buyLine.Visible = false;
            cancelLine.Visible = false;
            sellLine.Visible = false;

            topLine.Text = shopDialogueLines[4];        //Change the question line 
            bottomLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - bottomLine.Size.X * .5f, GameEnvironment.Screen.Y * .2f);

            foreach (Item x in shopItems.Children)
            {
                if (!x.selectedItem)        //Turn the non-selected items invisible
                {
                    x.Visible = false;
                }
                if (x.selectedItem)         //Change the position of the selected item
                {
                    x.Position = new Vector2(GameEnvironment.Screen.X / 2 - x.Sprite.Width / 2, GameEnvironment.Screen.Y / 3);

                }
            }

            //Make item amount, buy and sell buttons visible
            for (int i = 0; i < 6; i++)
            {
                shopButtons.shopButtons[i].Visible = true;

            }

            //Set the positions of the item amount, buy and sell buttons
            shopButtons.shopButtons[0].Position = new Vector2(GameEnvironment.Screen.X * 2 / 3 - shopButtons.shopButtons[0].Sprite.Width / 2, GameEnvironment.Screen.Y * 2 / 3);
            shopButtons.shopButtons[1].Position = new Vector2(GameEnvironment.Screen.X / 3 - shopButtons.shopButtons[1].Sprite.Width / 2, GameEnvironment.Screen.Y * 2 / 3);
        }
        public void InitSellPage()
        {
            //Turn off elements from Welcome page
            sellActive = true;
            shopActive = false;


            buyLine.Visible = false;
            sellLine.Visible = false;
            cancelLine.Visible = false;

            shopButtons.shopButtons[6].Visible = false;
            shopButtons.shopButtons[7].Visible = false;


            //Turn on the buy page elements of the shop true
            bottomLine.Visible = false;
            topLine.Text = shopDialogueLines[3];
            shopButtons.shopButtons[1].Position = new Vector2(GameEnvironment.Screen.X * 1 / 2 - shopButtons.shopButtons[1].Sprite.Width / 2, GameEnvironment.Screen.Y * .7f);

            //Draw the items in a row
            for (int i = 0; i < shopItems.Children.Count; i++)
            {
                Item item = (shopItems.Children[i] as Item);

                shopItems.Children[i].Visible = true;
                shopItems.Children[i].Position = new Vector2(GameEnvironment.Screen.X / 4 + (offset * i), GameEnvironment.Screen.Y / 2);
            }
            //Make item amount, buy and sell buttons invisible
            for (int i = 2; i < 6; i++)
            {
                shopButtons.shopButtons[i].Visible = false;
                shopButtons.shopButtons[0].Visible = false;
            }
        }
        public void InitConfirmSell()
        {
            //Turn off elements from sell page
            sellAmount = true;
            sellActive = false;
            bottomLine.Visible = true;
            buyLine.Visible = false;
            cancelLine.Visible = false;
            sellLine.Visible = false;
            topLine.Text = shopDialogueLines[5];        //Change the question line 
            bottomLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - bottomLine.Size.X * .5f, GameEnvironment.Screen.Y * .2f);

            foreach (Item x in shopItems.Children)
            {
                if (!x.selectedItem)        //Turn the non-selected items invisible
                {
                    x.Visible = false;
                }
                if (x.selectedItem)         //Change the position of the selected item
                {
                    x.Position = new Vector2(GameEnvironment.Screen.X / 2 - x.Sprite.Width / 2, GameEnvironment.Screen.Y / 3);
                }
            }
            //Make item amount, cancel and sell buttons visible
            for (int i = 0; i < 6; i++)
            {
                shopButtons.shopButtons[i].Visible = true;
            }
            //Set the positions of the item amount, cancel and sell buttons
            shopButtons.shopButtons[0].Position = new Vector2(GameEnvironment.Screen.X * 2 / 3 - shopButtons.shopButtons[0].Sprite.Width / 2, GameEnvironment.Screen.Y * 2 / 3);
            shopButtons.shopButtons[1].Position = new Vector2(GameEnvironment.Screen.X / 3 - shopButtons.shopButtons[1].Sprite.Width / 2, GameEnvironment.Screen.Y * 2 / 3);
        }
    }
}

