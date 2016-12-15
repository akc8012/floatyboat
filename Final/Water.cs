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
			Texture2D texture;
			Vector2 pos;
			Vector2 center;

			public Layer(Texture2D texture, Vector2 pos)
			{
				this.texture = texture;
				this.pos = pos;
				center = this.pos;
			}

			public void DoCircles(float radius, float t)
			{
				pos.X = center.X - radius + (radius * (float)Math.Cos(2 * Math.PI * t));
				pos.Y = center.Y +			(radius * (float)Math.Sin(2 * Math.PI * t));
			}

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
			layers = new Layer[3] {
				new Layer(layer0, new Vector2(-115, waterPoint+10)),
				new Layer(layer1, new Vector2(-115, waterPoint-15)),
				new Layer(layer2, new Vector2(-115, waterPoint-40))
			};
		}

		public void Update(int frames)
		{
			layers[0].DoCircles(7, frames * 0.01f);
			layers[1].DoCircles(9, -frames * 0.0075f);
			layers[2].DoCircles(10, frames * 0.015f);
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
