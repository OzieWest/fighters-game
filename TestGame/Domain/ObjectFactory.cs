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
	public class OFactory
	{
		public static OFactory Instance { get; private set; }

		Random _rnd;

		static OFactory()
		{
			Instance = new OFactory();
		}

		private OFactory()
		{
			_rnd = new Random();
		}

		public TileObject CreateRandomTile()
		{
			int rInt = _rnd.Next(1, 9); // todo: все тайлы, кроме дефолтного

			return CreateTileByType((TileTypes)rInt);
		}

		public BaseObject CreateBackground(String textureName)
		{
			var texture = Loader.Instance.GetTexture(textureName);

			var result = new BaseObject(texture, texture.Height);
			
			return result;
		}

		public TileObject CreateTileByType(TileTypes type)
		{
			var tex = Loader.Instance.GetTexture(type.ToString());
			var result = new TileObject(tex, type, tex.Height);

			return result;
		}
	}
}
