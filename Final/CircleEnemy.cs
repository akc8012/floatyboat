using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final
{
	class CircleEnemy : Enemy
	{
		int radius;
		Vector2 Center { get { return new Vector2(pos.X + radius, pos.Y + radius); } }

		public CircleEnemy(Vector2 pos, Boat boat, Camera camera, Texture2D texture) :
			base (pos, boat, camera, texture)
		{

		}

		public override void LoadContent(Texture2D texture)
		{
			this.texture = texture;
			radius = texture.Width/2;
		}

		protected override Vector2 GetSize()
		{
			return new Vector2(radius*2, radius*2);
		}

		protected override bool CollisionWithBoat()
		{
			return Vector2.Distance(Center, boat.Center) < (radius + boat.Radius);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, new Rectangle((int)pos.X + camera.getOffsetX(), (int)pos.Y + camera.getOffsetY(), radius*2, radius*2), Color.White);
		}
	}
}
