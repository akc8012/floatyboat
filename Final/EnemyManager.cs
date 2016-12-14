using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Final
{
	class EnemyManager
	{
		Boat boat;
		Camera camera;
		Texture2D cannonTex;
		Texture2D sharkTex;

		List<Enemy> enemies;
		enum EType { Cannon, Shark }

		public EnemyManager(Boat boat, Camera camera)
		{
			this.boat = boat;
			this.camera = camera;
			enemies = new List<Enemy>();
		}

		public void LoadContent(Texture2D cannonTex, Texture2D sharkTex)
		{
			this.cannonTex = cannonTex;
			this.sharkTex = sharkTex;

			//AddEnemy(EType.Cannon);
		}

		void AddEnemy(EType eType)
		{
			Enemy newEnemy = null;
			switch (eType)
			{
				case EType.Cannon:
					newEnemy = new CircleEnemy(new Vector2(Game1.width, 345), boat, camera, cannonTex); break;

				case EType.Shark:
					newEnemy = new BoxEnemy(new Vector2(Game1.width, 400), boat, camera, sharkTex); break;
			}
			enemies.Add(newEnemy);
		}

		public void Update(int frames)
		{
			if (frames % 50 == 0)
				AddEnemy(EType.Cannon);

			for (int i = 0; i < enemies.Count; i++)
			{
				enemies[i].Update();

				if (enemies[i].DeleteThis)
					enemies.RemoveAt(i);
			}
		}

		public void Reset()
		{
			enemies.Clear();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < enemies.Count; i++)
				enemies[i].Draw(spriteBatch);
		}
	}
}
