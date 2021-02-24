using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

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
        }
    }
}
