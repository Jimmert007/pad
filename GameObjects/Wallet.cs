using Microsoft.Xna.Framework;

namespace HarvestValley.GameObjects
{
    /// <summary>
    /// Niels Duivenvoorden, Luke Sikma
    /// Wallet class to display the coin amount
    /// Also contains functions to add/deduct from the wallet
    /// Has a add effect
    /// </summary>
    class Wallet : GameObjectList
    {
        TextGameObject text;            //text to be displayed
        SpriteGameObject bg, coin;      //background and coin sprite
        int money, newMoney;            //ints to read the money and moneyToBe
        public bool playedSound;        

        public Wallet()
        {
            //add all the UI elements
            Add(bg = new SpriteGameObject("UI/spr_money_container"));
            Add(text = new TextGameObject("Fonts/JimFont"));
            text.Color = Color.Black;
            Add(coin = new SpriteGameObject("UI/spr_coin"));

            //place the background and coin at the top right section
            bg.Position = new Vector2(GameEnvironment.Screen.X - bg.Sprite.Width - coin.Sprite.Width, 0);
            coin.Position = new Vector2(bg.Position.X + bg.Sprite.Width, bg.Position.Y + bg.Sprite.Height * .5f - coin.Sprite.Height * .5f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //increase/decrease money to the new money
            if (money != newMoney)
            {
                if (money < newMoney)
                {
                    money++;
                }
                else if (money > newMoney)
                {
                    money--;
                }
            }
            //change the text and place it in the center of the background sprite
            text.Text = money.ToString();
            text.Position = new Vector2(bg.Position.X + bg.Sprite.Width * .5f - text.Size.X * .5f, bg.Position.Y + bg.Sprite.Height * .5f - text.Size.Y * .5f);
        }

        public bool PlayCoinsound()
        {
            return (money == newMoney && money > 0 && !playedSound);
        }

        /// <summary>
        /// Add the given amount to the wallet
        /// </summary>
        /// <param name="amount"></param>
        public void AddMoney(int amount)
        {
            newMoney += amount;
        }

        /// <summary>
        /// Get the money value from wallet
        /// </summary>
        public int Money
        {
            get { return newMoney; }
        }
    }
}

