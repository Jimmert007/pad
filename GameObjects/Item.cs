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
        public int itemAmount = 0;
        public bool isStackable;
        public Item(string _assetName, bool stackable, int startItemAmount, float scale) : base(_assetName)
        {
            isStackable = stackable;
            itemAmount = startItemAmount;
            this.scale = scale;
        }
    }
}
