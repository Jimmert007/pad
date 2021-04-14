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
        SpriteGameObject empty, tree1stage1, treeCut;
        public int growthStage = 0;
        public bool soilHasBoom;
        public Tree(Vector2 _position, float _scale) : base()
        {
            position = _position;
            empty = new SpriteGameObject("spr_empty", 0, "0");
            tree1stage1 = new SpriteGameObject("spr_tree", 0, "1");
            treeCut = new SpriteGameObject("spr_tree_cut", 0, "2");
            Add(empty);
            Add(tree1stage1);
            Add(treeCut);
            soilHasBoom = false;
            for (int i = 0; i < children.Count; i++)
            {
                (children[i] as SpriteGameObject).Scale = _scale;
                (children[i] as SpriteGameObject).PerPixelCollisionDetection = false;
                (children[i] as SpriteGameObject).Visible = false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (SpriteGameObject SGO in Children)
            {
                SGO.Visible = false;
                if (SGO.Id == growthStage.ToString())
                {
                    SGO.Visible = true;
                }
                if (SGO.Visible)
                {
                    if (!treeHit && soilHasBoom)
                    {
                        growthStage = 1;
                    }

                    if (treeHit)
                    {
                        growthStage = 2;
                        hitTimer -= 1;
                        if (hitTimer <= 0)
                        {
                            treeHit = false;
                        }
                    }
                }
            }
        }
    }
}
