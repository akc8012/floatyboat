﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final
{
	class Boat
	{
		Texture2D texture;
		Vector2 pos;
		Vector2 vel;
		Point size;

		const float gravity = 0.8f;
		const int duckDist = 70;
		const int jumpOffset = 150;
		const int jumpFrames = 28;

		int waterPoint;
		Vector2 Center { get { return new Vector2(pos.X + size.X/2, pos.Y + size.Y/2); } }
		KeyboardState keyboard;
		KeyboardState lastKeyboard;
		int jumpCount = 0;
		Camera camera;

		public Boat(Camera camera)
		{
			Reset();
			waterPoint = Game1.height - 80;
			this.camera = camera;
		}

		void Reset()
		{
			pos = new Vector2(115, Game1.height/2 - size.Y);
			vel = new Vector2(0, 0);
		}

		public void LoadContent(Texture2D texture)
		{
			this.texture = texture;
			size = new Point(texture.Width, texture.Height);
		}

		void Move(int centerX, int centerY, float inertia, float k)
		{
			float distX = centerX - pos.X;
			float distY = centerY - pos.Y;

			vel.X = vel.X * inertia + distX*k;
			vel.Y = vel.Y * inertia + distY*k;

			pos += vel;
		}

		public void Update()
		{
			keyboard = Keyboard.GetState();
			int targetPoint = waterPoint;

			if (keyboard.IsKeyDown(Keys.Down))
				targetPoint += duckDist;

			bool canJump = pos.Y + size.Y > waterPoint;
			bool isJumping = (IsKeyPressed(Keys.Up) && jumpCount == 0 && canJump) || (jumpCount <= jumpFrames && jumpCount != 0);

			if (isJumping)
			{
				targetPoint -= jumpOffset;
				jumpCount++;
			}
			else
				jumpCount = 0;

			Move((int)pos.X, targetPoint-(size.Y/2), 0.9f, 0.025f);

			if (keyboard.IsKeyDown(Keys.R))
				Reset();

			lastKeyboard = keyboard;
		}

		bool IsKeyPressed(Keys key)
		{
			return keyboard.IsKeyDown(key) && lastKeyboard.IsKeyUp(key);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y + camera.getOffsetY(), size.X, size.Y), Color.White);
			spriteBatch.Draw(texture, new Rectangle(0, waterPoint + camera.getOffsetY(), Game1.width, 2), Color.Blue);
		}
	}
}