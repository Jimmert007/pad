using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using HarvestValley.GameObjects;
using HarvestValley.GameObjects.Tools;
using Microsoft.Xna.Framework.Graphics;

namespace HarvestValley.GameStates
{
    class PlayingState : GameObjectList
    {
        Map map;
        Player player;
        CraftingMenu craftingMenu;
        GameObjectList plants;
        GameObjectList trees;
        GameObjectList stones;
        GameObjectList sprinklers;
        SpriteGameObject mouseGO;
        EnergyBar energyBar;
        Sleeping sleeping;
        Hotbar hotbar;
        ItemList itemList;
        SpriteFont font;
        UIList uIList;
        Executer exec;

        public PlayingState()
        {
            SpriteSheet mapSpriteSheet = new SpriteSheet("tiles/spr_grass", 0);
            map = new Map(new Vector2(mapSpriteSheet.Width, mapSpriteSheet.Height));
            Add(map.cells);
            for (int i = 0; i < map.rows; i++)
            {
                for (int x = 0; x < map.cols; x++)
                {
                    Cell c = new Cell(mapSpriteSheet, new Vector2(mapSpriteSheet.Width / 2 * x, mapSpriteSheet.Height / 2 * i), .5f, x + (map.cols * i));
                    map.cells.Add(c);

                }
            }


            plants = new GameObjectList();
            Add(plants);
            for (int i = 0; i < map.rows; i++)
            {
                for (int x = 0; x < map.cols; x++)
                {
                    Plant p = new Plant(new Vector2(mapSpriteSheet.Width / 2 * x, mapSpriteSheet.Height / 2 * i), 2);
                    plants.Add(p);
                }
            }

            player = new Player("jorrit", new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2), .1f);
            Add(player);

            trees = new GameObjectList();
            Add(trees);
            for (int i = 0; i < map.rows; i++)
            {
                for (int x = 0; x < map.cols; x++)
                {
                    Tree t = new Tree(new Vector2(mapSpriteSheet.Width / 2 * x, mapSpriteSheet.Height / 2 * i), .5f);
                    trees.Add(t);
                }
            }
            stones = new GameObjectList();
            Add(stones);
            for (int i = 0; i < map.rows; i++)
            {
                for (int x = 0; x < map.cols; x++)
                {
                    Stone s = new Stone(new Vector2(mapSpriteSheet.Width / 2 * x, mapSpriteSheet.Height / 2 * i), .5f);
                    stones.Add(s);
                }
            }

            sprinklers = new GameObjectList();
            Add(sprinklers);
            for (int i = 0; i < map.rows; i++)
            {
                for (int x = 0; x < map.cols; x++)
                {
                    SprinklerObject s = new SprinklerObject(new Vector2(mapSpriteSheet.Width / 2 * x, mapSpriteSheet.Height / 2 * i), 1f);
                    sprinklers.Add(s);
                }

            }

            mouseGO = new SpriteGameObject("1px");
            Add(mouseGO);

            energyBar = new EnergyBar("spr_empty", GameEnvironment.Screen.X - 60, GameEnvironment.Screen.Y - 220, 40, 200);
            Add(energyBar);

            sleeping = new Sleeping("spr_empty");
            Add(sleeping);

            craftingMenu = new CraftingMenu();
            Add(craftingMenu);

            hotbar = new Hotbar("spr_empty");
            Add(hotbar);

            itemList = new ItemList();


            font = GameEnvironment.AssetManager.Content.Load<SpriteFont>("GameFont");

            foreach (Cell c in map.cells.Children)
            {
                if (c.Position.X < c.Width || c.Position.X + c.Width > GameEnvironment.Screen.X - c.Width
                    || c.Position.Y < c.Height || c.Position.Y + c.Height > GameEnvironment.Screen.Y - c.Height)
                {
                    c.cellHasTree = true;
                }
                if (c.cellHasTree)
                {
                    (trees.Children[c.cellID] as Tree).soilHasTree = true;
                    (trees.Children[c.cellID] as Tree).growthStage = 3;
                }

                if (!c.cellHasTree)
                {
                    int r = GameEnvironment.Random.Next(50);
                    if (r == 1)
                    {
                        c.cellHasStone = true;
                    }
                    if (c.cellHasStone)
                    {
                        (stones.Children[c.cellID] as Stone).soilHasStone = true;
                        (stones.Children[c.cellID] as Stone)._sprite = 1;
                    }
                }
            }

            //Initialize UI Elements
            Add(uIList = new UIList());
            Add(exec = new Executer());
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (sleeping.fadeAmount >= 1)
            {
                if (sleeping.fadeOut)
                {
                    for (int i = 0; i < map.cells.Children.Count; i++)
                    {
                        if ((map.cells.Children[i] as Cell).cellHasSprinkler)
                        {
                            foreach (Cell c in map.cells.Children)
                            {
                                if ((map.cells.Children[i] as Cell).Position + new Vector2(64, 0) == c.Position && c.cellIsTilled)
                                {
                                    c.cellHasWater = true;
                                    c.nextToSprinkler = true;
                                    c.ChangeSpriteTo(Cell.TILESOILWATER, .5f);
                                    (plants.Children[c.cellID] as Plant).soilHasWater = true;
                                }
                                if ((map.cells.Children[i] as Cell).Position + new Vector2(0, 64) == c.Position && c.cellIsTilled)
                                {
                                    c.cellHasWater = true;
                                    c.nextToSprinkler = true;
                                    c.ChangeSpriteTo(Cell.TILESOILWATER, .5f);
                                    (plants.Children[c.cellID] as Plant).soilHasWater = true;
                                }
                                if ((map.cells.Children[i] as Cell).Position + new Vector2(0, -64) == c.Position && c.cellIsTilled)
                                {
                                    c.cellHasWater = true;
                                    c.nextToSprinkler = true;
                                    c.ChangeSpriteTo(Cell.TILESOILWATER, .5f);
                                    (plants.Children[c.cellID] as Plant).soilHasWater = true;
                                }
                                if ((map.cells.Children[i] as Cell).Position + new Vector2(-64, 0) == c.Position && c.cellIsTilled)
                                {
                                    c.cellHasWater = true;
                                    c.nextToSprinkler = true;
                                    c.ChangeSpriteTo(Cell.TILESOILWATER, .5f);
                                    (plants.Children[c.cellID] as Plant).soilHasWater = true;
                                }
                            }
                        }
                    }


                    foreach (Cell c in map.cells.Children)
                    {
                        if (!c.cellHasWater && c.cellIsTilled)
                        {
                            if (c.randomGrass == 1)
                            {
                                c.cellHasPlant = false;
                                c.cellIsTilled = false;
                                c.ChangeSpriteTo(Cell.GRASS, .5f);

                                foreach (Plant p in plants.Children)
                                {
                                    if (p.Position == c.Position)
                                    {
                                        p.soilHasPlant = false;
                                        p.growthStage = 0;
                                    }
                                }
                            }
                            c.nextRandom = true;
                        }
                        foreach (Plant p in plants.Children)
                        {
                            if (p.soilHasPlant && p.soilHasWater)
                            {
                                p.growthStage++;
                                p.soilHasWater = false;
                            }
                        }
                        if (c.cellHasWater && !c.nextToSprinkler)
                        {
                            c.cellHasWater = false;
                            foreach (Plant p in plants.Children)
                            {
                                p.soilHasWater = false;
                            }
                            c.ChangeSpriteTo(Cell.TILESOIL, .5f);
                        }


                    }

                    energyBar.Reset();




                    foreach (Tree t in trees.Children)
                    {
                        if (t.soilHasTree)
                        {
                            t.growthStage++;
                        }
                    }
                }
                if (energyBar.passOut)
                {
                    sleeping.Sleep(gameTime);
                    sleeping.useOnce = false;
                }
                sleeping.Update(gameTime);
            }


            foreach (Item item in itemList.Children)
            {
                foreach (Tree t in trees.Children)
                {
                    if (t.health <= 0)
                    {
                        if (item is Wood)
                        {
                            item.itemAmount += GameEnvironment.Random.Next(3, 7);
                        }
                        if (item is TreeSeed)
                        {
                            item.itemAmount += GameEnvironment.Random.Next(2);
                            t.health = 4;
                        }

                    }
                }
                foreach (Stone s in stones.Children)
                {
                    if (s.health <= 0)
                    {
                        if (item is Rock)
                        {
                            item.itemAmount += GameEnvironment.Random.Next(2, 5);
                            s.health = 6;
                        }
                    }
                }
            }

            foreach (Cell c in map.cells.Children)
            {
                if (c.cellHasTree || c.cellHasSprinkler || c.cellHasStone)
                {
                    if (player.CollidesWith(c))
                    {
                        player.collision = true;
                        player.Position = player.lastPosition;
                    }
                    else
                    {
                        player.collision = false;
                    }

                }
                if (c.cellHasTree)
                {
                    (trees.Children[(int)c.cellID] as Tree).soilHasTree = true;
                }
            }

            if (energyBar.passOut)
            {
                sleeping.Sleep(gameTime);
                sleeping.useOnce = false;
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
                            if (itemList.itemSelected == "HOE" && !c.cellIsTilled && !c.cellHasTree && !c.cellHasSprinkler && !c.cellHasStone)
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

                            if (item is Sprinkler)
                            {
                                if (itemList.itemSelected == "SPRINKLER" && !c.cellHasPlant && !c.cellHasTree && !c.cellHasSprinkler && item.itemAmount > 0 && !c.cellHasStone)
                                {
                                    item.itemAmount -= 1;
                                    c.cellHasSprinkler = true;
                                    energyBar.percentageLost += energyBar.oneUse;
                                    (sprinklers.Children[c.cellID] as SprinklerObject).sprinklerSprite = 1;
                                }
                            }

                            Plant p = (plants.Children[c.cellID] as Plant);
                            if (itemList.itemSelected == "WATERINGCAN" && c.cellIsTilled)
                            {
                                c.cellHasWater = true;
                                p.soilHasWater = true;
                                c.ChangeSpriteTo(Cell.TILESOILWATER, .5f);
                            }

                            Stone s = (stones.Children[c.cellID] as Stone);
                            if (itemList.itemSelected == "PICKAXE" && c.cellHasStone && !s.stoneHit && s._sprite == 1)
                            {
                                s.stoneHit = true;
                                s.hitTimer = s.hitTimerReset;
                                s.health -= 1;
                                energyBar.percentageLost += energyBar.oneUse;
                                if (s.health <= 0)
                                {
                                    c.cellHasStone = false;
                                    s.soilHasStone = false;
                                }
                            }

                            Tree t = (trees.Children[c.cellID] as Tree);
                            if (itemList.itemSelected == "AXE" && c.cellHasTree && !t.treeHit && t.growthStage == 3)
                            {
                                t.treeHit = true;
                                t.hitTimer = t.hitTimerReset;
                                t.health -= 1;
                                energyBar.percentageLost += energyBar.oneUse;
                                if (t.health <= 0)
                                {
                                    c.cellHasTree = false;
                                    t.soilHasTree = false;
                                }
                            }

                            if (item is TreeSeed)
                            {
                                if (itemList.itemSelected == "TREESEED" && !c.cellIsTilled && !c.cellHasPlant && item.itemAmount > 0 && !c.cellHasTree && !c.cellHasSprinkler && !c.cellHasStone)
                                {
                                    item.itemAmount -= 1;
                                    c.cellHasTree = true;
                                    t.soilHasTree = true;
                                    t.growthStage = 1;
                                    energyBar.percentageLost += energyBar.oneUse;
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
                itemList.itemSelected = "PICKAXE";
                hotbar.selectedSquarePosition.X = hotbar.Position.X + hotbar.squareSize * 2;
            }
            else if (inputHelper.KeyPressed(Keys.D4))
            {
                itemList.itemSelected = "WATERINGCAN";
                hotbar.selectedSquarePosition.X = hotbar.Position.X + hotbar.squareSize * 3;
            }
            else if (inputHelper.KeyPressed(Keys.D5))
            {
                itemList.itemSelected = "SEED";
                hotbar.selectedSquarePosition.X = hotbar.Position.X + hotbar.squareSize * 4;
            }
            else if (inputHelper.KeyPressed(Keys.D6))
            {
                itemList.itemSelected = "WOOD";
                hotbar.selectedSquarePosition.X = hotbar.Position.X + hotbar.squareSize * 5;
            }
            else if (inputHelper.KeyPressed(Keys.D7))
            {
                itemList.itemSelected = "TREESEED";
                hotbar.selectedSquarePosition.X = hotbar.Position.X + hotbar.squareSize * 6;
            }
            else if (inputHelper.KeyPressed(Keys.D8))
            {
                itemList.itemSelected = "ROCK";
                hotbar.selectedSquarePosition.X = hotbar.Position.X + hotbar.squareSize * 7;
            }
            else if (inputHelper.KeyPressed(Keys.D9))
            {
                itemList.itemSelected = "SPRINKLER";
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
                    spriteBatch.Draw(item.Sprite.Sprite, new Rectangle((int)hotbar.Position.X + 5 + hotbar.squareSize * i, (int)hotbar.Position.Y + 5, (int)hotbar.squareSize - 10, (int)hotbar.squareSize - 10), Color.White);
                    if (item.isStackable)
                    {
                        spriteBatch.DrawString(font, item.itemAmount.ToString(), new Vector2((int)hotbar.Position.X + 5 + hotbar.squareSize * i, (int)hotbar.Position.Y + 5), Color.Black);
                    }
                }
            }
        }
    }
}