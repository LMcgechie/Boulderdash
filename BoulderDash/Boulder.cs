using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BoulderDash
{
    class Boulder : FallingObject
    {
        Level thisLevel;
        public Boulder(Texture2D newTexture, Level newLevel)
            : base(newTexture)
        {
            thisLevel = newLevel;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            base.fall(thisLevel, GetTilePosition());
        }

        public bool TryPush(Vector2 direction)
        {
            // New position the box will be in after the push
            Vector2 newGridPos = GetTilePosition() + direction;


            // Ask the level what is in this slot already
            Tile tileInDirection = thisLevel.GetTileAtPosition(newGridPos);

            // If the target tile is a wall, we can't move there - return false.
            if (tileInDirection != null && tileInDirection is Wall)
            {
                return false;
            }
            if (tileInDirection != null && tileInDirection is FallingObject)
            {
                return false;
            }
            if (tileInDirection != null && tileInDirection is Dirt)
            {
                return false;
            }
            if (tileInDirection != null && tileInDirection is Goal)
            {
                return false;
            }
            // Move our tile (box) to the new position
            return thisLevel.TryMoveTile(this, newGridPos);
        }
    }
}
