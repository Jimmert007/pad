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
        Tools tools;
        Hoe hoe;
        Tilling tilling;
        Plant plant;
        public PlayingState()
        {
            tools = new Tools("spr_empty");
            hoe = new Hoe("spr_hoe", GameEnvironment.Screen.X / 2 - 25, GameEnvironment.Screen.Y - 70, 50, 50);
            tilling = new Tilling("spr_soil", 100, 100, 100, 100);
            plant = new Plant("spr_seed1_stage1", 100, 100, 100, 100);
            //gameObjectList.Add(new GameObject("spr_background"));  
            gameObjectList.Add(new Cell("test", new Vector2(10, 10), new Vector2(0, 0)));
            gameObjectList.Add(tools);
            gameObjectList.Add(hoe);
            gameObjectList.Add(tilling);
            gameObjectList.Add(plant);
            tilling.plant = plant;
            tilling.tools = tools;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
