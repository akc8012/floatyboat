using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final
{
	public class Game1 : Game
	{
		public static int width;
		public static int height;
		public static Random rand = new Random();

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Boat boat;
		EnemyManager enemyManager;
		Camera camera;

		SpriteFont hugeFont;
		Texture2D heart;

		enum State { Title, Game, End }
		State state;

		int frames = 0;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			width = graphics.PreferredBackBufferWidth;
			height = graphics.PreferredBackBufferHeight;
			IsMouseVisible = true;

			camera = new Camera();
			boat = new Boat(camera, this);
			enemyManager = new EnemyManager(boat, camera);
			state = State.Title;

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			heart = Content.Load<Texture2D>("heartTex");
			hugeFont = Content.Load<SpriteFont>("hugeFont");

			boat.LoadContent(Content.Load<Texture2D>("boatTex"), Content.Load<Texture2D>("cannonTex"));
			enemyManager.LoadContent(Content.Load<Texture2D>("cannonTex"), Content.Load<Texture2D>("sharkTex"));
		}

		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		void StartNewGame()
		{
			state = State.Game;
			boat.Reset();
			enemyManager.Reset();
		}

		public void Die()
		{
			state = State.End;
		}

		protected override void Update(GameTime gameTime)
		{
			frames++;
			KeyboardState keyboard = Keyboard.GetState();

			if (keyboard.IsKeyDown(Keys.Escape))
				Exit();

			// start a new game depending on state and keypress
			if (state == State.Title && keyboard.GetPressedKeys().Length > 0 || 
				Mouse.GetState().LeftButton == ButtonState.Pressed)
				StartNewGame();
			if (state == State.End && keyboard.IsKeyDown(Keys.R))
				StartNewGame();

			if (state == State.Game)
			{
				if (keyboard.IsKeyDown(Keys.Down))
					camera.SetOffset(new Vector2(0, -60), 0.075f, false);
				else
					camera.SetOffset(new Vector2(0, 0), 0.15f, true);

				boat.Update();
				enemyManager.Update(frames);
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.SkyBlue);

			spriteBatch.Begin();

			// game
			boat.Draw(spriteBatch);
			enemyManager.Draw(spriteBatch);

			// HUD
			spriteBatch.Draw(heart, new Rectangle(10, 15, 36, 32), new Rectangle((boat.GetHeart(0) ? 0 : 36), 0, 36, 32), Color.White);
			spriteBatch.Draw(heart, new Rectangle(47, 15, 36, 32), new Rectangle((boat.GetHeart(1) ? 0 : 36), 0, 36, 32), Color.White);
			spriteBatch.Draw(heart, new Rectangle(84, 15, 36, 32), new Rectangle((boat.GetHeart(2) ? 0 : 36), 0, 36, 32), Color.White);
			spriteBatch.DrawString(hugeFont, boat.Score+"", new Vector2((width-25)-hugeFont.MeasureString(boat.Score+"").X, 12), Color.Black);

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
