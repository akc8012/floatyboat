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

		bool addedScore = false;
		bool deleteThis = false;
		bool boatJumped = false;
		public bool DeleteThis { get { return deleteThis; } }

		public Enemy(Vector2 pos, Boat boat, Camera camera, Texture2D texture)
		{
			this.pos = pos;
			this.boat = boat;
			this.camera = camera;
			vel = new Vector2(-10, 0);
			LoadContent(texture);
		}

		public abstract void LoadContent(Texture2D texture);

		public virtual void Update()
		{
			pos += vel;

			if (pos.X < -GetSize().X)
			{
				//pos.X = Game1.width;
				//visible = true;
				deleteThis = true;
			}

			if (CollisionWithBoat() && visible)
			{
				boat.LoseHeart();
				visible = false;
			}

			if ((pos.Y > boat.GetRectangle.Y + boat.GetRectangle.Height) &&
				(pos.X > boat.GetRectangle.X && pos.X + GetSize().X < boat.GetRectangle.X + boat.GetRectangle.Width))
			{
				boatJumped = true;
			}

			if (pos.X + GetSize().X < boat.GetRectangle.X && visible && !addedScore)
			{
				boat.AddScore(boatJumped ? 10 : 5);
				Console.WriteLine(boatJumped);
				addedScore = true;
			}
		}

		protected abstract Vector2 GetSize();

		protected abstract bool CollisionWithBoat();

		public abstract void Draw(SpriteBatch spriteBatch);
	}
}
