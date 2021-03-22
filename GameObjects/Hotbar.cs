using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BaseProject
{
    class Hotbar : GameObject
    {
        public List<GameObject> hotbarItemList;
        public Texture2D hotbar, selectedSquare;
        public int squareSize;
        public int HBWidth;
        public Vector2 selectedSquarePosition;
        public Hotbar(string _assetName) : base(_assetName)
        {
            hotbarItemList = new List<GameObject>();
            hotbar = GameEnvironment.ContentManager.Load<Texture2D>("spr_hotbar");
            selectedSquare = GameEnvironment.ContentManager.Load<Texture2D>("spr_selected_square");
            
            HBWidth = 450;
            squareSize = HBWidth/9;
            size.X = HBWidth;
            size.Y = HBWidth/9;
            position.X = GameEnvironment.screen.X / 2 - HBWidth /2;
            position.Y = GameEnvironment.screen.Y - size.Y;
            selectedSquarePosition.X = position.X;
            selectedSquarePosition.Y = position.Y;
        }
    }
}
