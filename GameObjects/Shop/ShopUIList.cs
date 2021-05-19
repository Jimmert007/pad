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
        TextGameObject welcomeLine, questionLine, buyLine, sellLine, cancelLine;
        UIBox uIBox;
        SpriteGameObject Coin;
        ShopButtons shopButtons;
        public bool shopActive, buyActive, buyAmount, sellActive, sellAmount = false;
        private int shopItemAmount = 0, offset = 128;
        public string[] shopDialogueLines = { "Welcome to the shop", "What do you want to do?", "What do you want to buy?", "What do you want to sell?", "How many do you want to buy?", "How many do you want to sell?", "Buy", "Sell", "Cancel" };
        public int[] shopItemAmmount = { 0, 0, 1, -1, 10, -10, 0, 0 };

        public ShopMenuUIList()
        {
            //Add all the instances of the Shop elements
            Add(uIBox = new UIBox("ui_bar"));
            Add(shopButtons = new ShopButtons());
            Add(shopItems = new ShopItems());
            Add(welcomeLine = new TextGameObject("JimFont"));
            Add(questionLine = new TextGameObject("JimFont"));
            Add(buyLine = new TextGameObject("JimFont"));
            Add(sellLine = new TextGameObject("JimFont"));
            Add(cancelLine = new TextGameObject("JimFont"));
            Add(mouseGO = new SpriteGameObject("1px"));

            ResetShop();
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

            if (inputHelper.KeyPressed(Keys.B)) { ResetShop(); }

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

        public void ResetShop()  //Resets all the shop elements
        {
            //Set the text lines for the shop
            welcomeLine.Text = shopDialogueLines[0];
            questionLine.Text = shopDialogueLines[1];
            buyLine.Text = shopDialogueLines[6];
            sellLine.Text = shopDialogueLines[7];
            cancelLine.Text = shopDialogueLines[8];

            //Set the position of Shop elements
            uIBox.Position = new Vector2(GameEnvironment.Screen.X * .5f- uIBox.Sprite.Width *.5f, GameEnvironment.Screen.Y * .35f);
            welcomeLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - welcomeLine.Size.X * .5f, GameEnvironment.Screen.Y * .1f);
            questionLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - questionLine.Size.X * .5f, GameEnvironment.Screen.Y * .2f);

            buyLine.Position = new Vector2(GameEnvironment.Screen.X * 1 / 3 - buyLine.Size.X * .5f, GameEnvironment.Screen.Y * .7f);
            sellLine.Position = new Vector2(GameEnvironment.Screen.X * .5f - sellLine.Size.X * .5f, GameEnvironment.Screen.Y * .7f);
            cancelLine.Position = new Vector2(GameEnvironment.Screen.X * 2 / 3 - cancelLine.Size.X * .5f, GameEnvironment.Screen.Y * .7f);

            shopButtons.shopButtons[1].Position = new Vector2(GameEnvironment.Screen.X * 2 / 3 - shopButtons.shopButtons[1].Sprite.Width / 2, GameEnvironment.Screen.Y * .6f);

            //Make Shop Items invisible
            foreach (Item x in shopItems.Children)
            {
                x.Visible = false;
            }
            //Set the ui lines invisible
            uIBox.Visible = false;
            welcomeLine.Visible = false;
            questionLine.Visible = false;
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
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Debug.WriteLine(buyActive);

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
                shopButtons.shopButtons[1].Position = new Vector2(GameEnvironment.Screen.X * 1 / 2 - shopButtons.shopButtons[1].Sprite.Width / 2, GameEnvironment.Screen.Y * .6f);

                //Draw the items in a row
                for (int i = 0; i < shopItems.Children.Count; i++)
                {
                    Item item = (shopItems.Children[i] as Item);

                    shopItems.Children[i].Visible = true;
                    shopItems.Children[i].Position = new Vector2(GameEnvironment.Screen.X / 4 + (offset * i), GameEnvironment.Screen.Y / 2);
                }
            }
            if (sellActive)
            {
                questionLine.Text = shopDialogueLines[3];
                shopButtons.shopButtons[1].Position = new Vector2(GameEnvironment.Screen.X * 1 / 2 - shopButtons.shopButtons[1].Sprite.Width / 2, GameEnvironment.Screen.Y * .6f);
                for (int i = 0; i < shopItems.Children.Count; i++)
                {
                    Item item = (shopItems.Children[i] as Item);

                    shopItems.Children[i].Visible = true;
                    shopItems.Children[i].Position = new Vector2(GameEnvironment.Screen.X / 4 + (offset * i), GameEnvironment.Screen.Y / 2);
                }
            }
            if (buyAmount)
            {
                buyActive = false;                                 //Turn of the buying page
                questionLine.Text = shopDialogueLines[4];        //Change the question line
                
                foreach (Item x in shopItems.Children)
                {   
                    if (!x.selectedItem)        //Turn the non-selected items invisible
                    {
                        x.Visible = false;
                    }
                    if (x.selectedItem)         //Change the position of the selected item
                    {
                        x.Position = new Vector2(GameEnvironment.Screen.X / 2 - x.Sprite.Width/2, GameEnvironment.Screen.Y / 3);
                    }
                }

                //Make item amount, buy and sell buttons visible
                for (int i = 2; i < 8; i++)
                {
                    shopButtons.shopButtons[i].Visible = true;
                }
                
                //Set the positions of the item amount, buy and sell buttons
                shopButtons.shopButtons[6].Position = new Vector2(GameEnvironment.Screen.X * 2 / 3 - shopButtons.shopButtons[6].Sprite.Width/2, GameEnvironment.Screen.Y*2/3 );
                shopButtons.shopButtons[7].Position = new Vector2(GameEnvironment.Screen.X / 3- shopButtons.shopButtons[7].Sprite.Width/2, GameEnvironment.Screen.Y* 2/ 3);
            }
            //if (sellAmount) { questionLine.Text = shopDialogueLines[5]; }
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

