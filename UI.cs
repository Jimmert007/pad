using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace BaseProject
{
    class UI : GameObject
    {
        public bool playerDescision, UIActive = false;

        public UI(string _assetName) : base( _assetName)
        {
        }
    }
}
