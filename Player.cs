using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseProject
{
    class Player
    {
        public Vector2 velocity, position;
        public Texture2D texture;

        public Player() {
            texture = Global.content.Load<Texture2D>("spr_red_invader");
            position.X = Global.width/2;
            position.Y = Global.height / 2;
        }

        public void Draw() {
            Global.spriteBatch.Draw(texture, position, Color.White);
        }

        public void Update()
        {
            //Continuesly set movement to 0
            velocity.X = 0;
            velocity.Y = 0;

            //Movement inputs
            if (Global.keys.IsKeyDown(Keys.A)) { velocity.X = -10.0f; }
            if (Global.keys.IsKeyDown(Keys.D)) { velocity.X =  10.0f; }
            if (Global.keys.IsKeyDown(Keys.W)) { velocity.Y = -10.0f; }
            if (Global.keys.IsKeyDown(Keys.S)) { velocity.Y =  10.0f; }


        }
    }
}

