using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HarvestValley
{
    class Tree : SpriteGameObject
    {
        Tree() : base("tree")
        { }

        public override void Reset()
        {
            base.Reset();
            position.X = GameEnvironment.Screen.X / 4 - this.sprite.Width / 2;
            position.Y = GameEnvironment.Screen.Y / 4 - this.sprite.Height / 2;
        }


    }
}
