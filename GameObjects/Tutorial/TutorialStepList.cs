using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HarvestValley.GameObjects.Tutorial
{
    class TutorialStepList : GameObjectList
    {
        MouseGameObject mouseGO;
        SpriteGameObject tutorialBox;
        public int step = 1;
        public bool mouseCollides;
        public string
            step1 = "To start growing plants, you need to till some land first (within playerreach). \n" +
                    "Select the hoe by pressing 1 and click on a piece of land to till it.",
            step2 = "When the land is tilled you can plant a seed by selecting the seed \n" +
                    "by pressing 5 and then clicking on the tilled piece of land.",
            step3 = "To grow the plant you need to give it water by using the watering can. \n" +
                    "Press 4 to select it and click on the plant to water it.",
            step4 = "You can also break trees or rocks by using your pickaxe or axe, \n" +
                    "when you do, you receive wood and maybe a tree seed or stone. \n" +
                    "When you perform an action you lose energy in the bottom right corner.",
            step5 = "When you sleep, by walking into the door of the tent, the plants and trees \n" +
                    "grow.",
            step6 = "When they are fully grown you can harvest plants by pressing right mouse \n" +
                    "button on the plants.",
            step7 = "You can sell the harvested wheat, rocks and wood in the shop next to the tent. \n" +
                    "Click on the computer to open it.",
            step8 = "";

        public TutorialStepList(MouseGameObject mouseGO)
        {
            this.mouseGO = mouseGO;
            tutorialBox = new SpriteGameObject("UI/QuestionMark");
            Add(tutorialBox);
            Add(new TutorialStep(1, step1));
            Add(new TutorialStep(2, step2));
            Add(new TutorialStep(3, step3));
            Add(new TutorialStep(4, step4));
            Add(new TutorialStep(5, step5));
            Add(new TutorialStep(6, step6));
            Add(new TutorialStep(7, step7));
            Add(new TutorialStep(8, step8));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (mouseGO.CollidesWith(tutorialBox))
            {
                mouseGO.Children[0].Visible = false;
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
                mouseGO.Children[0].Visible = true;
                for (int i = 1; i < children.Count; i++)
                {
                    (children[i] as TutorialStep).mouseCollides = false;
                }
            }
        }
    }
}
