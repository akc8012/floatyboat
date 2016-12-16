using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Final
{
	public class Game1 : Game
	{
		public static int width;
		public static int height;
		public static Random rand = new Random();
		public static int frames = 0;
		int highScore = 0;

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Boat boat;
		EnemyManager enemyManager;
		Camera camera;
		Water water;

		SpriteFont hugeFont;
		Texture2D heart;
		Texture2D background;
		Texture2D titleScreen;
		Texture2D gameOverScreen;

		enum State { Title, Game, End }
		State state;

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

			camera = new Camera(this);
			boat = new Boat(camera, this);
			enemyManager = new EnemyManager(boat, camera);
			water = new Water(camera);
			state = State.Title;

			LoadSaveFile();
			base.Initialize();
		}

		// read in the persistent high score from save.txt on every start up
		void LoadSaveFile()
		{
			using (StreamReader sr = new StreamReader("save.txt"))
			{
				string line;
				// Read and display lines from the file until the end of 
				// the file is reached.
				while ((line = sr.ReadLine()) != null)
				{
					highScore = Convert.ToInt32(line);
				}
			}
		}

		// called whenever we get a new high score. write it to the text file
		void WriteToSaveFile(int newScore)
		{
			string score = newScore + "";
			File.WriteAllText("save.txt", score);
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			heart = Content.Load<Texture2D>("heartTex");
			background = Content.Load<Texture2D>("backgroundTex");
			titleScreen = Content.Load<Texture2D>("title");
			gameOverScreen = Content.Load<Texture2D>("gameOver");
			hugeFont = Content.Load<SpriteFont>("hugeFont");

			boat.LoadContent(Content.Load<Texture2D>("boatTex"), Content.Load<Texture2D>("greatJump"), Content.Load<Texture2D>("niceDodge"));
			enemyManager.LoadContent(Content.Load<Texture2D>("cannonTex"), Content.Load<Texture2D>("sharkTex"));
			water.LoadContent(Content.Load<Texture2D>("waterLayer0"), Content.Load<Texture2D>("waterLayer1"), Content.Load<Texture2D>("waterLayer2"));
			SoundMan.Instance.LoadContent(Content);
		}

		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		public void StartNewGame()
		{
			state = State.Game;
			boat.Reset();
			enemyManager.Reset();
			camera.Reset();
		}

		public void Die()
		{
			state = State.End;

			if (boat.Score > highScore)
			{
				highScore = boat.Score;
				WriteToSaveFile(highScore);
			}
		}

		protected override void Update(GameTime gameTime)
		{
			frames++;
			KeyboardState keyboard = Keyboard.GetState();

			if (keyboard.IsKeyDown(Keys.Escape))
				Exit();

			if (keyboard.IsKeyDown(Keys.M))
				SoundMan.Instance.Mute = true;

			// start a new game depending on state and keypress
			if (state == State.Title && (keyboard.GetPressedKeys().Length > 0 ||
				Mouse.GetState().LeftButton == ButtonState.Pressed))
				camera.DoScrollDown = true;		//StartNewGame();
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
			camera.Update(frames);
			water.Update(frames);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.SkyBlue);

			spriteBatch.Begin();
			// background
			spriteBatch.Draw(background, new Rectangle(camera.getOffsetX(), camera.getOffsetY() - Camera.startY, background.Bounds.Width, background.Bounds.Height), Color.White);

			// game
			water.Draw(spriteBatch, 2);
			water.Draw(spriteBatch, 1);
			boat.Draw(spriteBatch);
			enemyManager.Draw(spriteBatch);
			water.Draw(spriteBatch, 0);
			boat.DrawText(spriteBatch);

			if (state == State.Title)
			{
				float alpha = 1 - (float)(Math.Abs(camera.getOffsetY() - Camera.startY)) / Camera.startY;
				Color screenCol = new Color(alpha, alpha, alpha, alpha);
				spriteBatch.Draw(titleScreen, new Rectangle(0, 0, width, height), screenCol);
			}
			else
			{
				// HUD
				spriteBatch.Draw(heart, new Rectangle(10, 15, 36, 32), new Rectangle((boat.GetHeart(0) ? 0 : 36), 0, 36, 32), Color.White);
				spriteBatch.Draw(heart, new Rectangle(47, 15, 36, 32), new Rectangle((boat.GetHeart(1) ? 0 : 36), 0, 36, 32), Color.White);
				spriteBatch.Draw(heart, new Rectangle(84, 15, 36, 32), new Rectangle((boat.GetHeart(2) ? 0 : 36), 0, 36, 32), Color.White);

				if (state != State.End)
				{
					spriteBatch.DrawString(hugeFont, boat.Score+"", new Vector2((width-25) - hugeFont.MeasureString(boat.Score + "").X+2, 14), new Color(0.1f, 0.1f, 0.1f, 0.8f));
					spriteBatch.DrawString(hugeFont, boat.Score+"", new Vector2((width-25) - hugeFont.MeasureString(boat.Score + "").X, 12), Color.Black);
				}
			}

			if (state == State.End)
			{
				spriteBatch.Draw(gameOverScreen, new Rectangle(0, 0, width, height), Color.White);
				spriteBatch.DrawString(hugeFont, boat.Score+"", new Vector2(470+2, 168+2), new Color(0.1f, 0.1f, 0.1f, 0.8f));
				spriteBatch.DrawString(hugeFont, boat.Score+"", new Vector2(470, 168), Color.Black);

				spriteBatch.DrawString(hugeFont, highScore+"", new Vector2(470+2, 262+2), new Color(0.1f, 0.1f, 0.1f, 0.8f));
				spriteBatch.DrawString(hugeFont, highScore+"", new Vector2(470, 262), Color.Black);
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
