using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HarvestValley.GameObjects
{
    class Tree : GameObjectList
    {
        public int hitTimer;
        public int hitTimerReset = 60;
        public bool treeHit = false;
        public int health = 4;
        SpriteGameObject empty, tree1stage1, tree1stage2, tree1stage3, treeCut;
        public int growthStage = 0;
        public bool soilHasTree;
        public Tree(Vector2 _position, float _scale) : base()
        {
            position = _position;
            empty = new SpriteGameObject("spr_empty", 0, "0");
            tree1stage1 = new SpriteGameObject("spr_tree", 0, "1");
            tree1stage2 = new SpriteGameObject("spr_tree", 0, "2");
            tree1stage3 = new SpriteGameObject("spr_tree", 0, "3");
            treeCut = new SpriteGameObject("spr_tree_cut", 0, "4");
            Add(empty);
            Add(tree1stage1);
            Add(tree1stage2);
            Add(tree1stage3);
            Add(treeCut);
            soilHasTree = false;

            tree1stage1.Origin = new Vector2(0, 45);
            tree1stage2.Origin = new Vector2(0, 45);
            tree1stage3.Origin = new Vector2(0, 45);

            tree1stage1.Position = new Vector2(16, 16);
            tree1stage2.Position = new Vector2(8, 8);

            treeCut.Origin = new Vector2(5, 50);


            for (int i = 0; i < children.Count; i++)
            {
                (children[i] as SpriteGameObject).Scale = _scale;
                (children[i] as SpriteGameObject).PerPixelCollisionDetection = false;
                (children[i] as SpriteGameObject).Visible = false;
            }

            tree1stage1.scale = .25f;
            tree1stage2.scale = .375f;
            tree1stage3.scale = .5f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (growthStage > 3)
            {
                growthStage = 3;
            }
            foreach (SpriteGameObject SGO in Children)
            {
                SGO.Visible = false;
                if (soilHasTree)
                {
                    if (SGO.Id == growthStage.ToString())
                    {
                        SGO.Visible = true;
                    }
                    if (treeHit)
                    {
                        growthStage = 4;
                        hitTimer -= 1;
                        if (hitTimer <= 0)
                        {
                            treeHit = false;
                        }
                    }
                    //if (SGO.Visible)
                    //{
                    //    if (!treeHit && soilHasTree)
                    //    {
                    //        growthStage = 3;
                    //    }

                    //    if (treeHit)
                    //    {
                    //        growthStage = 4;
                    //        hitTimer -= 1;
                    //        if (hitTimer <= 0)
                    //        {
                    //            treeHit = false;
                    //        }
                    //    }
                    //}
                }
            }
        }
    }
}
