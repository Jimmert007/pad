using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarvestValley.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HarvestValley.GameStates
{
    class PlayingState : GameObjectList
    {
        Map map;
        Player player;
        GameObjectList plants;
        GameObjectList trees;
        SpriteGameObject mouseGO;
        EnergyBar energyBar;
        Sleeping sleeping;
        Hotbar hotbar;
        ItemList itemList;
        SpriteFont font;
        UI ui;
        UIButton yesButton, noButton;
        UIDialogueBox dialogueBox;
        UIText dialogueText;
        int iLines = 0;
        Wallet wallet;

        string[] dialogueLines = { "Dit is een test", "Hallo", "Het werkt", "INSERT TEXT", "Proleet" };

        public PlayingState()
        {
            SpriteSheet mapSpriteSheet = new SpriteSheet("tiles/Niels/cutOutTilesNiels@3x4", 0);
            map = new Map(new Vector2(mapSpriteSheet.Width, mapSpriteSheet.Height));
            Add(map.cells);
            for (int i = 0; i < map.rows; i++)
            {
                for (int x = 0; x < map.cols; x++)
                {
                    Cell c = new Cell(mapSpriteSheet, new Vector2(mapSpriteSheet.Width * x, mapSpriteSheet.Height * i), 1, x + (map.cols * i));
                    map.cells.Add(c);
                }
            }

            plants = new GameObjectList();
            Add(plants);
            for (int i = 0; i < map.rows; i++)
            {
                for (int x = 0; x < map.cols; x++)
                {
                    Plant p = new Plant(new Vector2(mapSpriteSheet.Width * x, mapSpriteSheet.Height * i), 4);
                    plants.Add(p);
                }
            }

            trees = new GameObjectList();
            Add(trees);
            for (int i = 0; i < map.rows; i++)
            {
                for (int x = 0; x < map.cols; x++)
                {
                    Tree t = new Tree(new Vector2(mapSpriteSheet.Width * x, mapSpriteSheet.Height * i), .1f);
                    trees.Add(t);
                }
            }

            player = new Player("jorrit", new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2), .2f);
            Add(player);

            mouseGO = new SpriteGameObject("1px");
            Add(mouseGO);

            energyBar = new EnergyBar("spr_empty", GameEnvironment.Screen.X - 60, GameEnvironment.Screen.Y - 220, 40, 200);
            Add(energyBar);

            sleeping = new Sleeping("spr_empty");
            Add(sleeping);

            hotbar = new Hotbar("spr_empty");
            Add(hotbar);

            itemList = new ItemList();
            Add(itemList);

            font = GameEnvironment.AssetManager.Content.Load<SpriteFont>("GameFont");

            foreach (Cell c in map.cells.Children)
            {
                if (c.cellHasTree)
                {
                    (trees.Children[c.cellID] as Tree).growthStage = 1;
                    (trees.Children[c.cellID] as Tree).soilHasTree = true;
                }
            }

            //Initialize UI Elements
            ui = new UI("ui_bar");
            dialogueBox = new UIDialogueBox("ui_bar", -1000, -1000, 1);
            dialogueText = new UIText();
            yesButton = new UIButton("play", -1000, -1000, .5f);
            noButton = new UIButton("cancel", -1000, -1000, .5f);

            Add(dialogueBox);
            Add(yesButton);
            Add(noButton);
            Add(dialogueText);

            wallet = new Wallet("spr_wallet");
            Add(wallet);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (sleeping.fadeAmount >= 1)
            {
                foreach (Plant p in plants.Children)
                {
                    if (sleeping.fadeOut)
                    {
                        energyBar.Reset();
                        if (p.soilHasPlant)
                        {
                            p.growthStage++;
                        }
                    }
                    if (energyBar.passOut)
                    {
                        sleeping.Sleep(gameTime);
                        sleeping.useOnce = false;
                    }
                    sleeping.Update(gameTime);
                }
            }

            if (energyBar.passOut)
            {
                sleeping.Sleep(gameTime);
                sleeping.useOnce = false;
            }

            //Continuesly draw the UI on top of the UI box
            dialogueText.Position = new Vector2(dialogueBox.Position.X + yesButton.Sprite.Width / 3, dialogueBox.Position.Y + dialogueBox.Sprite.Height / 3);

            dialogueText.Text = dialogueLines[iLines].ToString();
            //UI textBox
            if (ui.UIActive)
            {   //Change UI textBox position
                dialogueBox.Position = new Vector2(GameEnvironment.Screen.X / 4, GameEnvironment.Screen.Y / 4);
            }
            //  UI for Player Actions
            if (ui.playerDescision)
            {
                //Change UI Choice Positions
                yesButton.Position = new Vector2(dialogueBox.Position.X, (dialogueBox.Position.Y + (dialogueBox.Sprite.Height * 2)));

                noButton.Position = new Vector2((int)(dialogueBox.Position.X + dialogueBox.Sprite.Width - noButton.Sprite.Width), (int)(yesButton.Position.Y));
            }
            else if (!ui.UIActive)
            {
                dialogueBox.Visible = false;
            }
            else if (!ui.playerDescision)
            {
                yesButton.Position = new Vector2(-10000, -10000);
                noButton.Position = new Vector2(-10000, -10000);
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            mouseGO.Position = inputHelper.MousePosition;

            foreach (Cell c in map.cells.Children)
            {
                foreach (Item item in itemList.Children)
                {
                    if (c.CollidesWith(mouseGO))
                    {
                        if (inputHelper.MouseLeftButtonDown())
                        {
                            if (itemList.itemSelected == "HOE" && !c.cellIsTilled && !c.cellHasTree)
                            {
                                c.ChangeSpriteTo(Cell.TILESOIL, .5f);
                                c.cellIsTilled = true;
                                energyBar.percentageLost += energyBar.oneUse;
                            }

                            if (item is Seed)
                            {
                                if (itemList.itemSelected == "SEED" && c.cellIsTilled && !c.cellHasPlant && item.itemAmount > 0)
                                {
                                    item.itemAmount -= 1;
                                    c.cellHasPlant = true;
                                    energyBar.percentageLost += energyBar.oneUse;
                                    (plants.Children[c.cellID] as Plant).growthStage = 1;
                                    (plants.Children[c.cellID] as Plant).soilHasPlant = true;
                                }
                            }

                            Tree t = (trees.Children[c.cellID] as Tree);
                            if (itemList.itemSelected == "AXE" && c.cellHasTree && !t.treeHit)
                            {
                                t.treeHit = true;
                                t.hitTimer = t.hitTimerReset;
                                t.health -= 1;
                                energyBar.percentageLost += energyBar.oneUse;
                                if (t.health <= 0)
                                {
                                    c.cellHasTree = false;
                                }
                            }
                        }

                        if (inputHelper.MouseRightButtonDown())
                        {
                            if (c.cellHasPlant)
                            {
                                if ((plants.Children[c.cellID] as Plant).growthStage >= 4)
                                {
                                    //(receive product and new seed)
                                    (plants.Children[c.cellID] as Plant).soilHasPlant = false;
                                    c.cellHasPlant = false;
                                    (plants.Children[c.cellID] as Plant).growthStage = 0;
                                }
                            }
                        }
                    }
                }
            }
            #region Item Selection
            if (inputHelper.KeyPressed(Keys.D1))
            {
                itemList.itemSelected = "HOE";
                hotbar.selectedSquarePosition.X = hotbar.Position.X;
            }
            else if (inputHelper.KeyPressed(Keys.D2))
            {
                itemList.itemSelected = "AXE";
                hotbar.selectedSquarePosition.X = hotbar.Position.X + hotbar.squareSize;
            }
            else if (inputHelper.KeyPressed(Keys.D3))
            {
                itemList.itemSelected = "WATERINGCAN";
                hotbar.selectedSquarePosition.X = hotbar.Position.X + hotbar.squareSize * 2;
            }
            else if (inputHelper.KeyPressed(Keys.D4))
            {
                itemList.itemSelected = "SEED";
                hotbar.selectedSquarePosition.X = hotbar.Position.X + hotbar.squareSize * 3;
            }
            else if (inputHelper.KeyPressed(Keys.D5))
            {
                //itemList.itemSelected = "AXE";
                hotbar.selectedSquarePosition.X = hotbar.Position.X + hotbar.squareSize * 4;
            }
            else if (inputHelper.KeyPressed(Keys.D6))
            {
                //itemList.itemSelected = "WATERINGCAN";
                hotbar.selectedSquarePosition.X = hotbar.Position.X + hotbar.squareSize * 5;
            }
            else if (inputHelper.KeyPressed(Keys.D7))
            {
                //itemList.itemSelected = "SEED";
                hotbar.selectedSquarePosition.X = hotbar.Position.X + hotbar.squareSize * 6;
            }
            else if (inputHelper.KeyPressed(Keys.D8))
            {
                //itemList.itemSelected = "WATERINGCAN";
                hotbar.selectedSquarePosition.X = hotbar.Position.X + hotbar.squareSize * 7;
            }
            else if (inputHelper.KeyPressed(Keys.D9))
            {
                //itemList.itemSelected = "SEED";
                hotbar.selectedSquarePosition.X = hotbar.Position.X + hotbar.squareSize * 8;
            }
            #endregion

            if (inputHelper.KeyPressed(Keys.U)) { ui.UIActive = true; }
            if (inputHelper.KeyPressed(Keys.I)) { ui.playerDescision = true; }
            if (inputHelper.KeyPressed(Keys.J)) { ui.UIActive = false; }
            if (inputHelper.KeyPressed(Keys.K)) { ui.playerDescision = false; }

            //Pick a sepcific line to display 
            //Cycle through lines on input
            if (inputHelper.KeyPressed(Keys.P))
            {
                iLines++;
                if (iLines >= dialogueLines.Length)
                {
                    iLines = 0;
                }
            }

            if (ui.playerDescision)
            {
                //Choices for Player Actions on UI interaction when UI Choice is active
                for (int iLines = 0; iLines < dialogueLines.Length; iLines++)
                {
                    if (yesButton.CollidesWith(mouseGO) && ui.UIActive && iLines == 0)
                    {
                        ui.playerDescision = false;
                        //Accept player command for action 0
                    }
                    if (yesButton.CollidesWith(mouseGO) && ui.UIActive && iLines == 1)
                    {
                        ui.playerDescision = false;
                        //Accept player command for action 1
                    }
                    if (yesButton.CollidesWith(mouseGO) && ui.UIActive && iLines == 2)
                    {
                        ui.playerDescision = false;
                        //Accept player command for action 2
                    }
                    if (yesButton.CollidesWith(mouseGO) && ui.UIActive && iLines == 3)
                    {
                        ui.playerDescision = false;
                        //Accept player command for action 3
                    }
                    if (noButton.CollidesWith(mouseGO) && ui.UIActive)
                    {
                        ui.playerDescision = false;
                        //reject player command
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(hotbar.selectedSquare.Sprite.Sprite, new Rectangle((int)hotbar.selectedSquarePosition.X, (int)hotbar.selectedSquarePosition.Y, (int)hotbar.squareSize + 5, (int)hotbar.squareSize + 5), Color.White); ;
            for (int i = 0; i < itemList.Children.Count; i++)
            {
                Item item = (itemList.Children[i] as Item);
                if (item.itemAmount > 0)
                {
                    spriteBatch.Draw(item.Sprite.Sprite, new Rectangle((int)hotbar.Position.X + hotbar.squareSize * i, (int)hotbar.Position.Y, (int)hotbar.squareSize, (int)hotbar.squareSize), Color.White);
                    if (item.isStackable)
                    {
                        spriteBatch.DrawString(font, item.itemAmount.ToString(), new Vector2((int)hotbar.Position.X + 5 + hotbar.squareSize * i, (int)hotbar.Position.Y + 5), Color.Black);
                    }
                }
            }

            #region wallet Draw
            spriteBatch.Draw(wallet.wallet.Sprite, new Rectangle((int)wallet.Position.X, (int)wallet.Position.Y, (int)wallet.Sprite.Width, (int)wallet.Sprite.Height), Color.White);
            spriteBatch.Draw(wallet.moneySquare.Sprite, new Rectangle((int)wallet.moneySquarePosition.X, (int)wallet.moneySquarePosition.Y, (int)wallet.moneySquareSize, (int)wallet.Sprite.Height), Color.White); ;

            wallet.money++;
            for (int i = 0; i < wallet.money.ToString().Length; i++)
            {
                for (int r = 0; r < wallet.money.ToString().Length;)
                {
                    #region lelijke if
                    if (wallet.money.ToString()[r] == '0')
                    {

                        spriteBatch.DrawString(font, "0", new Vector2((int)wallet.Position.X + wallet.moneySquareSize / 2 - 10 + wallet.moneySquareSize * r, (int)wallet.Position.Y), Color.Black);
                    }
                    else if (wallet.money.ToString()[r] == '1')
                    {
                        spriteBatch.DrawString(font, "1", new Vector2((int)wallet.Position.X + wallet.moneySquareSize / 2 - 10 + wallet.moneySquareSize * r, (int)wallet.Position.Y), Color.Black);
                    }
                    else if (wallet.money.ToString()[r] == '2')
                    {
                        spriteBatch.DrawString(font, "2", new Vector2((int)wallet.Position.X + wallet.moneySquareSize / 2 - 10 + wallet.moneySquareSize * r, (int)wallet.Position.Y), Color.Black);
                    }
                    else if (wallet.money.ToString()[r] == '3')
                    {
                        spriteBatch.DrawString(font, "3", new Vector2((int)wallet.Position.X + wallet.moneySquareSize / 2 - 10 + wallet.moneySquareSize * r, (int)wallet.Position.Y), Color.Black);
                    }
                    else if (wallet.money.ToString()[r] == '4')
                    {
                        spriteBatch.DrawString(font, "4", new Vector2((int)wallet.Position.X + wallet.moneySquareSize / 2 - 10 + wallet.moneySquareSize * r, (int)wallet.Position.Y), Color.Black);
                    }
                    else if (wallet.money.ToString()[r] == '5')
                    {
                        spriteBatch.DrawString(font, "5", new Vector2((int)wallet.Position.X + wallet.moneySquareSize / 2 - 10 + wallet.moneySquareSize * r, (int)wallet.Position.Y), Color.Black);
                    }
                    else if (wallet.money.ToString()[r] == '6')
                    {
                        spriteBatch.DrawString(font, "6", new Vector2((int)wallet.Position.X + wallet.moneySquareSize / 2 - 10 + wallet.moneySquareSize * r, (int)wallet.Position.Y), Color.Black);
                    }
                    else if (wallet.money.ToString()[r] == '7')
                    {
                        spriteBatch.DrawString(font, "7", new Vector2((int)wallet.Position.X + wallet.moneySquareSize / 2 - 10 + wallet.moneySquareSize * r, (int)wallet.Position.Y), Color.Black);
                    }
                    else if (wallet.money.ToString()[r] == '8')
                    {
                        spriteBatch.DrawString(font, "8", new Vector2((int)wallet.Position.X + wallet.moneySquareSize / 2 - 10 + wallet.moneySquareSize * r, (int)wallet.Position.Y), Color.Black);
                    }
                    else if (wallet.money.ToString()[r] == '9')
                    {
                        spriteBatch.DrawString(font, "9", new Vector2((int)wallet.Position.X + wallet.moneySquareSize / 2 - 10 + wallet.moneySquareSize * r, (int)wallet.Position.Y), Color.Black);
                    }
                    #endregion
                    r++;
                }
            }
            #endregion
        }
    }
}