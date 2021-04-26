using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HarvestValley.GameObjects
{
    class Executer : GameObjectList
    {
        public Executer()
        {
            Add(new SpriteGameObject("1px"));
        }

        public static string Sleep()
        {
            return("i am the sleep");
        }
    }
}
