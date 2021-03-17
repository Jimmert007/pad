using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace HarvestValley
{
    class Tilling : SpriteGameObject
    {
        //Jim van de Burgwal

        //creating variables

        public String item = "SEED";
        public SpriteSheet tilledSoilTexture;


        public Tilling(string _assetName, int _x, int _y, int _w, int _h) : base(_assetName)
        {
            position.X = _x;
            position.Y = _y;
            /*size.X = _w;
            size.Y = _h;*/
            tilledSoilTexture = new SpriteSheet("spr_tilled_soil");
        }
    }
}
