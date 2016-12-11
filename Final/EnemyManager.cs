using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final
{
	class EnemyManager
	{
		Texture2D cannonTex;
		Texture2D sharkTex;
		Enemy enemy;

		public EnemyManager(Boat boat, Camera camera)
		{
			enemy = new CircleEnemy(new Vector2(Game1.width, 345), boat, camera);
		}

		public void LoadContent(Texture2D cannonTex, Texture2D sharkTex)
		{
			this.cannonTex = cannonTex;
			this.sharkTex = sharkTex;

			enemy.LoadContent(this.cannonTex);
		}

		public void Update()
		{
			enemy.Update();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			enemy.Draw(spriteBatch);
		}
	}
}
