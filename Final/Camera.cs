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
		bool doScrollDown = false;
		float t = 0.0f;
		public bool DoScrollDown { set { doScrollDown = value; } }

		public Camera(Game1 game1)
		{
			this.game1 = game1;
			offset = new Vector2(0, startY);
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

		public void Update()
		{
			if (doScrollDown)
				ScrollDown(t += 0.01f);
		}

		public void SetOffset(Vector2 target, float speed, bool snap)
		{
			offset = Vector2.Lerp(offset, target, speed);

			if (snap && Vector2.Distance(offset, target) < 0.01f)
				offset = target;
		}

		public int getOffsetX() { return (int)offset.X; }
		public int getOffsetY() { return (int)offset.Y; }
		public Vector2 getOffset() { return offset; }
	}
}
