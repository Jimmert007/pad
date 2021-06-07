namespace HarvestValley.GameObjects
{
    /// <summary>
    /// Niels Duivenvoorden
    /// A button class that displays a button and is able to detect Overlap and when the button gets clicked on
    /// </summary>
    class TargetButton : SpriteGameObject
    {
        SpriteGameObject mouseGO;
        private bool _onClick;
        public TargetButton(string _assetName = "UI/spr_yes_button") : base(_assetName)
        {
            origin = sprite.Center;
            mouseGO = new SpriteGameObject("Player/1px");
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            mouseGO.Position = inputHelper.MousePosition;
            OnClick = inputHelper.MouseLeftButtonDown() && Overlap();
        }

        /// <summary>
        /// Checks if the button collides with the mouse sprite
        /// </summary>
        /// <returns></returns>
        bool Overlap()
        {
            return (CollidesWith(mouseGO));
        }

        /// <summary>
        /// Property once the button gets clicked
        /// </summary>
        public bool OnClick
        {
            get { return _onClick; }
            set { _onClick = value; }
        }
    }
}
