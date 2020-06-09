using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BoulderDash
{
    class Dirt : Tile
    {
        // ------------------
        // Data
        // ------------------
        private Level ourLevel;
        private bool onGoal = false;
        private Texture2D dirtTexture;

        // ------------------
        // Behaviour
        // ------------------
        public Dirt(Texture2D newDirt, Level newLevel)
            : base(newDirt)
        {
            ourLevel = newLevel;
            dirtTexture = newDirt;
        }
        // ------------------
    }
}
