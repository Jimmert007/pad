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

        public Tilling(string _assetName, Vector2 _position, float _scale) : base(_assetName)
        {
            position = _position;
            scale = _scale;
            tilledSoilTexture = new SpriteSheet("spr_tilled_soil");
        }
    }
}
