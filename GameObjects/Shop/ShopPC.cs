using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley.GameObjects.Shop
{
    class ShopPC : GameObjectList
    {
        SpriteGameObject shopPC;
        public ShopPC(Tent _tent)
        {
            Add(shopPC = new SpriteGameObject("UI/spr_selected_square"));
            Position = _tent.Children[0].Position + new Vector2((_tent.Children[0] as SpriteGameObject).Sprite.Width, 0);
            shopPC.PerPixelCollisionDetection = false;
        }

        public SpriteGameObject Sprite
        {
            get { return (children[0] as SpriteGameObject); }
        }

        public bool CollidesWith(SpriteGameObject obj)
        {
            return ((children[0] as SpriteGameObject).CollidesWith(obj));
        }
    }
}
