using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Content;
using TestGame.Domain;

namespace TestGame
{
	public class TileFactory
	{
		protected Random _rnd;
		public int FrameOffset { get; set; }

		public TextureController TextureController { get; set; }

		public TileFactory()
		{
			_rnd = new Random();
		}

		public void Init(int frameOffset, TextureController texController)
		{
			FrameOffset = frameOffset;
			TextureController = texController;
		}

		public TileObject CreateTile()
		{
			int rInt = _rnd.Next(1, 9); // todo: все тайлы, кроме дефолтного

			var result = this.CreateTileByType((TileTypes)rInt);

			return result;
		}

		protected TileObject CreateTileByType(TileTypes type)
		{
			var tex = TextureController.GetTexture(type.ToString());
			var result = new TileObject(tex, type, tex.Height, FrameOffset);

			return result;
		}
	}
}
