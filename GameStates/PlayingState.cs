using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BaseProject

{
    class PlayingState : GameState
    {
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

        public PlayingState()
        {
            player = new Player("jorrit", 0, 0, 100, 100);
            tools = new Tools("spr_empty");
            hoe = new Hoe("spr_hoe", GameEnvironment.Screen.X / 2 - 25, GameEnvironment.Screen.Y - 70, 50, 50);
            tilling = new Tilling("spr_soil", 100, 100, 100, 100);
            map = new Map("1px", new Vector2(0, 0), new Vector2(50, 50));
            globalTime = new GlobalTime("test");
            sleeping = new Sleeping("test");
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
                    Plant newPlant = new Plant("spr_seed1_stage1", 0, 0, 0, 0);
                    plants.Add(newPlant);
                    gameObjectList.Add(newPlant);
                }
            }
            gameObjectList.Add(player);
            

            ScreenWidth = GameEnvironment.screen.X;
            ScreenHeight = GameEnvironment.screen.Y;

            //gameObjectList.Add(new GameObject("spr_background"));  
            //gameObjectList.Add(new Cell("test", new Vector2(10, 10), new Vector2(0, 0)));


            hotbar = new Hotbar("test");
            //gameObjectList.Add(hotbar);


            for (int i = 0; i < HotbarCount; i++)
            {

                GameObject hItem = new GameObject("test");

                hotbar.hotbarItemList.Add(hItem);
                gameObjectList.Add(hItem);

                HalfHotbar = HotbarCount / 2 * hotbar.hotbarItemList[i].texture.Width;

                hotbar.hotbarItemList[i].position.X = ScreenWidth / 2 - HalfHotbar;
                hotbar.hotbarItemList[i].position.X += 80 * i;
                hotbar.hotbarItemList[i].position.Y = ScreenHeight - hotbar.hotbarItemList[i].texture.Height;
                /*                                                                                                     Debug.Print("X " + i + " = " + hotbar.hotbarItemList[i].position.X.ToString());
                                                                                                                       Debug.Print("Y " + i + " = " + hotbar.hotbarItemList[i].position.Y.ToString());
                */
            }
            gameObjectList.Add(sleeping);
        }



        public override void Update(GameTime gameTime)
        {
            GameObject mouseGO = new GameObject("test");
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
                            }
                            if (tools.toolSelected == "HOE")
                            {
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
                    if (map.cells[i].soilHasPlant && sleeping.fadeAmount >= 1f)
                    {
                        plants[i].growthStage++;
                    }
                }
               
            }
            sleeping.Update(globalTime, tilling);
            globalTime.Update(gameTime);
        }
    }
}
