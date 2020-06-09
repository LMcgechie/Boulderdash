using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.IO;

namespace BoulderDash
{
    class Level : Screen
    {
        // ------------------
        // Data
        // ------------------
        private Game1 game;
        private Tile[,] tiles;
        private Tile[,] floorTiles;
        private int currentLevel;
        private int diamondCount;
        private bool loadNextLevel = false;
        private Player player;
        private Goal goal;

        // Constants
        private const int LAST_LEVEL = 5;

        // Assets
        Texture2D wallSprite;
        Texture2D playerSprite;
        Texture2D dirtSprite;
        Texture2D goalOnSprite;
        Texture2D goalOffSprite;
        Texture2D floorSprite;
        Texture2D diamondSprite;
        Texture2D BoulderSprite;

        // ------------------
        // Behaviour
        // ------------------
        public Level(Game1 newGame)
        {
            game = newGame;
        }
        // ------------------
        public void LoadContent(ContentManager content, GraphicsDevice graphics)
        {
            wallSprite = content.Load<Texture2D>("graphics/Wall");
            playerSprite = content.Load<Texture2D>("graphics/PlayerStatic");
            dirtSprite = content.Load<Texture2D>("graphics/Dirt");
            goalOnSprite = content.Load<Texture2D>("graphics/DoorOpen");
            goalOffSprite = content.Load<Texture2D>("graphics/DoorClosed");
            floorSprite = content.Load<Texture2D>("graphics/Floor");
            diamondSprite = content.Load<Texture2D>("graphics/Gem");
            BoulderSprite = content.Load<Texture2D>("graphics/meteorGrey_big4");
            // TEMP - this will be moved later
            LoadLevel(1);
        }
        // ------------------
        public void LoadLevel(int levelNum)
        {
            currentLevel = levelNum;
            string baseLevelName = "LevelFiles/level_";
            LoadLevel(baseLevelName + levelNum.ToString() + ".txt");
        }
        // ------------------
        public void LoadLevel(string fileName)
        {
            // Clear any existing level data
            ClearLevel();

            // Create filestream to open the file and get it ready for reading
            Stream fileStream = TitleContainer.OpenStream(fileName);

            // Before we read in the individual tiles in the level, we need to know 
            // how big the level is overall to create the arrays to hold the data
            int lineWidth = 0; // Eventually will be levelWidth
            int numLines = 0;  // Eventually will be levelHeight
            List<string> lines = new List<string>();    // this will contain all the strings of text in the file
            StreamReader reader = new StreamReader(fileStream); // This will let us read each line from the file
            string line = reader.ReadLine(); // Get the first line
            lineWidth = line.Length; // Assume the overall line width is the same as the length of the first line
            while (line != null) // For as long as line exists, do something
            {
                lines.Add(line); // Add the current line to the list
                if (line.Length != lineWidth)
                {
                    // This means our lines are different sizes and that is a big problem
                    throw new Exception("Lines are different widths - error occured on line " + lines.Count);
                }

                // Read the next line to get ready for the next step in the loop
                line = reader.ReadLine();
            }

            // We have read in all the lines of the file into our lines list
            // We can now know how many lines there were
            numLines = lines.Count;

            // Now we can set up our tile array
            tiles = new Tile[lineWidth, numLines];
            floorTiles = new Tile[lineWidth, numLines];

            // Loop over every tile position and check the letter
            // there and load a tile based on  that letter
            for (int y = 0; y < numLines; ++y)
            {
                for (int x = 0; x < lineWidth; ++x)
                {
                    // Load each tile
                    char tileType = lines[y][x];
                    // Load the tile
                    LoadTile(tileType, x, y);
                }
            }
        }
        // ------------------
        private void LoadTile(char tileType, int tileX, int tileY)
        {
            switch (tileType)
            {
                // Wall
                case 'W':
                    CreateWall(tileX, tileY);
                    CreateFloor(tileX, tileY);
                    break;

                // Player
                case 'P':
                    CreatePlayer(tileX, tileY);
                    CreateFloor(tileX, tileY);
                    break;

                // Dirt
                case 'D':
                    CreateDirt(tileX, tileY);
                    CreateFloor(tileX, tileY);
                    break;

                // Diamond
                case 'S':
                    CreateDiamond(tileX, tileY);
                    CreateFloor(tileX, tileY);
                    break;

                // Boulder
                case 'B':
                    CreateBoulder(tileX, tileY);
                    CreateFloor(tileX, tileY);
                    break;

                // Goal
                case 'G':
                    CreateGoal(tileX, tileY);
                    break;

                // Blank space
                case '.':
                    CreateFloor(tileX, tileY);
                    break; // Do nothing

                // Any non-handled symbol
                default:
                    throw new NotSupportedException("Level contained unsupported symbol " + tileType + " at line " + tileY + " and character " + tileX);
            }
        }
        // ------------------
        private void ClearLevel()
        {
            diamondCount = 0;
        }
        // ------------------
        private void CreateWall(int tileX, int tileY)
        {
            Wall tile = new Wall(wallSprite);
            tile.SetTilePosition(new Vector2(tileX, tileY));
            tiles[tileX, tileY] = tile;
        }
        // ------------------
        private void CreatePlayer(int tileX, int tileY)
        {
            player = new Player(playerSprite, this);
            player.SetTilePosition(new Vector2(tileX, tileY));
            tiles[tileX, tileY] = player;
        }
        // ------------------
        private void CreateDirt(int tileX, int tileY)
        {
            Dirt tile = new Dirt(dirtSprite, this);
            tile.SetTilePosition(new Vector2(tileX, tileY));
            tiles[tileX, tileY] = tile;
        }
        // ------------------
        private void CreateGoal(int tileX, int tileY)
        {
            goal = new Goal(goalOnSprite, goalOffSprite, this);
            goal.SetTilePosition(new Vector2(tileX, tileY));
            floorTiles[tileX, tileY] = goal;
        }
        // ------------------
        private void CreateFloor(int tileX, int tileY)
        {
            Floor tile = new Floor(floorSprite);
            tile.SetTilePosition(new Vector2(tileX, tileY));
            floorTiles[tileX, tileY] = tile;
        }
        // ------------------
        private void CreateDiamond(int tileX, int tileY)
        {
            Diamond tile = new Diamond(diamondSprite, this);
            tile.SetTilePosition(new Vector2(tileX, tileY));
            tiles[tileX, tileY] = tile;
            diamondCount++;
        }
        // ------------------
        private void CreateBoulder(int tileX, int tileY)
        {
            Boulder tile = new Boulder(BoulderSprite, this);
            tile.SetTilePosition(new Vector2(tileX, tileY));
            tiles[tileX, tileY] = tile;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in floorTiles)
            {
                if (tile != null)
                    tile.Draw(spriteBatch);
            }
            foreach (Tile tile in tiles)
            {
                if (tile != null)
                    tile.Draw(spriteBatch);
            }
        }
        // ------------------
        public override void Update(GameTime gameTime)
        {
            foreach (Tile tile in floorTiles)
            {
                if (tile != null)
                    tile.Update(gameTime);
            }
            foreach (Tile tile in tiles)
            {
                if (tile != null)
                    tile.Update(gameTime);
            }

            // If we were waiting to load a new level, do it now
            if (loadNextLevel == true)
            {
                if (currentLevel == LAST_LEVEL)
                {
                    LoadLevel(1); // restart so they can load again
                    game.ChangeScreen("win");
                }
                else
                {
                    LoadLevel(currentLevel + 1);
                }
                loadNextLevel = false;
            }
            if (player.GetScore() == diamondCount)
            {
                goal.SetGoal(true);
                goal.SetActive(true);
            }
        }
        // ------------------
        public bool TryMoveTile(Tile toMove, Vector2 newPosition)
        {
            // Get the current tile position
            Vector2 currentTilePosition = toMove.GetTilePosition();

            // Check if the new position is within bounds
            int newPosX = (int)newPosition.X;
            int newPosY = (int)newPosition.Y;
            if (newPosX >= 0 && newPosY >= 0 && newPosX < tiles.GetLength(0) && newPosY < tiles.GetLength(1))
            {
                toMove.SetTilePosition(newPosition);
                tiles[newPosX, newPosY] = toMove;
                tiles[(int)currentTilePosition.X, (int)currentTilePosition.Y] = null;

                return true;
            }
            else
            {
                return false;
            }
        }
        // ------------------
        public Tile GetTileAtPosition(Vector2 tilePos)
        {
            // Check if the position is within bounds
            int posX = (int)tilePos.X;
            int posY = (int)tilePos.Y;
            if (posX >= 0
                && posY >= 0
                && posX < tiles.GetLength(0) // gets the length in the X direction
                && posY < tiles.GetLength(1)) // gets the array length in the Y direction
            {
                // Yes, this coordinate is legal
                return tiles[posX, posY];
            }
            else
            {
                // NO, this coordinate is NOT legal (out of bounds of array / tile grid)
                return null;
            }

        }
        // ------------------
        public Tile GetFloorAtPosition(Vector2 tilePos)
        {
            // Check if the position is within bounds
            int posX = (int)tilePos.X;
            int posY = (int)tilePos.Y;
            if (posX >= 0
                && posY >= 0
                && posX < floorTiles.GetLength(0) // gets the length in the X direction
                && posY < floorTiles.GetLength(1)) // gets the array length in the Y direction
            {
                // Yes, this coordinate is legal
                return floorTiles[posX, posY];
            }
            else
            {
                // NO, this coordinate is NOT legal (out of bounds of array / tile grid)
                return null;
            }

        }
        // ------------------
        public void NextLevel()
        {
            if (goal.GetActive())
                loadNextLevel = true;
            else
                return;
        }


        public void RestrartLevel()
        {
            LoadLevel(currentLevel);
        }
    }
}
