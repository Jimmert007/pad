using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseProject
{
    class UINo : UI
    {
        UIDialogueBox uIDialogueBox;
        UINo() : base()
        {

        }

        public override void Init()
        {
            //Set inital position of the No box
            position.X = uIDialogueBox.position.X + uIDialogueBox.texture.Width - this.texture.Width/2;
            position.Y = +uIDialogueBox.position.Y + uIDialogueBox.texture.Height * 1.25f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            texture = no;
        }

        public override void Update()
        {
            base.Update();
            //Activate button on collision detection & click
            if (MouseCollission() /*&& Mouse.GetState*/ && playerDescision && UIActive)
            {
                // activate command /Reject player action/
                playerDescision = false;
                UIActive = false;
            }

        }
    }
}
