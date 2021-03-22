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

        public Player(string _assetName, int _x, int _y, int _w, int _h) : base(_assetName)
        {
            position.X = _x;
            position.Y = _y;
            size.X = _w;
            size.Y = _h;
            texture = GameEnvironment.ContentManager.Load<Texture2D>("jorrit");
            //left = GameEnvironment.ContentManager.Load<Texture2D>("spr_red_invader");
            //right = GameEnvironment.ContentManager.Load<Texture2D>("spr_green_invader");
            //up = GameEnvironment.ContentManager.Load<Texture2D>("spr_yellow_invader");
            //down = GameEnvironment.ContentManager.Load<Texture2D>("spr_blue_invader");

        }

        override public void Init()
        {
            base.Init();
            position.X = GameEnvironment.Screen.X / 2 - this.texture.Width / 2;
            position.Y = GameEnvironment.Screen.Y / 2 - this.texture.Height / 2;
            size.X = 100;
            size.Y = 100;

        }

       public bool PlayerCanReach()
        {
            if (position.X + size.X / 2 - GameEnvironment.MouseState.X <= 75
                & position.X + size.X / 2 - GameEnvironment.MouseState.X >= -75
                & position.Y + size.Y / 2 - GameEnvironment.MouseState.Y <= 75
                & position.Y + size.Y / 2 - GameEnvironment.MouseState.Y >= -75)
            {
                return true;
            }
            return false;
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

            //Texture changes
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
        }
    }
}

