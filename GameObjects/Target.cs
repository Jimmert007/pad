﻿using HarvestValley.GameObjects.Tools;
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
        TextGameObject welcomeText, targetText;
        TargetButton button;
        int targetAmount, min = 100, max = 200, currentAmount;
        Item targetItem;
        string targetName;
        GameObjectList stackableItemsList;
        public Target(ItemList _itemList)
        {
            Add(panel_bg = new SpriteGameObject("spr_target_bg"));
            panel_bg.Origin = panel_bg.Sprite.Center;
            panel_bg.Position = GameEnvironment.Screen.ToVector2() * .5f;
            Add(welcomeText = new TextGameObject("JimFont"));
            welcomeText.Color = Color.Black;
            targetAmount = GameEnvironment.Random.Next(min, max);
            stackableItemsList = new GameObjectList();
            foreach (Item item in _itemList.Children)
            {
                if (item.isStackable && !(item is Sprinkler))
                {
                    stackableItemsList.Add(item);
                }
            }

            //targetItem = (stackableItemsList.Children[GameEnvironment.Random.Next(stackableItemsList.Children.Count)] as Item);
            targetItem = (stackableItemsList.Children[0] as Item);
            targetName = targetItem.Sprite.Sprite.Name;

            string[] removeFromString = { "spr", "stage", "1" };
            for (int i = 0; i < removeFromString.Length; i++)
            {
                if (targetName.Contains(removeFromString[i]))
                {
                    targetName = targetName.Replace(removeFromString[i], "");
                }
            }

            if (targetName.Contains("_"))
            {
                targetName = targetName.Replace("_", " ");
            }

            Debug.WriteLine("target " + targetName);

            welcomeText.Text =
                "Welkom bij Harvest Valley!\n\n" +
                "Het is de bedoeling om het land waar je op staat\n" +
                "om te toveren naar een productieve boerderij.\n" +
                "Je eerste doel is om " + targetAmount.ToString() + " " + targetName + " te verzamelen.\n" +
                "Veel plezier!";
            welcomeText.Position = panel_bg.Position - welcomeText.Size * .5f;
            Add(button = new TargetButton());
            button.Position = panel_bg.Position + new Vector2(panel_bg.Width * .5f - button.Sprite.Width, panel_bg.Height * .5f - button.Sprite.Height * 1.5f);
            Add(targetText = new TextGameObject("JimFont"));
            targetText.Color = Color.Black;
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
            }
        }

        public void AddToTarget(int addition)
        {
            TargetAmount += addition;
        }

        public int TargetAmount
        {
            set { targetAmount = value; }
            get { return targetAmount; }
        }
    }
}