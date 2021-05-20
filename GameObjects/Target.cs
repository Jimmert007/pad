using HarvestValley.GameObjects.HarvestValley.GameObjects;
using HarvestValley.GameObjects.Tools;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HarvestValley.GameObjects
{
    class Target : GameObjectList
    {
        SpriteGameObject panel_bg;
        TextGameObject welcomeText, targetText, congratsText;
        TargetButton button;
        int targetAmount, currentAmount;
        int[] minWoodSeedStone = { 100, 25, 75 };
        public Item targetItem;
        string targetName;
        GameObjectList stackableItemsList;
        int rewardAmount;
        Wallet wallet;
        Player player;
        public bool collected;
        public Target(ItemList _itemList, Wallet _wallet, Player _player)
        {
            Add(panel_bg = new SpriteGameObject("UI/spr_target_bg"));
            panel_bg.Origin = panel_bg.Sprite.Center;
            panel_bg.Position = GameEnvironment.Screen.ToVector2() * .5f;
            Add(welcomeText = new TextGameObject("Fonts/JimFont"));
            welcomeText.Color = Color.Black;
            stackableItemsList = new GameObjectList();
            foreach (Item item in _itemList.Children)
            {
                if (item.isStackable && !(item is Sprinkler) && !(item is Seed))//seed nu uit want kan je niet krijgen
                {
                    stackableItemsList.Add(item);
                }
            }

            int r = GameEnvironment.Random.Next(stackableItemsList.Children.Count);
            targetItem = (stackableItemsList.Children[r] as Item);
            targetName = targetItem.Sprite.Sprite.Name;

            targetAmount = GameEnvironment.Random.Next(minWoodSeedStone[r], minWoodSeedStone[r] * 2);

            string[] removeFromString = { "spr", "stage", "1", "Items", "/", "_", "Environment" };
            for (int i = 0; i < removeFromString.Length; i++)
            {
                if (targetName.Contains(removeFromString[i]))
                {
                    targetName = targetName.Replace(removeFromString[i], "");
                }
            }

            //Debug.WriteLine("target " + targetName);

            welcomeText.Text =
                "Welkom bij Harvest Valley!\n\n" +
                "Het is de bedoeling om het land waar je op staat\n" +
                "om te toveren naar een productieve boerderij.\n" +
                "Je eerste doel is om " + targetAmount.ToString() + " " + targetName + " te verzamelen.\n" +
                "Veel plezier!";
            welcomeText.Position = panel_bg.Position - welcomeText.Size * .5f;
            Add(button = new TargetButton());
            button.Position = panel_bg.Position + new Vector2(panel_bg.Width * .5f - button.Sprite.Width, panel_bg.Height * .5f - button.Sprite.Height * 1.5f);
            Add(targetText = new TextGameObject("Fonts/JimFont"));
            targetText.Color = Color.Black;
            targetText.Position = new Vector2(0, 50);
            rewardAmount = 500;
            Add(congratsText = new TextGameObject("Fonts/JimFont"));
            congratsText.Color = Color.Black;
            congratsText.Text =
                "Gefeliciteerd!\n\n" +
                "Je hebt het doel bereikt, \n" +
                "Klik op de knop om verder te spelen en .\n" +
                "je prijs (" + rewardAmount + " coins) te verzilveren.\n" +
                "Veel plezier nog!";
            congratsText.Position = panel_bg.Position - congratsText.Size * .5f;
            congratsText.Visible = false;
            wallet = _wallet;
            player = _player;
            player.sleeping = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            targetText.Text = "Doel " + currentAmount + " / " + targetAmount + " " + targetName;
            if (button.OnClick)
            {
                panel_bg.Visible = false;
                welcomeText.Visible = false;
                button.Visible = false;
                player.sleeping = false;
                if (currentAmount >= targetAmount)
                {
                    collected = true;
                    congratsText.Visible = false;
                    wallet.AddMoney(rewardAmount);
                    targetText.Visible = false;
                }
            }
            if (currentAmount >= targetAmount && !collected)
            {
                MadeIt();
            }
        }

        void MadeIt()
        {
            player.sleeping = true;
            panel_bg.Visible = true;
            button.Visible = true;
            congratsText.Visible = true;
        }

        public void AddToTarget(int addition)
        {
            CurrentAmount += addition;
        }

        public int CurrentAmount
        {
            set { currentAmount = value; }
            get { return currentAmount; }
        }
    }
}
