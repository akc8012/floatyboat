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

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Boat boat;
		Cannon cannon;
		Camera camera;

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
			boat = new Boat(camera);
			cannon = new Cannon(new Vector2(width, 345), camera);
			
			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			boat.LoadContent(Content.Load<Texture2D>("boatTex"));
			cannon.LoadContent(Content.Load<Texture2D>("cannonTex"));
		}

		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		protected override void Update(GameTime gameTime)
		{
			KeyboardState keyboard = Keyboard.GetState();

			if (keyboard.IsKeyDown(Keys.Escape))
				Exit();

			if (keyboard.IsKeyDown(Keys.Down))
				camera.SetOffset(new Vector2(0, -60), 0.075f, false);
			else
				camera.SetOffset(new Vector2(0, 0), 0.15f, true);

			boat.Update();
			cannon.Update();
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.SkyBlue);

			spriteBatch.Begin();
			boat.Draw(spriteBatch);
			cannon.Draw(spriteBatch);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
