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
        Tilling tilling;
        Plant plant;
        public PlayingState()
        {
            tilling = new Tilling("spr_soil", 100, 100, 100, 100);
            plant = new Plant("spr_seed1_stage1", 100, 100, 100, 100);
            //gameObjectList.Add(new GameObject("spr_background"));  
            gameObjectList.Add(new Cell("test", new Vector2(10, 10), new Vector2(0, 0)));
            gameObjectList.Add(tilling);
            gameObjectList.Add(plant);
            tilling.plant = plant;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
