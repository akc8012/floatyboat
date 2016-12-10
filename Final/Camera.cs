using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final
{
	class Camera
	{
		Vector2 offset;

		public Camera()
		{
			offset = new Vector2(0, 0);
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
