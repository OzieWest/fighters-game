using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
	public static class Sound
	{
		public static Song Background { get; set; }
		public static SoundEffect FX1 { get; set; }

		public static void Load(ContentManager content)
		{
			Background = content.Load<Song>("sound/music.wav");
			FX1 = content.Load<SoundEffect>("sound/fall.wav");
		}
	}
}
