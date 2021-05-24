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
        public int hitTimerReset = 120;
        public bool treeHit = false;
        public int health = 4;
        SpriteGameObject treeHitbox;
        GameObjectList trees, stages;

        public int growthStage = 0;
        int activeSeason;
        public Tree(Vector2 _position, float _scale, int growthStage) : base()
        {
            position = _position;
            Add(treeHitbox = new SpriteGameObject("UI/ObjectBackground", 0, "0"));
            for (int i = 0; i < children.Count; i++)
            {
                (children[i] as SpriteGameObject).Scale = _scale;
                (children[i] as SpriteGameObject).PerPixelCollisionDetection = false;
                (children[i] as SpriteGameObject).Visible = false;
            }
            treeHitbox.scale = 1;

            trees = new GameObjectList();
            trees.Add(new SpriteGameObject("Environment/spr_tree", 0, "0"));
            trees.Add(new SpriteGameObject("Environment/spr_tree_red", 0, "1"));
            trees.Add(new SpriteGameObject("Environment/spr_tree_white", 0, "2"));
            trees.Add(new SpriteGameObject("Environment/spr_tree_lightgreen", 0, "3"));
            Add(trees);

            for (int i = 0; i < trees.Children.Count; i++)
            {
                (trees.Children[i] as SpriteGameObject).Scale = _scale;
                (trees.Children[i] as SpriteGameObject).PerPixelCollisionDetection = false;
                (trees.Children[i] as SpriteGameObject).Visible = false;
            }

            stages = new GameObjectList();
            foreach (SpriteGameObject SGO in trees.Children)
            {
                for (int i = 0; i < 4; i++)
                {
                    SpriteGameObject _stage;
                    if (i < 3)
                    {
                        _stage = new SpriteGameObject(SGO.Sprite.Sprite.Name, 0, i.ToString());
                        _stage.Origin = new Vector2(0, 45);
                        if (i == 0)
                        {
                            _stage.Position = new Vector2(16, 16);
                            _stage.scale = .25f;
                        }
                        else if (i == 1)
                        {
                            _stage.Position = new Vector2(8, 8);
                            _stage.scale = .375f;
                        }
                        else
                        {
                            _stage.scale = .5f;
                        }
                    }
                    else
                    {
                        _stage = new SpriteGameObject("Environment/spr_tree_cut", 0, i.ToString());
                        _stage.Origin = new Vector2(5, 50);
                    }
                    _stage.PerPixelCollisionDetection = false;
                    _stage.Visible = false;
                    stages.Add(_stage);
                }
            }

            this.growthStage = growthStage;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (SpriteGameObject SGO in trees.Children)
            {
                if (SGO.Id == activeSeason.ToString())
                {
                    
                }
            }
        }

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
