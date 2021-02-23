using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace BaseProject
{
    class Tilling
    {
        //Jim van de Burgwal

        //creating variables
        String tool = "HOE";
        String item = "SEED";
        Boolean soilIsTilled = false;
        Boolean soilHasPlant = false;
        Boolean rightButtonPressed = false;
        int rectSize = 100;
        int growthStage = 0;
        Vector2 landPosition;
        Texture2D texture;
        Texture2D soilTexture;
        Texture2D tilledSoilTexture;
        Texture2D plantTexture;
        Texture2D seed1stage1;
        Texture2D seed1stage2;
        Texture2D seed1stage3;
        Texture2D seed1stage4;
        Game1 game1;

        public Tilling(int _x, int _y, Game1 _game1)
        {
            landPosition.X = _x;
            landPosition.Y = _y;
            game1 = _game1;
            soilTexture = game1.Content.Load<Texture2D>("spr_soil");
            tilledSoilTexture = game1.Content.Load<Texture2D>("spr_tilled_soil");
            seed1stage1 = game1.Content.Load<Texture2D>("spr_seed1_stage1");
            seed1stage2 = game1.Content.Load<Texture2D>("spr_seed1_stage2");
            seed1stage3 = game1.Content.Load<Texture2D>("spr_seed1_stage3");
            seed1stage4 = game1.Content.Load<Texture2D>("spr_seed1_stage4");
        }

        public void Update()
        {
            //tilled soil update
            if (!soilIsTilled)
            {
                texture = soilTexture;
            }
            else if (soilIsTilled)
            {
                texture = tilledSoilTexture;
            }

            //seed growth stages update
            if (growthStage == 0)
            {
                plantTexture = seed1stage1;
            }
            else if (growthStage == 1)
            {
                plantTexture = seed1stage2;
            }
            else if (growthStage == 2)
            {
                plantTexture = seed1stage3;
            }
            else if (growthStage >= 3)
            {
                plantTexture = seed1stage4;
            }

            Till();
            Plant();
            Grow();
            Harvest();
        }

        public void Draw()
        {
            game1.spriteBatch.Draw(texture, new Rectangle(rectSize, rectSize, rectSize, rectSize), Color.White);
            if (soilHasPlant)
            {
                game1.spriteBatch.Draw(plantTexture, new Rectangle(rectSize, rectSize, rectSize, rectSize), Color.White);
            }
        }

        public void Till()
        {
            MouseState state = Mouse.GetState();
            Debug.Print("x" + state.X.ToString() + " y " + state.Y.ToString());
            //tilling the soil
            if (tool == "HOE")
            {
                if (state.X > rectSize & state.X < rectSize * 2 & state.Y > rectSize & state.Y < rectSize * 2)
                {
                    if (state.LeftButton == ButtonState.Pressed)
                    {
                        soilIsTilled = true;
                    }
                }
            }
        }

        public void Plant()
        {
            MouseState state = Mouse.GetState();
            Debug.Print("x" + state.X.ToString() + " y " + state.Y.ToString());
            //planting a seed
            if (item == "SEED")
            {
                if (soilIsTilled)
                {
                    if (state.X > rectSize & state.X < rectSize * 2 & state.Y > rectSize & state.Y < rectSize * 2)
                    {
                        if (state.LeftButton == ButtonState.Pressed)
                        {
                            soilHasPlant = true;
                            growthStage = 0;
                        }
                    }
                }
            }
        }

        public void Grow()
        {
            MouseState state = Mouse.GetState();
            Debug.Print("x" + state.X.ToString() + " y " + state.Y.ToString());
            //growing the plant
            if (soilHasPlant)
            {
                if (state.RightButton == ButtonState.Pressed & !rightButtonPressed)
                {
                    rightButtonPressed = true;
                    growthStage += 1;
                }
                if (state.RightButton == ButtonState.Released)
                {
                    rightButtonPressed = false;
                }
            }
        }

        public void Harvest()
        {
            MouseState state = Mouse.GetState();
            Debug.Print("x" + state.X.ToString() + " y " + state.Y.ToString());
            if (soilHasPlant)
            {
                if (growthStage >= 3)
                {
                    if (state.X > rectSize & state.X < rectSize * 2 & state.Y > rectSize & state.Y < rectSize * 2)
                    {
                        if (state.LeftButton == ButtonState.Pressed)
                        {
                            //(receive product and new seed)
                            soilHasPlant = false;
                        }
                    }
                }
            }
        }
    }
}
