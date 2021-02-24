using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace BaseProject
{
    class UI : GameObject
    {
        public bool playerDescision, UIActive;
        Texture2D no, cancel, mainBody;

        public UI() : base("ui_bar")
        {
            //Initialize UI Images
            //no = GameEnvironment.ContentManager.Load<Texture2D>("no");
            //cancel = GameEnvironment.ContentManager.Load<Texture2D>("cancel");
            //mainBody = GameEnvironment.ContentManager.Load<Texture2D>("mainBody");
        }

        public override void Init()
        {
            base.Init();
            //Initialize UI position
            position.X = GameEnvironment.Screen.X / 2 - this.texture.Width / 2;
            position.Y = GameEnvironment.Screen.Y / 2 - this.texture.Height / 2;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            //Draw the UI 
            // drop this inside the playerstate, if (UIActive){}
            texture = mainBody;
            texture = cancel;
        }
        public override void Update()
        {
            base.Update();

            //Update UI behavior
            //On mouse click + collision activate button 
            /*if (Overlaps(MouseCursor))
            {
            activate command
            }
            */
        }
    }
}
