using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final
{
	class Water
	{
		public static int waterPoint = Game1.height-80;

		Camera camera;
		Texture2D[] layers;

		public Water(Camera camera)
		{
			this.camera = camera;
		}

		public void LoadContent(Texture2D layer0, Texture2D layer1, Texture2D layer2)
		{
			layers = new Texture2D[3];
			layers[0] = layer0;
			layers[1] = layer1;
			layers[2] = layer2;
		}

		public void Update()
		{
			
		}

		public void Draw(SpriteBatch spriteBatch, int i)
		{
			int yOffset = 0;
			switch (i) { case 0: yOffset = 0; break; case 1: yOffset = 25; break; case 2: yOffset = 50; break; }

			spriteBatch.Draw(layers[i], new Rectangle(0, waterPoint + camera.getOffsetY() - yOffset, layers[i].Bounds.Width, layers[i].Bounds.Height), Color.White);
		}

	}
}
