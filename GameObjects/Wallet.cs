using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HarvestValley.GameObjects
{
    class Wallet : SpriteGameObject
    {
        public List<GameObject> walletMoneyList;
        public SpriteSheet wallet, moneySquare;
        public int moneySquareSize;
        //als je deze aan past moet je de font in content ook aanpassen
        public int walletWidth = 160;
        public Vector2 moneySquarePosition;
        public int money;

        public Wallet(string _assetName) : base(_assetName)
        {
            position.X = GameEnvironment.Screen.X - walletWidth;
            position.Y = 0;

            walletMoneyList = new List<GameObject>();
            wallet = new SpriteSheet("spr_wallet");
            moneySquare = new SpriteSheet("spr_empty");

            moneySquareSize = walletWidth / 5;

            moneySquarePosition.X = position.X;
            moneySquarePosition.Y = position.Y;
            money = 0;
        }
    }

}
