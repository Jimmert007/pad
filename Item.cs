using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace BaseProject
{
    class Item : GameObject
    {
        public List<Item> items = new List<Item>();
        public string itemSelected = "HOE";
        public int spacing;
        public int totalWidth;
        public Texture2D hotbar;
        public Item(string _assetName) : base(_assetName)
        {
            position.X = GameEnvironment.Screen.X / 4;
            position.Y = GameEnvironment.Screen.Y - GameEnvironment.Screen.Y / 18;
            size.X = GameEnvironment.Screen.Y / 18;
            size.Y = GameEnvironment.Screen.Y / 18;
            spacing = GameEnvironment.Screen.Y / 18;
            totalWidth = GameEnvironment.Screen.Y / 2;
            hotbar = GameEnvironment.ContentManager.Load<Texture2D>("spr_hotbar");
        }

        public override void Update()
        {
            //add tool selection
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D1))
            {
                itemSelected = "HOE";
            } else if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D2))
            {
                itemSelected = "AXE";
            } else if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D3))
            {
                itemSelected = "WATERINGCAN";
            } else if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D4))
            {
                itemSelected = "SEED";
            }
        }
    }
}
