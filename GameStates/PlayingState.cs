using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BaseProject

{
    class PlayingState : GameObjectList
    {
        EnergyBar energyBar;
        Sleeping sleeping;
        GlobalTime globalTime;
        Player player;
        Map map;
        public GameObjectList cells;
        Tilling tilling;
        List<Plant> plants = new List<Plant>();
        Tools tools;
        Hoe hoe;

        public int ScreenWidth;
        public int ScreenHeight;
        Hotbar hotbar;
        float HotbarCount = 9;
        float HalfHotbar;

        public PlayingState()
        {
            energyBar = new EnergyBar("EnergyBarBackground", GameEnvironment.Screen.X - 60, GameEnvironment.Screen.Y - 220, 40, 200);
            player = new Player("jorrit", 0, 0, 100, 100);
            tools = new Tools("spr_empty");
            hoe = new Hoe("spr_hoe", GameEnvironment.Screen.X / 2 - 25, GameEnvironment.Screen.Y - 70, 50, 50);
            tilling = new Tilling("spr_soil", 0, 0, 100, 100);
            map = new Map(new Vector2(0, 0), new Vector2(50, 50));
            globalTime = new GlobalTime();
            sleeping = new Sleeping("spr_empty");
            Add(globalTime);

            Add(map);
            Add(new Cell("1px", new Vector2(map.size.X, map.size.Y), map.size, map.cols));

            /*for (int i = 0; i < map.cols; i++)
            {
                for (int x = 0; x < map.rows; x++)
                {
                    Cell newCell = new Cell("1px", new Vector2(map.size.X * i, map.size.Y * x), map.size, i + x * map.cols);
                    Add(newCell);
                    *//*Plant newPlant = new Plant("spr_empty", 0, 0, (int)map.size.X, (int)map.size.Y);
                    plants.Add(newPlant);
                    Add(newPlant);*//*
                }
            }*/

            Add(player);

            Add(tools);
            Add(hoe);
            Add(tilling);

            ScreenWidth = GameEnvironment.Screen.X;
            ScreenHeight = GameEnvironment.Screen.Y;

            hotbar = new Hotbar("spr_empty");
            Add(hotbar);


            /*for (int i = 0; i < HotbarCount; i++)
            {

                SpriteGameObject hItem = new SpriteGameObject("spr_empty");

                hotbar.hotbarItemList.Add(hItem);
                Add(hItem);

                HalfHotbar = HotbarCount / 2 * hotbar.hotbarItemList[i].sprite.Width;

                hotbar.hotbarItemList[i].Position.X = ScreenWidth / 2 - HalfHotbar;
                hotbar.hotbarItemList[i].Position.X += 80 * i;
                hotbar.hotbarItemList[i].Position.Y = ScreenHeight - hotbar.hotbarItemList[i].sprite.Height;*//*
                //Debug.Print("X " + i + " = " + hotbar.hotbarItemList[i].position.X.ToString());
                //Debug.Print("Y " + i + " = " + hotbar.hotbarItemList[i].position.Y.ToString());
            }*/
            Add(energyBar);
            Add(sleeping);
            }

            public override void Update(GameTime gameTime)
            {
                SpriteGameObject mouseGO = new SpriteGameObject("1px");
                mouseGO.Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                /*for (int i = 0; i < map.cells.Count; i++)
                {
                    if (map.cells[i].CollidesWith(mouseGO))
                    {
                        if (player.PlayerCanReach())
                        {
                            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                            {
                                if (tilling.item == "SEED" && !map.cells[i].soilHasPlant)
                                {
                                    map.cells[i].soilHasPlant = true;
                                    plants[i].Position = map.cells[i].Position;
                                    //plants[i].size = map.cells[i].size;
                                    //plants[i].growthStage = 1;
                                    energyBar.percentageLost += energyBar.onePercent;
                                }
                                if (tools.toolSelected == "HOE")
                                {
                                    //map.cells[i].sourceRect = new Rectangle(0,0,tilling.sprite.Width,tilling.sprite.Height);
                                    //map.cells[i].sprite = tilling.tilledSoilTexture;
                                }

                            }
                            if (Mouse.GetState().RightButton == ButtonState.Pressed)
                            {
                                if (map.cells[i].soilHasPlant)
                                {
                                    if (plants[i].growthStage >= 4)
                                    {
                                        //(receive product and new seed)
                                        map.cells[i].soilHasPlant = false;
                                        plants[i].growthStage = 0;
                                    }
                                }
                            }
                        }
                    }

                }
                base.Update(gameTime);
                for (int i = 0; i < plants.Count; i++)
                {
                    if (sleeping.fadeOut)
                    {
                        energyBar.Reset();
                        if (map.cells[i].soilHasPlant && sleeping.fadeAmount >= 1f)
                        {
                            plants[i].growthStage++;
                        }
                    }
                }
                if (energyBar.passOut)
                {
                    sleeping.Sleep(globalTime);
                    sleeping.useOnce = false;
                }
                sleeping.Update(globalTime);
                globalTime.Update(gameTime);*/
            }
        }
    }
