using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject
{
    class Camera : GameObject
    {
        private Matrix transform;
        public Matrix Transform => transform;

        private Vector2 centre;

      public  Camera(string _assetName) : base(_assetName)
        {}

        public void Update(GameTime gameTime, Player player)
        {
            centre = new Vector2(player.position.X + (player.size.X /2 - 300), player.position.Y + (player.size.Y / 2 - 250));
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
        }
    }
}
