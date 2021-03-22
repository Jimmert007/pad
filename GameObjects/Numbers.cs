using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace BaseProject
{
    class Numbers : GameObject
    {
        public List<Item> numbers = new List<Item>();
        
        //public Texture2D hotbar;
        public Numbers(string _assetName) : base(_assetName)
        {
            //hotbar = GameEnvironment.ContentManager.Load<Texture2D>("spr_hotbar");
        }
    }
    #region Alle cijfers een class geven
    class C0 : Item
    {
        public C0(string _assetName ) : base(_assetName)
        {
        }
    }
    class C1 : Item
    {
        public C1(string _assetName ) : base(_assetName)
        {
        }
    }
    class C2 : Item
    {
        public C2(string _assetName ) : base(_assetName)
        {
        }
    }
    class C3 : Item
    {
        public C3(string _assetName ) : base(_assetName)
        {
        }
    }
    class C4 : Item
    {
        public C4(string _assetName ) : base(_assetName)
        {
        }
    }
    class C5 : Item
    {
        public C5(string _assetName ) : base(_assetName)
        {
        }
    }
    class C6 : Item
    {
        public C6(string _assetName ) : base(_assetName)
        {
        }
    }
    class C7 : Item
    {
        public C7(string _assetName ) : base(_assetName)
        {
        }
    }
    class C8 : Item
    {
        public C8(string _assetName ) : base(_assetName)
        {
        }
    }
    class C9 : Item
    {
        public C9(string _assetName ) : base(_assetName)
        {
        }
    }
    #endregion
}
