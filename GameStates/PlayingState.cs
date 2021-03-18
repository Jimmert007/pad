using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BaseProject

{
    class PlayingState : GameState
    {
        int iLines = 0;
        Sleeping sleeping;
        GlobalTime globalTime;
        Player player;
        Map map;
        Tilling tilling;
        List<Plant> plants = new List<Plant>();
        Tools tools;
        Hoe hoe;
        public UI ui;
        public UIButton yesButton, noButton;
        public UIDialogueBox dialogueBox;
        public UIText dialogueText;

        public string[] dialogueLines = { "Dit is een test", "Hallo", "Het werkt", "INSERT TEXT", "Proleet" };


        public int ScreenWidth;
        public int ScreenHeight;
        Hotbar hotbar;
        float HotbarCount = 9;
        float HalfHotbar;

        public PlayingState()
        {
            player = new Player("jorrit", 0, 0, 100, 100);
            tools = new Tools("spr_empty");
            hoe = new Hoe("spr_hoe", GameEnvironment.Screen.X / 2 - 25, GameEnvironment.Screen.Y - 70, 50, 50);
            tilling = new Tilling("spr_soil", 100, 100, 100, 100);
            map = new Map("1px", new Vector2(0, 0), new Vector2(50, 50));
            gameObjectList.Add(map);
            gameObjectList.Add(tools);
            gameObjectList.Add(hoe);
            gameObjectList.Add(tilling);

            //Initialize UI Elements
            ui = new UI("ui_bar");
            dialogueBox = new UIDialogueBox("ui_bar", -1000, -1000, GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 4);
            dialogueText = new UIText();
            yesButton = new UIButton("play", -1000, -1000, (int)dialogueBox.size.X / 3, (int)dialogueBox.size.Y / 3);
            noButton = new UIButton("cancel", -1000, -1000, (int)dialogueBox.size.X / 3, (int)dialogueBox.size.Y / 3);

            for (int i = 0; i < map.cols; i++)
            {
                for (int x = 0; x < map.rows; x++)
                {
                    Cell newCell = new Cell("1px", new Vector2(map.size.X * i, map.size.Y * x), map.size, i + x * map.cols);
                    map.cells.Add(newCell);
                    gameObjectList.Add(newCell);
                    Plant newPlant = new Plant("spr_seed1_stage1", 0, 0, 0, 0);
                    plants.Add(newPlant);
                    gameObjectList.Add(newPlant);
                }
            }
            gameObjectList.Add(player);

            ScreenWidth = GameEnvironment.screen.X;
            ScreenHeight = GameEnvironment.screen.Y;

            //gameObjectList.Add(new GameObject("spr_background"));  
            //gameObjectList.Add(new Cell("test", new Vector2(10, 10), new Vector2(0, 0)));


            hotbar = new Hotbar("test");
            //gameObjectList.Add(hotbar);

            //Add new instance for UI elements
            gameObjectList.Add(dialogueBox);
            gameObjectList.Add(yesButton);
            gameObjectList.Add(noButton);
            gameObjectList.Add(dialogueText);

            for (int i = 0; i < HotbarCount; i++)
            {

                GameObject hItem = new GameObject("test");

                hotbar.hotbarItemList.Add(hItem);
                gameObjectList.Add(hItem);

                HalfHotbar = HotbarCount / 2 * hotbar.hotbarItemList[i].texture.Width;

                hotbar.hotbarItemList[i].position.X = ScreenWidth / 2 - HalfHotbar;
                hotbar.hotbarItemList[i].position.X += 80 * i;
                hotbar.hotbarItemList[i].position.Y = ScreenHeight - hotbar.hotbarItemList[i].texture.Height;
                /*                                                                                                     Debug.Print("X " + i + " = " + hotbar.hotbarItemList[i].position.X.ToString());
                                                                                                                       Debug.Print("Y " + i + " = " + hotbar.hotbarItemList[i].position.Y.ToString());
                */
            }

        }

        public override void Update(GameTime gameTime)
        {
            GameObject mouseGO = new GameObject("test");
            mouseGO.position.X = GameEnvironment.MouseState.X;
            mouseGO.position.Y = GameEnvironment.MouseState.Y;
            for (int i = 0; i < map.cells.Count; i++)
            {
                if (map.cells[i].Overlaps(mouseGO))
                {
                    if (player.PlayerCanReach())
                    {

                        if (GameEnvironment.MouseState.LeftButton == ButtonState.Pressed)
                        {


                            if (tilling.item == "SEED")
                            {
                                Debug.WriteLine("hallo :)");
                                tilling.soilHasPlant = true;
                                plants[i].position = map.cells[i].position;
                                plants[i].size = map.cells[i].size;
                                plants[i].growthStage = 1;
                            }
                            if (tools.toolSelected == "HOE")
                            {
                                map.cells[i].texture = tilling.tilledSoilTexture;
                            }
                            if (tilling.soilHasPlant)
                            {
                                if (plants[i].growthStage >= 4)
                                {
                                    //(receive product and new seed)
                                    tilling.soilHasPlant = false;
                                }
                            }
                        }
                    }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.U)) { ui.UIActive = true; }
            if (Keyboard.GetState().IsKeyDown(Keys.I)) { ui.playerDescision = true; }
            if (Keyboard.GetState().IsKeyDown(Keys.J)) { ui.UIActive = false; }
            if (Keyboard.GetState().IsKeyDown(Keys.K)) { ui.playerDescision = false; }

            //Continuesly draw the UI on top of the UI box
            dialogueText.position = new Vector2(dialogueBox.position.X + yesButton.size.X / 3, dialogueBox.position.Y + dialogueBox.size.Y / 3);

            //Pick a sepcific line to display 
            //Cycle through lines on input
            //bool Ppressed = true;
            //bool PlastFramePressed = false;
            //if (Keyboard.GetState().IsKeyDown(Keys.P))
            //{
            //    Ppressed = true;
            //    PlastFramePressed = false;
            //}
            //else if (!Keyboard.GetState().IsKeyDown(Keys.P)) {
            //    PlastFramePressed = true; 
            //    Ppressed = false;  
            //}
            //if (Ppressed && PlastFramePressed)
            if(Keyboard.GetState().IsKeyDown(Keys.P))
            {
                iLines++;
                if (iLines >= dialogueLines.Length)
                {
                    iLines = 0;
                }
            }
            dialogueText.Text = dialogueLines[iLines].ToString();

            if (ui.UIActive)
            {
                dialogueBox.position.X = GameEnvironment.Screen.X / 4;
                dialogueBox.position.Y = GameEnvironment.Screen.Y / 4;
            }

            if (ui.playerDescision)
            {
                yesButton.position.X = (int)(dialogueBox.position.X);
                yesButton.position.Y = (int)(dialogueBox.position.Y + (dialogueBox.size.Y * 2));

                noButton.position.X = (int)(dialogueBox.position.X + dialogueBox.size.X - noButton.size.X);
                noButton.position.Y = (int)(yesButton.position.Y);
            }
            else if (!ui.UIActive)
            {
                dialogueBox.position.X = -10000;
                dialogueBox.position.Y = -10000;
                iLines = 0;
            }
            else if (!ui.playerDescision)
            {
                yesButton.position.X = -10000;
                yesButton.position.Y = -10000;
                noButton.position.X = -10000;
                noButton.position.Y = -10000;
            }

            if (yesButton.Overlaps(mouseGO) && ui.UIActive)
            {
                ui.playerDescision = false;
                //Accept player command
            }
            if (noButton.Overlaps(mouseGO) && ui.UIActive)
            {
                ui.playerDescision = false;
                //reject player command
            }

            base.Update(gameTime);
            //globalTime.Update(gameTime);
            //sleeping.Update(globalTime, plants[0], tilling);
        }
    }
}
