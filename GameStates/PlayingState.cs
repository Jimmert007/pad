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

namespace HarvestValley
{
    class PlayingState : GameObjectList
    {
        Map map;
        Player player;
        GameObjectList plants;
        Tilling tilling;
        Tools tools;
        Hoe hoe;
        EnergyBar energyBar;
        Sleeping sleeping;
        GlobalTime globalTime;

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
            Debug.WriteLine("aantal planten " + plants.Children.Count);

            player = new Player("jorrit", new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2), .2f);
            Add(player);

            tilling = new Tilling("spr_soil", new Vector2(100, 100), .1f);
            Add(tilling);

            tools = new Tools("spr_empty");
            Add(tools);

            hoe = new Hoe("spr_hoe", new Vector2(GameEnvironment.Screen.X / 2 - 25, GameEnvironment.Screen.Y / 2 - 75), .1f);
            Add(hoe);

            energyBar = new EnergyBar("EnergyBarBackground", GameEnvironment.Screen.X - 60, GameEnvironment.Screen.Y - 220, 40, 200);
            Add(energyBar);

            sleeping = new Sleeping("spr_empty");
            Add(sleeping);

            globalTime = new GlobalTime();
            Add(globalTime);
        }

        public override void Update(GameTime gameTime)
        {
            SpriteGameObject mouseGO = new SpriteGameObject("spr_empty");
            mouseGO.Position = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);

            foreach (Plant p in plants.Children)
            {
                if (p.CollidesWith(mouseGO))
                {
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        if (tilling.item == "SEED" && !p.soilHasPlant)
                        {
                            p.soilHasPlant = true;
                            p.growthStage = 1;
                            //energyBar.percentageLost += energyBar.onePercent;

                        }
                        /*if (tools.toolSelected == "HOE")
                        {
                            p.texture = tilling.tilledSoilTexture;
                        }*/

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
                if (sleeping.fadeOut)
                {
                    energyBar.Reset();
                    if (p.soilHasPlant && sleeping.fadeAmount >= 1f)
                    {
                        p.growthStage++;
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
            base.Update(gameTime);
        }
    }
}

