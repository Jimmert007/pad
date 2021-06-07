using Microsoft.Xna.Framework;
using System;

namespace HarvestValley
{
    class Tilling : SpriteGameObject
    {
        //Jim van de Burgwal

        //creating variables

        public String item = "SEED";
        public string tilled = "tiles/spr_tilled_soil";

        public Tilling(string _assetName, Vector2 _position, float _scale) : base(_assetName)
        {
            position = _position;
            Scale = _scale;
            sprite = new SpriteSheet(tilled);
        }
    }
}
