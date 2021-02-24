﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BaseProject
{
    class PlayingState : GameState
    {
        Map map;
        public PlayingState()
        {
            map = new Map("1px", new Vector2(0, 0), new Vector2(50, 50));
            gameObjectList.Add(map);
            for (int i = 0; i < map.cols; i++)
            {
                for (int x = 0; x < map.rows; x++)
                {
                    Cell newCell = new Cell("tiles/Grassland 2 Color 2@128x128", new Vector2(map.size.X * x, map.size.Y * i), map.size);
                    map.cells.Add(newCell);
                    gameObjectList.Add(newCell);
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
