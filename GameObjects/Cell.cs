using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HarvestValley
{
    class Cell : GameObjectList
    {
        public int cellID, randomGrass, spriteID;               //every cell has an ID, randomgrass to reset plowed lands, id of sprite to set the visible sprite of the object
        public SpriteGameObject tileSoil, tileSoilWater, grass; //different sprites, soil, watered soil and grass
        public bool cellIsTilled, cellHasPlant, cellHasTree, cellHasWater, cellHasSprinkler, nextRandom, nextToSprinkler, cellHasStone, cellHasTent, cellHasShop; //bool to know what the cell carries
        public const int CELL_SIZE = 64;                        //width of the sprites

        /// <summary>
        /// Niels Duivenvoorden
        /// Every tile on the map is a cell
        /// Cells can be either grass, soil or watered soil
        /// </summary>
        /// <param name="_position"></param>
        /// <param name="_scale"></param>
        /// <param name="_id"></param>
        public Cell(Vector2 _position, float _scale, int _id) : base()
        {
            position = _position; 
            cellID = _id;
            randomGrass = GameEnvironment.Random.Next(4);

            grass = new SpriteGameObject("tiles/spr_grass", 0, "0");
            tileSoil = new SpriteGameObject("tiles/spr_tilled_soil", 0, "1");
            tileSoilWater = new SpriteGameObject("tiles/spr_tilled_soil_water", 0, "2");

            Add(grass);
            Add(tileSoil);
            Add(tileSoilWater);

            //make all children invisible
            for (int i = 0; i < children.Count; i++)
            {
                (children[i] as SpriteGameObject).Scale = _scale;
                (children[i] as SpriteGameObject).PerPixelCollisionDetection = false;
                (children[i] as SpriteGameObject).Visible = false;
            }

            //set the grass tile visible
            children[0].Visible = true;
            spriteID = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //if the palyer sleeps 1/4 chance to unplow the land
            if (nextRandom)
            {
                randomGrass = GameEnvironment.Random.Next(4);
                nextRandom = false;
            }

            //set all childs invisible
            foreach (SpriteGameObject SGO in Children)
            {
                SGO.Visible = false;
                if (SGO.Id == spriteID.ToString())
                {
                    //only activate the wanted sprite
                    SGO.Visible = true;
                }
            }
        }

        /// <summary>
        /// Check if any sprite collides with the given SpriteGameObject
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool CellCollidesWith(SpriteGameObject other)
        {
            foreach (SpriteGameObject SGO in children)
            {
                if (SGO.CollidesWith(other))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Change the sprite to a declared sprite
        /// </summary>
        /// <param name="_spriteID"></param>
        public void ChangeSpriteTo(int _spriteID)
        {
            //error prevention
            if (_spriteID > children.Count)
            {
                Debug.WriteLine("Verkeerde spriteID van de Cell");
                return;
            }
            //change the sprite
            spriteID = _spriteID;
        }

        /// <summary>
        /// Check if the cell carries an object that needs collision
        /// </summary>
        public bool HasCollision
        {
            get { return cellHasTree || cellHasSprinkler || cellHasStone || cellHasTent || cellHasShop; }
        }
    }
}