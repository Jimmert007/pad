using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace HarvestValley.GameObjects
{
    class Tent : GameObjectList
    {
        public SpriteGameObject tent, tentCollision, sleepCollision;
        public Tent()
        {
            Add(tentCollision = new SpriteGameObject("TentCollision"));
            Add(sleepCollision = new SpriteGameObject("SleepCollision"));
            Add(tent = new SpriteGameObject("Tent"));
            tent.Position = new Vector2(128, 128);
            tent.Origin = new Vector2(0, 55);
            sleepCollision.Position = new Vector2(tent.Position.X + 39, tent.Position.Y + 60);
            sleepCollision.PerPixelCollisionDetection = false;
            tentCollision.Position = new Vector2(tent.Position.X, tent.Position.Y + 25);
            tentCollision.PerPixelCollisionDetection = false;
        }

        public bool CollidesWith(SpriteGameObject obj)
        {
            return ((children[0] as SpriteGameObject).CollidesWith(obj));
        }

        public bool CollidesWithSleep(SpriteGameObject obj)
        {
            return ((children[1] as SpriteGameObject).CollidesWith(obj));
        }
    }
}
