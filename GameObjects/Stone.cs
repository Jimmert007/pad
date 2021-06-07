using Microsoft.Xna.Framework;

namespace HarvestValley.GameObjects
{
    /// <summary>
    /// Jim van de Burgwal
    /// This script contains multiple sprites for the stone, hitbox and when the stone gets hit
    /// It also contains the stone health and has its own collision function
    /// </summary>
    class Stone : GameObjectList
    {
        public int _sprite;     //keeps track of the sprite
        public int hitTimer, hitTimerReset = 60, health = 6;   //timer for hit cooldown, amount of frams for hit cooldown, amount of health the stone has
        public bool stoneHit;   //boolean for when the stone is hit
        SpriteGameObject stoneHitbox, stone1, stoneMine;    //SpriteGameObjects for the stone, hitbox and when the stone gets hit
        public Stone(Vector2 _position, float _scale) : base()
        {
            position = _position;   //sets the given position
            stoneHitbox = new SpriteGameObject("UI/ObjectBackground", 0, "0");  //Sets an invisible scquare SpriteGameObject for the collision hitbox
            stone1 = new SpriteGameObject("Environment/Stone", 0, "1");         //SpriteGameObject for the stone
            stoneMine = new SpriteGameObject("Environment/Stone", 0, "2");      //SpriteGameObject for when the stone gets hit
            //Adds the SpriteGameObjects
            Add(stoneHitbox);
            Add(stone1);
            Add(stoneMine);

            //Sets the origin for the sprite when the stone is hit higher and to the left to make ik look like the stone got hit
            stoneMine.Origin = new Vector2(5, 5);

            //sets all SpriteGameObjects to the given scale and makes them invisible
            for (int i = 0; i < children.Count; i++)
            {
                (children[i] as SpriteGameObject).Scale = _scale;
                (children[i] as SpriteGameObject).PerPixelCollisionDetection = false; //Niels Duivenvoorden gemaakt
                (children[i] as SpriteGameObject).Visible = false;
            }

            //sets the sprite to SpriteGameObject stone
            _sprite = 1;

            //sets the collision hitbox to the right size
            stoneHitbox.scale = 1;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //only shows the SpriteGameObject when its id is equal to the _sprite
            foreach (SpriteGameObject SGO in Children)
            {
                SGO.Visible = false;

                if (SGO.Id == _sprite.ToString())
                {
                    SGO.Visible = true;
                }
                if (stoneHit)   //when a tree is hit
                {
                    _sprite = 2;    //only show the stone hit SpriteGameObject
                    hitTimer -= 1;  //the cooldown timer gets reset in the playingstate, here it counts down per frame
                    if (hitTimer <= 0)  //when the timer hits 0
                    {
                        stoneHit = false;   //stone hit is false
                    }
                }
                if (!stoneHit)
                {
                    _sprite = 1;    //when stone hit is false the regular stone SpriteGameObject is visible
                }
            }
            stoneHitbox.Visible = true; //always have the hitbox visible/active
        }

        /// <summary>
        /// By "overwriting" the CollidesWith bool we can use it for just the first child in the list, which is the collision hitbox
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CollidesWith(SpriteGameObject obj)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if ((children[0] as SpriteGameObject).CollidesWith(obj))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
