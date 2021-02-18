using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    class GameObject
    {
        Vector2 position;
        Vector2 velocity;
        Texture2D texture;

        public GameObject(string assetName)
        {
            //texture = Global.content.Load<Texture2D>(assetName);
            Start();
        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public void Draw()
        {
            //Global.spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
