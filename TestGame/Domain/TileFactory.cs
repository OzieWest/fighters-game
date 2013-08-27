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
		#region Values
		protected String _folder;
		protected Random _rnd;
		#endregion

		#region Property
		public int FrameOffset { get; set; }
		#endregion

		#region Injects
		protected TextureController _textureController;
		#endregion

		public TileFactory(ContentManager contentManager)
		{
			_rnd = new Random();

			_textureController = new TextureController(contentManager);

			_folder = "set1"; //todo: указывать в config
			FrameOffset = 10;  //todo: указывать в config
		}

		public TileObject CreateTile()
		{
			int rInt = _rnd.Next(1, 9); // все тайлы, кроме дефолтного

			var result = this.CreateTileByType((TileTypes)rInt);

			return result;
		}

		protected TileObject CreateTileByType(TileTypes type)
		{
			var tex = _textureController.GetTextureByType(type);
			var result = new TileObject(tex, type, tex.Height, FrameOffset);

			return result;
		}
	}
}
