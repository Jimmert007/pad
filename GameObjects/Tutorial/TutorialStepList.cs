using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HarvestValley.GameObjects.Tutorial
{
    class TutorialStepList : GameObjectList
    {
        SpriteGameObject tutorialBox;
        public int step = 1;
        public bool mouseCollides, step1completed, step2completed, step3completed;
        public string
            step1 = "To start growing plants, you need to till some land first (within playerreach)." + Environment.NewLine +
                    "Select the hoe by pressing 1 and click on a piece of land to till it.",
            step2 = "When the land is tilled you can plant a seed by selecting the seed" + Environment.NewLine +
                    "by pressing 5 and then clicking on the tilled piece of land.",
            step3 = "To grow the plant you need to give it water by using the watering can." + Environment.NewLine +
                    "Press 4 to select it and click on the plant to water it.";
        public TutorialStepList()
        {
            tutorialBox = new SpriteGameObject("TutorialSquare");
            Add(tutorialBox);
            Add(new TutorialStep(1, step1));
            Add(new TutorialStep(2, step2));
            Add(new TutorialStep(3, step3));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (mouseCollides)
            {
                for (int i = 1; i < children.Count; i++)
                {
                    if ((children[i] as TutorialStep).step == step)
                    {
                        (children[i] as TutorialStep).mouseCollides = true;

                    }
                    else
                    {

                        (children[i] as TutorialStep).mouseCollides = false;
                    }
                }
            }
            else
            {
                for (int i = 1; i < children.Count; i++)
                {
                    (children[i] as TutorialStep).mouseCollides = false;
                }
            }
        }
    }
}
