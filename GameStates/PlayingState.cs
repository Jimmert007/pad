using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using HarvestValley.GameObjects;

namespace HarvestValley.GameStates
{
    class PlayingState : GameObjectList
    {
        UIList uIList;
        Executer exec;
        public PlayingState()
        {
            Add(uIList = new UIList());
            Add(exec = new Executer());
        }
    }
}
