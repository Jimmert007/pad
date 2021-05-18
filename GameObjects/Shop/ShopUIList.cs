using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using HarvestValley.GameObjects.Shop;
using HarvestValley.GameObjects;
using HarvestValley.GameObjects.UI;
using HarvestValley.GameObjects.Tools;

namespace HarvestValley.GameObjects.Shop
{
    class ShopMenuUIList : GameObjectList
    {
        SpriteGameObject mouseGO;
        ItemList shopItems;
        //Button confirm, cancel, addItem, reduceItem, add10Items, reduce10Items, buy, sell;
        TextGameObject welcomeLine, questionLine, buyLine, sellLine, cancelLine;
        UIBox uIBox;
        SpriteGameObject Coin;
        ShopButtons shopButtons;
        public bool shopActive, buyActive, buyAmount, sellActive, sellAmount= false;
        private int shopItemAmount = 0;
        public string[] shopDialogueLines = { "Welcome to the shop", "What do you want to do?", "What do you want to buy?", "What do you want to sell?", "How many do you want to buy?", "How many do you want to sell?", "Buy", "Sell", "Cancel" };
        public int[] shopItemAmmount = { 0, 0, 1, -1, 10, -10, 0, 0 };

        public ShopMenuUIList()
        {
            //Add all the instances of the Shop elements
            Add(uIBox = new UIBox("ui_bar"));
            Add(shopButtons = new ShopButtons());
            Add(shopItems = new ItemList());
            Add(welcomeLine = new TextGameObject("JimFont"));
            Add(questionLine = new TextGameObject("JimFont"));
            Add(buyLine = new TextGameObject("JimFont"));
            Add(sellLine = new TextGameObject("JimFont"));
            Add(cancelLine = new TextGameObject("JimFont"));
            Add(mouseGO = new SpriteGameObject("1px"));

            //Set the text lines for the unchangable dialogue lines
            welcomeLine.Text = shopDialogueLines[0];
            questionLine.Text = shopDialogueLines[1];
            buyLine.Text = shopDialogueLines[6];
            sellLine.Text = shopDialogueLines[7];
            cancelLine.Text = shopDialogueLines[8];

            //Set the position of Shop elements
            uIBox.Position = new Vector2(GameEnvironment.Screen.X * .5f, GameEnvironment.Screen.Y * .35f);
            welcomeLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - welcomeLine.Size.X * .5f, GameEnvironment.Screen.Y * .1f);
            questionLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - questionLine.Size.X * .5f, GameEnvironment.Screen.Y * .2f);

            buyLine.Position = new Vector2(GameEnvironment.Screen.X * 1 / 3 - buyLine.Size.X * .5f, GameEnvironment.Screen.Y * .7f);
            sellLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - sellLine.Size.X * .5f, GameEnvironment.Screen.Y * .7f);
            cancelLine.Position = new Vector2(GameEnvironment.Screen.X * 2 / 3 - cancelLine.Size.X * .5f, GameEnvironment.Screen.Y * .7f);

            //Set UI elements invisible at the start of the game 
            uIBox.Visible = false; welcomeLine.Visible = false; questionLine.Visible = false; buyLine.Visible = false; cancelLine.Visible = false; sellLine.Visible = false;
            for (int i = 0; i < shopButtons.Children.Count; i++)
            {
                shopButtons.shopButtons[i].Visible = false;
            }
            foreach (Item x in shopItems.Children)
            {
                x.Visible = false;
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            mouseGO.Position = inputHelper.MousePosition;

            //Activate UI bools
            if (inputHelper.KeyPressed(Keys.V))
            {
                shopActive = true;
            }

            if (shopActive)
            {
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[6].collidesWithMouse(inputHelper.MousePosition)) { buyActive = true; shopActive = false; }
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[7].collidesWithMouse(inputHelper.MousePosition)) { sellActive = true; shopActive = false; }
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[1].collidesWithMouse(inputHelper.MousePosition)) { shopActive = false; }
            }
            if (buyActive)
            {
                foreach (Item x in shopItems.Children) { if (inputHelper.MouseLeftButtonPressed() && x.CollidesWith(mouseGO) && x.isStackable) { x.selectedItem = true; buyAmount = true; } }
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[1].collidesWithMouse(inputHelper.MousePosition)) { buyActive = false; shopActive = true; }
            }
            //if (sellActive)
            {
                //prop een selectedItem bool in iedere item, foreach x.sleecteditem = true;
                foreach (Item x in shopItems.Children) { if (inputHelper.MouseLeftButtonPressed() && x.CollidesWith(mouseGO)) { x.selectedItem = true; sellAmount = true; } }
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[1].collidesWithMouse(inputHelper.MousePosition)) { shopActive = true; sellActive = false; }
            }
            //if (buyAmount)
            {
                foreach (Item x in shopItems.Children)
                {
                    if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[6].collidesWithMouse(inputHelper.MousePosition) && x.selectedItem) { x.itemAmount += shopItemAmount; }
                    if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[1].collidesWithMouse(inputHelper.MousePosition)) { buyAmount = false; buyActive = true; }
                }
            }
            //if (sellAmount)
            {
                foreach (Item x in shopItems.Children)
                {
                    if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[6].collidesWithMouse(inputHelper.MousePosition) && x.selectedItem) { x.itemAmount -= shopItemAmount; }
                    if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[1].collidesWithMouse(inputHelper.MousePosition)) { sellAmount = false; sellActive = true; }
                }
                for (int i = 0; i < shopButtons.shopButtons.Length; i++)
                {
                    if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[i].collidesWithMouse(inputHelper.MousePosition))
                    {
                        shopItemAmount += shopItemAmmount[i];
                    }
                }
            }
        }

        public void ResetShop()
        {
            //Resets all the shop elements
            uIBox.Visible = false;
            welcomeLine.Visible = false;
            questionLine.Visible = false;
            buyLine.Visible = false;
            cancelLine.Visible = false;
            sellLine.Visible = false;

            for (int i = 0; i < shopButtons.shopButtons.Length; i++)
            {
                shopButtons.shopButtons[i].Visible = false;
            }

            shopActive = false;
            buyActive = false;
            sellActive = false;
            buyAmount = false;
            sellAmount = false;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (shopActive)
            {   //Set the welcome page elements of the shop on true
                uIBox.Visible = true;
                welcomeLine.Visible = true;
                questionLine.Visible = true;
                buyLine.Visible = true;
                cancelLine.Visible = true;
                sellLine.Visible = true;
                shopButtons.shopButtons[6].Visible = true;
                shopButtons.shopButtons[7].Visible = true;
                shopButtons.shopButtons[1].Visible = true;
            }
            if (buyActive)
            {
                //Deactivate the welcome page elements
                welcomeLine.Visible = false;
                buyLine.Visible = false;
                cancelLine.Visible = false;
                sellLine.Visible = false;
                shopButtons.shopButtons[7].Visible = false;
                shopButtons.shopButtons[6].Visible = false;

                //Turn on the buy page elements of the shop true
                questionLine.Text = shopDialogueLines[2];
                shopButtons.shopButtons[1].Position = new Vector2(GameEnvironment.Screen.X* 1 / 2 - shopButtons.shopButtons[1].Sprite.Width / 2, GameEnvironment.Screen.Y * .6f);
                foreach (Item x in shopItems.Children)
                {
                    if (x.isStackable)
                    {
                        for (int i = 0; i > shopItems.Children.Count - 1; i++)
                        {
                            x.Position = new Vector2(GameEnvironment.Screen.X / shopItems.Children.Count * i, GameEnvironment.Screen.Y / 6);
                            x.Visible = true;
                        }
                    }
                    else { x.Visible = false; }
                }
            }
            if (sellActive)
            {
                questionLine.Text = shopDialogueLines[3];
                foreach (Item x in shopItems.Children)
                {
                    if (x.isStackable)
                    {
                        for (int i = 0; i < shopItems.Children.Count; i++)
                        {
                            x.Position = new Vector2(GameEnvironment.Screen.X / shopItems.Children.Count, GameEnvironment.Screen.Y / 6);
                            x.Visible = true;
                        }
                    }
                }
            }
            if (buyAmount)
            {
                //Question line
                questionLine.Text = shopDialogueLines[4];

                foreach (Item x in shopItems.Children)
                {
                    if (x.selectedItem)
                    {
                        x.Position = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
                    }
                }
                //Buy and sell Buttons
                shopButtons.shopButtons[6].Position = new Vector2(GameEnvironment.Screen.X / 3, GameEnvironment.Screen.Y * 2 / 3);
                shopButtons.shopButtons[7].Position = new Vector2(GameEnvironment.Screen.X / 3, GameEnvironment.Screen.Y / 3);
            }
            if (sellAmount) { questionLine.Text = shopDialogueLines[5]; }
        }

        public void BuyItem()
        {
            foreach (Item x in shopItems.Children)
            {

                if (x is Seed)
                {
                    x.itemAmount += GameEnvironment.Random.Next(2);
                }
                if (x is Wood)
                {
                    x.itemAmount += GameEnvironment.Random.Next(3, 7);
                }
                if (x is TreeSeed)
                {
                    x.itemAmount += GameEnvironment.Random.Next(2);
                }
                if (x is Rock)
                {
                    x.itemAmount += GameEnvironment.Random.Next(3, 7);
                }
                if (x is Sprinkler)
                {
                    x.itemAmount += GameEnvironment.Random.Next(2);
                }
            }
        }

        public void SellItem()
        {
            if (sellActive) { }
        }
    }
}

