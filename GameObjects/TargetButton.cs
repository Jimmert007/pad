using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HarvestValley.GameObjects
{
    class TargetButton : SpriteGameObject
    {
        SpriteGameObject mouseGO;
        public TargetButton(string _assetName = "spr_yes_button") : base(_assetName)
        {
            origin = sprite.Center;
            mouseGO = new SpriteGameObject("1px");
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            mouseGO.Position = inputHelper.MousePosition;
        }

        public bool Overlap()
        {
            return (CollidesWith(mouseGO));
        }
    }
}
