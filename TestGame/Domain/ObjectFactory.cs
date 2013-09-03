using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public static class OFactory
	{
		public static Random _rnd;
		public static int FrameOffset { get; set; }

		static OFactory()
		{
			_rnd = new Random();
		}

		public static TileObject CreateRandomTile(int frameOffset)
		{
			FrameOffset = frameOffset;

			int rInt = _rnd.Next(1, 9); // todo: все тайлы, кроме дефолтного

			var result = OFactory.CreateTileByType((TileTypes)rInt);

			return result;
		}

		public static BaseObject CreateBackground(String textureName)
		{
			var texture = Loader.GetTexture(textureName);

			var result = new BaseObject(texture);
			result.Position.SetFrame(0);
			result.Position.Rectangle = new Rectangle()
			{
				X = 0,
				Y = 0,
				Width = texture.Width,
				Height = texture.Height
			};

			return result;
		}

		public static TileObject CreateTileByType(TileTypes type)
		{
			var tex = Loader.GetTexture(type.ToString());
			var result = new TileObject(tex, type, tex.Height, FrameOffset);

			return result;
		}
	}
}
