using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    class Hotbar
    {

        //Screen
        int screenwidth = 800;
        int screenheight = 480;
        int screenHalfWidth;

        //Hotbar
        int hotbarWidth = 200;
        int hotbarheight = 50;
        int hotbarXPos;
        int hotbarYPos;


        Vector2 position;
        Texture2D texture;
        Game1 game1;

        public Hotbar(int _x, int _y, Game1 _game1)
        {
            position.X = _x;
            position.Y = _y;
            game1 = _game1;
            texture = game1.Content.Load<Texture2D>("test");


            //Calculations
            screenHalfWidth = screenwidth / 2;
            hotbarXPos = screenHalfWidth - hotbarWidth / 2;
            hotbarYPos = screenheight - hotbarheight;
        }

        public void Display()
        {
            
            game1.spriteBatch.Draw(texture, new Rectangle(hotbarXPos, hotbarYPos, hotbarWidth, hotbarheight), Color.Gray);

        }
    }
}
