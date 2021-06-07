using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HarvestValley
{
    /// <summary>
    /// Niels Duivenvoorden
    /// A spritegameobject that flips its sprite based on input
    /// to disable player input, use the sleeping bool
    /// also loses energy based on input and controls how fast the player 'moves'
    /// </summary>
    class Player : SpriteGameObject
    {
        public bool sleeping, sleepingPosition;
        public Vector2 newSleepingPosition = new Vector2(0, 10);
        float time, maxTimer = 120;
        public int speed = 3;
        bool _deductEnergy;
        public SpriteGameObject playerReach, moveLeft, moveRight;

        public Player(string _assetName, Vector2 _position) : base(_assetName)
        {
            position = _position - new Vector2(32, 32);                     //place the player in the center
            PerPixelCollisionDetection = false;                             //bool to fix the collision
            playerReach = new SpriteGameObject("Player/spr_player_reach");  //the interactible section around the player
            moveLeft = new SpriteGameObject("Player/jorritLeft");           //flipped sprite
            moveRight = new SpriteGameObject("Player/jorrit");              //normal sprite
            playerReach.Position = GameEnvironment.Screen.ToVector2() * .5f - new Vector2(playerReach.Width * .5f, playerReach.Height * .5f); //set the playerreach 
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            if (inputHelper.IsKeyDown(Keys.A))  //change sprite to face leftwards
            {
                sprite = moveLeft.Sprite;
            }
            if (inputHelper.IsKeyDown(Keys.D))  //change sprite to face rightwards
            {
                sprite = moveRight.Sprite;
            }
            if (inputHelper.IsKeyDown(Keys.A) || inputHelper.IsKeyDown(Keys.D) || inputHelper.IsKeyDown(Keys.W) || inputHelper.IsKeyDown(Keys.S)) //check if the player is moving to detect energyloss
            {
                time++;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //timer system to lose energy every maxTimer frames
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

        /// <summary>
        /// public property to read if the energy bar should lose energy
        /// </summary>
        public bool DeductEnergy
        {
            get { return _deductEnergy; }
            set { _deductEnergy = value; }
        }
    }
}

