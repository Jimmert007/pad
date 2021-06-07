using Microsoft.Xna.Framework;

namespace HarvestValley.GameObjects.Shop
{
    /// <summary>
    /// Mohammad Al Hadiansyah Suwandhy, 500843466
    /// Class that places the board to function as a shop
    /// Also is able to detect collision and give its own sprite
    /// </summary>
    class ShopBoard : GameObjectList
    {
        SpriteGameObject shopBoard;
        public ShopBoard(Tent _tent)
        {
            Add(shopBoard = new SpriteGameObject("Environment/spr_shop"));                                                      //Add a Board object
            Position = _tent.Children[0].Position + new Vector2((_tent.Children[0] as SpriteGameObject).Sprite.Width, 0);       //Set the position of the Board next to the Tent
            shopBoard.PerPixelCollisionDetection = false;
        }
        public SpriteGameObject Sprite                      //Get the Child as a SpriteGameObject
        {
            get { return (children[0] as SpriteGameObject); }
        }
        public bool CollidesWith(SpriteGameObject obj)      //Check collision with a Child as a SpriteGameObject 
        {
            return ((children[0] as SpriteGameObject).CollidesWith(obj));
        }
    }
}
