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
    class Tilling : GameObject
    {
        //Jim van de Burgwal

        //creating variables

        String item = "SEED";
        Boolean soilIsTilled = false;
        Boolean soilHasPlant = false;
        Boolean rightButtonPressed = false;
        int rectSize = 100; 
        public Texture2D soilTexture;
        public Texture2D tilledSoilTexture;
        public Plant plant;
        public Tools tools;



        public Tilling(string _assetName, int _x, int _y, int _w, int _h) : base(_assetName)
        {
            position.X = _x;
            position.Y = _y;
            size.X = _w;
            size.Y = _h;
            soilTexture = GameEnvironment.ContentManager.Load<Texture2D>("spr_soil");
            tilledSoilTexture = GameEnvironment.ContentManager.Load<Texture2D>("spr_tilled_soil");
        }

        public override void Update()
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

            Till();
            Plant();
            Grow();
            Harvest();
        }

        public void Till()
        {
            //tilling the soil
            if (tools.toolSelected == "HOE")
            {
                if (GameEnvironment.MouseState.X > rectSize & GameEnvironment.MouseState.X < rectSize * 2 & GameEnvironment.MouseState.Y > rectSize & GameEnvironment.MouseState.Y < rectSize * 2)
                {
                    if (GameEnvironment.MouseState.LeftButton == ButtonState.Pressed)
                    {
                        soilIsTilled = true;
                    }
                }
            }
        }

        public void Plant()
        {
            //planting a seed
            if (!soilHasPlant)
            {
                if (item == "SEED")
                {
                    if (soilIsTilled)
                    {
                        if (GameEnvironment.MouseState.X > rectSize & GameEnvironment.MouseState.X < rectSize * 2 & GameEnvironment.MouseState.Y > rectSize & GameEnvironment.MouseState.Y < rectSize * 2)
                        {
                            if (GameEnvironment.MouseState.LeftButton == ButtonState.Pressed)
                            {
                                soilHasPlant = true;
                                plant.growthStage = 1;
                            }
                        }
                    }
                }
            }
        }

        public void Grow()
        {
            //growing the plant
            if (soilHasPlant)
            {
                if (GameEnvironment.MouseState.RightButton == ButtonState.Pressed & !rightButtonPressed)
                {
                    rightButtonPressed = true;
                    plant.growthStage += 1;
                }
                if (GameEnvironment.MouseState.RightButton == ButtonState.Released)
                {
                    rightButtonPressed = false;
                }
            }
        }

        public void Harvest()
        {
            if (soilHasPlant)
            {
                if (plant.growthStage >= 4)
                {
                    if (GameEnvironment.MouseState.X > rectSize & GameEnvironment.MouseState.X < rectSize * 2 & GameEnvironment.MouseState.Y > rectSize & GameEnvironment.MouseState.Y < rectSize * 2)
                    {
                        if (GameEnvironment.MouseState.LeftButton == ButtonState.Pressed)
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
