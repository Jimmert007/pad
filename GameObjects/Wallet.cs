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
    namespace HarvestValley.GameObjects
    {
        class Wallet : GameObjectList
        {
            TextGameObject text;
            SpriteGameObject bg, coin;
            int money, newMoney;
            public bool playedSound;

            public Wallet()
            {
                Add(bg = new SpriteGameObject("UI/spr_money_container"));
                Add(text = new TextGameObject("Fonts/JimFont"));
                text.Color = Color.Black;
                Add(coin = new SpriteGameObject("UI/spr_coin"));
                bg.Position = new Vector2(GameEnvironment.Screen.X - bg.Sprite.Width - coin.Sprite.Width, 0);
                coin.Position = new Vector2(bg.Position.X + bg.Sprite.Width, bg.Position.Y + bg.Sprite.Height * .5f - coin.Sprite.Height * .5f);
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);
                if (money < newMoney)
                {
                    money++;
                }
                text.Text = money.ToString();
                text.Position = new Vector2(bg.Position.X + bg.Sprite.Width * .5f - text.Size.X * .5f, bg.Position.Y + bg.Sprite.Height * .5f - text.Size.Y * .5f);
            }

            public bool PlayCoinsound()
            {
                return (money == newMoney && money > 0 && !playedSound);
            }

            public void AddMoney(int amout)
            {
                newMoney = money + amout;
            }
        }
    }
}
