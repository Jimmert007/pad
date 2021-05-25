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
        public SpriteGameObject playerReach;
        float time, maxTimer = 120;
        bool _deductEnergy;

        public Player(string _assetName, Vector2 _position, float _scale) : base(_assetName)
        {
            position = _position - new Vector2(32, 32);
            scale = _scale;
            PerPixelCollisionDetection = false;
            playerReach = new SpriteGameObject("Player/spr_player_reach");
            playerReach.Position = GameEnvironment.Screen.ToVector2() * .5f - new Vector2(playerReach.Width * .5f, playerReach.Height * .5f);
        }

        override public void Reset()
        {
            base.Reset();
            position = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
        }
        public bool PlayerCanReach()
        {
            if (position.X + sprite.Width / 2 - Mouse.GetState().X <= 10
                & position.X + sprite.Width / 2 - Mouse.GetState().X >= -10
                & position.Y + sprite.Height / 2 - Mouse.GetState().Y <= 10
                & position.Y + sprite.Height / 2 - Mouse.GetState().Y >= -10)
            {
                return true;
            }
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (time > maxTimer)
            {
                time = 0;
                DeductEnergy = true;
            }
            else
            {
                DeductEnergy = false;
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.IsKeyDown(Keys.A) || inputHelper.IsKeyDown(Keys.D) || inputHelper.IsKeyDown(Keys.W) || inputHelper.IsKeyDown(Keys.S))
            {
                time++;
            }
        }

        public bool DeductEnergy
        {
            get { return _deductEnergy; }
            set { _deductEnergy = value; }
        }

        /*public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(playerReach.Sprite.Sprite, new Rectangle((int)playerReach.Position.X, (int)playerReach.Position.Y, playerReach.Width, playerReach.Height), new Color(255,0,0,.5f));
        }*/
    }
}

