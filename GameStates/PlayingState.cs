using System;
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
            map = new Map("1px", new Vector2(0, 0), new Vector2(100, 100));
            gameObjectList.Add(map);
            for (int i = 0; i < map.cols; i++)
            {
                for (int x = 0; x < map.rows; x++)
                {
                    Cell newCell = new Cell("1px", new Vector2(map.size.X * i, map.size.Y * x), map.size, i + x * map.cols);
                    map.cells.Add(newCell);
                    gameObjectList.Add(newCell);
                }
            }
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
                    Debug.WriteLine(map.cells[i].id);
                }
            }
            base.Update(gameTime);
        }
    }
}
