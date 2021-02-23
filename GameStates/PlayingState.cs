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
        public PlayingState()
        {
            float scale = .5f; //50% smaller
            //gameObjectList.Add(new GameObject("spr_background"));  
            //gameObjectList.Add(new Cell("test", new Vector2(10, 10), new Vector2(0, 0)));
            gameObjectList.Add(new Hotbar("test",   new Vector2(0, 0)));
           
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
