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
        UIDialogueBox uIDialogueBox;
        UIYes() : base()
        {
            //Initialize UI Images
            yes = GameEnvironment.ContentManager.Load<Texture2D>("yes");
        }

        public override void Init()
        {
            //Set intial position of the Yes box
            position.X = uIDialogueBox.position.X;
            position.Y = +uIDialogueBox.position.Y + uIDialogueBox.texture.Height*1.25f;
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
            if (MouseCollission() /*&& Mouse.GetState*/ && playerDescision && UIActive)
            {
                // activate command /Accept player action/
                playerDescision = false;
                UIActive = false;
            }
           
        }

    }
}
