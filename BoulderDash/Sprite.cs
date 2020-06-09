using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BoulderDash
{
    class Sprite
    {
        // ------------------
        // Data
        // ------------------
        protected Vector2 position;
        protected Texture2D texture;
        protected bool visible = true;


        // ------------------
        // Behaviour
        // ------------------
        public Sprite(Texture2D newTexture)
        {
            texture = newTexture;
        }
        // ------------------
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (visible == true)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }
        // ------------------
        public bool GetVisible()
        {
            return visible;
        }
        // ------------------
        public void SetVisible(bool newVisible)
        {
            visible = newVisible;
        }
        // ------------------
        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }
        // ------------------
        public virtual Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        // ------------------
        public void DrawBounds(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            // Use our bounds to calculate texture size etc
            Rectangle bounds = GetBounds();

            // Create the empty texture to be drawn
            Texture2D boundsTexture = new Texture2D(graphics, bounds.Width, bounds.Height);

            // Fill in the texture with white
            Color[] colorData = new Color[bounds.Width * bounds.Height];
            for (int i = 0; i < colorData.Length; ++i)
            {
                colorData[i] = Color.White;
            }
            boundsTexture.SetData(colorData);

            // Draw the texture
            spriteBatch.Draw(boundsTexture, position, Color.White);
        }
        // ------------------
    }
}
