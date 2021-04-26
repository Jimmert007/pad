using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley.GameObjects
{
    /*   class Wallet : SpriteGameObject
       {
           public List<GameObject> walletMoneyList;
           public SpriteSheet wallet, moneySquare;
           public int moneySquareSize;

           //als je deze aan past moet je de font in content ook aanpassen
           public int walletWidth ;
           public Vector2 moneySquarePosition;
           public int money;

           public Wallet(string _assetName) : base(_assetName)
           {
               walletWidth = GameEnvironment.Screen.X / 4;

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

   }*/




    namespace HarvestValley.GameObjects
    {
        class Wallet : GameObjectList
        {
            TextGameObject text;
            SpriteGameObject bg;
            int money, newMoney;

            public Wallet()
            {
                Add(bg = new SpriteGameObject("spr_wallet"));
                Add(text = new TextGameObject("JimFont"));
                text.Position = new Vector2(bg.Position.X + bg.Sprite.Width * .3f, text.Position.Y);
                position.X = GameEnvironment.Screen.X - bg.Sprite.Width * .3f;
                bg.Scale *= .3f;
                text.Color = Color.Black;
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);
                if (money < newMoney)
                {
                    money++;
                }
                text.Text = money.ToString();
                text.Position = new Vector2(bg.Position.X + bg.Sprite.Width * .3f / 5 * .5f - text.Size.X / text.Text.Length * .5f, bg.Position.Y + bg.Sprite.Height * .3f * .5f - text.Size.Y * .5f);
            }

            public override void HandleInput(InputHelper inputHelper)
            {
                base.HandleInput(inputHelper);
                if (inputHelper.KeyPressed(Keys.Z))
                {
                    newMoney = money + 10;
                }
            }
        }
    }
}
