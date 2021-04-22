using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HarvestValley
{
    class Cell : SpriteGameObject
    {
        public int cellID, randomGrass;
        private int _sheetIndex;
        private bool _mirror;
        public static SpriteGameObject TILESOIL = new SpriteGameObject("tiles/spr_tilled_soil");
        public static SpriteGameObject TILESOILWATER = new SpriteGameObject("tiles/spr_tilled_soil_water");
        public static SpriteGameObject GRASS = new SpriteGameObject("tiles/spr_grass");
        public bool cellIsTilled, cellHasPlant, cellHasTree, cellHasWater, cellHasSprinkler, nextRandom, nextToSprinkler, cellHasStone;

        public Cell(SpriteSheet _sprite, Vector2 _position, float _scale, int _id) : base(_sprite)
        {
            /* TILESOIL = new SpriteGameObject("spr_tilled_soil");*/
            scale = _scale;
            position = _position;
            cellID = _id;
            _mirror = false;
            randomGrass = GameEnvironment.Random.Next(4);
            //Debug.WriteLine(cellID);
            //Debug.WriteLine(Position);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
           
            if (nextRandom)
            {
                randomGrass = GameEnvironment.Random.Next(4);
                nextRandom = false;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sprite.SheetIndex = _sheetIndex;
            sprite.Mirror = _mirror;
            sprite.Draw(spriteBatch, Position, origin, scale);
        }

        public void ChangeSpriteTo(SpriteGameObject SGO)
        {
            sprite = SGO.Sprite;
        }

        public void ChangeSpriteTo(SpriteGameObject SGO, float _scale)
        {
            scale = _scale;
            sprite = SGO.Sprite;
        }
    }
}