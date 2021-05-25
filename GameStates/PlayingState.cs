using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using HarvestValley.GameObjects;
using HarvestValley.GameObjects.Tools;
using HarvestValley.GameObjects.UI;
using HarvestValley.GameObjects.Shop;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using HarvestValley.GameObjects.Tutorial;
using Microsoft.Xna.Framework.Media;

namespace HarvestValley.GameStates
{
    class PlayingState : GameObjectList
    {
        Map map;
        Options options;
        GameObjectList cells;
        Player player;
        CraftingMenu craftingMenu;
        GameObjectList plants;
        GameObjectList trees;
        GameObjectList stones;
        GameObjectList sprinklers;
        GameObjectList cliff;
        GameObjectList borderGrass;
        MouseGameObject MouseGO;
        TutorialStepList tutorialStepList;
        EnergyBar energyBar;
        Sleeping sleeping;
        Hotbar hotbar;
        ItemList itemList;
        SpriteFont jimFont;
        UIList uIList;
        ShopMenuUIList shopUI;
        GameObjectList shopPC;
        Wallet wallet;
        GameObjectList tent;
        Vector2 prevPos;
        Target target;
        Sounds sounds;

        public PlayingState()
        {
            sounds = new Sounds();
            MouseGO = new MouseGameObject();

            SpriteSheet mapSpriteSheet = new SpriteSheet("tiles/spr_grass", 0);
            map = new Map();
            cells = new GameObjectList();
            for (int i = 0; i < map.rows; i++)
            {
                for (int x = 0; x < map.cols; x++)
                {
                    Cell c = new Cell(new Vector2(mapSpriteSheet.Width / 2 * x - GameEnvironment.Screen.X, mapSpriteSheet.Height / 2 * i - GameEnvironment.Screen.Y), .5f, x + (map.cols * i));
                    cells.Add(c);
                }
            }
            Add(cells);

            cliff = new GameObjectList();
            Add(cliff);

            borderGrass = new GameObjectList();
            Add(borderGrass);
            BuildBorder();

            plants = new GameObjectList();
            Add(plants);

            player = new Player("Player/jorrit", new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2), .1f);
            Add(player);

            stones = new GameObjectList();
            Add(stones);

            trees = new GameObjectList();
            Add(trees);

            sprinklers = new GameObjectList();
            Add(sprinklers);

            energyBar = new EnergyBar("UI/spr_empty", GameEnvironment.Screen.X - 60, GameEnvironment.Screen.Y - 220, 40, 200);
            Add(energyBar);

            tent = new GameObjectList();
            tent.Add(new Tent());
            Add(tent);

            sleeping = new Sleeping("UI/spr_empty");
            Add(sleeping);

            craftingMenu = new CraftingMenu();
            Add(craftingMenu);

            options = new Options(MouseGO);
            Add(options);

            itemList = new ItemList();

            hotbar = new Hotbar(itemList);
            Add(hotbar);

            jimFont = GameEnvironment.AssetManager.Content.Load<SpriteFont>("Fonts/JimFont");

            shopPC = new GameObjectList();
            shopPC.Add(new ShopPC(tent.Children[0] as Tent));
            Add(shopPC);

            wallet = new Wallet();

            ////Initialize UI Elements
            Add(uIList = new UIList());
            Add(shopUI = new ShopMenuUIList(itemList, (tent.Children[0] as Tent), MouseGO, wallet));

            Add(wallet);

            Add(target = new Target(itemList, wallet, player, sounds));

            tutorialStepList = new TutorialStepList();
            Add(tutorialStepList);

            Add(MouseGO);
            SpawnTent();
            SpawnPC();
            PlaceStonesAndTrees();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!target.panel_bg.Visible && !options.optionsVisible && !options.exitConfirmation)
            {
                SleepActions(gameTime);
                CheckMouseCollisionWithTutorial();
                CheckSleepHitbox();
                CheckPlantsWater();
                PlayerEnergy();
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (!target.panel_bg.Visible && !options.optionsVisible && !options.exitConfirmation && !shopUI.IsActive)
            {
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
                ToggleShopMenu(inputHelper);
            }
        }

        void PlayerEnergy()
        {
            if (player.DeductEnergy)
            {
                energyBar.percentageLost += energyBar.oneUse;
            }
        }

        void ToggleShopMenu(InputHelper inputHelper)
        {
            //Activate UI bools
            if (inputHelper.MouseLeftButtonPressed() && MouseGO.CollidesWith((shopPC.Children[0] as ShopPC).Sprite) && player.playerReach.CollidesWith((shopPC.Children[0] as ShopPC).Sprite))
            {
                shopUI.InitShopWelcomePage();
            }
        }

        void ConvertFromHotbarToMoney(Item item, int amount)
        {
            if (item == target.targetItem && item.isStackable && !target.collected)
            {
                target.AddToTarget(amount);
            }
        }

        /// <summary>
        void CheckSleepHitbox()
        {
            if ((tent.Children[0] as Tent).CollidesWithSleep(player))
            {
                sleeping.sleepHitboxHit = true;
                if (!tutorialStepList.step5completed && tutorialStepList.step == 5)
                {
                    tutorialStepList.step += 1;
                    tutorialStepList.step5completed = true;
                }
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
                foreach (Tent t in tent.Children)
                {
                    if (c.CellCollidesWith(t.Children[0] as SpriteGameObject))
                    {
                        c.cellHasTent = true;
                    }
                }
            }
        }

        void SpawnPC()
        {
            foreach (Cell c in cells.Children)
            {
                if (c.CellCollidesWith((shopPC.Children[0] as ShopPC).Sprite))
                {
                    c.cellHasShop = true;
                }
            }
        }
        void CheckMouseCollisionWithTutorial()
        {
            if (MouseGO.CollidesWith(tutorialStepList.Children[0] as SpriteGameObject))
            {
                tutorialStepList.mouseCollides = true;
            }
            else if (!MouseGO.CollidesWith(tutorialStepList.Children[0] as SpriteGameObject))
            {
                tutorialStepList.mouseCollides = false;
            }
        }

        void BuildBorder()
        {
            for (int x = 0; x < map.cols; x++)
            {
                cliff.Add(new Cliff(new Vector2(-map.mapSizeX + map.cellSize * x, -map.mapSizeY - map.cellSize), 0));
                cliff.Add(new Cliff(new Vector2(-map.mapSizeX + map.cellSize * x, map.rows * map.cellSize - map.mapSizeY), 180));
            }
            cliff.Add(new Cliff(new Vector2(-map.mapSizeX - map.cellSize, -map.mapSizeY - map.cellSize), 0, 2));
            cliff.Add(new Cliff(new Vector2(GameEnvironment.Screen.X + map.mapSizeX, -map.mapSizeY - map.cellSize), 90, 2));
            for (int y = 0; y < map.rows; y++)
            {
                cliff.Add(new Cliff(new Vector2(-map.mapSizeX - map.cellSize, -map.mapSizeY + map.cellSize * y), 270));
                cliff.Add(new Cliff(new Vector2(GameEnvironment.Screen.X + map.mapSizeX, -map.mapSizeY + map.cellSize * y), 90));
            }
            cliff.Add(new Cliff(new Vector2(-map.mapSizeX - map.cellSize, map.rows * map.cellSize - map.mapSizeY), 270, 2));
            cliff.Add(new Cliff(new Vector2(GameEnvironment.Screen.X + map.mapSizeX, map.rows * map.cellSize - map.mapSizeY), 180, 2));

            for (int i = 0; i < map.cols + 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    borderGrass.Add(new SpriteGameObject("tiles/spr_grass") { Position = new Vector2(-map.mapSizeX - map.cellSize + map.cellSize * i, -map.mapSizeY - map.cellSize * 6 + map.cellSize * j), scale = .5f });
                    borderGrass.Add(new SpriteGameObject("tiles/spr_grass") { Position = new Vector2(-map.mapSizeX - map.cellSize + map.cellSize * i, map.rows * map.cellSize - map.mapSizeY + map.cellSize * 5 - map.cellSize * j), scale = .5f });
                }
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < map.rows + 12; j++)
                {
                    borderGrass.Add(new SpriteGameObject("tiles/spr_grass") { Position = new Vector2(-map.mapSizeX - map.cellSize * 10 + map.cellSize * i, -map.mapSizeY - map.cellSize * 6 + map.cellSize * j), scale = .5f });
                    borderGrass.Add(new SpriteGameObject("tiles/spr_grass") { Position = new Vector2(GameEnvironment.Screen.X + map.mapSizeX + map.cellSize + map.cellSize * i, -map.mapSizeY - map.cellSize * 6 + map.cellSize * j), scale = .5f });
                }
            }
        }

        /// <summary> 
        /// Places the initial stones and trees on the map
        /// </summary>
        void PlaceStonesAndTrees()
        {
            foreach (Cell c in cells.Children)
            {
                if (!c.cellHasTent && !c.cellHasShop)
                {
                    #region outer ring
                    if (c.Position.X > -map.mapSizeX - 10 && c.Position.X < -map.mapSizeX + 5 * map.cellSize
                    && c.Position.Y > -map.mapSizeY + 4 * map.cellSize && c.Position.Y < GameEnvironment.Screen.Y + map.mapSizeY - 5 * map.cellSize)
                    {
                        int r = GameEnvironment.Random.Next(map.outerringRandomTree);
                        if (r > 0 && !c.CellCollidesWith(player.playerReach))
                        {
                            c.cellHasTree = true;
                            trees.Add(new Tree(c.Position, .5f, 3));
                        }
                        if (!c.cellHasTree)
                        {
                            int s = GameEnvironment.Random.Next(map.outerringRandomStone);
                            if (s == 0 && !c.CellCollidesWith(player.playerReach))
                            {
                                c.cellHasStone = true;
                                stones.Add(new Stone(c.Position, .5f));
                            }
                        }
                    }
                    if (c.Position.X > GameEnvironment.Screen.X + map.mapSizeX - 5 * map.cellSize - 10 && c.Position.X < GameEnvironment.Screen.X + map.mapSizeX
                    && c.Position.Y > -map.mapSizeY + 4 * map.cellSize && c.Position.Y < GameEnvironment.Screen.Y + map.mapSizeY - 5 * map.cellSize)
                    {
                        int r = GameEnvironment.Random.Next(map.outerringRandomTree);
                        if (r > 0 && !c.CellCollidesWith(player.playerReach))
                        {
                            c.cellHasTree = true;
                            trees.Add(new Tree(c.Position, .5f, 3));
                        }
                        if (!c.cellHasTree)
                        {
                            int s = GameEnvironment.Random.Next(map.outerringRandomStone);
                            if (s == 0 && !c.CellCollidesWith(player.playerReach))
                            {
                                c.cellHasStone = true;
                                stones.Add(new Stone(c.Position, .5f));
                            }
                        }
                    }
                    if (c.Position.Y > -map.mapSizeY - 10 && c.Position.Y < -map.mapSizeY + 5 * map.cellSize)
                    {
                        int r = GameEnvironment.Random.Next(map.outerringRandomTree);
                        if (r > 0 && !c.CellCollidesWith(player.playerReach))
                        {
                            c.cellHasTree = true;
                            trees.Add(new Tree(c.Position, .5f, 3));
                        }
                        if (!c.cellHasTree)
                        {
                            int s = GameEnvironment.Random.Next(map.outerringRandomStone);
                            if (s == 0 && !c.CellCollidesWith(player.playerReach))
                            {
                                c.cellHasStone = true;
                                stones.Add(new Stone(c.Position, .5f));
                            }
                        }
                    }
                    if (c.Position.Y > GameEnvironment.Screen.Y + map.mapSizeY - 5 * map.cellSize - 60 && c.Position.Y < GameEnvironment.Screen.Y + map.mapSizeY)
                    {
                        int r = GameEnvironment.Random.Next(map.outerringRandomTree);
                        if (r > 0 && !c.CellCollidesWith(player.playerReach))
                        {
                            c.cellHasTree = true;
                            trees.Add(new Tree(c.Position, .5f, 3));
                        }
                        if (!c.cellHasTree)
                        {
                            int s = GameEnvironment.Random.Next(map.outerringRandomStone);
                            if (s == 0 && !c.CellCollidesWith(player.playerReach))
                            {
                                c.cellHasStone = true;
                                stones.Add(new Stone(c.Position, .5f));
                            }
                        }
                    }
                    #endregion

                    #region middle ring
                    if (c.Position.X > -map.mapSizeX - 10 + 5 * map.cellSize && c.Position.X < -map.mapSizeX + 10 * map.cellSize
                    && c.Position.Y > -map.mapSizeY + 4 * map.cellSize && c.Position.Y < GameEnvironment.Screen.Y + map.mapSizeY - 5 * map.cellSize)
                    {
                        int r = GameEnvironment.Random.Next(map.middleringRandomTree);
                        if (r == 0 && !c.CellCollidesWith(player.playerReach))
                        {
                            c.cellHasTree = true;
                            trees.Add(new Tree(c.Position, .5f, 3));
                        }
                        if (!c.cellHasTree)
                        {
                            int s = GameEnvironment.Random.Next(map.middleringRandomStone);
                            if (s == 0 && !c.CellCollidesWith(player.playerReach))
                            {
                                c.cellHasStone = true;
                                stones.Add(new Stone(c.Position, .5f));
                            }
                        }
                    }
                    if (c.Position.X > GameEnvironment.Screen.X + map.mapSizeX - 10 * map.cellSize - 10 && c.Position.X < GameEnvironment.Screen.X + map.mapSizeX - 5 * map.cellSize
                    && c.Position.Y > -map.mapSizeY + 4 * map.cellSize && c.Position.Y < GameEnvironment.Screen.Y + map.mapSizeY - 5 * map.cellSize)
                    {
                        int r = GameEnvironment.Random.Next(map.middleringRandomTree);
                        if (r == 0 && !c.CellCollidesWith(player.playerReach))
                        {
                            c.cellHasTree = true;
                            trees.Add(new Tree(c.Position, .5f, 3));
                        }
                        if (!c.cellHasTree)
                        {
                            int s = GameEnvironment.Random.Next(map.middleringRandomStone);
                            if (s == 0 && !c.CellCollidesWith(player.playerReach))
                            {
                                c.cellHasStone = true;
                                stones.Add(new Stone(c.Position, .5f));
                            }
                        }
                    }
                    if (c.Position.Y > -map.mapSizeY - 10 + 5 * map.cellSize && c.Position.Y < -map.mapSizeY + 10 * map.cellSize
                    && c.Position.X > -map.mapSizeX + 9 * map.cellSize && c.Position.X < GameEnvironment.Screen.X + map.mapSizeX - 10 * map.cellSize)
                    {
                        int r = GameEnvironment.Random.Next(map.middleringRandomTree);
                        if (r == 0 && !c.CellCollidesWith(player.playerReach))
                        {
                            c.cellHasTree = true;
                            trees.Add(new Tree(c.Position, .5f, 3));
                        }
                        if (!c.cellHasTree)
                        {
                            int s = GameEnvironment.Random.Next(map.middleringRandomStone);
                            if (s == 0 && !c.CellCollidesWith(player.playerReach))
                            {
                                c.cellHasStone = true;
                                stones.Add(new Stone(c.Position, .5f));
                            }
                        }
                    }
                    if (c.Position.Y > GameEnvironment.Screen.Y + map.mapSizeY - 11 * map.cellSize && c.Position.Y - 60 < GameEnvironment.Screen.Y + map.mapSizeY - 6 * map.cellSize
                        && c.Position.X > -map.mapSizeX + 9 * map.cellSize && c.Position.X < GameEnvironment.Screen.X + map.mapSizeX - 10 * map.cellSize)
                    {
                        int r = GameEnvironment.Random.Next(map.middleringRandomTree);
                        if (r == 0 && !c.CellCollidesWith(player.playerReach))
                        {
                            c.cellHasTree = true;
                            trees.Add(new Tree(c.Position, .5f, 3));
                        }
                        if (!c.cellHasTree)
                        {
                            int s = GameEnvironment.Random.Next(map.middleringRandomStone);
                            if (s == 0 && !c.CellCollidesWith(player.playerReach))
                            {
                                c.cellHasStone = true;
                                stones.Add(new Stone(c.Position, .5f));
                            }
                        }
                    }
                    #endregion

                    #region inner ring
                    if (c.Position.X > -map.mapSizeX + 10 * map.cellSize && c.Position.X < GameEnvironment.Screen.X + map.mapSizeX - 10 * map.cellSize
                    && c.Position.Y > -map.mapSizeY + 10 * map.cellSize && c.Position.Y < GameEnvironment.Screen.Y + map.mapSizeY - 10 * map.cellSize)
                    {
                        int r = GameEnvironment.Random.Next(map.innerringRandomTree);
                        if (r == 0 && !c.CellCollidesWith(player.playerReach))
                        {
                            c.cellHasTree = true;
                            trees.Add(new Tree(c.Position, .5f, 3));
                        }
                        if (!c.cellHasTree)
                        {
                            int s = GameEnvironment.Random.Next(map.innerringRandomStone);
                            if (s == 0 && !c.CellCollidesWith(player.playerReach))
                            {
                                c.cellHasStone = true;
                                stones.Add(new Stone(c.Position, .5f));
                            }
                        }
                    }
                    #endregion
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

                //Play RoosterCrowing
                GameEnvironment.AssetManager.PlaySound(sounds.SEIs[6]);
                //Play PersonYawns
                //GameEnvironment.AssetManager.PlaySound(SEIs[5]);
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

        void CheckPlantsWater()
        {
            foreach (Cell c in cells.Children)
            {
                for (int i = plants.Children.Count - 1; i >= 0; i--)
                {
                    if (c.Position == plants.Children[i].Position)
                    {
                        if (c.cellHasWater)
                        {
                            (plants.Children[i] as Plant).soilHasWater = true;
                        }
                        if (!c.cellHasWater)
                        {
                            (plants.Children[i] as Plant).soilHasWater = false;
                        }
                    }
                }
            }
        }

        void CameraSystem(InputHelper inputHelper)
        {
            Vector2 moveVector = Vector2.Zero;

            if (!inputHelper.IsKeyDown(Keys.A) && !inputHelper.IsKeyDown(Keys.S) && !inputHelper.IsKeyDown(Keys.D) && !inputHelper.IsKeyDown(Keys.W))
            {
                GameEnvironment.AssetManager.StopSound(sounds.SEIs[0]);
            }
            else
            {
                GameEnvironment.AssetManager.PlayOnce(sounds.SEIs[0]);
            }
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
                    SetPreviousPosition();
                }
            }

            foreach (Cliff c in cliff.Children)
            {
                foreach (RotatingSpriteGameObject r in c.Children)
                {
                    if (r.CollidesWith(player))
                    {
                        SetPreviousPosition();
                    }
                }
            }

            if (player.sleepingPosition)
            {
                cells.Position = prevPos - player.newSleepingPosition;
                trees.Position = prevPos - player.newSleepingPosition;
                stones.Position = prevPos - player.newSleepingPosition;
                sprinklers.Position = prevPos - player.newSleepingPosition;
                plants.Position = prevPos - player.newSleepingPosition;
                tent.Position = prevPos - player.newSleepingPosition;
                cliff.Position = prevPos - player.newSleepingPosition;
                borderGrass.Position = prevPos - player.newSleepingPosition;
                shopPC.Position = prevPos - player.newSleepingPosition;
                player.sleepingPosition = false;
            }

            if (player.sleeping)
            {
                SetPreviousPosition();
            }

            if ((tent.Children[0] as Tent).CollidesWith(player))
            {
                SetPreviousPosition();
            }

            for (int i = stones.Children.Count - 1; i >= 0; i--)
            {
                if ((stones.Children[i] as Stone).CollidesWith(player))
                {
                    SetPreviousPosition();
                }
            }

            for (int i = sprinklers.Children.Count - 1; i >= 0; i--)
            {
                if ((sprinklers.Children[i] as SprinklerObject).CollidesWith(player))
                {
                    SetPreviousPosition();
                }
            }

            if ((shopPC.Children[0] as ShopPC).CollidesWith(player))
            {
                SetPreviousPosition();
            }

            prevPos = cells.Position;
            borderGrass.Position += moveVector;
            cliff.Position += moveVector;
            tent.Position += moveVector;
            cells.Position += moveVector;
            trees.Position += moveVector;
            stones.Position += moveVector;
            sprinklers.Position += moveVector;
            plants.Position += moveVector;
            shopPC.Position += moveVector;
        }

        void SetPreviousPosition()
        {
            cells.Position = prevPos;
            trees.Position = prevPos;
            stones.Position = prevPos;
            sprinklers.Position = prevPos;
            plants.Position = prevPos;
            tent.Position = prevPos;
            cliff.Position = prevPos;
            borderGrass.Position = prevPos;
            shopPC.Position = prevPos;
        }

        void CheckHoeInput(InputHelper inputHelper)
        {
            foreach (Cell c in cells.Children)
            {
                if (c.CellCollidesWith(MouseGO.HitBox) && c.CellCollidesWith(player.playerReach))
                {
                    if (inputHelper.MouseLeftButtonDown())
                    {
                        if (itemList.itemSelected == "HOE" && !c.cellIsTilled && !c.cellHasTent && !c.cellHasTree && !c.cellHasSprinkler && !c.cellHasStone && !c.cellHasShop)
                        {
                            if (!tutorialStepList.step1completed && tutorialStepList.step == 1)
                            {
                                tutorialStepList.step += 1;
                                tutorialStepList.step1completed = true;
                            }
                            //Play HittingGround
                            GameEnvironment.AssetManager.PlaySound(sounds.SEIs[8]);

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
                if (c.CellCollidesWith(MouseGO.HitBox) && c.CellCollidesWith(player.playerReach))
                {
                    if (inputHelper.MouseLeftButtonDown())
                    {
                        foreach (Item item in itemList.Children)
                        {
                            if (item is Seed)
                            {
                                if (itemList.itemSelected == "SEED" && c.cellIsTilled && !c.cellHasPlant && item.itemAmount > 0 && !c.cellHasSprinkler)
                                {
                                    if (!tutorialStepList.step2completed && tutorialStepList.step == 2)
                                    {
                                        tutorialStepList.step += 1;
                                        tutorialStepList.step2completed = true;
                                    }

                                    //Play Shakking1
                                    GameEnvironment.AssetManager.PlaySound(sounds.SEIs[9]);

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
                if (c.CellCollidesWith(MouseGO.HitBox) && c.CellCollidesWith(player.playerReach) && !c.CellCollidesWith(player))
                {
                    if (inputHelper.MouseLeftButtonDown())
                    {
                        foreach (Item item in itemList.Children)
                        {
                            if (item is Sprinkler)
                            {
                                if (itemList.itemSelected == "SPRINKLER" && !c.cellHasPlant && !c.cellHasTent && !c.cellHasTree && !c.cellHasSprinkler && item.itemAmount > 0 && !c.cellHasStone && !c.cellHasShop)
                                {
                                    item.itemAmount -= 1;
                                    c.cellHasSprinkler = true;
                                    energyBar.percentageLost += energyBar.oneUse;
                                    sprinklers.Add(new SprinklerObject(c.Position, 1));
                                    //Play WaterSplash
                                    GameEnvironment.AssetManager.PlaySound(sounds.SEIs[7]);
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
                if (c.CellCollidesWith(MouseGO.HitBox) && c.CellCollidesWith(player.playerReach))
                {
                    if (inputHelper.MouseLeftButtonPressed())
                    {
                        if (itemList.itemSelected == "WATERINGCAN" && c.cellIsTilled && !c.cellHasWater)
                        {
                            if (!tutorialStepList.step3completed && tutorialStepList.step == 3)
                            {
                                tutorialStepList.step += 1;
                                tutorialStepList.step3completed = true;
                            }
                            //Play WaterSplash
                            GameEnvironment.AssetManager.PlaySound(sounds.SEIs[4]);

                            c.cellHasWater = true;
                            c.ChangeSpriteTo(2);
                        }
                    }
                }
            }
        }

        void CheckTreeSeedInput(InputHelper inputHelper)
        {
            foreach (Cell c in cells.Children)
            {
                if (c.CellCollidesWith(MouseGO.HitBox) && c.CellCollidesWith(player.playerReach) && !c.CellCollidesWith(player))
                {
                    if (inputHelper.MouseLeftButtonDown())
                    {
                        foreach (Item item in itemList.Children)
                        {
                            for (int i = trees.Children.Count - 1; i >= 0; i--)
                            {
                                if (item is TreeSeed)
                                {
                                    if (itemList.itemSelected == "TREESEED" && !c.cellIsTilled && !c.cellHasTent && !c.cellHasPlant && item.itemAmount > 0 && !c.cellHasTree && !c.cellHasSprinkler && !c.cellHasStone && !c.cellHasShop)
                                    {
                                        GameEnvironment.AssetManager.PlayOnce(sounds.SEIs[13]);
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
                if (s.CollidesWith(MouseGO.HitBox) && s.CollidesWith(player.playerReach))
                {
                    if (inputHelper.MouseLeftButtonPressed())
                    {
                        if (itemList.itemSelected == "PICKAXE" && !(stones.Children[i] as Stone).stoneHit && (stones.Children[i] as Stone)._sprite == 1)
                        {
                            //play PickaxeSwing
                            GameEnvironment.AssetManager.PlaySound(sounds.SEIs[2]);

                            (stones.Children[i] as Stone).stoneHit = true;
                            (stones.Children[i] as Stone).hitTimer = (stones.Children[i] as Stone).hitTimerReset;
                            (stones.Children[i] as Stone).health -= 1;
                            energyBar.percentageLost += energyBar.oneUse;
                            if ((stones.Children[i] as Stone).health <= 0)
                            {
                                if (!tutorialStepList.step4completed && tutorialStepList.step == 4)
                                {
                                    tutorialStepList.step += 1;
                                    tutorialStepList.step4completed = true;
                                }
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
                                        int randomAddition = GameEnvironment.Random.Next(2, 5);
                                        item.itemAmount += randomAddition;
                                        ConvertFromHotbarToMoney(item, randomAddition);
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
                if ((trees.Children[i] as Tree).CollidesWith(MouseGO.HitBox) && (trees.Children[i] as Tree).CollidesWith(player.playerReach))
                {
                    if (inputHelper.MouseLeftButtonPressed())
                    {
                        if (itemList.itemSelected == "AXE" && !(trees.Children[i] as Tree).treeHit && (trees.Children[i] as Tree).growthStage == 3)
                        {
                            GameEnvironment.AssetManager.PlaySound(sounds.SEIs[1]);
                            (trees.Children[i] as Tree).treeHit = true;
                            (trees.Children[i] as Tree).hitTimer = (trees.Children[i] as Tree).hitTimerReset;
                            (trees.Children[i] as Tree).health -= 1;
                            energyBar.percentageLost += energyBar.oneUse;
                            if ((trees.Children[i] as Tree).health <= 0)
                            {
                                if (!tutorialStepList.step4completed && tutorialStepList.step == 4)
                                {
                                    tutorialStepList.step += 1;
                                    tutorialStepList.step4completed = true;
                                }
                                (trees.Children[i] as Tree).treeHit = false;
                                foreach (Cell c in cells.Children)
                                {
                                    if (c.Position == (trees.Children[i] as Tree).Position)
                                    {
                                        c.cellHasTree = false;
                                    }
                                }
                                trees.Remove(trees.Children[i]);

                                //play TreeFalling
                                GameEnvironment.AssetManager.PlaySound(sounds.SEIs[3]);

                                foreach (Item item in itemList.Children)
                                {
                                    if (item is Wood)
                                    {
                                        int randomAddition = GameEnvironment.Random.Next(3, 7);
                                        item.itemAmount += randomAddition;
                                        ConvertFromHotbarToMoney(item, randomAddition);
                                    }
                                    if (item is TreeSeed)
                                    {
                                        int randomAddition = GameEnvironment.Random.Next(2);
                                        item.itemAmount += randomAddition;
                                        ConvertFromHotbarToMoney(item, randomAddition);
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
                if (c.CellCollidesWith(MouseGO.HitBox) && c.CellCollidesWith(player.playerReach))
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
                                        if (!tutorialStepList.step6completed && tutorialStepList.step == 6)
                                        {
                                            tutorialStepList.step += 1;
                                            tutorialStepList.step6completed = true;
                                        }
                                        foreach (Item item in itemList.Children)
                                        {
                                            if (item is Wheat)
                                            {
                                                int randomAddition = GameEnvironment.Random.Next(1, 3);
                                                item.itemAmount += randomAddition;
                                                ConvertFromHotbarToMoney(item, randomAddition);
                                            }
                                            if (item is Seed)
                                            {
                                                int randomAddition = GameEnvironment.Random.Next(1, 3);
                                                item.itemAmount += randomAddition;
                                                ConvertFromHotbarToMoney(item, randomAddition);
                                            }
                                        }
                                        c.cellHasPlant = false;
                                        plants.Remove(plants.Children[i]);
                                        //Play WheatPickup
                                        GameEnvironment.AssetManager.PlaySound(sounds.SEIs[11]);
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
            //keyboard input
            #region keyboard input
            if (inputHelper.KeyPressed(Keys.D1))
            {
                itemList.itemSelected = "HOE";
                hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[0].Position;
            }
            else if (inputHelper.KeyPressed(Keys.D2))
            {
                itemList.itemSelected = "AXE";
                hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[1].Position;
            }
            else if (inputHelper.KeyPressed(Keys.D3))
            {
                itemList.itemSelected = "PICKAXE";
                hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[2].Position;
            }
            else if (inputHelper.KeyPressed(Keys.D4))
            {
                itemList.itemSelected = "WATERINGCAN";
                hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[3].Position;
            }
            else if (inputHelper.KeyPressed(Keys.D5))
            {
                itemList.itemSelected = "SEED";
                hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[4].Position;
            }
            else if (inputHelper.KeyPressed(Keys.D6))
            {
                itemList.itemSelected = "TREESEED";
                hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[5].Position;
            }
            else if (inputHelper.KeyPressed(Keys.D7))
            {
                itemList.itemSelected = "SPRINKLER";
                hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[6].Position;
            }
            else if (inputHelper.KeyPressed(Keys.D8))
            {
                itemList.itemSelected = "WOOD";
                hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[7].Position;
            }
            else if (inputHelper.KeyPressed(Keys.D9))
            {
                itemList.itemSelected = "ROCK";
                hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[8].Position;
            }
            else if (inputHelper.KeyPressed(Keys.D0))
            {
                itemList.itemSelected = "WHEAT";
                hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[9].Position;
            }
            #endregion


            //mouse input
            #region mouse input
            if (inputHelper.MouseLeftButtonPressed())
            {
                if (MouseGO.CollidesWith(hotbar.hotbarSquares.Children[0] as SpriteGameObject))
                {
                    itemList.itemSelected = "HOE";
                    hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[0].Position;
                }
                else if (MouseGO.CollidesWith(hotbar.hotbarSquares.Children[1] as SpriteGameObject))
                {
                    itemList.itemSelected = "AXE";
                    hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[1].Position;
                }
                else if (MouseGO.CollidesWith(hotbar.hotbarSquares.Children[2] as SpriteGameObject))
                {
                    itemList.itemSelected = "PICKAXE";
                    hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[2].Position;
                }
                else if (MouseGO.CollidesWith(hotbar.hotbarSquares.Children[3] as SpriteGameObject))
                {
                    itemList.itemSelected = "WATERINGCAN";
                    hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[3].Position;
                }
                else if (MouseGO.CollidesWith(hotbar.hotbarSquares.Children[4] as SpriteGameObject))
                {
                    itemList.itemSelected = "SEED";
                    hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[4].Position;
                }
                else if (MouseGO.CollidesWith(hotbar.hotbarSquares.Children[5] as SpriteGameObject))
                {
                    itemList.itemSelected = "TREESEED";
                    hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[5].Position;
                }
                else if (MouseGO.CollidesWith(hotbar.hotbarSquares.Children[6] as SpriteGameObject))
                {
                    itemList.itemSelected = "SPRINKLER";
                    hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[6].Position;
                }
                else if (MouseGO.CollidesWith(hotbar.hotbarSquares.Children[7] as SpriteGameObject))
                {
                    itemList.itemSelected = "WOOD";
                    hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[7].Position;
                }
                else if (MouseGO.CollidesWith(hotbar.hotbarSquares.Children[8] as SpriteGameObject))
                {
                    itemList.itemSelected = "ROCK";
                    hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[8].Position;
                }
                else if (MouseGO.CollidesWith(hotbar.hotbarSquares.Children[9] as SpriteGameObject))
                {
                    itemList.itemSelected = "WHEAT";
                    hotbar.selectedSquare.Position = hotbar.hotbarSquares.Children[9].Position;
                }
            }
            #endregion
        }
    }
}
