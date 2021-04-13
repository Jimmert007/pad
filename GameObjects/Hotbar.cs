using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HarvestValley.GameObjects
{
    class Hotbar : SpriteGameObject
    {
        public List<GameObject> hotbarItemList;
        public SpriteGameObject hotbar, selectedSquare;
        public int squareSize;
        public int HBWidth;
        public Vector2 selectedSquarePosition;
        public Hotbar(string _assetName) : base(_assetName)
        {
            hotbarItemList = new List<GameObject>();
            hotbar = new SpriteGameObject("spr_hotbar");
            selectedSquare = new SpriteGameObject("spr_selected_square");
            sprite = hotbar.Sprite;

            HBWidth = 450;
            squareSize = HBWidth / 9;
            hotbar.Scale = 1f;
            position.X = GameEnvironment.Screen.X * .5f - hotbar.Sprite.Width * .5f;
            position.Y = GameEnvironment.Screen.Y - hotbar.Sprite.Height;
            selectedSquarePosition.X = position.X;
            selectedSquarePosition.Y = position.Y;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            selectedSquare.scale = 100;
        }
    }
}