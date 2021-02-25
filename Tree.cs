using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseProject
{
    class Tree : GameObject
    {
        Tree() : base("tree")
        { }

        public override void Init()
        {
            base.Init();
            position.X = GameEnvironment.Screen.X / 4 - this.texture.Width / 2;
            position.Y = GameEnvironment.Screen.Y / 4 - this.texture.Height / 2;
        }


    }
}
