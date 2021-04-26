using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley.GameObjects
{
    class CraftingMenu : GameObjectList
    {
        SpriteGameObject CraftingBackground;
        TextGameObject crafting, sprinklerCost;
        public CraftingMenu() : base()
        {
            CraftingBackground = new SpriteGameObject("EnergyBarBackground");
            //Add(CraftingBackground);
        }
    }
}
