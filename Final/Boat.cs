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
		int iFrames = -1;
		public bool IsInvincible { get { return iFrames >= 0; } }
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
			jumpCount = 0;
			heartsLeft = 3;
			iFrames = -1;
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

			if (IsKeyPressed(Keys.Down)) SoundMan.Instance.PlaySound(SoundMan.Instance.bubbles);
			if (keyboard.IsKeyDown(Keys.Down))
				targetPoint += duckDist;

			bool canJump = pos.Y+size.Y > Water.waterPoint;
			bool isJumping = (IsKeyPressed(Keys.Up) && jumpCount == 0 && canJump) || (jumpCount <= jumpFrames && jumpCount != 0);

			if (isJumping)
			{
				if (jumpCount == 0) SoundMan.Instance.PlaySound(SoundMan.Instance.jump);
				targetPoint -= jumpOffset;
				jumpCount++;
			}
			else if (jumpCount != 0)
			{
				SoundMan.Instance.PlaySound(SoundMan.Instance.land);
				jumpCount = 0;
			}

			Move((int)pos.X, targetPoint-(size.Y/2), 0.9f, 0.025f);

			if (iFrames >= 0)
				iFrames--;

			lastKeyboard = keyboard;
		}

		bool IsKeyPressed(Keys key)
		{
			return keyboard.IsKeyDown(key) && lastKeyboard.IsKeyUp(key);
		}

		public void LoseHeart()
		{
			/*SoundMan.Instance.PlaySound(SoundMan.Instance.getHit);
			if (heartsLeft > 0)
			{
				hearts[heartsLeft-1] = false;
				heartsLeft--;
				iFrames = 60;
				camera.DoScreenShake(Game1.frames);
			}

			if (heartsLeft <= 0)
			{
				game1.Die();
				iFrames = -1;
			}*/
		}

		public void AddScore(int change) { score += change; }

		public void Draw(SpriteBatch spriteBatch)
		{
			Color drawCol = iFrames >= 0 ? new Color(0.45f, 0.45f, 0.45f, 0.45f) : Color.White;
			spriteBatch.Draw(texture, new Rectangle((int)pos.X + camera.getOffsetX(), (int)pos.Y + camera.getOffsetY(), size.X, size.Y), drawCol);
		}
	}
}
