using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HarvestValley.GameObjects
{

    /// <summary>
    /// Luke Sikma, Jim van de Burgwal: 
    /// Class for getting every item and putting the tools you start with in the hotbar 
    /// and checks the items u can have multiple of if you have it and how many
    /// </summary>
     

    class Hotbar : GameObjectList
    {
        public SpriteGameObject selectedSquare, hotbarSquare;   // declare the selescted square and the induvidual hotbarsquare
        public GameObjectList hotbarSquares;                    // declare the squares
        public GameObjectList itemAmountText;                   // declare the item amount
        public ItemList itemList;                               // declare the List of items out of the Class ItemList
        public int slots = 10;                                  // declare the amount of hotbar slots
        public Hotbar(ItemList itemList)
        {
            //Adding the hotbar squares, giving it a Sprite and giving them a position
            hotbarSquares = new GameObjectList();
            hotbarSquare = new SpriteGameObject("UI/HotbarSquare");
            for (int i = 0; i < slots; i++)
            {
                hotbarSquares.Add(new SpriteGameObject("UI/HotbarSquare"));
                hotbarSquares.Children[i].Position = new Vector2(GameEnvironment.Screen.X / 2 - slots * hotbarSquare.Sprite.Width / 2 + hotbarSquare.Sprite.Width * i, GameEnvironment.Screen.Y - hotbarSquare.Sprite.Height);
            }
            Add(hotbarSquares);

            // Adding and loading the selected square, giving it a Sprite and giving it the first hotbar item als sleected
            selectedSquare = new SpriteGameObject("UI/HotbarSquareSelected");
            Add(selectedSquare);
            selectedSquare.Position = hotbarSquares.Children[0].Position;

            //Adding the items and giving them a position
            this.itemList = itemList;
            Add(this.itemList);
            for (int i = 0; i < slots; i++)
            {
                this.itemList.Children[i].Position = hotbarSquares.Children[i].Position;
            }

            //Adding the item amount, setting a font, giving it a position and a color
            itemAmountText = new GameObjectList();
            Add(itemAmountText);
            for (int i = 0; i < slots; i++)
            {
                itemAmountText.Add(new TextGameObject("Fonts/JimFont"));
                itemAmountText.Children[i].Position = hotbarSquares.Children[i].Position + new Vector2(4, 4);
                (itemAmountText.Children[i] as TextGameObject).Color = Color.Black;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            // Looping through the items to see if there are items and how many there are and if there are non then don't display the item
            for (int i = 0; i < slots; i++)
            {
                (itemAmountText.Children[i] as TextGameObject).Text = (itemList.Children[i] as Item).itemAmount.ToString();

                if ((itemList.Children[i] as Item).itemAmount > 0)
                {
                    itemAmountText.Children[i].Visible = true;
                }
                else if ((itemList.Children[i] as Item).itemAmount == 0)
                {
                    itemAmountText.Children[i].Visible = false;
                }
                
                if ((itemList.Children[i] as Item).itemAmount > 0)
                {
                    itemList.Children[i].Visible = true;
                }
                else if ((itemList.Children[i] as Item).itemAmount == 0 && (itemList.Children[i] as Item).isStackable)
                {
                    itemList.Children[i].Visible = false;
                }
            }
        }
    }
}