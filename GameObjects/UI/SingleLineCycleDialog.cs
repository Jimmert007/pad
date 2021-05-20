using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace HarvestValley.GameObjects.UI
{
    class SingleLineCycleDialog : GameObjectList
    {
        public string[] strings = { "Do you want to sleep?", "Welcome to the shop", "What do you want to do?", "Buy", "Sell", "Cancel", "Throw item away?", "Buy item?", "Sell item?", "tttttring???" };
        public int curActive = 0;
        public bool uiDesiscion = false;
        public SingleLineCycleDialog()
        {
            for (int i = 0; i < strings.Length; i++)
            {
                TextGameObject x = new TextGameObject("Fonts/JimFont");
                x.Text = strings[i];
                x.Position = new Vector2(GameEnvironment.Screen.X * .5f - x.Size.X * .5f, GameEnvironment.Screen.Y * .3f + x.Size.Y * .5f);
                Add(x);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (curActive > strings.Length - 1) { curActive = 0; } //reset the counter if the counter exceeds the max number of lines
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
            if (uiDesiscion) { Visible = true; }
            else { Visible = false; }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            //Go through all the dialogue lines on input 
            if (inputHelper.KeyPressed(Keys.U))
            {
                curActive += 1; //For every input the counter goes up by 1
            }
        }

        public string GetString()
        {
            return strings[curActive];
        }
    }
}