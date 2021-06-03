using HarvestValley.GameObjects.Tools;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HarvestValley.GameObjects
{
    /// <summary>
    ///Niels Duivenvoorden
    ///This script gives the player a random chosen quest to complete
    ///Once completed it generates a new quest with a higher gathering amount made with difficulty
    ///It also handles the UI for these elements where the player sees the target, gets congratulated with a screen
    ///The item amount are retrieved from their source name where this script builds a new string to display a proper name
    /// </summary>
    class Target : GameObjectList
    {
        public SpriteGameObject panel_bg, targetUI;                         //UI elements
        TextGameObject titleText, welcomeText, targetText, congratsText;    //UI elements
        TargetButton button;                                                //UI elements
        int targetAmount, currentAmount;                                    //ints to keep track of the target and the current amounts
        int[] minSeedTSeedWoodRockWheat = { 50, 10, 90, 60, 50 };           //array minimums for the different gatherable items
        public Item targetItem;                                             //the random chosen item
        string targetName;                                                  //the name of the item
        GameObjectList stackableItemsList;                                  //a list to gather all stackable items
        int rewardAmount = 50;                                              //coins retrieved once finished
        Wallet wallet;                                                      //ref to wallet
        Player player;                                                      //ref to player
        public bool collected;                                              //collected money bool
        Sounds sounds;                                                      //ref sounds
        int difficulty = 1;                                                 //dificulty int that increases everytime a quest gets generated
        public Target(ItemList _itemList, Wallet _wallet, Player _player, Sounds _sounds)
        {
            sounds = _sounds;
            wallet = _wallet;
            player = _player;
            //place background in the center of the screen
            Add(panel_bg = new SpriteGameObject("UI/spr_target_bg"));
            panel_bg.Origin = panel_bg.Sprite.Center;
            panel_bg.Position = GameEnvironment.Screen.ToVector2() * .5f;

            //place a title based on the panels position
            Add(titleText = new TextGameObject("Fonts/JimFont"));
            titleText.Color = Color.Black;
            titleText.Text = "Quest";
            titleText.Position = panel_bg.Position - new Vector2(titleText.Size.X * .5f, panel_bg.Sprite.Height * .5f - titleText.Size.Y * 1.5f);

            //place a welcome text message cetered based on the panel
            Add(welcomeText = new TextGameObject("Fonts/JimFont"));
            welcomeText.Color = Color.Black;
            //retrieve all stackable items
            stackableItemsList = new GameObjectList();
            foreach (Item item in _itemList.Children)
            {
                if (item.isStackable && !(item is Sprinkler))
                {
                    stackableItemsList.Add(item);
                }
            }

            //generate a new target
            NewTarget();

            welcomeText.Text =
                "Welcome to Harvest Valley!\n\n" +
                "Try to convert the messy landscape\n" +
                "to a productive farm\n" +
                "Your target is to gather " + targetAmount.ToString() + " " + targetName + ".\n" +
                "Enjoy!";
            welcomeText.Position = panel_bg.Position - welcomeText.Size * .5f;

            //place a button based on panel position
            Add(button = new TargetButton());
            button.Position = panel_bg.Position + new Vector2(panel_bg.Width * .5f - button.Sprite.Width, panel_bg.Height * .5f - button.Sprite.Height);

            //place a permanently visible sprite to keep track of the quest
            Add(targetUI = new SpriteGameObject("UI/spr_target_ui_bar"));
            targetUI.Position = new Vector2(GameEnvironment.Screen.X * .5f - targetUI.Sprite.Width * .5f, 0);

            //add the text for the targetUI box
            Add(targetText = new TextGameObject("Fonts/JimFont"));
            targetText.Color = Color.Black;

            //add the text displayed once the player finished the quest based on the panels position
            Add(congratsText = new TextGameObject("Fonts/JimFont"));
            congratsText.Color = Color.Black;
            congratsText.Text =
                "Congrats!\n\n" +
                "You've completed the task, \n" +
                "Press the button to continue and \n" +
                "collect your reward (" + rewardAmount + " coins).\n" +
                "Have fun!";
            congratsText.Position = panel_bg.Position - congratsText.Size * .5f;
            congratsText.Visible = false;   //disable this text at start
            player.sleeping = true;         //deactivate the players input on land
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            targetText.Text = "Quest: " + currentAmount + " / " + targetAmount + " " + targetName; //build the string seen at runtime
            targetText.Position = targetUI.Position + (new Vector2(targetUI.Width, targetUI.Height) * .5f) - targetText.Size * .5f; //center the text based on the UI element
            //once the button gets clicked on
            if (button.OnClick)
            {
                // Play ButtonClick
                GameEnvironment.AssetManager.PlayOnce(sounds.SEIs[9]);

                //disable UI elements
                titleText.Visible = false;
                panel_bg.Visible = false;
                welcomeText.Visible = false;
                button.Visible = false;
                player.sleeping = false;

                //retrieve price section with disable of UI elements
                if (currentAmount >= targetAmount)
                {
                    collected = true;
                    congratsText.Visible = false;
                    wallet.AddMoney(rewardAmount);

                    // Play CoinDrop
                    GameEnvironment.AssetManager.PlayOnce(sounds.SEIs[11]);

                    NewTarget();
                }
            }
            //target reached
            if (currentAmount >= targetAmount && !collected)
            {
                MadeIt();
            }
        }
        /// <summary>
        /// Generate a new target with the stackable items and a difficulty
        /// </summary>
        void NewTarget()
        {
            CurrentAmount = 0; //reset the currentamount
            collected = false;
            int r = GameEnvironment.Random.Next(stackableItemsList.Children.Count); //make a random
            targetItem = (stackableItemsList.Children[r] as Item); //randomly select an item out the stackable item list
            targetName = targetItem.Sprite.Sprite.Name; //set the name based on the sprite name of the item

            targetAmount = GameEnvironment.Random.Next(minSeedTSeedWoodRockWheat[r], minSeedTSeedWoodRockWheat[r] * 2) * difficulty; //generate an amount to gether with minimums per item and a difficulty

            //clean string
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

            difficulty *= 2; //increase difficulty exponentially
        }

        /// <summary>
        /// Function to enable UI elements once quest completed
        /// </summary>
        void MadeIt()
        {
            titleText.Visible = true;
            player.sleeping = true;
            panel_bg.Visible = true;
            button.Visible = true;
            congratsText.Visible = true;
        }

        /// <summary>
        /// Add the given amount to the currentAmout
        /// </summary>
        /// <param name="addition"></param>
        public void AddToTarget(int addition)
        {
            CurrentAmount += addition;
        }

        /// <summary>
        /// Property to get and set the currentAmount
        /// </summary>
        public int CurrentAmount
        {
            set { currentAmount = value; }
            get { return currentAmount; }
        }
    }
}
