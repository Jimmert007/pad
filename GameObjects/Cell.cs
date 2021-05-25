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
        public int cellID, randomGrass, spriteID;
        public SpriteGameObject tileSoil, tileSoilWater, grass;
        public bool cellIsTilled, cellHasPlant, cellHasTree, cellHasWater, cellHasSprinkler, nextRandom, nextToSprinkler, cellHasStone, cellHasTent, cellHasShop;

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

            for (int i = 0; i < children.Count; i++)
            {
                (children[i] as SpriteGameObject).Scale = _scale;
                (children[i] as SpriteGameObject).PerPixelCollisionDetection = false;
                (children[i] as SpriteGameObject).Visible = false;
            }
            children[0].Visible = true;
            spriteID = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (nextRandom)
            {
                randomGrass = GameEnvironment.Random.Next(4);
                nextRandom = false;
            }

            foreach (SpriteGameObject SGO in Children)
            {
                SGO.Visible = false;
                if (SGO.Id == spriteID.ToString())
                {
                    SGO.Visible = true;
                }
            }
        }

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

        public void ChangeSpriteTo(int _spriteID)
        {
            if (_spriteID > children.Count)
            {
                Debug.WriteLine("Verkeerde spriteID van de Cell");
                return;
            }
            spriteID = _spriteID;
        }

        public bool HasCollision
        {
            get { return cellHasTree || cellHasSprinkler || cellHasStone || cellHasTent || cellHasShop; }
        }
    }
}