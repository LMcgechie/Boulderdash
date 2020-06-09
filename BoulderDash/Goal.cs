using Microsoft.Xna.Framework.Graphics;

namespace BoulderDash
{
    class Goal : Tile
    {
        private Level ourLevel;
        private Texture2D offTexture;
        private Texture2D onTexture;
        bool active = false;
        // ------------------
        // Behaviour
        // ------------------
        public Goal(Texture2D newOffTexture, Texture2D newOnTexture, Level newLevel)
            : base(newOffTexture)
        {
            ourLevel = newLevel;
            onTexture = newOnTexture;
            offTexture = newOffTexture;
        }
        // ------------------

        public void SetGoal(bool complete)
        {
            if (complete)
                texture = onTexture;
            else
                texture = offTexture;
        }

        public bool GetActive()
        {
            return active;
        }

        public void SetActive(bool newActive)
        {
            active = newActive;
        }
    }
}
