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
            gameObjectList.Add(new GameObject("test"));
            map = new Map("test", new Vector2(0, 0), new Vector2(10,10));
            gameObjectList.Add(map);
            for (int i = 0; i < map.cols; i++)
            {
                for (int x = 0; x < map.rows; x++)
                {
                    map.cells.Add(new Cell("test", new Vector2(map.texture.Width * x, map.texture.Height * i), new Vector2(10,10)));
                }
            }
            for (int i = 0; i < map.cells.Count; i++)
            {
                gameObjectList.Add(map.cells[i]);
            }
            Debug.Print(map.cells.Count.ToString());
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
