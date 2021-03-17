using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace BaseProject
{
    class TextGameObject : GameObject
    {
        protected SpriteFont spriteFont;
        protected Color color;
        protected string text;

        public TextGameObject(string assetname) : base("tree")
        {
            spriteFont = GameEnvironment.ContentManager.Load<SpriteFont>(assetname);
            color = Color.White;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, text, position, color);
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public Vector2 Size
        {
            get
            { return spriteFont.MeasureString(text); }
        }
    }
}
