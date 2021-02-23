using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace BaseProject
{
    class Game1 : GameEnvironment
    {
/*        private GraphicsDeviceManager _graphics;
        public SpriteBatch spriteBatch;
        Cell cell;
        Hotbar hotbar;
        Texture2D background;
        public static int BHeight;
        public static int BWidth;

        



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            cell = new Cell(10,10, this);
            hotbar = new Hotbar(10, 10, this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            background = Content.Load<Texture2D>("test");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
           // BHeight = background.Height;
            // TODO: Add your drawing code here
            spriteBatch.Begin();

                cell.Display();

                //spriteBatch.Draw(background, new Rectangle(0, 0, BHeight , BWidth ), Color.Black);
            hotbar.Display();
            spriteBatch.End();
          
*/
        protected override void LoadContent()
        {
            base.LoadContent();
            screen = new Point(520, 300);
            ApplyResolutionSettings();


            gameStateList.Add(new PlayingState());
            GameEnvironment.SwitchTo(0);
        }
    }
}
