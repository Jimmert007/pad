using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BaseProject
{
    class Hotbar : GameObject
    {
        public List<GameObject> hotbarItemList;
        public Texture2D hotBarBackground;
        public int ScreenWidth;
        public int ScreenHeight;

        public int HBWidth;

        public Hotbar(string _assetName) : base(_assetName)
        {
            hotbarItemList = new List<GameObject>();
            hotBarBackground = GameEnvironment.ContentManager.Load<Texture2D>("1px");

            ScreenWidth = GameEnvironment.screen.X;
            ScreenHeight = GameEnvironment.screen.Y;
            
            HBWidth = 455;
            size.X = HBWidth;
            size.Y = 50;
            position.X = ScreenWidth / 2 - HBWidth /2;
            position.Y = ScreenHeight - size.Y; 

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(hotBarBackground, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y), Color.Black);
            
        }
    }
}
