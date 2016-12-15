using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final
{
	class Camera
	{
		public static int startY = 440;

		Game1 game1;
		Vector2 offset;
		Vector2 offsetPreShake = new Vector2(-1, -1);
		int shakeFrame = -1;
		const int shakeTime = 10;
		bool doScrollDown = false;
		float t = 0.0f;
		public bool DoScrollDown { set { doScrollDown = value; } }

		public Camera(Game1 game1)
		{
			this.game1 = game1;
			offset = new Vector2(0, startY);
		}

		public void Reset()
		{
			offset = Vector2.Zero;
			shakeFrame = -1;
		}

		void ScrollDown(float t)
		{
			offset.Y += (0 - offset.Y) * (t * t);

			if (t >= 1)
			{
				this.t = 0.0f;
				offset.Y = 0;
				doScrollDown = false;
				game1.StartNewGame();
			}
		}

		public void Update(int frames)
		{
			if (doScrollDown)
				ScrollDown(t += 0.01f);

			if ((frames - shakeFrame)+shakeTime < shakeTime)
			{
				ScreenShake();
			}
			else
			{
				shakeFrame = -1;
				offsetPreShake = -Vector2.One;
			}
		}

		public void SetOffset(Vector2 target, float speed, bool snap)
		{
			offset.Y = Vector2.Lerp(offset, target, speed).Y;

			if (snap && Vector2.Distance(offset, target) < 0.01f)
				offset.Y = target.Y;
		}

		public void DoScreenShake(int frames)
		{
			shakeFrame = frames+shakeTime;
		}

		void ScreenShake()
		{
			if (offsetPreShake == -Vector2.One)
				offsetPreShake = offset;

			offset = offsetPreShake;

			do offset += new Vector2(Game1.rand.Next(-8, 8), Game1.rand.Next(-8, 8));
			while (offset == -Vector2.One);

			Console.WriteLine(-Vector2.One);
		}

		public int getOffsetX() { return (int)offset.X; }
		public int getOffsetY() { return (int)offset.Y; }
		public Vector2 getOffset() { return offset; }
	}
}
