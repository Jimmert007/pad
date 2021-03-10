using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace BaseProject
{
    class Tools : GameObject
    {
        public String toolSelected = "HOE";
        public Tools(string _assetName) : base(_assetName)
        {
            texture = GameEnvironment.ContentManager.Load<Texture2D>("spr_empty");
        }

        public override void Update()
        {
            //add tool selection
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D1))
            {
                toolSelected = "HOE";
            } else if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D2))
            {
                toolSelected = "AXE";
            }
        }
    }
}
