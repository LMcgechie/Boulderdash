using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace BoulderDash
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Dictionary<string, Screen> screens = new Dictionary<string, Screen>();
        Screen currentScreen = null;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width - 50;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height - 100;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Level level = new Level(this);
            level.LoadContent(Content, GraphicsDevice);
            screens.Add("level", level);

            TitleScreen title = new TitleScreen(this);
            title.LoadContent(Content, GraphicsDevice);
            screens.Add("title", title);

            WinScreen win = new WinScreen(this);
            win.LoadContent(Content, GraphicsDevice);
            screens.Add("win", win);

            currentScreen = title;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            currentScreen.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            currentScreen.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ChangeScreen(string screenName)
        {
            // Check and make sure our dictionary actually contains this key
            // Before attempting to access it (otherwise we crasssshhh)
            if (screens.ContainsKey(screenName))
            {
                // The screen DOES exist
                // Set the current screen to it
                currentScreen = screens[screenName];
            }
            // TODO: use an assert or exception if the key is not in the dictionary
        }
    }
}
