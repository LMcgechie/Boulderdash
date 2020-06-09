using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BoulderDash
{
    class Diamond : FallingObject
    {
        Level thisLevel;

        public Diamond(Texture2D newTexture, Level newLevel)
            : base(newTexture)
        {
            thisLevel = newLevel;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            base.fall(thisLevel, GetTilePosition());
        }


    }
}
