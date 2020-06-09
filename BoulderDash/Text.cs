using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BoulderDash
{
    class Text
    {
        // ------------------
        // Enums
        // ------------------
        public enum Alignment
        {
            TOP_LEFT,
            TOP,
            TOP_RIGHT,
            LEFT,
            CENTRE,
            RIGHT,
            BOTTOM_LEFT,
            BOTTOM,
            BOTTOM_RIGHT
        }


        // ------------------
        // Data
        // ------------------
        SpriteFont font;
        string textString;
        Vector2 position;
        Color color = Color.White;
        Alignment alignment;


        // ------------------
        // Behaviour
        // ------------------
        public Text(SpriteFont newFont)
        {
            font = newFont;
        }
        // ------------------
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 adjustedPosition = position;
            Vector2 textSize = font.MeasureString(textString);

            switch (alignment)
            {
                case Alignment.TOP_LEFT:
                    // Do nothing, already alligned this way by default
                    break;
                case Alignment.TOP:
                    adjustedPosition.X -= textSize.X / 2;
                    break;
                case Alignment.TOP_RIGHT:
                    adjustedPosition.X -= textSize.X;
                    break;
                case Alignment.LEFT:
                    adjustedPosition.Y -= textSize.Y / 2;
                    break;
                case Alignment.CENTRE:
                    adjustedPosition.Y -= textSize.Y / 2;
                    adjustedPosition.X -= textSize.X / 2;
                    break;
                case Alignment.RIGHT:
                    adjustedPosition.Y -= textSize.Y / 2;
                    adjustedPosition.X -= textSize.X;
                    break;
                case Alignment.BOTTOM_LEFT:
                    adjustedPosition.Y -= textSize.Y;
                    break;
                case Alignment.BOTTOM:
                    adjustedPosition.Y -= textSize.Y;
                    adjustedPosition.X -= textSize.X / 2;
                    break;
                case Alignment.BOTTOM_RIGHT:
                    adjustedPosition.Y -= textSize.Y;
                    adjustedPosition.X -= textSize.X;
                    break;
            }

            spriteBatch.DrawString(font, textString, adjustedPosition, color);
        }
        // ------------------
        public void SetTextString(string newString)
        {
            textString = newString;
        }
        // ------------------
        public void SetColor(Color newColor)
        {
            color = newColor;
        }
        // ------------------
        public void SetAlignment(Alignment newAlignment)
        {
            alignment = newAlignment;
        }
        // ------------------
        public void SetPosition(Vector2 newPosition)
        {
            position = newPosition;
        }
        // ------------------
    }
}
