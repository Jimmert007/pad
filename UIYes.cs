using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace BaseProject
{
    class UIYes : UI
    {
        Texture2D yes;
        UIYes() : base()
        {
            //Initialize UI Images
            yes = GameEnvironment.ContentManager.Load<Texture2D>("yes");
        }

        public override void Init()
        {
            position.X = GameEnvironment.Screen.X / 3 - this.texture.Width / 2;
            position.X = GameEnvironment.Screen.X * 2/3 - this.texture.Width / 2;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Draw the yes button
           // drop this inside playstate,  if (UIActive && playerDescision)
                texture = yes;
        }
        public override void Update()
        {
            base.Update();
            //Activate button on collision detection & click
        }

    }
}
