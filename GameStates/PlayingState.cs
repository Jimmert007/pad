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
 

        public PlayingState()
        {
            map = new Map(new Vector2(102.4f, 102.4f));
            Add(map.cells);
            SpriteSheet mapSpriteSheet = new SpriteSheet("tiles/Niels/cutOutTilesNiels@3x4", 0);
            for (int i = 0; i < map.rows; i++)
            {
                for (int x = 0; x < map.cols; x++)
                {
                    Cell c = new Cell(mapSpriteSheet, new Vector2(102.4f * x, 102.4f * i), .1f, x + (map.cols * i));
                    map.cells.Add(c);
                }
            }

            plants = new GameObjectList();
            for (int i = 1; i < map.rows - 1; i++)
            {
                for (int x = 1; x < map.cols - 1; x++)
                {
                    Plant p = new Plant("spr_empty", 0, 0, 102.4f, 102.4f, 1f);
                    plants.Add(p);
                }
            }
            Debug.WriteLine("aantal planten " + plants.Children.Count);



            player = new Player("jorrit", new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2), .1f);
            Add(player);
        }
    }
}
