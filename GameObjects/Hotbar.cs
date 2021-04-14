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
           // selectedSquare.Origin = selectedSquare.Sprite.Center;
            sprite = hotbar.Sprite;

            HBWidth = sprite.Width;
            squareSize = HBWidth / 9;
            position.X = GameEnvironment.Screen.X * .5f - hotbar.Sprite.Width * .5f;
            position.Y = GameEnvironment.Screen.Y - hotbar.Sprite.Height;
            selectedSquarePosition.X = position.X;
            selectedSquarePosition.Y = position.Y;
        }
    }
}