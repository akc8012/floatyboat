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

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			width = graphics.PreferredBackBufferWidth;
			height = graphics.PreferredBackBufferHeight;

			boat = new Boat();
			cannon = new Cannon(new Vector2(width, 345));
			
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
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

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
