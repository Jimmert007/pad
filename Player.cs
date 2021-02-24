using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseProject
{
    class Player : GameObject
    {
        public bool openInventory;
        public bool openMap;
        Texture2D left, right, up, down ; 

        public Player() : base("spr_red_invader")
        {
            left = GameEnvironment.ContentManager.Load<Texture2D>("spr_red_invader");
            right = GameEnvironment.ContentManager.Load<Texture2D>("spr_green_invader");
            up = GameEnvironment.ContentManager.Load<Texture2D>("spr_yellow_invader");
            down = GameEnvironment.ContentManager.Load<Texture2D>("spr_blue_invader");

        }

        override public void Init()
        {
            base.Init();
            position.X = GameEnvironment.Screen.X / 2 - this.texture.Width / 2;
            position.Y = GameEnvironment.Screen.Y / 2 - this.texture.Height / 2;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            texture = GameEnvironment.ContentManager.Load<Texture2D>("jorrit");
            /*
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                texture = up;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                texture = down;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                texture = left;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                texture = right;
            }
            */
            base.Draw(spriteBatch);
        }

        override public void Update()
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

            /*Player action inputs
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) { }
            if (Keyboard.GetState().IsKeyDown(Keys.F)) { openInventory }
            if (Keyboard.GetState().IsKeyDown(Keys.F) && openInventory) { !openInventory }
            if (Keyboard.GetState().IsKeyDown(Keys.M)) { openMap }
            if (Keyboard.GetState().IsKeyDown(Keys.M) && openMap) { !openMap }
            */

            //Control movement
            position.X += velocity.X;
            position.Y += velocity.Y;
        }
    }
}

