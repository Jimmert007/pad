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
        SpriteGameObject mouseGO;
        EnergyBar energyBar;
        Sleeping sleeping;
        Hotbar hotbar;
        ItemList itemList;
        SpriteFont font;

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
            for (int i = 1; i < map.rows - 1; i++)
            {
                for (int x = 1; x < map.cols - 1; x++)
                {
                    Plant p = new Plant(new Vector2(mapSpriteSheet.Width * x, mapSpriteSheet.Height * i), 4);
                    plants.Add(p);
                }
            }

            player = new Player("jorrit", new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2), .2f);
            Add(player);

            mouseGO = new SpriteGameObject("1px");
            Add(mouseGO);

            energyBar = new EnergyBar("EnergyBarBackground", GameEnvironment.Screen.X - 60, GameEnvironment.Screen.Y - 220, 40, 200);
            Add(energyBar);

            sleeping = new Sleeping("spr_empty");
            Add(sleeping);

            hotbar = new Hotbar("spr_empty");
            Add(hotbar);

            itemList = new ItemList();
            Add(itemList);

            font = GameEnvironment.AssetManager.Content.Load<SpriteFont>("GameFont");
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
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            mouseGO.Position = inputHelper.MousePosition;

            foreach (Cell c in map.cells.Children)
            {
                if (c.cellIsTilled)
                {
                Debug.WriteLine(c.cellID + " " + c.cellIsTilled);

                }
                foreach (Item item in itemList.Children)
                {
                    if (c.CollidesWith(mouseGO))
                    {
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            if (itemList.itemSelected == "HOE" && !c.cellIsTilled)
                            {
                                c.ChangeSpriteTo(Cell.TILESOIL, .5f);
                                c.cellIsTilled = true;
                                energyBar.percentageLost += energyBar.oneUse;
                            }
                        }
                    }
                }
            }

            foreach (Plant p in plants.Children)
            {
                foreach (Item item in itemList.Children)
                {
                    foreach (Cell c in map.cells.Children)
                    {
                        if (p.CollidesWith(mouseGO))
                        {
                            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                            {
                                if (item is Seed)
                                {
                                    if (itemList.itemSelected == "SEED" && !c.cellHasPlant && c.cellIsTilled && item.itemAmount > 0)
                                    {
                                        item.itemAmount -= 1;
                                        c.cellHasPlant = true;
                                        energyBar.percentageLost += energyBar.oneUse;
                                        p.growthStage = 1;
                                    }
                                }
                            }

                            if (Mouse.GetState().RightButton == ButtonState.Pressed)
                            {
                                if (p.soilHasPlant)
                                {
                                    if (p.growthStage >= 4)
                                    {
                                        //(receive product and new seed)
                                        p.soilHasPlant = false;
                                        p.growthStage = 0;
                                    }
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
                        spriteBatch.DrawString(font, item.itemAmount.ToString(), new Vector2((int)hotbar.Position.X + hotbar.squareSize * i, (int)hotbar.Position.Y), Color.Black);
                    }
                }
            }
        }
    }
}