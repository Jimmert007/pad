using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace BaseProject
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;


        Sleeping sleeping;
        GlobalTime globalTime;

        private SpriteBatch spriteBatch;

        Cell cell;
        Tilling tilling;
        Texture2D background;


        public Game1()
        {
            sleeping = new Sleeping();
            _graphics = new GraphicsDeviceManager(this);
            globalTime = new GlobalTime();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }



        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            background = Content.Load<Texture2D>("test");
        }
    }
}
