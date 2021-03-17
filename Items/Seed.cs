using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    class Seed : Item
    {
        public Seed(string _assetName, bool stackable, int startItemAmount) : base(_assetName, stackable, startItemAmount)
        {
        }
    }
}
