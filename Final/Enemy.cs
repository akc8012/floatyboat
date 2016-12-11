using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final
{
	abstract class Enemy
	{
		protected Texture2D texture;
		protected Vector2 pos;
		protected Vector2 vel;
		protected Boat boat;
		protected Camera camera;
		protected bool visible = true;

		public Enemy(Vector2 pos, Boat boat, Camera camera)
		{
			this.pos = pos;
			this.boat = boat;
			this.camera = camera;
			vel = new Vector2(-10, 0);
		}

		public abstract void LoadContent(Texture2D texture);

		public virtual void Update()
		{
			pos += vel;

			if (pos.X < 0)
			{
				pos.X = Game1.width;
				visible = true;
			}

			if (CollisionWithBoat() && visible)
				visible = false;
		}

		protected abstract bool CollisionWithBoat();

		public abstract void Draw(SpriteBatch spriteBatch);
	}
}
