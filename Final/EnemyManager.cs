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
		enum EType { Cannon, TwoCannon, Shark }

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
		}

		void AddEnemy(EType eType)
		{
			switch (eType)
			{
				case EType.Cannon:
					enemies.Add(new CircleEnemy(new Vector2(Game1.width, 315), boat, camera, cannonTex));
					SoundMan.Instance.PlaySound(SoundMan.Instance.cannonFire);
					break;

				case EType.TwoCannon:
					enemies.Add(new CircleEnemy(new Vector2(Game1.width, 315), boat, camera, cannonTex));
					enemies.Add(new CircleEnemy(new Vector2(Game1.width, 200), boat, camera, cannonTex));
					SoundMan.Instance.PlaySound(SoundMan.Instance.twoCannonFire);
					break;

				case EType.Shark:
					enemies.Add(new BoxEnemy(new Vector2(Game1.width, 350), boat, camera, sharkTex));
					SoundMan.Instance.PlaySound(SoundMan.Instance.sharkRoar);
					break;
			}
		}

		public void Update(int frames)
		{
			if (frames % 50 == 0)
				AddEnemy((EType)Game1.rand.Next(0, 3));

			for (int i = enemies.Count-1; i >= 0; i--)
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
