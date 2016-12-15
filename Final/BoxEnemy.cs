using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final
{
	class BoxEnemy : Enemy
	{
		Point size;
		Rectangle GetRectangle { get { return new Rectangle((int)pos.X, (int)pos.Y, size.X, size.Y); } }

		public BoxEnemy(Vector2 pos, Boat boat, Camera camera, Texture2D texture) :
			base (pos, boat, camera, texture)
		{
			
		}

		public override void LoadContent(Texture2D texture)
		{
			this.texture = texture;
			size = new Point(texture.Width, texture.Height);
		}

		protected override Vector2 GetSize()
		{
			return new Vector2(GetCollisionRect().Width, GetCollisionRect().Height);
		}

		public Rectangle GetCollisionRect()
		{
			Rectangle colRect = GetRectangle;
			colRect.Width /= 2;
			colRect.X += size.X / 2;
			colRect.Y += 5;
			return colRect;
		}

		protected override bool CollisionWithBoat()
		{
			return GetCollisionRect().Intersects(boat.GetRectangle);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (visible) spriteBatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y + camera.getOffsetY(), GetRectangle.Width, GetRectangle.Height), Color.White);
		}
	}
}
