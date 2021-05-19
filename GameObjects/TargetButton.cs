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
        private bool _onClick;
        public TargetButton(string _assetName = "UI/spr_yes_button") : base(_assetName)
        {
            origin = sprite.Center;
            mouseGO = new SpriteGameObject("Player/1px");
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            mouseGO.Position = inputHelper.MousePosition;
            if (inputHelper.MouseLeftButtonDown() && Overlap())
            {
                _onClick = true;
            }
            else
            {
                _onClick = false;
            }
        }

        public bool Overlap()
        {
            return (CollidesWith(mouseGO));
        }
        public bool OnClick
        {
            get { return _onClick; }
            set { _onClick = value; }
        }
    }
}
