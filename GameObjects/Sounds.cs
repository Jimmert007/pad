using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using HarvestValley.GameObjects;
using HarvestValley.GameObjects.Tools;
using HarvestValley.GameObjects.HarvestValley.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using HarvestValley.GameObjects.Tutorial;


namespace HarvestValley.GameObjects
{
    public class Sounds : GameObjectList
    {
        public string[] soundEffectStrings = { "FootstepsOnGrass", "AxeSwing", "PickaxeSwing", "TreeFalling", "WaterSplash", "PersonYawns", "RoosterCrowing", "MetalRattling", "HittingGround", "Shaking1", "ButtonClick", "WheatPickup", "CoinDrop" , "PlacingTree" };

        public SoundEffect[] SFXs;
        public SoundEffectInstance[] SEIs;

        public Sounds()
        {
            SFXs = new SoundEffect[soundEffectStrings.Length];
            SEIs = new SoundEffectInstance[SFXs.Length];

            for (int s = 0; s < SFXs.Length; s++)
            {
                SFXs[s] = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/" + soundEffectStrings[s]);
                SEIs[s] = SFXs[s].CreateInstance();
            }
        }
    }

}
