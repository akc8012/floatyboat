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
		class Layer
		{
			public Texture2D texture;
			public Vector2 pos;

			public void Draw(SpriteBatch spriteBatch, int yOffset, Color layerColor)
			{
				spriteBatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y + yOffset, texture.Bounds.Width, texture.Bounds.Height), layerColor);
			}
		}
		Layer[] layers;

		public Water(Camera camera)
		{
			this.camera = camera;
		}

		public void LoadContent(Texture2D layer0, Texture2D layer1, Texture2D layer2)
		{
			layers = new Layer[3] { new Layer(), new Layer(), new Layer() };
			layers[0].texture = layer0;
			layers[0].pos = new Vector2(-115, waterPoint);
			layers[1].texture = layer1;
			layers[1].pos = new Vector2(-115, waterPoint-25);
			layers[2].texture = layer2;
			layers[2].pos = new Vector2(-115, waterPoint-50);
		}

		public void Update()
		{
			
		}

		Color GetLayerColor(int i)
		{
			if (i == 0)
			{
				float camY = camera.getOffsetY();
				float alpha = 1 - Math.Abs(camY / 60);
				return new Color(alpha, alpha, alpha, alpha);
			}
			else return Color.White;
		}

		public void Draw(SpriteBatch spriteBatch, int i)
		{
			Color layerColor = GetLayerColor(i);
			layers[i].Draw(spriteBatch, camera.getOffsetY(), layerColor);
		}

	}
}
