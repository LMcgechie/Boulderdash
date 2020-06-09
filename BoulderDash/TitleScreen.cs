using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace BoulderDash
{
    class TitleScreen : Screen
    {
        // ------------------
        // Data
        // ------------------
        private Text gameName;
        private Text startPrompt;
        private Game1 game;


        // ------------------
        // Behaviour
        // ------------------
        public TitleScreen(Game1 newGame)
        {
            game = newGame;
        }
        // ------------------
        public void LoadContent(ContentManager content, GraphicsDevice graphics)
        {
            SpriteFont titleFont = content.Load<SpriteFont>("fonts/largeFont");
            SpriteFont smallFont = content.Load<SpriteFont>("fonts/mainFont");

            gameName = new Text(titleFont);
            gameName.SetTextString("Boulderdash");
            gameName.SetAlignment(Text.Alignment.CENTRE);
            gameName.SetColor(Color.White);
            gameName.SetPosition(new Vector2(graphics.Viewport.Bounds.Width / 2, 100));

            startPrompt = new Text(smallFont);
            startPrompt.SetTextString("[Press ENTER to start]");
            startPrompt.SetAlignment(Text.Alignment.CENTRE);
            startPrompt.SetColor(Color.White);
            startPrompt.SetPosition(new Vector2(graphics.Viewport.Bounds.Width / 2, 200));
        }
        // ------------------
        public override void Draw(SpriteBatch spriteBatch)
        {
            gameName.Draw(spriteBatch);
            startPrompt.Draw(spriteBatch);
        }
        // ------------------
        public override void Update(GameTime gameTime)
        {
            // Check if the player has pressed enter

            // Get the current keyboard state
            KeyboardState keyboardState = Keyboard.GetState();

            // if the player has pressed enter....
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                game.ChangeScreen("level");
            }
        }
        // ------------------
    }
}
