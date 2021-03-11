using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace BaseProject
{
    class Tilling : GameObject
    {
        //Jim van de Burgwal

        //creating variables
        public Texture2D tilledSoilTexture;


        public Tilling(string _assetName, int _x, int _y, int _w, int _h) : base(_assetName)
        {
            position.X = _x;
            position.Y = _y;
            size.X = _w;
            size.Y = _h;
            tilledSoilTexture = GameEnvironment.ContentManager.Load<Texture2D>("spr_tilled_soil");
        }
    }
}
