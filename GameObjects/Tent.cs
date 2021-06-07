using Microsoft.Xna.Framework;

namespace HarvestValley.GameObjects
{
    /// <summary>
    /// Jim van de Burgwal
    /// This class contains all the SpriteGameObjects for the tent and has 2 collision functions for different purposes
    /// </summary>
    class Tent : GameObjectList
    {
        public SpriteGameObject tent, tentCollision, sleepCollision;
        public Tent()
        {
            Add(tentCollision = new SpriteGameObject("UI/TentCollision"));  //Adds tent collision, so you can walk behind the tent to give it a 3D effect
            Add(sleepCollision = new SpriteGameObject("Environment/SleepCollision")); //Adds sleep collision, so you can walk into the door to sleep
            Add(tent = new SpriteGameObject("Environment/Tent")); //Adds the tent SpriteGameObject
            //Sets right position and origin for the SpriteGameObjects
            tent.Position = new Vector2(128, 128);
            tent.Origin = new Vector2(0, 55);
            sleepCollision.Position = new Vector2(tent.Position.X + 39, tent.Position.Y + 60);
            sleepCollision.PerPixelCollisionDetection = false;
            tentCollision.Position = new Vector2(tent.Position.X, tent.Position.Y + 25);
            tentCollision.PerPixelCollisionDetection = false;
        }

        /// <summary>
        /// This bool checks collision with the tent itself
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CollidesWith(SpriteGameObject obj)
        {
            return ((children[0] as SpriteGameObject).CollidesWith(obj));
        }

        /// <summary>
        /// This bool checks collision with the door, so the player can sleep
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CollidesWithSleep(SpriteGameObject obj)
        {
            return ((children[1] as SpriteGameObject).CollidesWith(obj));
        }
    }
}
