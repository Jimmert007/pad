using Microsoft.Xna.Framework;

namespace HarvestValley.GameObjects.UI
{
    class Button : SpriteGameObject
    {
        public Button(string _assetName = "Player/1px") : base(_assetName)
        {
        }

        public bool collidesWithMouse(Vector2 _mousePosition)
        {
            return (position.X < _mousePosition.X && position.X + sprite.Width > _mousePosition.X && position.Y < _mousePosition.Y && position.Y + sprite.Height > _mousePosition.Y && Visible);
        }
    }
}
