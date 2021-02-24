using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject

{
    class PlayingState : GameState
    {
        public PlayingState()
        {
            //gameObjectList.Add(new GameObject("spr_background"));
            gameObjectList.Add(new Player());
        }
    }
}
