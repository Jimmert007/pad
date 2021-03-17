using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject
{
    class SpriteGameObject : GameObject
    {
        public SpriteGameObject(String assetName) : base("tree")
        {
            if (assetName.Length > 0)
                texture = GameEnvironment.ContentManager.Load<Texture2D>(assetName);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}

