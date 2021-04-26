using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley.GameObjects
    {
    class DesiscionDialog : GameObjectList
    {
        public string[] strings = { "Do you want to sleep?", "Do you want to throw this item away?", "Do you want to buy this item?", "Do you want to sell this item?", "tttttring???" };
        public int curActive = 0;
        public DesiscionDialog()
        {
            for (int i = 0; i < strings.Length; i++)
            {
                TextGameObject x = new TextGameObject("JimFont");
                x.Text = strings[i];
                x.Position = new Vector2(GameEnvironment.Screen.X * .5f - x.Size.X * .5f, GameEnvironment.Screen.Y * .2f + x.Size.Y * .5f);
                Add(x);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (TextGameObject TGO in Children)
            {
                if (TGO.Text == strings[curActive])
                {
                    TGO.Visible = true;
                }
                else
                {
                    TGO.Visible = false;
                }
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.AnyKeyPressed)
            {
                if (curActive < strings.Length - 1)
                {
                    curActive++;
                }
                else
                {
                    curActive = 0;
                }
            }
        }

        public string GetString()
        {
            return strings[curActive];
        }
    }
}