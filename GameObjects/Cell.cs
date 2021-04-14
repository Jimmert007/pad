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
        public int cellID;
        private int _sheetIndex;
        private bool _mirror;
        public static SpriteGameObject TILESOIL = new SpriteGameObject("spr_tilled_soil");
        public bool cellIsTilled, cellHasPlant, cellHasTree;

        public Cell(SpriteSheet _sprite, Vector2 _position, float _scale, int _id) : base(_sprite)
        {
            /* TILESOIL = new SpriteGameObject("spr_tilled_soil");*/
            scale = _scale;
            position = _position;
            cellID = _id;
            _mirror = false;
            //Debug.WriteLine(cellID);
            //Debug.WriteLine(Position);

            if (cellID > 0 && cellID < 9)
            {
                _sheetIndex = 7; //bovenste rij bomen zonder hoeken
            }
            else if (cellID > 40 && cellID < 49)
            {
                _sheetIndex = 1; //onderste rij bomen zonder hoeken
            }
            else if (cellID % 10 == 0 && cellID > 9 && cellID < 40)
            {
                _sheetIndex = 5; //linker rij bomen zonder hoeken
            }
            else if (cellID % 10 == 9 && cellID < 40 && cellID > 9)
            {
                _sheetIndex = 3; //rechter rij bomen zonder hoeken
            }
            else if (cellID == 0) //TL
            {
                _sheetIndex = 11;
                _mirror = true;
            }
            else if (cellID == 9) //TR
            {
                _sheetIndex = 11;
            }
            else if (cellID == 40) //BL
            {
                _sheetIndex = 10;
                _mirror = true;
            }
            else if (cellID == 49) //BR
            {
                _sheetIndex = 10;
            }
            else if (cellID == 25 || cellID == 26)
            {
                _sheetIndex = 9;
                cellHasTree = true;
            }
            else
            {
                _sheetIndex = 9;
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