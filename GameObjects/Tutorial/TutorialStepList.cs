using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HarvestValley.GameObjects.Tutorial
{
    /// <summary>
    /// Jim van de Burgwal
    /// This class handles the tutorial steps which can be seen in de top left corner
    /// </summary>
    class TutorialStepList : GameObjectList
    {
        MouseGameObject mouseGO;        //The tutorial uses the mouse, so it's added here
        SpriteGameObject tutorialBox;   //The box with a questionmark is declared
        public int step = 1;            //This int keeps track of the current step
        public bool mouseCollides;      //Uses this bool keep track if the mouse collides
        public string                   //For each step there is a string declared
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
            step8 = "In the shop next to the tent you can sell items, but also buy usefull items \n" +
                    "like sprinklers to keep your plants watered.";

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
            if (mouseGO.CollidesWith(tutorialBox))      //If the mouse collides with the questionmark box
            {
                mouseGO.Children[0].Visible = false;    //The mouse is invisible
                for (int i = 1; i < children.Count; i++)    //For each child
                {
                    if ((children[i] as TutorialStep).step == step) //It checks if the given step in TutorialStep equals the step in this class
                    {
                        (children[i] as TutorialStep).mouseCollides = true; //If that happens, the correct TutorialStep will be visible
                    }
                    else
                    {

                        (children[i] as TutorialStep).mouseCollides = false;//All other TutorialSteps are made invisible
                    }
                }
            }
            else //If the mouse doesn't collide with the questionmark box
            {
                mouseGO.Children[0].Visible = true; //The mouse is visible
                for (int i = 1; i < children.Count; i++)
                {
                    (children[i] as TutorialStep).mouseCollides = false;    //All TutorialSteps are invisible
                }
            }
        }
    }
}
