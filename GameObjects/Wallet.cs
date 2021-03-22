using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BaseProject
{
    class Wallet : GameObject
    {
        public List<GameObject> walletMoneyList;
        public Texture2D wallet, moneySquare;
        public int moneySquareSize;
        //als je deze aan past moet je de font in content ook aanpassen
        public int walletWidth = 160;
        public Vector2 moneySquarePosition;
        public int money;

        public Wallet(string _assetName) : base(_assetName)
        {
            size.X = walletWidth;
            size.Y = size.X /4;
            position.X = GameEnvironment.Screen.X - size.X;
            position.Y = 0;

            walletMoneyList = new List<GameObject>();
            wallet = GameEnvironment.ContentManager.Load<Texture2D>("spr_wallet");
            moneySquare = GameEnvironment.ContentManager.Load<Texture2D>("spr_empty");

            moneySquareSize = walletWidth / 5;

            moneySquarePosition.X = position.X;
            moneySquarePosition.Y = position.Y;
            money = 0;
        }
    }

}
