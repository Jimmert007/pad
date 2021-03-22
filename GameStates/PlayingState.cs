using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseProject

{
    class PlayingState : GameState
    {
        EnergyBar energyBar;
        Sleeping sleeping;
        GlobalTime globalTime;
        Player player;
        Map map;
        Tilling tilling;
        List<Plant> plants = new List<Plant>();
        List<Tree> trees = new List<Tree>();
        Item item;
        Numbers number;
        Wallet wallet;

        #region Adding Items
        Hoe hoe;
        Axe axe;
        WateringCan wateringCan;
        Seed seed;
        #endregion

        public int ScreenWidth;
        public int ScreenHeight;
        public int itemAmount = 4;
        Hotbar hotbar;
        float HotbarCount = 9;
        float HalfHotbar;
        float vak;
        //C0 C0;

        public SpriteFont font;

        public PlayingState()
        {
            


            energyBar = new EnergyBar("EnergyBarBackground", GameEnvironment.Screen.X - 60, GameEnvironment.Screen.Y - 220, 40, 200);
            player = new Player("jorrit", 0, 0, 100, 100);
            item = new Item("spr_empty");
            tilling = new Tilling("spr_soil", 0, 0, 100, 100);
            map = new Map("1px", new Vector2(0, 0), new Vector2(50, 50));
            globalTime = new GlobalTime("spr_empty");
            sleeping = new Sleeping("spr_empty");
            gameObjectList.Add(globalTime);
            
            gameObjectList.Add(map);
            gameObjectList.Add(item);
            //gameObjectList.Add(number);

            #region Adding Items
                item.items.Add(new Hoe("spr_hoe", false));
                item.items.Add(new Axe("spr_empty", false));
                item.items.Add(new WateringCan("spr_empty", false));
                item.items.Add(new Seed("spr_seed1_stage1", true));
            #endregion




            gameObjectList.Add(tilling);

            for (int i = 0; i < map.cols; i++)
            {
                for (int x = 0; x < map.rows; x++)
                {
                    Cell newCell = new Cell("1px", new Vector2(map.size.X * i, map.size.Y * x), map.size, i + x * map.cols);
                    map.cells.Add(newCell);
                    gameObjectList.Add(newCell);
                    Plant newPlant = new Plant("spr_empty", 0, 0, (int)map.size.X, (int)map.size.Y);
                    plants.Add(newPlant);
                    gameObjectList.Add(newPlant);
                    Tree newTree = new Tree("spr_empty", 0, 0, (int)map.size.X, (int)map.size.Y);
                    trees.Add(newTree);
                    gameObjectList.Add(newTree);
                }
            }
            gameObjectList.Add(player);

            ScreenWidth = GameEnvironment.screen.X;
            ScreenHeight = GameEnvironment.screen.Y;

            //gameObjectList.Add(new GameObject("spr_background"));  
            //gameObjectList.Add(new Cell("test", new Vector2(10, 10), new Vector2(0, 0)));

            wallet = new Wallet("spr_wallet");
            hotbar = new Hotbar("1px");
            gameObjectList.Add(hotbar);
            font = GameEnvironment.ContentManager.Load<SpriteFont>("File");

            gameObjectList.Add(wallet);
            gameObjectList.Add(energyBar);
            gameObjectList.Add(sleeping);

           
        }

        public override void Update(GameTime gameTime)
        {
            GameObject mouseGO = new GameObject("spr_empty");
            mouseGO.position.X = GameEnvironment.MouseState.X;
            mouseGO.position.Y = GameEnvironment.MouseState.Y;
            for (int i = 0; i < map.cells.Count; i++)
            {
                if (trees[i].health <= 0)
                {
                    map.cells[i].cellHasTree = false;
                    map.cells[i].sourceRect = new Rectangle(900, 0, 380, 380);
                    map.cells[i].texture = map.cells[i].sprites[1];
                    trees[i].health = 4;
                }
                if (map.cells[i].cellHasTree)
                {
                    map.cells[i].texture = trees[i].texture;
                }
                if (map.cells[i].Overlaps(mouseGO))
                {
                    if (player.PlayerCanReach())
                    {
                        if (GameEnvironment.MouseState.LeftButton == ButtonState.Pressed)
                        {
                            if (item.itemSelected == "SEED" && !map.cells[i].cellHasPlant && map.cells[i].cellIsTilled)
                            {
                                map.cells[i].cellHasPlant = true;
                                plants[i].position = map.cells[i].position;
                                plants[i].size = map.cells[i].size;
                                plants[i].growthStage = 1;
                                energyBar.percentageLost += energyBar.oneUse;
                            }
                            if (item.itemSelected == "HOE" && !map.cells[i].cellHasTree && !map.cells[i].cellIsTilled)
                            {
                                map.cells[i].cellIsTilled = true;
                                map.cells[i].sourceRect = new Rectangle(0, 0, tilling.texture.Width, tilling.texture.Height);
                                map.cells[i].texture = tilling.tilledSoilTexture;
                                energyBar.percentageLost += energyBar.oneUse;
                            }
                            if (item.itemSelected == "AXE" && map.cells[i].cellHasTree && !trees[i].treeHit)
                            {
                                trees[i].treeHit = true;
                                trees[i].hitTimer = trees[i].hitTimerReset;
                                trees[i].health -= 1;
                                energyBar.percentageLost += energyBar.oneUse;
                            }
                        }
                        if (GameEnvironment.MouseState.RightButton == ButtonState.Pressed)
                        {
                            if (map.cells[i].cellHasPlant)
                            {
                                if (plants[i].growthStage >= 4)
                                {
                                    //(receive product and new seed)
                                    map.cells[i].cellHasPlant = false;
                                    plants[i].growthStage = 0;
                                }
                            }
                        }
                    }
                }
            }
            base.Update(gameTime);
            for (int i = 0; i < plants.Count; i++)
            {
                if (sleeping.fadeOut)
                {
                    energyBar.Reset();
                    if (map.cells[i].cellHasPlant && sleeping.fadeAmount >= 1f)
                    {
                        plants[i].growthStage++;
                    }
                }
            }
            if (energyBar.passOut)
            {
                sleeping.Sleep(globalTime);
                sleeping.useOnce = false;
            }
            sleeping.Update(globalTime);
            globalTime.Update(gameTime);

            #region Item Selection
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D1))
            {
                item.itemSelected = "HOE";
                hotbar.selectedSquarePosition.X = hotbar.position.X;
            }
            else if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D2))
            {
                item.itemSelected = "AXE";
                hotbar.selectedSquarePosition.X = hotbar.position.X + hotbar.squareSize;
            }
            else if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D3))
            {
                item.itemSelected = "WATERINGCAN";
                hotbar.selectedSquarePosition.X = hotbar.position.X + hotbar.squareSize * 2;
            }
            else if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D4))
            {
                item.itemSelected = "SEED";
                hotbar.selectedSquarePosition.X = hotbar.position.X + hotbar.squareSize * 3;
            }
            else if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D5))
            {
                //item.itemSelected = "AXE";
                hotbar.selectedSquarePosition.X = hotbar.position.X + hotbar.squareSize * 4;
            }
            else if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D6))
            {
                //item.itemSelected = "WATERINGCAN";
                hotbar.selectedSquarePosition.X = hotbar.position.X + hotbar.squareSize * 5;
            }
            else if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D7))
            {
                //item.itemSelected = "SEED";
                hotbar.selectedSquarePosition.X = hotbar.position.X + hotbar.squareSize * 6;
            }
            else if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D8))
            {
                //item.itemSelected = "WATERINGCAN";
                hotbar.selectedSquarePosition.X = hotbar.position.X + hotbar.squareSize * 7;
            }
            else if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D9))
            {
                //item.itemSelected = "SEED";
                hotbar.selectedSquarePosition.X = hotbar.position.X + hotbar.squareSize * 8;
            }
            #endregion


            #region Item Selection vervormen naar money
            if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D1))
            {
                item.itemSelected = "HOE";
                hotbar.selectedSquarePosition.X = hotbar.position.X;
            }
            else if (GameEnvironment.KeyboardState.IsKeyDown(Keys.D2))
            {
                item.itemSelected = "AXE";
                hotbar.selectedSquarePosition.X = hotbar.position.X + hotbar.squareSize;
            }

            #endregion

          
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(hotbar.hotbar, new Rectangle((int)hotbar.position.X, (int)hotbar.position.Y, (int)hotbar.size.X, (int)hotbar.size.Y), Color.White);
            spriteBatch.Draw(hotbar.selectedSquare, new Rectangle((int)hotbar.selectedSquarePosition.X, (int)hotbar.selectedSquarePosition.Y, (int)hotbar.squareSize, (int)hotbar.squareSize), Color.White); ;
            for (int i = 0; i < item.items.Count; i++)
            {
                spriteBatch.Draw(item.items[i].texture, new Rectangle((int)hotbar.position.X + hotbar.squareSize * i, (int)hotbar.position.Y, (int)hotbar.squareSize, (int)hotbar.squareSize), Color.White);
            }

            #region wallet Draw
            spriteBatch.Draw(wallet.wallet, new Rectangle((int)wallet.position.X, (int)wallet.position.Y, (int)wallet.size.X, (int)wallet.size.Y), Color.White);
            spriteBatch.Draw(wallet.moneySquare, new Rectangle((int)wallet.moneySquarePosition.X, (int)wallet.moneySquarePosition.Y, (int)wallet.moneySquareSize, (int)wallet.size.Y), Color.White); ;
            
            wallet.money++;
            for (int i = 0; i < wallet.money.ToString().Length; i++)
            {
                for (int r = 0; r < wallet.money.ToString().Length;)
                {
                    #region lelijke if
                    if (wallet.money.ToString()[r] == '0')
                        {
                        
                            spriteBatch.DrawString(font, "0", new Vector2((int)wallet.position.X + wallet.moneySquareSize /2 -10  + wallet.moneySquareSize * r, (int)wallet.position.Y), Color.Black);
                            Debug.WriteLine(" 0?");
                        }
                        else if (wallet.money.ToString()[r] == '1')
                        {
                        spriteBatch.DrawString(font, "1", new Vector2((int)wallet.position.X + wallet.moneySquareSize / 2 - 10 + wallet.moneySquareSize * r, (int)wallet.position.Y), Color.Black);
                    }
                        else if (wallet.money.ToString()[r] == '2')
                        {
                        spriteBatch.DrawString(font, "2", new Vector2((int)wallet.position.X + wallet.moneySquareSize / 2 - 10 + wallet.moneySquareSize * r, (int)wallet.position.Y), Color.Black);
                        Debug.WriteLine(" 2?");
                        }
                        else if (wallet.money.ToString()[r] == '3')
                        {
                        spriteBatch.DrawString(font, "3", new Vector2((int)wallet.position.X + wallet.moneySquareSize / 2 - 10 + wallet.moneySquareSize * r, (int)wallet.position.Y), Color.Black);
                        Debug.WriteLine(" 3?");
                        }
                        else if (wallet.money.ToString()[r] == '4')
                        {
                        spriteBatch.DrawString(font, "4", new Vector2((int)wallet.position.X + wallet.moneySquareSize / 2 - 10 + wallet.moneySquareSize * r, (int)wallet.position.Y), Color.Black);
                        Debug.WriteLine(" 4?");
                        }
                        else if (wallet.money.ToString()[r] == '5')
                        {
                        spriteBatch.DrawString(font, "5", new Vector2((int)wallet.position.X + wallet.moneySquareSize / 2 - 10 + wallet.moneySquareSize * r, (int)wallet.position.Y), Color.Black);
                        Debug.WriteLine(" 5?");
                        }
                        else if (wallet.money.ToString()[r] == '6')
                        {
                        spriteBatch.DrawString(font, "6", new Vector2((int)wallet.position.X + wallet.moneySquareSize / 2 - 10 + wallet.moneySquareSize * r, (int)wallet.position.Y), Color.Black);
                        Debug.WriteLine(" 6?");
                        }
                        else if (wallet.money.ToString()[r] == '7')
                        {
                        spriteBatch.DrawString(font, "7", new Vector2((int)wallet.position.X + wallet.moneySquareSize / 2 - 10 + wallet.moneySquareSize * r, (int)wallet.position.Y), Color.Black);
                        Debug.WriteLine(" 7?");
                        }
                        else if (wallet.money.ToString()[r] == '8')
                        {
                        spriteBatch.DrawString(font, "8", new Vector2((int)wallet.position.X + wallet.moneySquareSize / 2 - 10 + wallet.moneySquareSize * r, (int)wallet.position.Y), Color.Black);
                        Debug.WriteLine(" 8?");
                        }
                        else if (wallet.money.ToString()[r] == '9')
                        {
                        spriteBatch.DrawString(font, "9", new Vector2((int)wallet.position.X + wallet.moneySquareSize / 2 - 10 + wallet.moneySquareSize * r, (int)wallet.position.Y), Color.Black);
                        Debug.WriteLine(" 9?");
                        }
                    #endregion
                    r++;
                }
            }
            #endregion
        }
    }
}
