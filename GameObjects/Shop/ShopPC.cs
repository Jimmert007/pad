using Microsoft.Xna.Framework;

namespace HarvestValley.GameObjects.Shop
{
    class ShopPC : GameObjectList
    {
        SpriteGameObject shopPC;
        public ShopPC(Tent _tent)
        {
            Add(shopPC = new SpriteGameObject("Environment/spr_shop"));                                                         //Add a PC object
            Position = _tent.Children[0].Position + new Vector2((_tent.Children[0] as SpriteGameObject).Sprite.Width, 0);       //Set the position of the PC next to the Tent
            shopPC.PerPixelCollisionDetection = false;
        }
        public SpriteGameObject Sprite                  //Get the Child as a SpriteGameObject
        {
            get { return (children[0] as SpriteGameObject); }
        }
        public bool CollidesWith(SpriteGameObject obj)      //Check collision with a Child as a SpriteGameObject 
        {
            return ((children[0] as SpriteGameObject).CollidesWith(obj));
        }
    }
}
