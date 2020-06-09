using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BoulderDash
{
    class Player : Tile
    {
        // ------------------
        // Data
        // ------------------
        private Level ourLevel;
        private float timeSinceLastMove = 0;
        private int score = 0;

        private const float MOVE_COOLDOWN = 0.2f;

        // ------------------
        // Behaviour
        // ------------------
        public Player(Texture2D newTexture, Level newLevel)
            : base(newTexture)
        {
            ourLevel = newLevel;
        }
        // ------------------
        public override void Update(GameTime gameTime)
        {
            // Add to time since we last moved
            float frameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceLastMove += frameTime;

            // Get the current keyboard state
            KeyboardState keyboardState = Keyboard.GetState();

            // Check specific keys and record movement
            Vector2 movementInput = Vector2.Zero;

            // Check each key
            if (keyboardState.IsKeyDown(Keys.A))
            {
                movementInput.X = -1.0f;
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                movementInput.X = 1.0f;
            }
            else if (keyboardState.IsKeyDown(Keys.W))
            {
                movementInput.Y = -1.0f;
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                movementInput.Y = 1.0f;
            }

            // If we have pressed any direction, try to move there!
            if (movementInput != Vector2.Zero && timeSinceLastMove >= MOVE_COOLDOWN)
            {
                TryMove(movementInput);
                timeSinceLastMove = 0;
            }
        }
        // ------------------
        private bool TryMove(Vector2 direction)
        {
            Vector2 newGridPos = GetTilePosition() + direction;

            // Ask the level what is in this slot already
            Tile tileInDirection = ourLevel.GetTileAtPosition(newGridPos);

            // If the target tile is a wall, we can't move there - return false.
            if (tileInDirection != null && tileInDirection is Wall)
            {
                // TODO: Play bump SFX
                return false;
            }

            if (tileInDirection != null && tileInDirection is Diamond)
            {
                score++;
            }

            // If the target tile is a box, try to push it.
            if (tileInDirection != null && tileInDirection is Boulder)
            {
                Boulder targetBox = tileInDirection as Boulder;
                bool pushSuccess = targetBox.TryPush(direction);

                if (pushSuccess == false)
                {
                    return false;
                }
            }

            Tile floorInDirection = ourLevel.GetFloorAtPosition(newGridPos);

            if (floorInDirection != null && floorInDirection is Goal)
            {
                ourLevel.NextLevel();
            }


            bool moveResult = ourLevel.TryMoveTile(this, newGridPos);

            // return true or false based on move successfullness
            return moveResult;
        }
        // ------------------
        public int GetScore()
        {
            return score;
        }
    }
}
