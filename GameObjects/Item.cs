using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace HarvestValley.GameObjects
{
    class Item : SpriteGameObject
    {
      /*  public List<Item> items = new List<Item>();*/
        public int itemAmount = 0;
        public bool isStackable;
        public bool selectedItem = false;
        public SpriteSheet hotbar;
        public Item(string _assetName, bool stackable, int startItemAmount) : base(_assetName)
        {
            isStackable = stackable;
            itemAmount = startItemAmount;
            hotbar = new SpriteSheet("spr_hotbar");
        }
    }
}
