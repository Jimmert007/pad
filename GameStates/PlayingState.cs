using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using HarvestValley.GameObjects;
using HarvestValley.GameObjects.Tools;
using HarvestValley.GameObjects.HarvestValley.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using HarvestValley.GameObjects.Tutorial;

namespace HarvestValley.GameStates
{
    class PlayingState : GameObjectList
    {
        Map map;
        GameObjectList cells;
        Player player;
        CraftingMenu craftingMenu;
        GameObjectList plants;
        GameObjectList trees;
        GameObjectList stones;
        GameObjectList sprinklers;
        SpriteGameObject mouseGO;
        TutorialStepList tutorialStepList;
        EnergyBar energyBar;
        Sleeping sleeping;
        Hotbar hotbar;
        ItemList itemList;
        SpriteFont jimFont;
        UIList uIList;
        Executer exec;
        Wallet wallet;
        GameObjectList UI;
        GameObjectList tent;
        Vector2 prevPos;

        public PlayingState()
        {
            SpriteSheet mapSpriteSheet = new SpriteSheet("tiles/spr_grass", 0);
            map = new Map();
            cells = new GameObjectList();
            for (int i = 0; i < map.rows; i++)
            {
                for (int x = 0; x < map.cols; x++)
                {
                    Cell c = new Cell(new Vector2(mapSpriteSheet.Width / 2 * x, mapSpriteSheet.Height / 2 * i), .5f, x + (map.cols * i));
                    cells.Add(c);
                }
            }
            Add(cells);

            plants = new GameObjectList();
            Add(plants);

            player = new Player("jorrit", new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2), .1f);
            Add(player);

            stones = new GameObjectList();
            Add(stones);

            trees = new GameObjectList();
            Add(trees);

            sprinklers = new GameObjectList();
            Add(sprinklers);

            mouseGO = new SpriteGameObject("1px");
            Add(mouseGO);

            energyBar = new EnergyBar("spr_empty", GameEnvironment.Screen.X - 60, GameEnvironment.Screen.Y - 220, 40, 200);
            Add(energyBar);

            tent = new GameObjectList();
            tent.Add(new Tent());
            Add(tent);

            sleeping = new Sleeping("spr_empty");
            Add(sleeping);

            craftingMenu = new CraftingMenu();
            Add(craftingMenu);

            hotbar = new Hotbar("spr_empty");
            Add(hotbar);

            itemList = new ItemList();

            jimFont = GameEnvironment.AssetManager.Content.Load<SpriteFont>("JimFont");

            SpawnTent();
            PlaceStonesAndTrees();

            //Initialize UI Elements
            Add(uIList = new UIList());
            Add(exec = new Executer());

            wallet = new Wallet();
            Add(wallet);

            tutorialStepList = new TutorialStepList();
            Add(tutorialStepList);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            SleepActions(gameTime); //works
            CheckMouseCollisionWithTutorial();
            CheckSleepHitbox();
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            mouseGO.Position = inputHelper.MousePosition;
            CameraSystem(inputHelper);

            CheckHoeInput(inputHelper);
            CheckSeedInput(inputHelper);
            CheckSprinklerInput(inputHelper);
            CheckWateringCanInput(inputHelper);
            CheckTreeSeedInput(inputHelper);
            CheckPickaxeInput(inputHelper);
            CheckAxeInput(inputHelper);
            CheckPlantPickup(inputHelper);

            CheckHotbarSelection(inputHelper);
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
                        spriteBatch.DrawString(jimFont, item.itemAmount.ToString(), new Vector2((int)hotbar.Position.X + 5 + hotbar.squareSize * i, (int)hotbar.Position.Y + 5), Color.Black);
                    }
                }
            }
        }

        void CheckSleepHitbox()
        {
            if ((tent.Children[0] as Tent).CollidesWithSleep(player))
            {
                sleeping.sleepHitboxHit = true;
            }
            else
            {
                sleeping.sleepHitboxHit = false;
            }
        }

        void SpawnTent()
        {
            foreach (Cell c in cells.Children)
            {
                if (c.Position == new Vector2(128, 128)
                    || c.Position == new Vector2(192, 128)
                    || c.Position == new Vector2(128, 192)
                    || c.Position == new Vector2(192, 192))
                {
                    c.cellHasTent = true;
                }
            }
        }

        void CheckMouseCollisionWithTutorial()
        {
            if (mouseGO.CollidesWith(tutorialStepList.Children[0] as SpriteGameObject))
            {
                tutorialStepList.mouseCollides = true;
            }
            else if (!mouseGO.CollidesWith(tutorialStepList.Children[0] as SpriteGameObject))
            {
                tutorialStepList.mouseCollides = false;
            }
        }

        /// <summary> 
        /// Places the initial stones and trees on the map
        /// </summary>
        void PlaceStonesAndTrees()
        {
            foreach (Cell c in cells.Children)
            {
                if (!c.cellHasTent)
                {
                    if (c.Position.X < c.grass.Width || c.Position.X >= GameEnvironment.Screen.X - c.grass.Width
                        || c.Position.Y < c.grass.Height || c.Position.Y > GameEnvironment.Screen.Y - c.grass.Height)
                    {
                        c.cellHasTree = true;
                        trees.Add(new Tree(c.Position, .5f, 3));
                    }

                    if (!c.cellHasTree)
                    {
                        int r = GameEnvironment.Random.Next(50);
                        if (r == 1 && !c.CellCollidesWith(player.playerReach))
                        {
                            c.cellHasStone = true;
                            stones.Add(new Stone(c.Position, .5f));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Contains all the actions surrounding sleeping
        /// </summary>
        void SleepActions(GameTime gameTime)
        {
            if (sleeping.fadeIn)
            {
                player.sleeping = true;
                player.Visible = false;
            }
            else if (sleeping.fadeOut)
            {
                player.sleeping = false;
                player.Visible = true;
            }
            if (sleeping.fadeAmount >= 1)
            {
                player.sleepingPosition = true;
                if (sleeping.fadeOut)
                {
                    for (int i = 0; i < cells.Children.Count; i++)
                    {
                        if ((cells.Children[i] as Cell).cellHasSprinkler) //planten naast een sprinkler
                        {
                            foreach (Cell c in cells.Children)
                            {
                                if ((cells.Children[i] as Cell).Position + new Vector2(64, 0) == c.Position && c.cellIsTilled) // rechts
                                {
                                    c.cellHasWater = true;
                                    c.nextToSprinkler = true;
                                    c.ChangeSpriteTo(2);
                                    if (c.cellHasPlant)
                                    {
                                        for (int x = plants.Children.Count - 1; x >= 0; x--)
                                        {
                                            if (plants.Children[x].Position == c.Position)
                                            {
                                                (plants.Children[x] as Plant).soilHasWater = true;
                                            }
                                        }
                                    }
                                }
                                if ((cells.Children[i] as Cell).Position + new Vector2(0, 64) == c.Position && c.cellIsTilled)
                                {
                                    c.cellHasWater = true;
                                    c.nextToSprinkler = true;
                                    c.ChangeSpriteTo(2);
                                    if (c.cellHasPlant)
                                    {
                                        for (int x = plants.Children.Count - 1; x >= 0; x--)
                                        {
                                            if (plants.Children[x].Position == c.Position)
                                            {
                                                (plants.Children[x] as Plant).soilHasWater = true;
                                            }
                                        }
                                    }
                                }
                                if ((cells.Children[i] as Cell).Position + new Vector2(0, -64) == c.Position && c.cellIsTilled)
                                {
                                    c.cellHasWater = true;
                                    c.nextToSprinkler = true;
                                    c.ChangeSpriteTo(2);
                                    if (c.cellHasPlant)
                                    {
                                        for (int x = plants.Children.Count - 1; x >= 0; x--)
                                        {
                                            if (plants.Children[x].Position == c.Position)
                                            {
                                                (plants.Children[x] as Plant).soilHasWater = true;
                                            }
                                        }
                                    }
                                }
                                if ((cells.Children[i] as Cell).Position + new Vector2(-64, 0) == c.Position && c.cellIsTilled)
                                {
                                    c.cellHasWater = true;
                                    c.nextToSprinkler = true;
                                    c.ChangeSpriteTo(2);
                                    if (c.cellHasPlant)
                                    {
                                        for (int x = plants.Children.Count - 1; x >= 0; x--)
                                        {
                                            if (plants.Children[x].Position == c.Position)
                                            {
                                                (plants.Children[x] as Plant).soilHasWater = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    foreach (Cell c in cells.Children)
                    {
                        if (!c.cellHasWater && c.cellIsTilled)
                        {
                            if (c.randomGrass == 1) //remove grass randomly
                            {
                                c.ChangeSpriteTo(0);

                                if (c.cellHasPlant)
                                {
                                    for (int x = plants.Children.Count - 1; x >= 0; x--)
                                    {
                                        if (plants.Children[x].Position == c.Position)
                                        {
                                            plants.Remove(plants.Children[x]);
                                        }
                                    }
                                }

                                c.cellHasPlant = false;
                                c.cellIsTilled = false;
                            }
                            c.nextRandom = true;
                        }
                        foreach (Plant p in plants.Children)
                        {
                            if (p.soilHasWater)
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
                            c.ChangeSpriteTo(1);
                        }
                    }

                    energyBar.Reset();
                    for (int i = trees.Children.Count - 1; i >= 0; i--)
                    {
                        (trees.Children[i] as Tree).growthStage++;
                    }
                }
                if (energyBar.passOut)
                {
                    sleeping.Sleep(gameTime);
                    sleeping.useOnce = false;
                }
                sleeping.Update(gameTime);
            }
            if (energyBar.passOut)
            {
                sleeping.Sleep(gameTime);
                sleeping.useOnce = false;
            }
        }

        void CameraSystem(InputHelper inputHelper)
        {
            Vector2 moveVector = Vector2.Zero;
            if (inputHelper.IsKeyDown(Keys.A))
            {
                moveVector = new Vector2(player.speed, moveVector.Y);
            }
            if (inputHelper.IsKeyDown(Keys.D))
            {
                moveVector = new Vector2(-player.speed, moveVector.Y);
            }
            if (inputHelper.IsKeyDown(Keys.D) && inputHelper.IsKeyDown(Keys.A))
            {
                moveVector = new Vector2(0, moveVector.Y);
            }
            if (inputHelper.IsKeyDown(Keys.S))
            {
                moveVector = new Vector2(moveVector.X, -player.speed);
            }
            if (inputHelper.IsKeyDown(Keys.W))
            {
                moveVector = new Vector2(moveVector.X, player.speed);
            }
            if (inputHelper.IsKeyDown(Keys.W) && inputHelper.IsKeyDown(Keys.S))
            {
                moveVector = new Vector2(moveVector.X, 0);
            }

            for (int i = trees.Children.Count - 1; i >= 0; i--)
            {
                if ((trees.Children[i] as Tree).CollidesWith(player))
                {
                    cells.Position = prevPos;
                    trees.Position = prevPos;
                    stones.Position = prevPos;
                    sprinklers.Position = prevPos;
                    plants.Position = prevPos;
                    tent.Position = prevPos;
                }
            }

            if (player.sleeping)
            {
                cells.Position = prevPos;
                trees.Position = prevPos;
                stones.Position = prevPos;
                sprinklers.Position = prevPos;
                plants.Position = prevPos;
                tent.Position = prevPos;
            }

            if ((tent.Children[0] as Tent).CollidesWith(player))
            {
                cells.Position = prevPos;
                trees.Position = prevPos;
                stones.Position = prevPos;
                sprinklers.Position = prevPos;
                plants.Position = prevPos;
                tent.Position = prevPos;
            }

            for (int i = stones.Children.Count - 1; i >= 0; i--)
            {
                if ((stones.Children[i] as Stone).CollidesWith(player))
                {
                    cells.Position = prevPos;
                    trees.Position = prevPos;
                    stones.Position = prevPos;
                    sprinklers.Position = prevPos;
                    plants.Position = prevPos;
                    tent.Position = prevPos;
                }
            }

            for (int i = sprinklers.Children.Count - 1; i >= 0; i--)
            {
                if ((sprinklers.Children[i] as SprinklerObject).CollidesWith(player))
                {
                    cells.Position = prevPos;
                    trees.Position = prevPos;
                    stones.Position = prevPos;
                    sprinklers.Position = prevPos;
                    plants.Position = prevPos;
                    tent.Position = prevPos;
                }
            }

            prevPos = cells.Position;
            tent.Position += moveVector;
            cells.Position += moveVector;
            trees.Position += moveVector;
            stones.Position += moveVector;
            sprinklers.Position += moveVector;
            plants.Position += moveVector;
        }

        void CheckHoeInput(InputHelper inputHelper)
        {
            foreach (Cell c in cells.Children)
            {
                if (c.CellCollidesWith(mouseGO) && c.CellCollidesWith(player.playerReach))
                {
                    if (inputHelper.MouseLeftButtonDown())
                    {
                        if (itemList.itemSelected == "HOE" && !c.cellIsTilled && !c.cellHasTent && !c.cellHasTree && !c.cellHasSprinkler && !c.cellHasStone)
                        {
                            if (!tutorialStepList.step1completed)
                            {
                                tutorialStepList.step += 1;
                                tutorialStepList.step1completed = true;
                            }
                            c.ChangeSpriteTo(1);
                            c.cellIsTilled = true;
                            energyBar.percentageLost += energyBar.oneUse;
                        }
                    }
                }
            }
        }

        void CheckSeedInput(InputHelper inputHelper)
        {
            foreach (Cell c in cells.Children)
            {
                if (c.CellCollidesWith(mouseGO) && c.CellCollidesWith(player.playerReach))
                {
                    if (inputHelper.MouseLeftButtonDown())
                    {
                        foreach (Item item in itemList.Children)
                        {
                            if (item is Seed)
                            {
                                if (itemList.itemSelected == "SEED" && c.cellIsTilled && !c.cellHasPlant && item.itemAmount > 0)
                                {
                                    if (!tutorialStepList.step2completed)
                                    {
                                        tutorialStepList.step += 1;
                                        tutorialStepList.step2completed = true;
                                    }
                                    item.itemAmount -= 1;
                                    c.cellHasPlant = true;
                                    energyBar.percentageLost += energyBar.oneUse;
                                    plants.Add(new Plant(c.Position, 2));
                                }
                            }
                        }
                    }
                }
            }
        }

        void CheckSprinklerInput(InputHelper inputHelper)
        {
            foreach (Cell c in cells.Children)
            {
                if (c.CellCollidesWith(mouseGO) && c.CellCollidesWith(player.playerReach) && !c.CellCollidesWith(player))
                {
                    if (inputHelper.MouseLeftButtonDown())
                    {
                        foreach (Item item in itemList.Children)
                        {
                            if (item is Sprinkler)
                            {
                                if (itemList.itemSelected == "SPRINKLER" && !c.cellHasPlant && !c.cellHasTent && !c.cellHasTree && !c.cellHasSprinkler && item.itemAmount > 0 && !c.cellHasStone)
                                {
                                    item.itemAmount -= 1;
                                    c.cellHasSprinkler = true;
                                    energyBar.percentageLost += energyBar.oneUse;
                                    sprinklers.Add(new SprinklerObject(c.Position, 1));
                                }
                            }
                        }
                    }
                }
            }
        }

        void CheckWateringCanInput(InputHelper inputHelper)
        {
            foreach (Cell c in cells.Children)
            {
                if (c.CellCollidesWith(mouseGO) && c.CellCollidesWith(player.playerReach))
                {
                    if (inputHelper.MouseLeftButtonDown())
                    {
                        for (int i = plants.Children.Count - 1; i >= 0; i--)
                        {
                            if (itemList.itemSelected == "WATERINGCAN" && c.cellIsTilled)
                            {
                                if (!tutorialStepList.step3completed)
                                {
                                    tutorialStepList.step += 1;
                                    tutorialStepList.step3completed = true;
                                }
                                c.cellHasWater = true;
                                (plants.Children[i] as Plant).soilHasWater = true;
                                c.ChangeSpriteTo(2);
                            }
                        }
                    }
                }
            }
        }

        void CheckTreeSeedInput(InputHelper inputHelper)
        {
            foreach (Cell c in cells.Children)
            {
                if (c.CellCollidesWith(mouseGO) && c.CellCollidesWith(player.playerReach) && !c.CellCollidesWith(player))
                {
                    if (inputHelper.MouseLeftButtonDown())
                    {
                        foreach (Item item in itemList.Children)
                        {
                            for (int i = trees.Children.Count - 1; i >= 0; i--)
                            {
                                if (item is TreeSeed)
                                {
                                    if (itemList.itemSelected == "TREESEED" && !c.cellIsTilled && !c.cellHasTent && !c.cellHasPlant && item.itemAmount > 0 && !c.cellHasTree && !c.cellHasSprinkler && !c.cellHasStone)
                                    {
                                        item.itemAmount -= 1;
                                        c.cellHasTree = true;
                                        trees.Add(new Tree(c.Position, .5f, 1));
                                        energyBar.percentageLost += energyBar.oneUse;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        void CheckPickaxeInput(InputHelper inputHelper)
        {
            for (int i = stones.Children.Count - 1; i >= 0; i--)
            {
                Stone s = stones.Children[i] as Stone;
                if (s.CollidesWith(mouseGO) && s.CollidesWith(player.playerReach))
                {
                    if (inputHelper.MouseLeftButtonPressed())
                    {
                        if (itemList.itemSelected == "PICKAXE" && !(stones.Children[i] as Stone).stoneHit && (stones.Children[i] as Stone)._sprite == 0)
                        {
                            (stones.Children[i] as Stone).stoneHit = true;
                            (stones.Children[i] as Stone).hitTimer = (stones.Children[i] as Stone).hitTimerReset;
                            (stones.Children[i] as Stone).health -= 1;
                            energyBar.percentageLost += energyBar.oneUse;
                            if ((stones.Children[i] as Stone).health <= 0)
                            {
                                foreach (Cell c in cells.Children)
                                {
                                    if (c.Position == s.Position)
                                    {
                                        c.cellHasStone = false;
                                    }
                                }
                                stones.Remove(stones.Children[i]);
                                foreach (Item item in itemList.Children)
                                {
                                    if (item is Rock)
                                    {
                                        item.itemAmount += GameEnvironment.Random.Next(2, 5);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        void CheckAxeInput(InputHelper inputHelper)
        {
            for (int i = trees.Children.Count - 1; i >= 0; i--)
            {
                if ((trees.Children[i] as Tree).CollidesWith(mouseGO) && (trees.Children[i] as Tree).CollidesWith(player.playerReach))
                {
                    if (inputHelper.MouseLeftButtonPressed())
                    {
                        if (itemList.itemSelected == "AXE" && !(trees.Children[i] as Tree).treeHit && (trees.Children[i] as Tree).growthStage == 3)
                        {
                            (trees.Children[i] as Tree).treeHit = true;
                            (trees.Children[i] as Tree).hitTimer = (trees.Children[i] as Tree).hitTimerReset;
                            (trees.Children[i] as Tree).health -= 1;
                            energyBar.percentageLost += energyBar.oneUse;
                            if ((trees.Children[i] as Tree).health <= 0)
                            {
                                (trees.Children[i] as Tree).treeHit = false;
                                foreach (Cell c in cells.Children)
                                {
                                    if (c.Position == (trees.Children[i] as Tree).Position)
                                    {
                                        c.cellHasTree = false;
                                    }
                                }
                                trees.Remove(trees.Children[i]);
                                foreach (Item item in itemList.Children)
                                {
                                    if (item is Wood)
                                    {
                                        item.itemAmount += GameEnvironment.Random.Next(3, 7);
                                    }
                                    if (item is TreeSeed)
                                    {
                                        item.itemAmount += GameEnvironment.Random.Next(2);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        void CheckPlantPickup(InputHelper inputHelper)
        {
            foreach (Cell c in cells.Children)
            {
                if (c.CellCollidesWith(mouseGO) && c.CellCollidesWith(player.playerReach))
                {
                    if (inputHelper.MouseRightButtonDown())
                    {
                        if (c.cellHasPlant)
                        {
                            for (int i = plants.Children.Count - 1; i >= 0; i--)
                            {
                                if (plants.Children[i].Position == c.Position)
                                {
                                    if ((plants.Children[i] as Plant).growthStage >= 4)
                                    {
                                        //(receive product and new seed)
                                        c.cellHasPlant = false;
                                        plants.Remove(plants.Children[i]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        void CheckHotbarSelection(InputHelper inputHelper)
        {
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
        }
    }
}
