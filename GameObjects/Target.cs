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
        public SpriteGameObject panel_bg, targetUI;
        TextGameObject welcomeText, targetText, congratsText;
        TargetButton button;
        int targetAmount, currentAmount;
        int[] minTSeedWoodRockWheat = { 25, 100, 75, 100 };
        public Item targetItem;
        string targetName;
        GameObjectList stackableItemsList;
        int rewardAmount;
        Wallet wallet;
        Player player;
        public bool collected;
        Sounds sounds;
        public Target(ItemList _itemList, Wallet _wallet, Player _player)
        {
            sounds = new Sounds();
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
            //Debug.WriteLine(r);
            targetItem = (stackableItemsList.Children[r] as Item);
            targetName = targetItem.Sprite.Sprite.Name;

            targetAmount = GameEnvironment.Random.Next(minTSeedWoodRockWheat[r], minTSeedWoodRockWheat[r] * 2);

            string[] removeFromString = { "spr", "stage", "1", "Items", "/", "_", "Environment" };
            for (int i = 0; i < removeFromString.Length; i++)
            {
                if (targetName.Contains(removeFromString[i]))
                {
                    targetName = targetName.Replace(removeFromString[i], "");
                }
            }

            targetName = targetName.ToLower();

            if (targetName[targetName.Length - 1] != 's' && targetName != "wood" && targetName != "wheat")
            {
                targetName += "s";
            }

            //Debug.WriteLine("target " + targetName);

            welcomeText.Text =
                "Welcome to Harvest Valley!\n\n" +
                "Try to convert the messy landscape\n" +
                "to a productive farm\n" +
                "Your target is to gather " + targetAmount.ToString() + " " + targetName + ".\n" +
                "Enjoy!";
            welcomeText.Position = panel_bg.Position - welcomeText.Size * .5f;
            Add(button = new TargetButton());
            button.Position = panel_bg.Position + new Vector2(panel_bg.Width * .5f - button.Sprite.Width, panel_bg.Height * .5f - button.Sprite.Height);
            Add(targetUI = new SpriteGameObject("UI/spr_target_ui_bar"));
            targetUI.Position = new Vector2(GameEnvironment.Screen.X * .5f - targetUI.Sprite.Width * .5f, 0);
            Add(targetText = new TextGameObject("Fonts/JimFont"));
            targetText.Color = Color.Black;
            rewardAmount = 500;
            Add(congratsText = new TextGameObject("Fonts/JimFont"));
            congratsText.Color = Color.Black;
            congratsText.Text =
                "Congrats!\n\n" +
                "You've completed the task, \n" +
                "Press the button to continue and \n" +
                "collect your reward (" + rewardAmount + " coins).\n" +
                "Have fun!";
            congratsText.Position = panel_bg.Position - congratsText.Size * .5f;
            congratsText.Visible = false;
            wallet = _wallet;
            player = _player;
            player.sleeping = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            targetText.Text = "Task: " + currentAmount + " / " + targetAmount + " " + targetName;
            targetText.Position = targetUI.Position + (new Vector2(targetUI.Width, targetUI.Height) * .5f) - targetText.Size * .5f;

            if (button.OnClick)
            {
                GameEnvironment.AssetManager.PlayOnce(sounds.SEIs[10]);
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
                    targetUI.Visible = false;
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
