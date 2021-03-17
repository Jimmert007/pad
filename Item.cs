using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace BaseProject
{
    class Item : GameObject
    {
        public List<Item> items = new List<Item>();
        public string itemSelected = "HOE";
        public int itemAmount = 0;
        public bool isStackable;
        public Texture2D hotbar;
        public Item(string _assetName, bool stackable, int startItemAmount) : base(_assetName)
        {
            isStackable = stackable;
            itemAmount = startItemAmount;
            hotbar = GameEnvironment.ContentManager.Load<Texture2D>("spr_hotbar");
        }
    }
}
