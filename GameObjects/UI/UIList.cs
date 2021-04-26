using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley.GameObjects
{
    class UIList : GameObjectList
    {
        GameObjectList buttons;
        Button yes, no;
        DesiscionDialog uiDesiscionDialog;
        public bool uiActive, uiDesiscion;

        public UIList()
        {
            //Add(new SpriteGameObject("spr_background"));
            Add(buttons = new GameObjectList());
            buttons.Add(yes = new Button("checkmark"));
            yes.Position = new Vector2(GameEnvironment.Screen.X * .3f, GameEnvironment.Screen.Y * .5f);
            //yes.Scale = .5f;
            buttons.Add(no = new Button("cross"));
            no.Position = new Vector2(GameEnvironment.Screen.X * .6f, GameEnvironment.Screen.Y * .5f);
            //no.Scale = .5f;
            Add(uiDesiscionDialog = new DesiscionDialog());

            uiActive = false;
            uiDesiscion = false;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            foreach (Button button in buttons.Children)
            {
                if (inputHelper.MouseLeftButtonPressed() && button.collidesWithMouse(inputHelper.MousePosition))
                {
                    if (uiDesiscionDialog.curActive == 0)
                    {
                        button.PrintDialog(uiDesiscionDialog.GetString() + " " + Executer.Sleep());
                        //insert Sleep() here
                    }
                    else if(uiDesiscionDialog.curActive == 1)
                    {
                        //insert DeleteItem() here
                    }
                    else if (uiDesiscionDialog.curActive == 2)
                    {
                        //insert BuyItem() here
                    }
                    else if (uiDesiscionDialog.curActive == 3)
                    {
                        //insert SellItem() here
                    }
                    else
                    {
                        button.PrintDialog(uiDesiscionDialog.GetString());
                    }
                    uiDesiscion = false;
                    uiActive = false;
                }

                //Go through all the dialogue lines on input 
                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.U))
                {
                    //uiDesiscionDialog.curActive += 1; //For every input the counter goes up by 1
                    if(uiDesiscionDialog.curActive> uiDesiscionDialog.strings.Length) { uiDesiscionDialog.curActive = 0; } //reset the counter if the counter exceeds the max number of lines
                }
                if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.I))
                {
                    uiActive = true;
                    uiDesiscion = true;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (uiActive) { uiDesiscionDialog.Visible = true; }
            else { uiDesiscionDialog.Visible = false; }
            if (uiDesiscion) { yes.Visible = true; no.Visible = true; }
            else { yes.Visible = false; no.Visible = false; }
        }
    }
}
