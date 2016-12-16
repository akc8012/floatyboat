using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Final
{
	class SoundMan
	{
		private static SoundMan instance = null;
		private SoundMan() { }
		public static SoundMan Instance
		{
			get
			{
				if (instance == null)
					instance = new SoundMan();

				return instance;
			}
		}

		bool mute = false;
		public bool Mute { set { mute = value; } get { return mute; } }

		public SoundEffect cannonFire;
		public SoundEffect twoCannonFire;
		public SoundEffect sharkRoar;
		public SoundEffect jump;
		public SoundEffect land;
		public SoundEffect getHit;
		public SoundEffect bubbles;
		public SoundEffect pointsSink;
		public SoundEffect pointsJump;

		public void LoadContent(ContentManager Content)
		{
			cannonFire = Content.Load<SoundEffect>("cannonFire");
			twoCannonFire = Content.Load<SoundEffect>("twoCannonFire");
			sharkRoar = Content.Load<SoundEffect>("sharkRoar");
			jump = Content.Load<SoundEffect>("jump");
			land = Content.Load<SoundEffect>("land");
			getHit = Content.Load<SoundEffect>("getHit");
			bubbles = Content.Load<SoundEffect>("bubbles");
			pointsSink = Content.Load<SoundEffect>("pointsSink");
			pointsJump = Content.Load<SoundEffect>("pointsJump");
		}

		public void PlaySound(SoundEffect sound, float volume = 1.0f, float pitch = 0.0f, float pan = 0.0f)
		{
			if (!mute) sound.Play(volume, pitch, pan);
		}
	}
}
