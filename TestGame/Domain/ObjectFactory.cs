using Microsoft.Xna.Framework;
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
	public class ObjectFactory
	{
		protected Random _rnd;
		protected int FrameOffset { get; set; }

		public TextureController TextureController { get; set; }

		public void Init()
		{
			_rnd = new Random();
			TextureController = IoC.GetSingleton<TextureController>();
		}

		public TileObject CreateRandomTile(int frameOffset)
		{
			FrameOffset = frameOffset;

			int rInt = _rnd.Next(1, 10); // todo: все тайлы, кроме дефолтного

			var result = this.CreateTileByType((TileTypes)rInt);

			return result;
		}

		public BaseObject CreateBackground(String textureName)
		{
			var texture = TextureController.GetTexture(textureName);

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

		public Texture2D CreateTexture(String textureName)
		{
			return TextureController.GetTexture(textureName);
		}

		protected TileObject CreateTileByType(TileTypes type)
		{
			var tex = TextureController.GetTexture(type.ToString());
			var result = new TileObject(tex, type, tex.Height, FrameOffset);

			return result;
		}
	}
}
