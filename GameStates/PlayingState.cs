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
    class PlayingState : GameState
    {
        EnergyBar energyBar;
        Sleeping sleeping;
        GlobalTime globalTime;
        Player player;
        Map map;
        Tilling tilling;
        List<Plant> plants = new List<Plant>();
        Tools tools;
        Hoe hoe;

        public int ScreenWidth;
        public int ScreenHeight;
        Hotbar hotbar;
        float HotbarCount = 9;
        float HalfHotbar;
        float vak;

        public PlayingState()
        {
            energyBar = new EnergyBar("EnergyBarBackground", GameEnvironment.Screen.X - 60, GameEnvironment.Screen.Y - 220, 40, 200);
            player = new Player("jorrit", 0, 0, 100, 100);
            tools = new Tools("spr_empty");
            hoe = new Hoe("spr_hoe", GameEnvironment.Screen.X / 2 - 25, GameEnvironment.Screen.Y - 70, 50, 50);
            tilling = new Tilling("spr_soil", 0, 0, 100, 100);
            map = new Map("1px", new Vector2(0, 0), new Vector2(50, 50));
            globalTime = new GlobalTime("spr_empty");
            sleeping = new Sleeping("spr_empty");
            gameObjectList.Add(globalTime);
            
            gameObjectList.Add(map);
            gameObjectList.Add(tools);
            gameObjectList.Add(hoe);
            gameObjectList.Add(tilling);

            for (int i = 0; i < map.cols; i++)
            {
                for (int x = 0; x < map.rows; x++)
                {
                    Cell newCell = new Cell("1px", new Vector2(map.size.X * i, map.size.Y * x), map.size, i + x * map.cols);
                    map.cells.Add(newCell);
                    gameObjectList.Add(newCell);
                    Plant newPlant = new Plant("spr_empty", 0, 0, (int)map.size.X, (int)map.size.Y);
                    plants.Add(newPlant);
                    gameObjectList.Add(newPlant);
                }
            }
            gameObjectList.Add(player);
            

            ScreenWidth = GameEnvironment.screen.X;
            ScreenHeight = GameEnvironment.screen.Y;

            //gameObjectList.Add(new GameObject("spr_background"));  
            //gameObjectList.Add(new Cell("test", new Vector2(10, 10), new Vector2(0, 0)));


            hotbar = new Hotbar("1px");
            gameObjectList.Add(hotbar);


           
            gameObjectList.Add(energyBar);
            gameObjectList.Add(sleeping);

        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < HotbarCount; i++)
            {
                GameObject hItem = new GameObject("1px");
                hItem.size.X = 45;
                hItem.size.Y = 45;
                vak = hItem.size.X + 5;

                hotbar.hotbarItemList.Add(hItem);
                gameObjectList.Add(hItem);

                hotbar.hotbarItemList[i].position.X = hotbar.position.X + 5;
                hotbar.hotbarItemList[i].position.X += vak * i ;
                hotbar.hotbarItemList[i].position.Y = ScreenHeight - hItem.size.Y;
               
            }

            GameObject mouseGO = new GameObject("spr_empty");
            mouseGO.position.X = GameEnvironment.MouseState.X;
            mouseGO.position.Y = GameEnvironment.MouseState.Y;
            for (int i = 0; i < map.cells.Count; i++)
            {
                if (map.cells[i].Overlaps(mouseGO))
                {
                    if (player.PlayerCanReach())
                    {
                        if (GameEnvironment.MouseState.LeftButton == ButtonState.Pressed)
                        {
                            if (tilling.item == "SEED" && !map.cells[i].soilHasPlant)
                            {
                                map.cells[i].soilHasPlant = true;
                                plants[i].position = map.cells[i].position;
                                plants[i].size = map.cells[i].size;
                                plants[i].growthStage = 1;
                                energyBar.percentageLost += energyBar.onePercent;
                            }
                            if (tools.toolSelected == "HOE")
                            {
                                map.cells[i].sourceRect = new Rectangle(0,0,tilling.texture.Width,tilling.texture.Height);
                                map.cells[i].texture = tilling.tilledSoilTexture;
                            }
                            
                        }
                        if (GameEnvironment.MouseState.RightButton == ButtonState.Pressed)
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
            globalTime.Update(gameTime);

        }
    }
}
