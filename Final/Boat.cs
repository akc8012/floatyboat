using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final
{
	class Boat
	{
		Camera camera;
		Texture2D texture;
		Texture2D circleTex;
		Game1 game1;

		Vector2 pos;
		Vector2 vel;
		Point size;
		int radius;
		public int Radius { get { return radius; } }

		const int duckDist = 70;
		const int jumpOffset = 150;
		const int jumpFrames = 28;
		int jumpCount = 0;

		public Vector2 Center { get { return new Vector2(pos.X + size.X/2, pos.Y + radius); } }
		public Rectangle GetRectangle { get { return new Rectangle((int)pos.X, (int)pos.Y, size.X, size.Y); } }
		KeyboardState keyboard;
		KeyboardState lastKeyboard;

		bool[] hearts = new bool[] { true, true, true };    // data for the on-screen hearts
		public bool GetHeart(int i) { return hearts[i]; }
		int heartsLeft = 3;
		int score = 0;
		public int Score { get { return score; } }

		public Boat(Camera camera, Game1 game1)
		{
			this.camera = camera;
			this.game1 = game1;
		}

		public void Reset()
		{
			pos = new Vector2(115, Water.waterPoint - size.Y/2);
			vel = new Vector2(0, 0);
			heartsLeft = 3;
			score = 0;
			hearts = new bool[] { true, true, true };   // reset our hearts display
		}

		public void LoadContent(Texture2D texture, Texture2D circle)
		{
			this.texture = texture;
			circleTex = circle;
			size = new Point(texture.Width, texture.Height);
			radius = size.Y/2;

			Reset();
		}

		void Move(int centerX, int centerY, float inertia, float k)
		{
			float distX = centerX - pos.X;
			float distY = centerY - pos.Y;

			vel.X = vel.X * inertia + distX*k;
			vel.Y = vel.Y * inertia + distY*k;

			pos += vel;
		}

		public void Update()
		{
			keyboard = Keyboard.GetState();
			int targetPoint = Water.waterPoint;

			if (keyboard.IsKeyDown(Keys.Down))
				targetPoint += duckDist;

			bool canJump = pos.Y+size.Y > Water.waterPoint;
			bool isJumping = (IsKeyPressed(Keys.Up) && jumpCount == 0 && canJump) || (jumpCount <= jumpFrames && jumpCount != 0);

			if (isJumping)
			{
				targetPoint -= jumpOffset;
				jumpCount++;
			}
			else
				jumpCount = 0;

			Move((int)pos.X, targetPoint-(size.Y/2), 0.9f, 0.025f);
			lastKeyboard = keyboard;
		}

		bool IsKeyPressed(Keys key)
		{
			return keyboard.IsKeyDown(key) && lastKeyboard.IsKeyUp(key);
		}

		public void LoseHeart()
		{
			if (heartsLeft > 0)
			{
				hearts[heartsLeft-1] = false;
				heartsLeft--;
			}

			if (heartsLeft <= 0)
				game1.Die();
		}

		public void AddScore(int change) { score += change; }

		public void Draw(SpriteBatch spriteBatch)
		{
			// boat
			spriteBatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y + camera.getOffsetY(), size.X, size.Y), Color.White);

			// collision circle
			//spriteBatch.Draw(circleTex, new Rectangle((int)Center.X - (size.Y/2), (int)pos.Y + camera.getOffsetY(), radius*2, radius*2), Color.Blue);

			// water line
			spriteBatch.Draw(texture, new Rectangle(0, Water.waterPoint + camera.getOffsetY(), Game1.width, 2), Color.Blue);
		}
	}
}
