using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace HarvestValley.GameObjects
{
    /// <summary>
    /// Jim van de Burgwal
    /// This class is used to make adding items easier
    /// </summary>
    class Item : SpriteGameObject
    {
        public int itemAmount = 0;
        public bool isStackable;
        public bool selectedItem = false;
        public Item(string _assetName, bool stackable, int startItemAmount, float _scale) : base(_assetName)
        {
            isStackable = stackable;
            itemAmount = startItemAmount;
            scale = _scale;
        }
    }
}
