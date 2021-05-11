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
        public bool shopActive, buyActive, buyAmount, sellActive, sellAmount, toolsTabActive, materialsTabActive = false;
        private int shopItemAmount = 0;
        public string[] shopDialogueLines = { "Welcome to the shop", "What do you want to do?", "What do you want to buy?", "What do you want to sell?", "How many do you want to buy?", "How many do you want to sell?", "Buy", "Sell", "Cancel" };
        public int[] shopItemAmmount = { 0, 0, 1, -1, 10, -10, 0, 0 };

        public ShopMenuUIList()
        {
            Add(uIBox = new UIBox("ui_bar"));
            uIBox.Position = new Vector2(GameEnvironment.Screen.X * .5f, GameEnvironment.Screen.Y * .35f);
            Add(shopButtons = new ShopButtons());
            Add(welcomeLine = new TextGameObject("JimFont"));
            welcomeLine.Text = shopDialogueLines[0];
            Add(questionLine = new TextGameObject("JimFont"));
            questionLine.Text = shopDialogueLines[1];
            Add(buyLine = new TextGameObject("JimFont"));
            buyLine.Text = shopDialogueLines[4];
            Add(sellLine = new TextGameObject("JimFont"));
            sellLine.Text = shopDialogueLines[6];
            Add(cancelLine = new TextGameObject("JimFont"));
            cancelLine.Text = shopDialogueLines[8];

            welcomeLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - welcomeLine.Size.X * .5f, GameEnvironment.Screen.Y * .1f);
            questionLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - welcomeLine.Size.X * .5f, GameEnvironment.Screen.Y * .2f);
            buyLine.Position = new Vector2(GameEnvironment.Screen.X * .3f - welcomeLine.Size.X * .5f, GameEnvironment.Screen.Y * .7f);
            sellLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - welcomeLine.Size.X * .5f, GameEnvironment.Screen.Y * .7f);
            cancelLine.Position = new Vector2(GameEnvironment.Screen.X * .6f - welcomeLine.Size.X * .5f, GameEnvironment.Screen.Y * .7f);

            Add(mouseGO = new SpriteGameObject("1px"));
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
                foreach (Item x in shopItems.Children) { if (inputHelper.MouseLeftButtonPressed() && x.CollidesWith(mouseGO)) { x.selectedItem = true; buyAmount = true; } }
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[1].collidesWithMouse(inputHelper.MousePosition)) { buyActive = false; shopActive = true; }
            }
            if (sellActive)
            {
                //prop een selectedItem bool in iedere item, foreach x.sleecteditem = true;
                foreach (Item x in shopItems.Children) { if (inputHelper.MouseLeftButtonPressed() && x.CollidesWith(mouseGO)) { x.selectedItem = true; sellAmount = true; } }
                if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[1].collidesWithMouse(inputHelper.MousePosition)) { shopActive = true; sellActive = false; }
            }
            if (buyAmount)
            {
                foreach (Item x in shopItems.Children)
                {
                    if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[6].collidesWithMouse(inputHelper.MousePosition) && x.selectedItem) { x.itemAmount += shopItemAmount; }
                    if (inputHelper.MouseLeftButtonPressed() && shopButtons.shopButtons[1].collidesWithMouse(inputHelper.MousePosition)) { buyAmount = false; buyActive = true; }
                }
            }
            if (sellAmount)
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
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (shopActive) { uIBox.Visible = true; welcomeLine.Visible = true; questionLine.Visible = true; buyLine.Visible = true; cancelLine.Visible = true; sellLine.Visible = true; shopButtons.shopButtons[6].Visible = true; shopButtons.shopButtons[7].Visible = true; shopButtons.shopButtons[1].Visible = true; }
            else { welcomeLine.Visible = false; questionLine.Visible = false; buyLine.Visible = false; sellLine.Visible = false; cancelLine.Visible = false; uIBox.Visible = false; shopButtons.shopButtons[6].Visible = false; shopButtons.shopButtons[7].Visible = false; shopButtons.shopButtons[1].Visible = false; }
            if (buyActive)
            {
                questionLine.Text = shopDialogueLines[2];
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
            else { shopButtons.shopButtons[0].Visible = false; shopButtons.shopButtons[1].Visible = false; questionLine.Text = shopDialogueLines[3]; }
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

