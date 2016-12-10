using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final
{
	class Cannon
	{
		Texture2D texture;
		Vector2 pos;
		Vector2 vel;
		Point size;

		public Cannon(Vector2 pos)
		{
			this.pos = pos;
			vel = new Vector2(-10, 0);
		}

		public void LoadContent(Texture2D texture)
		{
			this.texture = texture;
			size = new Point(texture.Width, texture.Height);
		}

		public void Update()
		{
			pos += vel;

			if (pos.X < 0)
				pos.X = Game1.width;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y, size.X, size.Y), Color.White);
		}
	}
}
