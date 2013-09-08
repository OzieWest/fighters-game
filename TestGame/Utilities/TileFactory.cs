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
	public class TileFactory
	{
		public TileObject GetTile()
		{
			int rInt = GameRoot.RND.Next(1, 9);

			return _createTileByType((TileTypes)rInt);
		}

		public BaseObject CreateBackground(String textureName)
		{
			var texture = GameRoot.Textures[textureName];

			var result = new BaseObject(texture, texture.Height);
			
			return result;
		}

		protected TileObject _createTileByType(TileTypes type)
		{
			var tex = GameRoot.Textures[type.ToString()];
			var result = new TileObject(tex, type, tex.Height);

			return result;
		}
	}
}
