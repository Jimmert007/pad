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
        public bool sleeping, sleepingPosition;
        public Vector2 newSleepingPosition = new Vector2(0, 10);
        public Vector2 lastPosition;
        public int speed = 3;
        public SpriteGameObject playerReach;

        public Player(string _assetName, Vector2 _position, float _scale) : base(_assetName)
        {
            position = _position - new Vector2(32, 32);
            scale = _scale;
            PerPixelCollisionDetection = false;
            playerReach = new SpriteGameObject("spr_player_reach");
            playerReach.Position = new Vector2(position.X + Width * .5f - playerReach.Sprite.Width * .5f, position.Y + Height * .5f - playerReach.Sprite.Height * .5f);
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
            /* if (Keyboard.GetState().IsKeyDown(Keys.A)) { velocity.X = -5.0f; }
             else if (Keyboard.GetState().IsKeyDown(Keys.D)) { velocity.X = 5.0f; }
             if (Keyboard.GetState().IsKeyDown(Keys.W)) { velocity.Y = -5.0f; }
             else if (Keyboard.GetState().IsKeyDown(Keys.S)) { velocity.Y = 5.0f; }
             if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)) { velocity *= 2; }*/

            //Control movement
            position += velocity;

            lastPosition = position;

            lastPosition -= velocity;
        }
    }
}

