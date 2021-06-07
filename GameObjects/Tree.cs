using Microsoft.Xna.Framework;

namespace HarvestValley.GameObjects
{
    /// <summary>
    /// Jim van de Burgwal
    /// This script contains multiple sprites for the different treestages, hitbox and when the tree is hit
    /// It also contains the tree health and has its own collision function
    /// </summary>
    class Tree : GameObjectList
    {
        public int hitTimer;                //timer for hit cooldown
        public int hitTimerReset = 120;     //amount of frames for hit cooldown
        public bool treeHit = false;        //boolean for when the tree is hit
        public int health = 4;              //amount of health the tree has
        public int growthStage;             //keeps track of sprite
        SpriteGameObject treeHitbox, tree1stage1, tree1stage2, tree1stage3, treeCut; //SpriteGameObjects for different stages, hitbox and when the tree is hit
        public Tree(Vector2 _position, float _scale, int growthStage) : base()
        {
            position = _position;           //sets the given position
            treeHitbox = new SpriteGameObject("UI/ObjectBackground", 0, "0");   //sets an invisible square SpriteGameObject for the collision hitbox
            tree1stage1 = new SpriteGameObject("Environment/spr_tree", 0, "1"); //SpriteGameObject for first growthstage
            tree1stage2 = new SpriteGameObject("Environment/spr_tree", 0, "2"); //SpriteGameObject for second growthstage
            tree1stage3 = new SpriteGameObject("Environment/spr_tree", 0, "3"); //SpriteGameObject for third growthstage
            treeCut = new SpriteGameObject("Environment/spr_tree_cut", 0, "4"); //SpriteGameObject for when the tree is hit
            //adds the SpriteGameObjects to the list
            Add(treeHitbox);
            Add(tree1stage1);
            Add(tree1stage2);
            Add(tree1stage3);
            Add(treeCut);

            //sets origin for the tree stages, because the tree is taller than a cell
            tree1stage1.Origin = new Vector2(0, 45);
            tree1stage2.Origin = new Vector2(0, 45);
            tree1stage3.Origin = new Vector2(0, 45);

            //sets the position more to the center, because stage 1 and 2 are smaller
            tree1stage1.Position = new Vector2(16, 16);
            tree1stage2.Position = new Vector2(8, 8);

            //sets the origin for the sprite when the tree is cut higher and to the left to make it look like it got hit
            treeCut.Origin = new Vector2(5, 50);

            //sets all SpriteGameObjects to the given scale and makes them invisible
            for (int i = 0; i < children.Count; i++)
            {
                (children[i] as SpriteGameObject).Scale = _scale;
                (children[i] as SpriteGameObject).PerPixelCollisionDetection = false; //Niels Duivenvoorden gemaakt
                (children[i] as SpriteGameObject).Visible = false;
            }

            //sets different scales for the treestages and collision hitbox
            treeHitbox.scale = 1;
            tree1stage1.scale = .25f;
            tree1stage2.scale = .375f;
            tree1stage3.scale = .5f;

            //sets the given growthstage
            this.growthStage = growthStage;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //growthstage gets 1 higher every sleepcycle, this keeps it from getting above the max and also resets the visible sprite after a tree hit
            if (growthStage > 3)
            {
                growthStage = 3;
            }
            //only shows the SpriteGameObject when its id is equal to the growthstage
            foreach (SpriteGameObject SGO in Children)
            {
                SGO.Visible = false;
                if (SGO.Id == growthStage.ToString())
                {
                    SGO.Visible = true;
                }
                if (treeHit) //when a tree is hit
                {
                    growthStage = 4; //only show the tree hit SpriteGameObject
                    hitTimer -= 1; //the cooldown timer gets reset in the playingstate, here it counts down per frame
                    if (hitTimer <= 0) //when the timer hits 0
                    {
                        treeHit = false; //tree hit is false
                    }
                }
            }
            treeHitbox.Visible = true; //always have the hitbox visible/active
        }

        /// <summary>
        /// By "overwriting" the CollidesWith bool we can use it for just the first child in the list, which is the collision hitbox
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CollidesWith(SpriteGameObject obj)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if ((children[0] as SpriteGameObject).CollidesWith(obj))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
