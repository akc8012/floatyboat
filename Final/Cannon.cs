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
		int radius;
		Boat boat;
		Camera camera;
		Vector2 Center { get { return new Vector2(pos.X + radius, pos.Y + radius); } }
		bool visible = true;

		public Cannon(Vector2 pos, Boat boat, Camera camera)
		{
			this.pos = pos;
			this.boat = boat;
			this.camera = camera;
			vel = new Vector2(-1, 0);
		}

		public void LoadContent(Texture2D texture)
		{
			this.texture = texture;
			radius = texture.Width/2;
		}

		public void Update()
		{
			pos += vel;

			if (pos.X < 0)
			{
				pos.X = Game1.width;
				visible = true;
			}

			if (Vector2.Distance(Center, boat.Center) < (radius + boat.Radius))
				visible = false;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (visible) spriteBatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y + camera.getOffsetY(), radius*2, radius*2), Color.White);
		}
	}
}
