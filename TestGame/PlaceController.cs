using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class PlaceController
	{
		protected List<List<TileObject>> _tiles;

		public PlaceController(List<List<TileObject>> tiles)
		{
			_tiles = tiles;
		}

		public void GenerateNeighbors()
		{
			for (var i = 0; i < _tiles.Count; i++)
			{
				for (var j = 0; j < _tiles[i].Count; j++)
				{
					this.SetNeighbors(i, j, _tiles[i][j]);
				}
			}
		}

		public void SetNeighbors(int x, int y, TileObject obj)
		{
			var max = _tiles.Count;

			obj.X = x;
			obj.Y = y;

			if (x > 0)
			{
				obj.Left = _tiles[x - 1][y];

				if (x < max - 1)
				{
					obj.Right = _tiles[x + 1][y];
				}
			}
			else
			{
				obj.Right = _tiles[x + 1][y];
			}

			if (y > 0)
			{
				obj.Top = _tiles[x][y - 1];

				if (y < max - 1)
				{
					obj.Bottom = _tiles[x][y + 1];
				}
			}
			else
			{
				obj.Bottom = _tiles[x][y + 1];
			}
		}
	}
}
