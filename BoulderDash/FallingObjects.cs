using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace BoulderDash
{
    class FallingObject : Tile
    {
        Vector2 left1 = new Vector2(-1, 0);
        Vector2 left2 = new Vector2(-1, 1);
        Vector2 below = new Vector2(0, 1);
        Vector2 right1 = new Vector2(1, 0);
        Vector2 right2 = new Vector2(1, 1);

        bool falling = false;

        float waitTime = 0.75f;
        float timer = 0f;

        Tile tileBelow;
        Tile tileLeft1;
        Tile tileLeft2;
        Tile tileRight1;
        Tile tileRight2;
        Level thisLevel;

        public FallingObject(Texture2D newTexture)
            : base(newTexture)
        {
        }

        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        protected void fall(Level newLevel, Vector2 gridPos)
        {
            thisLevel = newLevel;
            tileBelow = thisLevel.GetTileAtPosition(gridPos + below);
            tileLeft1 = thisLevel.GetTileAtPosition(gridPos + left1);
            tileLeft2 = thisLevel.GetTileAtPosition(gridPos + left2);
            tileRight1 = thisLevel.GetTileAtPosition(gridPos + right1);
            tileRight2 = thisLevel.GetTileAtPosition(gridPos + right2);

            if (tileBelow is FallingObject)
            {
                if (tileLeft1 == null && tileLeft2 == null)
                {
                    if (timer >= waitTime)
                    {
                        thisLevel.TryMoveTile(this, gridPos + left1);
                        timer = 0f;
                        falling = true;
                    }
                    else
                        return;
                }
                else if (tileRight1 == null && tileRight2 == null)
                {
                    if (timer >= waitTime)
                    {
                        thisLevel.TryMoveTile(this, gridPos + right1);
                        timer = 0f;
                        falling = true;
                    }
                    else
                        return;
                }
                else
                    return;
            }
            else if (falling && tileBelow is Player)
            {
                thisLevel.RestrartLevel();
            }
            else if (tileBelow == null)
            {
                if (timer >= waitTime)
                {
                    thisLevel.TryMoveTile(this, gridPos + below);
                    timer = 0f;
                }
                else
                    return;
            }
            else
                return;
        }
    }
}
