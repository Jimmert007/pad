using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HarvestValley.GameObjects
{
    class Hotbar : GameObjectList
    {
        public SpriteGameObject selectedSquare, hotbarSquare;
        public GameObjectList hotbarSquares;
        public GameObjectList itemAmountText;
        public ItemList itemList;
        public int slots = 10;
        public Hotbar(ItemList itemList)
        {

            hotbarSquares = new GameObjectList();
            hotbarSquare = new SpriteGameObject("UI/HotbarSquare");
            for (int i = 0; i < slots; i++)
            {
                hotbarSquares.Add(new SpriteGameObject("UI/HotbarSquare"));
                hotbarSquares.Children[i].Position = new Vector2(GameEnvironment.Screen.X / 2 - slots * hotbarSquare.Sprite.Width / 2 + hotbarSquare.Sprite.Width * i, GameEnvironment.Screen.Y - hotbarSquare.Sprite.Height);
            }
            Add(hotbarSquares);

            selectedSquare = new SpriteGameObject("UI/HotbarSquareSelected");
            Add(selectedSquare);
            selectedSquare.Position = hotbarSquares.Children[0].Position;

            this.itemList = itemList;
            Add(this.itemList);
            for (int i = 0; i < slots; i++)
            {
                this.itemList.Children[i].Position = hotbarSquares.Children[i].Position;
            }

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