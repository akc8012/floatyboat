using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final
{
	class BoxEnemy : Enemy
	{
		Point size;
		public Rectangle GetRectangle { get { return new Rectangle((int)pos.X, (int)pos.Y, size.X, size.Y); } }

		public BoxEnemy(Vector2 pos, Boat boat, Camera camera) :
			base (pos, boat, camera)
		{
			
		}

		public override void LoadContent(Texture2D texture)
		{
			this.texture = texture;
			size = new Point(texture.Width, texture.Height);
		}

		protected override bool CollisionWithBoat()
		{
			return GetRectangle.Intersects(boat.GetRectangle);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (visible) spriteBatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y + camera.getOffsetY(), GetRectangle.Width, GetRectangle.Height), Color.White);
		}
	}
}
