using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace BoulderDash
{
    class Tile : Sprite
    {
        // ------------------
        // Data
        // ------------------
        private Vector2 tilePosition;

        private const int TILE_SIZE = 100;
        // ------------------


        // ------------------
        // Behaviour
        // ------------------
        public Tile(Texture2D newTexture)
            : base(newTexture)
        {
        }
        // ------------------
        public void SetTilePosition(Vector2 newTilePosition)
        {
            tilePosition = newTilePosition;
            // Set our position based on tile position
            // Multiply our tile position by the tile size
            SetPosition(tilePosition * TILE_SIZE);
        }
        // ------------------
        // Write a getter for the tile position!
        public Vector2 GetTilePosition()
        {
            return tilePosition;
        }
        // ------------------
        public virtual void Update(GameTime gameTime)
        {
            // Blank - to be implemented by derived / child classes
        }
        // ------------------

    }
}
