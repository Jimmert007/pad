using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    class UIDialogueBox : UI
    {
        UIDialogueBox() : base()
        {

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
            texture = mainBody;

        }

        public override void Update()
        {
            base.Update();
            //On mouse click + collision activate button 
            /*if (Overlaps(MouseCursor) && leftmouseclick)
            {
            activate command /next page/
            }
            */
        }
    }
}
