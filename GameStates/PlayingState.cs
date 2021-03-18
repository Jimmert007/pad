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
        Hoe hoe;

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
            Add(plants);
            for (int i = 0; i < map.rows; i++)
            {
                for (int x = 0; x < map.cols; x++)
                {
                    Plant p = new Plant("spr_soil", new Vector2(102.4f * x, 102.4f * i), .4f);
                    plants.Add(p);
                }
            }
            Debug.WriteLine("aantal planten " + plants.Children.Count);

            player = new Player("jorrit", new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2), .2f);
            Add(player);

            tilling = new Tilling("spr_soil", new Vector2(100, 100), .1f);
            Add(tilling);

            hoe = new Hoe("spr_hoe", new Vector2(GameEnvironment.Screen.X / 2 - 25, GameEnvironment.Screen.Y / 2 - 75), .1f);
            Add(hoe);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Cell cell in map.cells.Children)
            {
                if (player.Position.X > cell.Position.X && player.Position.X + player.Width < cell.Position.X + cell.Width && player.Position.Y > cell.Position.Y && player.Position.Y + player.Height < cell.Position.Y + cell.Height)
                {
                    player.current = cell;
                }
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Cell c = map.cells.Children[player.current.cellID] as Cell;
                c.Visible = false;
                map.cells.Children[player.current.cellID] = c;
            }

            base.Update(gameTime);
        }
    }
}
