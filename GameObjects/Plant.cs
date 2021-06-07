using Microsoft.Xna.Framework;

namespace HarvestValley
{
    /// <summary>
    /// Jim van de Burgwal
    /// This script contains multiple sprites for the plants growthstages
    /// </summary>
    class Plant : GameObjectList
    {
        public int growthStage = 1; //When added the plant starts at the first growthstage
        public SpriteGameObject seed1stage1, seed1stage2, seed1stage3, seed1stage4; //Different SpriteGameObjects are declared for each growthstage
        public bool soilHasWater; //Boolean which checks if the plant has water

        public Plant(Vector2 _postition, float scale) : base()
        {
            position = _postition;
            seed1stage1 = new SpriteGameObject("Environment/spr_seed1_stage1", 0, "1");
            seed1stage2 = new SpriteGameObject("Environment/spr_seed1_stage2", 0, "2");
            seed1stage3 = new SpriteGameObject("Environment/spr_seed1_stage3", 0, "3");
            seed1stage4 = new SpriteGameObject("Environment/spr_seed1_stage4", 0, "4");
            Add(seed1stage1);
            Add(seed1stage2);
            Add(seed1stage3);
            Add(seed1stage4);
            for (int i = 0; i < children.Count; i++)
            {
                (children[i] as SpriteGameObject).Scale = scale;
                (children[i] as SpriteGameObject).PerPixelCollisionDetection = false;
                (children[i] as SpriteGameObject).Visible = false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            //Plant can't grow further if it's fully grown
            if (growthStage > 4)
            {
                growthStage = 4;
            }
            //Only have the right SpriteGameObject shown with the right growthstage
            foreach (SpriteGameObject SGO in Children)
            {
                SGO.Visible = false;
                if (SGO.Id == growthStage.ToString())
                {
                    SGO.Visible = true;
                }
            }
        }
    }
}
