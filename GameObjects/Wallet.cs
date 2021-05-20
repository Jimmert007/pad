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
            SpriteGameObject bg;
            int money, newMoney;
            public bool playedSound;

            public Wallet()
            {
                Add(bg = new SpriteGameObject("UI/spr_wallet"));
                Add(text = new TextGameObject("Fonts/JimFont"));
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
