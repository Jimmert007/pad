using System;
using System.Collections.Generic;
using System.Text;

namespace HarvestValley.GameObjects
{
    class MouseGameObject : GameObjectList
    {
        SpriteGameObject mouseSprite, collideSprite;
        public MouseGameObject(string _assetName = "UI/spr_mouse") : base()
        {
            collideSprite = new SpriteGameObject("Player/1px");
            collideSprite.PerPixelCollisionDetection = false;
            Add(mouseSprite = new SpriteGameObject(_assetName));
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            mouseSprite.Position = inputHelper.MousePosition;
            collideSprite.Position = inputHelper.MousePosition;
        }

        public bool CollidesWith(SpriteGameObject other)
        {
            return (collideSprite.CollidesWith(other));
        }

        public SpriteGameObject HitBox
        {
            get { return collideSprite; }
        }
    }
}
