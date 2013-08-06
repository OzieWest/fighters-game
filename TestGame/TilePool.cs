using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class TilePool
	{
		protected List<TileObject> _tiles;
		protected ContentManager _content;

		#region Values
		protected String _folder;
		protected Random _rnd;
		#endregion

		#region Property
		public int FrameOffset { get; set; }
		#endregion

		public TilePool(ContentManager contentManager)
		{
			_tiles = new List<TileObject>();
			_rnd = new Random();

			_content = contentManager;

			_folder = "set1"; //todo: указывать в config
			FrameOffset = 10;  //todo: указывать в config
		}

		public void Init(int x)
		{
			for (int i = 0; i < x * x; i++)
			{
				var tile = this.GetRandomTile();
				_tiles.Add(tile);
			}
		}

		public TileObject GetRandomTile()
		{
			int rInt = _rnd.Next(1, 8); // все тайлы, кроме дефолтного

			var result = this.CreateTileByType((TileTypes)rInt);

			return result;
		}

		protected TileObject CreateTileByType(TileTypes type)
		{
			var fileName = String.Empty;

			switch (type)
			{
				case TileTypes.first:
					fileName = "ntile_0";
					break;
				case TileTypes.second:
					fileName = "ntile_1";
					break;
				case TileTypes.third:
					fileName = "ntile_2";
					break;
				case TileTypes.foth:
					fileName = "ntile_3";
					break;
				case TileTypes.fifth:
					fileName = "ntile_4";
					break;
				case TileTypes.six:
					fileName = "ntile_5";
					break;
				case TileTypes.seven:
					fileName = "ntile_6";
					break;
			}

			var tex = _content.Load<Texture2D>(_folder + "/" + fileName);
			var result = new TileObject(tex, type, tex.Height, FrameOffset);

			return result;
		}

		public TileObject Take()
		{
			var result = _tiles[_tiles.Count - 1];

			_tiles.Remove(result);

			return result;
		}

		public void Release(TileObject obj)
		{
			obj.Erase();
 
			obj = null;

			var tile = this.GetRandomTile();
			_tiles.Add(tile);
		}
	}
}
