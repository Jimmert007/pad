using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseProject
{
    class PlayingState : GameObjectList
    {
        Map map;

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
        }
    }
}
