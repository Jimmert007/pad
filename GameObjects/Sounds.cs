using Microsoft.Xna.Framework.Audio;

namespace HarvestValley.GameObjects
{
    /// <summary>
    /// Luke Sikma, Niels Duivenvoorden
    /// Class to Load and initialize Sounds
    /// </summary>
    public class Sounds : GameObjectList
    {
        //Array with all the soundeffect names
        public string[] soundEffectStrings = { "FootstepsOnGrass", "AxeSwing", "PickaxeSwing", "TreeFalling", "WaterSplash", "RoosterCrowing", "MetalRattling", "HittingGround", "Shaking1", "ButtonClick", "WheatPickup", "CoinDrop", "PlacingTree" }; 

        public SoundEffect[] SFXs;
        public SoundEffectInstance[] SEIs; // A Array to use when wanted to play a sound

        public Sounds()
        {
            SFXs = new SoundEffect[soundEffectStrings.Length];
            SEIs = new SoundEffectInstance[SFXs.Length];

            //Looping through soundEffectStrings to load into SFXs and initialize into SEIs
            for (int s = 0; s < SFXs.Length; s++)
            {
                // Getting the soundseffects names to use them to load them
                SFXs[s] = GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/" + soundEffectStrings[s]);
                //Initialize the Current sound
                SEIs[s] = SFXs[s].CreateInstance();
            }
        }
    }
}
