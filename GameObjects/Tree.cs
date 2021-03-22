using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseProject
{
    class Tree : GameObject
    {
        public int hitTimer;
        public int hitTimerReset = 60;
        public bool treeHit = false;
        public int health = 4;
        public Texture2D treeCut, normalTree;
        public Tree(string assetName, int _x, int _y, int _w, int _h) : base(assetName)
        {
            position.X = _x;
            position.Y = _y;
            size.X = _w;
            size.Y = _h;
            normalTree = GameEnvironment.ContentManager.Load<Texture2D>("spr_tree_cut");
            treeCut = GameEnvironment.ContentManager.Load<Texture2D>("spr_tree_cut");

        }

        public override void Update()
        {
            base.Update();
            if (!treeHit)
            {
                texture = normalTree;
            }
            if (treeHit)
            {
                hitTimer -= 1;
                texture = treeCut;
                if (hitTimer <= 0)
                {
                    treeHit = false;
                }
            }
        }
    }
}
