using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
	public static class Fonts
	{
		public static SpriteFont S12 { get; set; }
		public static SpriteFont S14 { get; set; }
		public static SpriteFont S16 { get; set; }
		public static SpriteFont S18 { get; set; }
		public static SpriteFont S20 { get; set; }

		public static void Load(ContentManager content)
		{
			var folder = "fonts/";

			S12 = content.Load<SpriteFont>(folder + "font_12");
			S14 = content.Load<SpriteFont>(folder + "font_14");
			S16 = content.Load<SpriteFont>(folder + "font_16");
			S18 = content.Load<SpriteFont>(folder + "font_18");
			S20 = content.Load<SpriteFont>(folder + "font_20_bold");
		}
	}
}
