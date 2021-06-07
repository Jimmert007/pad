using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HarvestValley.GameObjects.UI
{
    class UIList : GameObjectList
    {
        GameObjectList buttons;
        Button yes, no;
        SingleLineCycleDialog uiDesiscionDialog;
        UIBox uIBox;
        public bool uiActive;

        public UIList()
        {
            Add(buttons = new GameObjectList());
            Add(uIBox = new UIBox("UI/ui_bar"));
            uIBox.Position = new Vector2(GameEnvironment.Screen.X * .5f, GameEnvironment.Screen.Y * .35f);
            buttons.Add(yes = new Button("UI/checkmark"));
            yes.Position = new Vector2(uIBox.Position.X - uIBox.Width / 3, GameEnvironment.Screen.Y * .6f);
            //yes.Scale = .5f;
            buttons.Add(no = new Button("UI/cross"));
            no.Position = new Vector2(uIBox.Position.X + uIBox.Width / 5, GameEnvironment.Screen.Y * .6f);
            //no.Scale = .5f;
            Add(uiDesiscionDialog = new SingleLineCycleDialog());

            uiActive = false;
            uiDesiscionDialog.uiDesiscion = false;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            //Activate UI bools
            if (inputHelper.KeyPressed(Keys.I))
            {
                //IMPORTANT
                //Activate specific text 
                uiActive = true;
                uiDesiscionDialog.uiDesiscion = true;
                uiDesiscionDialog.curActive = 3;
            }

            //Set different methods for each button
            foreach (Button button in buttons.Children)
            {
                if (inputHelper.MouseLeftButtonPressed() && button.collidesWithMouse(inputHelper.MousePosition))
                {
                    if (uiDesiscionDialog.curActive == 0)
                    {
                        //insert Sleep() here
                    }
                    else if (uiDesiscionDialog.curActive == 1)
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
                        // button.PrintDialog(uiDesiscionDialog.GetString());
                    }
                    uiDesiscionDialog.uiDesiscion = false;
                    uiActive = false;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (uiActive) { uiDesiscionDialog.Visible = true; uIBox.Visible = true; }
            else { uiDesiscionDialog.Visible = false; uIBox.Visible = false; }
            if (uiDesiscionDialog.uiDesiscion) { yes.Visible = true; no.Visible = true; }
            else { yes.Visible = false; no.Visible = false; }
        }
    }
}
