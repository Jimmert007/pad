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
        TextGameObject text;
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
            Add(text = new TextGameObject("JimFont"));
            text.Color = Color.Black;
            targetAmount = GameEnvironment.Random.Next(min, max);
            stackableItemsList = new GameObjectList();
            foreach (Item item in _itemList.Children)
            {
                if (item.isStackable && !(item is Sprinkler))
                {
                    stackableItemsList.Add(item);
                }
            }

            targetItem = (stackableItemsList.Children[GameEnvironment.Random.Next(stackableItemsList.Children.Count)] as Item);
            targetName = targetItem.Sprite.Sprite.Name;
            Debug.WriteLine(targetName);
            if (targetName.Contains("spr"))
            {
                targetName = targetName.Replace("spr", "");
            }
            foreach (char c in targetName)
            {
                if (c.ToString() == "_")
                {
                    c.ToString().Replace("_", " ");
                }
            }

            Debug.WriteLine("target " + targetName);

            text.Text = "Welkom bij Harvest Valley!\n \n " +
                "Het is de bedoeling om het land waar je op staat \n" +
                "om te toveren naar een productieve boerderij. \n " +
                "Je eerste doel is om " + targetAmount.ToString() + " " + targetName + " te verzamelen.\n" +
                "Veel plezier!";
            text.Position = panel_bg.Position - text.Size * .5f;
            Add(button = new TargetButton());
            button.Position = panel_bg.Position + new Vector2(panel_bg.Width * .5f - button.Sprite.Width, panel_bg.Height * .5f - button.Sprite.Height * 1.5f);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.MouseLeftButtonDown() && button.Overlap())
            {
                panel_bg.Visible = false;
                button.Visible = false;
                text.Visible = false;
            }
        }
    }
}
