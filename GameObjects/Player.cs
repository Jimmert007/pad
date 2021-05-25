using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HarvestValley
{
    class Player : SpriteGameObject
    {
        public bool sleeping, sleepingPosition;
        public Vector2 newSleepingPosition = new Vector2(0, 10);
        public Vector2 lastPosition;
        public int speed = 3;
        public SpriteGameObject playerReach, moveLeft, moveRight;

        public Player(string _assetName, Vector2 _position, float _scale) : base(_assetName)
        {
            position = _position - new Vector2(32, 32);
            scale = _scale;
            PerPixelCollisionDetection = false;
            playerReach = new SpriteGameObject("Player/spr_player_reach");
            moveLeft = new SpriteGameObject("Player/jorritLeft");
            moveRight = new SpriteGameObject("Player/jorrit");
            playerReach.Position = GameEnvironment.Screen.ToVector2() * .5f - new Vector2(playerReach.Width * .5f, playerReach.Height * .5f);
        }

        override public void Reset()
        {
            base.Reset();
            position = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.IsKeyDown(Keys.A))
            {
                sprite = moveLeft.Sprite;
            }
            if (inputHelper.IsKeyDown(Keys.D))
            {
                sprite = moveRight.Sprite;
            }
        }
    }
}

