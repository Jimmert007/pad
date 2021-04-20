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
            PerPixelCollisionDetection = false;
        }

        override public void Reset()
        {
            base.Reset();
            position = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
        }
        public bool PlayerCanReach()
        {
            if (position.X + sprite.Width / 2 - Mouse.GetState().X <= 10
                & position.X + sprite.Width / 2 - Mouse.GetState().X >= -10
                & position.Y + sprite.Height / 2 - Mouse.GetState().Y <= 10
                & position.Y + sprite.Height / 2 - Mouse.GetState().Y >= -10)
            {
                return true;
            }
            return false;
        }

        /*        public override void HandleInput(InputHelper inputHelper)
                {
                    SpriteGameObject MouseGO = new SpriteGameObject();
                    MouseGO.Position = Mouse.GetState().Position;
                    if(CollidesWith(new Mouse.GetState().Position)
                    Debug.WriteLine()
                    base.HandleInput(inputHelper);
                }*/

        override public void Update(GameTime gameTime)
        {
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

          /*  if (position.Y - sprite.Height < 0)
            {
                position.Y = 0;
            }
            if (position.Y > GameEnvironment.Screen.Y)
            {
                position.Y = GameEnvironment.Screen.Y;
            }
            if (position.X < 0)
            {
                position.X = 0;
            }
            if (position.X + sprite.Width > GameEnvironment.Screen.X)
            {
                position.X = GameEnvironment.Screen.X - sprite.Width;
            }*/
        }
    }
}

