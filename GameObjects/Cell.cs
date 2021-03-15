using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BaseProject
{
    class Cell : SpriteGameObject
    {
        public int cellID;
        public bool soilHasPlant = false;
         
        public Cell(SpriteSheet test, Vector2 _position, float _scale, int _id) : base(test)
        {
            scale = _scale;
            position = _position;
            cellID = _id;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            base.Draw(gameTime, spriteBatch);
        }
    }
}
