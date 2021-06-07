namespace HarvestValley.GameObjects
{
    /// <summary>
    /// Niels Duivnevoorden
    /// Class to draw a mouseobject
    /// Also contains collision with one pixel in th top left corner of the object
    /// </summary>
    class MouseGameObject : GameObjectList
    {
        SpriteGameObject mouseSprite, collideSprite;
        public MouseGameObject(string _assetName = "UI/spr_mouse") : base()
        {
            collideSprite = new SpriteGameObject("Player/1px");
            collideSprite.PerPixelCollisionDetection = false;
            Add(mouseSprite = new SpriteGameObject(_assetName));
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            //Set position to the mouse position
            mouseSprite.Position = inputHelper.MousePosition;
            collideSprite.Position = inputHelper.MousePosition;
        }

        /// <summary>
        /// Check collision between the mouse and given SGO
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool CollidesWith(SpriteGameObject other)
        {
            return (collideSprite.CollidesWith(other));
        }

        /// <summary>
        /// Use this SGO to get the hitbox of the mouse
        /// </summary>
        public SpriteGameObject HitBox
        {
            get { return collideSprite; }
        }
    }
}
