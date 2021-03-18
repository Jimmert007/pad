using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HarvestValley
{
    class Player : SpriteGameObject
    {
        public bool openInventory;
        public bool openMap;
        SpriteSheet left, right, up, down;
        public Cell current;

        public Player(string _assetName, Vector2 _position, float _scale) : base(_assetName)
        {
            position = _position;
            scale = _scale;
        }

        override public void Reset()
        {
            base.Reset();
            position = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
        }
        public bool PlayerCanReach()
        {
            if (position.X + Width / 2 - Mouse.GetState().X <= 75
                & position.X + Width / 2 - Mouse.GetState().X >= -75
                & position.Y + Height / 2 - Mouse.GetState().Y <= 75
                & position.Y + Height / 2 - Mouse.GetState().Y >= -75)
            {
                return true;
            }
            return false;
        }

        override public void Update(GameTime gameTime)
        {
           /* if (current != null)
            {
                Debug.WriteLine(gameTime.TotalGameTime + " " + current.cellID);
            }*/

            //Continuesly set movement to 0
            velocity.X = 0;
            velocity.Y = 0;

            //Movement inputs
            if (Keyboard.GetState().IsKeyDown(Keys.A)) { velocity.X = -10.0f; }
            else if (Keyboard.GetState().IsKeyDown(Keys.D)) { velocity.X = 10.0f; }
            if (Keyboard.GetState().IsKeyDown(Keys.W)) { velocity.Y = -10.0f; }
            else if (Keyboard.GetState().IsKeyDown(Keys.S)) { velocity.Y = 10.0f; }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)) { velocity *= 2; }

            //Control movement
            position.X += velocity.X;
            position.Y += velocity.Y;
        }
    }
}

