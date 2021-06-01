///Mohammad Al Hadiansyah Suwandhy, 500843466
///<Summary>
/// This class serves to adjust and control all the UI elements for the shop
///</Summary>
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
        MouseGameObject mouseGO;
        ShopItems shopItems;
        TextGameObject topLine, bottomLine, buyLine, sellLine, cancelLine;
        UIBox uIBox;
        ShopButtons shopButtons;
        Wallet wallet;
        ItemList itemList;
        Item selectedShopItem;

        public bool shopActive, buyActive, buyAmount, sellActive, sellAmount = false;
        private int shopItemAmount = 0, offset = 128;
        public string[] shopDialogueLines = { "Welcome to the shop", "What do you want to do?", "What do you want to buy?", "What do you want to sell?", "How many do you want to buy?", "How many do you want to sell?", "Buy", "Sell", "Cancel" };
        public int[] itemAmount = { 0, 0, 1, -1, 10, -10, 0, 0 };
        public int[] reduceMoney = { 10, 5, 7, 20, 100, 10 };
        public int[] addMoney = { 5, 2, 3, 10, 50, 5 };
        public int totalCost, totalGained;


        public ShopMenuUIList(ItemList _itemList, Tent tent, MouseGameObject MGO, Wallet _wallet)
        {
            //Add all the instances of the Shop elements
            wallet = _wallet;
            Add(uIBox = new UIBox("UI/spr_target_bg"));
            Add(shopButtons = new ShopButtons());
            Add(shopItems = new ShopItems());
            Add(topLine = new TextGameObject("Fonts/JimFont"));
            Add(bottomLine = new TextGameObject("Fonts/JimFont"));
            Add(buyLine = new TextGameObject("Fonts/JimFont"));
            Add(sellLine = new TextGameObject("Fonts/JimFont"));
            Add(cancelLine = new TextGameObject("Fonts/JimFont"));
            mouseGO = MGO;
            itemList = _itemList;

            ResetShop();
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (shopActive)     //Set button inputs when the Welcome page is active
            {
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[6].collidesWithMouse(inputHelper.MousePosition) && shopButtons.shopButtons[6].Visible) { InitBuyPage(); }       //Open the Buy page
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[7].collidesWithMouse(inputHelper.MousePosition) && shopButtons.shopButtons[7].Visible) { InitSellPage(); }      //Open the Sell page
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[1].collidesWithMouse(inputHelper.MousePosition) && shopButtons.shopButtons[1].Visible) { ResetShop(); shopActive = false; }     //Close the Shop
            }
            if (buyActive)      //Set button inputs when the Buy page is active
            {
                foreach (Item item in shopItems.Children) { if (inputHelper.MouseLeftButtonPressed() && mouseGO.CollidesWith(item) && item.Visible) { selectedShopItem = item; selectedShopItem.selectedItem = true; InitConfirmBuy(); } }          //Open the Confirm Buy page when clicking on an item
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[1].collidesWithMouse(inputHelper.MousePosition) && shopButtons.shopButtons[1].Visible) { bottomLine.Text = shopDialogueLines[1]; InitShopWelcomePage(); }       //Closes the Buy page and goes back to the Welcome page
            }
            if (sellActive)     //Set button inputs when the Sell page is active
            {
                foreach (Item item in shopItems.Children) { if (inputHelper.MouseLeftButtonPressed() && mouseGO.CollidesWith(item) && item.Visible) { selectedShopItem = item; selectedShopItem.selectedItem = true; InitConfirmSell(); } }         //Open the Confirm sellf page when clicking on an item
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[1].collidesWithMouse(inputHelper.MousePosition) && shopButtons.shopButtons[1].Visible) { bottomLine.Text = shopDialogueLines[1]; InitShopWelcomePage(); }       //Closes the Sell page and goes back to the Welcome page
            }
            if (buyAmount) //Set button inputs when the Confirm Buy page is active
            {
                //Pairs an item amount with each add/reduce button
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
                    foreach (Item x in itemList.Children)                           //Add item amount on the hotbar
                    {
                        if (x.GetType() == selectedShopItem.GetType())              //Checks if the selected item is the same as an item in the itemlist, which are the items in the HUD
                        {
                            for (int i = 0; i < reduceMoney.Length; i++)        
                            {
                                if (shopItems.Children[i].GetType() == selectedShopItem.GetType())      //Checks if the selected item is the same as an item in the shopItemList, which are the items for the shop
                                {
                                    totalCost = reduceMoney[i] * shopItemAmount;          //Calculates how much money the player needs to pay
                                    if (totalCost <= wallet.Money)
                                    {
                                        x.itemAmount += shopItemAmount;
                                        wallet.AddMoney(-totalCost);                     //Reduces money for  specifed item here
                                    }
                                }
                            }
                            selectedShopItem.selectedItem = false;                      //Reset the selected item 
                            InitBuyPage();
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
            if (sellAmount)         //Set button inputs when the Confirm Sell page is active
            {
                //Make item amount, buy and sell buttons visible
                for (int i = 2; i < 6; i++)
                {
                    if (shopButtons.shopButtons[i].collidesWithMouse(inputHelper.MousePosition) && inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[i].Visible)
                    {
                        foreach (Item x in itemList.Children)
                        {
                            if (x.GetType() == selectedShopItem.GetType())      //Checks if the selected item is the same as an item in the itemList
                            {
                                if (x.itemAmount > 0)
                                {
                                    shopItemAmount += itemAmount[i];            //Adds the item amount 
                                }
                            }
                        }
                    }
                }
                //Inputs for the confirm button
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[0].collidesWithMouse(inputHelper.MousePosition) && shopButtons.shopButtons[0].Visible)
                {
                    //Add item amount on the hotbar
                    foreach (Item x in itemList.Children)
                    {
                        if (x.GetType() == selectedShopItem.GetType())      //Checks if an item is the same as the selected item
                        {
                            if (shopItemAmount <= x.itemAmount)             //Prevents the sold item amount to exceed the item amount the player has
                            {
                                x.itemAmount -= shopItemAmount;
                                GainMoney();                                //Get money from selling item
                            }
                            selectedShopItem.selectedItem = false;
                            InitSellPage();
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
        /// <summary>
        /// Resets all the shop elements 
        /// </summary>
        public void ResetShop() 
        {
            //Set the text lines for the shop
            topLine.Text = shopDialogueLines[1];
            bottomLine.Text = shopDialogueLines[0];
            buyLine.Text = shopDialogueLines[6];
            sellLine.Text = shopDialogueLines[7];
            cancelLine.Text = shopDialogueLines[8];

            //Set the position of Shop Design elements
            uIBox.Position = new Vector2(GameEnvironment.Screen.X * .5f - uIBox.Sprite.Width / 2, GameEnvironment.Screen.Y / 2 - uIBox.Sprite.Height / 2);
            shopButtons.shopButtons[1].Position = new Vector2(GameEnvironment.Screen.X * 2 / 3 - shopButtons.shopButtons[1].Sprite.Width / 2, GameEnvironment.Screen.Y * .6f);

            //Set the positions of the UI textlines
            topLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - topLine.Size.X * .5f, GameEnvironment.Screen.Y * .2f);
            bottomLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - bottomLine.Size.X * .5f, GameEnvironment.Screen.Y * .4f);
            sellLine.Position = new Vector2(GameEnvironment.Screen.X * 1 / 2 - sellLine.Size.X / 2, GameEnvironment.Screen.Y * .6f);
            cancelLine.Position = new Vector2(GameEnvironment.Screen.X * 3 / 5 - cancelLine.Size.X / 2, GameEnvironment.Screen.Y * .6f);
            buyLine.Position = new Vector2(GameEnvironment.Screen.X * 2 / 5 - buyLine.Size.X / 2, GameEnvironment.Screen.Y * .6f);

            //Make the UI lines invisible
            uIBox.Visible = false;
            topLine.Visible = false;
            bottomLine.Visible = false;
            buyLine.Visible = false;
            cancelLine.Visible = false;
            sellLine.Visible = false;

            //Make the Shop Items invisible
            foreach (Item x in shopItems.Children)
            {
                x.Visible = false;
            }

            //Resets the shop buttons
            for (int i = 0; i < shopButtons.shopButtons.Length; i++)
            {
                shopButtons.shopButtons[i].Visible = false;
            }

            //Set positions of the 3 main shop buttons
            shopButtons.shopButtons[6].Position = new Vector2(GameEnvironment.Screen.X * 2 / 5 - shopButtons.shopButtons[6].Sprite.Width / 2, GameEnvironment.Screen.Y * .5f);      //Set the position of the Buy button
            shopButtons.shopButtons[7].Position = new Vector2(GameEnvironment.Screen.X * 1 / 2 - shopButtons.shopButtons[7].Sprite.Width / 2, GameEnvironment.Screen.Y * .5f);      //Set the position of the Sell button
            shopButtons.shopButtons[1].Position = new Vector2(GameEnvironment.Screen.X * 3 / 5 - shopButtons.shopButtons[1].Sprite.Width / 2, GameEnvironment.Screen.Y * .5f);      //Set the position of the Cancel button

            //Set positions of the itam amount buttons
            shopButtons.shopButtons[2].Position = new Vector2(GameEnvironment.Screen.X * 4 / 7 - shopButtons.shopButtons[2].Sprite.Width / 2, GameEnvironment.Screen.Y * 2 / 3 - shopButtons.shopButtons[2].Sprite.Height / 2);      //Add item button position
            shopButtons.shopButtons[3].Position = new Vector2(GameEnvironment.Screen.X * 3 / 7 - shopButtons.shopButtons[3].Sprite.Width / 2, GameEnvironment.Screen.Y * 2 / 3 - shopButtons.shopButtons[3].Sprite.Height / 2);      //Reduce item button position
            shopButtons.shopButtons[4].Position = new Vector2(GameEnvironment.Screen.X * .5f - shopButtons.shopButtons[4].Sprite.Width / 2, GameEnvironment.Screen.Y * 4 / 7);    //Add 10 itams button position
            shopButtons.shopButtons[5].Position = new Vector2(GameEnvironment.Screen.X * .5f - shopButtons.shopButtons[5].Sprite.Width / 2, GameEnvironment.Screen.Y * 5 / 7);    //Reduce 10 items position

            //Resets the Shop UI bools
            shopActive = false;
            buyActive = false;
            sellActive = false;
            buyAmount = false;
            sellAmount = false;

            //Reset shopAmount int
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
        
        ///<Summary>
        /// This script opens the Welcome page of the shop and simultaniously deactivates the other pages
        /// From here the player can navigate to the other pages of the shop UI
        /// </Summary>
        public void InitShopWelcomePage()
        {   
            //Set the Welcome page bool on true
            shopActive = true;

            //Set the visibility of the Welcome page elements of the shop on true
            uIBox.Visible = true;
            topLine.Visible = true;
            bottomLine.Visible = true;
            buyLine.Visible = true;
            cancelLine.Visible = true;
            sellLine.Visible = true;
            
            shopButtons.shopButtons[6].Visible = true;      //Set the visibility of the Buy button
            shopButtons.shopButtons[7].Visible = true;      //Set the visibility of the Sell button
            shopButtons.shopButtons[1].Visible = true;      //Set the visibility of the Cancel button

            shopButtons.shopButtons[6].Position = new Vector2(GameEnvironment.Screen.X * 2 / 5 - shopButtons.shopButtons[6].Sprite.Width / 2, GameEnvironment.Screen.Y * .5f);      //Set the visibility of the Buy button
            shopButtons.shopButtons[7].Position = new Vector2(GameEnvironment.Screen.X * 1 / 2 - shopButtons.shopButtons[7].Sprite.Width / 2, GameEnvironment.Screen.Y * .5f);      //Set the visibility of the Sell button
            shopButtons.shopButtons[1].Position = new Vector2(GameEnvironment.Screen.X * 3 / 5 - shopButtons.shopButtons[1].Sprite.Width / 2, GameEnvironment.Screen.Y * .5f);      //Set the visibility of the Cancel button

            //Set the UI lines text and positions for the welcome page
            topLine.Text = shopDialogueLines[1];
            bottomLine.Text = shopDialogueLines[0];
            bottomLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - bottomLine.Size.X * .5f, GameEnvironment.Screen.Y * .4f);
            topLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - topLine.Size.X * .5f, GameEnvironment.Screen.Y * .2f);

            //Deactivate the bools from other pages
            sellActive = false;
            buyActive = false;
            buyAmount = false;
            sellAmount = false;

            foreach (Item x in shopItems.Children) { x.Visible = false; }       //Make all the Shop items invisible
        }

        ///<Summary>
        /// This script opens the Buy page of the shop and simultaniously deactivates the other pages
        /// From here the player can buy items or go back to the Welcome page
        /// </Summary>
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
            shopItemAmount = 0;

            //Turn on the buy page elements of the shop true
            bottomLine.Visible = false;
            topLine.Text = shopDialogueLines[2];
            bottomLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - bottomLine.Size.X * .5f, GameEnvironment.Screen.Y * .4f);
            shopButtons.shopButtons[1].Position = new Vector2(GameEnvironment.Screen.X * 1 / 2 - shopButtons.shopButtons[1].Sprite.Width / 2, GameEnvironment.Screen.Y * .7f);

            //Draw the items in a row
            for (int i = 0; i < shopItems.Children.Count / 2; i++)
            {
                shopItems.Children[i].Visible = true;
                shopItems.Children[i].Position = new Vector2(GameEnvironment.Screen.X / 3 + offset / 2 + (offset * i), GameEnvironment.Screen.Y * 2 / 5);
            }
            for (int i = shopItems.Children.Count / 2; i < shopItems.Children.Count; i++)
            {
                shopItems.Children[i].Visible = true;
                shopItems.Children[i].Position = new Vector2(offset * .75f + (offset * i), GameEnvironment.Screen.Y * 3 / 5);
            }

            //Make item amount, buy and sell buttons invisible
            for (int i = 2; i < 6; i++)
            {
                shopButtons.shopButtons[i].Visible = false;
                shopButtons.shopButtons[0].Visible = false;
            }
        }

        ///<Summary>
        /// This script opens the Confirm Buy page of the shop and simultaniously deactivates the other pages
        /// From here the player confirms their purchase or go back to the buy page
        /// </Summary>
        public void InitConfirmBuy()
        {
            //Turn off elements from Buy page
            buyAmount = true;
            buyActive = false;

            //Settings for the Shop UI textlines
            bottomLine.Visible = true;
            buyLine.Visible = false;
            cancelLine.Visible = false;
            sellLine.Visible = false;
            topLine.Text = shopDialogueLines[4];        //Change the question line 
            bottomLine.Position = new Vector2(GameEnvironment.Screen.X * .5f, GameEnvironment.Screen.Y * .4f);
            
            //Make all the shop items invisible
            foreach (Item x in shopItems.Children) { x.Visible = false; }

            //Make the Selected item visible
            selectedShopItem.Visible = true;
            if (selectedShopItem.selectedItem)         //Change the position of the selected item
            {
                selectedShopItem.Position = new Vector2(GameEnvironment.Screen.X / 2 - selectedShopItem.Sprite.Width / 2, GameEnvironment.Screen.Y / 2 - selectedShopItem.Sprite.Height / 2);
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

        ///<Summary>
        /// This script opens the Sell page of the shop and simultaniously deactivates the other pages
        /// From here the player can sell items or go back to the Welcome page
        /// </Summary>
        public void InitSellPage()
        {
            //Turn off elements from Welcome page
            sellActive = true;
            shopActive = false;
            buyLine.Visible = false;
            sellLine.Visible = false;
            cancelLine.Visible = false;
            shopItemAmount = 0;
            shopButtons.shopButtons[6].Visible = false;
            shopButtons.shopButtons[7].Visible = false;
            bottomLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - bottomLine.Size.X * .5f, GameEnvironment.Screen.Y * .4f);

            //Turn on the buy page elements of the shop true
            bottomLine.Visible = false;

            topLine.Text = shopDialogueLines[3];
            shopButtons.shopButtons[1].Position = new Vector2(GameEnvironment.Screen.X * 1 / 2 - shopButtons.shopButtons[1].Sprite.Width / 2, GameEnvironment.Screen.Y * .7f);

            //Draw Items in a row
            for (int i = 0; i < shopItems.Children.Count / 2; i++)
            {
                shopItems.Children[i].Visible = true;
                shopItems.Children[i].Position = new Vector2(GameEnvironment.Screen.X / 3 + offset / 2 + (offset * i), GameEnvironment.Screen.Y * 2 / 5);
            }
            for (int i = shopItems.Children.Count / 2; i < shopItems.Children.Count; i++)
            {
                shopItems.Children[i].Visible = true;
                shopItems.Children[i].Position = new Vector2(offset * .75f + (offset * i), GameEnvironment.Screen.Y * 3 / 5);
            }
            //Make item amount, buy and sell buttons invisible
            for (int i = 2; i < 6; i++)
            {
                shopButtons.shopButtons[i].Visible = false;
                shopButtons.shopButtons[0].Visible = false;
            }
        }

        ///<Summary>
        /// This script opens the Confirm Sell page of the shop and simultaniously deactivates the other pages
        /// From here the player confirms sellng their items or go back to the sell page
        /// </Summary>
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
            bottomLine.Position = new Vector2(GameEnvironment.Screen.X * .5f, GameEnvironment.Screen.Y * .4f);

            foreach (Item x in shopItems.Children) { x.Visible = false; }
            selectedShopItem.Visible = true;

            if (selectedShopItem.selectedItem)         //Change the position of the selected item
            {
                selectedShopItem.Position = new Vector2(GameEnvironment.Screen.X / 2 - selectedShopItem.Sprite.Width / 2, GameEnvironment.Screen.Y / 2 - selectedShopItem.Sprite.Height / 2);
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

        /// <summary>
        /// This script controls how much money the player gains 
        /// </summary>
        public void GainMoney()
        {
            //Add money here for specifed item here
            for (int i = 0; i < reduceMoney.Length; i++)
            {
                if (shopItems.Children[i].GetType() == selectedShopItem.GetType())
                {
                    totalGained = addMoney[i] * shopItemAmount;
                    //Add money to the wallet
                    wallet.AddMoney(totalGained);
                }
            }
        }
        public bool IsActive
        {
            get { return shopActive || buyActive || buyAmount || sellActive || sellAmount; }
        }
    }
}

