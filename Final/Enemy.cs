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

		bool addedScore = false;
		bool deleteThis = false;
		bool boatJumped = false;
		public bool DeleteThis { get { return deleteThis; } }

		public Enemy(Vector2 pos, Boat boat, Camera camera, Texture2D texture)
		{
			this.pos = pos;
			this.boat = boat;
			this.camera = camera;
			vel = new Vector2(-12, 0);
			LoadContent(texture);
		}

		public abstract void LoadContent(Texture2D texture);

		public virtual void Update()
		{
			pos += vel;

			if (pos.X < -GetSize().X*2)
				deleteThis = true;

			if (!boat.IsInvincible && CollisionWithBoat())
			{
				boat.LoseHeart();
				deleteThis = true;
			}

			if ((pos.Y > boat.GetRectangle.Y + boat.GetRectangle.Height) &&
				(pos.X > boat.GetRectangle.X && pos.X + GetSize().X < boat.GetRectangle.X + boat.GetRectangle.Width))
			{
				boatJumped = true;
			}

			if (pos.X + GetSize().X < boat.GetRectangle.X && !addedScore)
			{
				SoundMan.Instance.PlaySound(boatJumped ? SoundMan.Instance.pointsJump : SoundMan.Instance.pointsSink);
				boat.AddScore(boatJumped ? 10 : 5);
				addedScore = true;
			}
		}

		protected abstract Vector2 GetSize();

		protected abstract bool CollisionWithBoat();

		public abstract void Draw(SpriteBatch spriteBatch);
	}
}
