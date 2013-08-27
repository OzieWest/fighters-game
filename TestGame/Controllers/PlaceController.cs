using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class PlaceController
	{
		protected TilesContainer _tiles;

		public PlaceController(TilesContainer tiles)
		{
			_tiles = tiles;
		}

		public void GenerateNeighbors()
		{
			for (var i = 0; i < _tiles.Count; i++)
			{
				for (var j = 0; j < _tiles[i].Count; j++)
				{
					if (_tiles[i][j] != null)
						_setNeighbors(i, j, _tiles[i][j]);
				}
			}
		}

		public void ChangePlace(TileObject one, TileObject two)
		{
			var onePos = one.Position;
			var twoPos = two.Position;

			two.MoveTo(onePos.X, onePos.Y);
			one.MoveTo(twoPos.X, twoPos.Y);

			_tiles[one.Grid.X][one.Grid.Y] = two;
			_tiles[two.Grid.X][two.Grid.Y] = one;
		}

		public void MoveColumns(TileFactory factory, GridController grid)
		{
			var start = -100;
			for (var e = 0; e < _tiles.Count; e++)
			{
				var list = _tiles.Column(e);

				for (var i = list.Count - 1; i > -1; i--)
				{
					if (list[i] == null)
					{
						var nextTile = this._getNextNotNull(list, i);

						if (nextTile != null)
						{
							nextTile.MoveTo(grid[i, e].X, grid[i, e].Y);

							var index = list.IndexOf(nextTile);

							list[index] = null;
							_tiles[index][e] = null;
						}
						else
						{
							nextTile = factory.CreateTile();
							nextTile.SetPosition(grid[i, e].X, start);
							nextTile.MoveTo(grid[i, e].X, grid[i, e].Y);
							start -= 100;
						}

						list[i] = nextTile;
						_tiles[i][e] = nextTile;
					}
				}
			}
		}

		protected TileObject _getNextNotNull(List<TileObject> list, int startIndex)
		{
			for (var i = startIndex; i > -1; i--)
			{
				if (list[i] != null)
					return list[i];
			}

			return null;
		}

		public List<TileObject> FindChain()
		{
			var mainChain = new List<TileObject>();

			for (var i = 0; i < _tiles.Count; i++)
			{
				for (var j = 0; j < _tiles[i].Count; j++)
				{
					var tile = _tiles[i][j];
					var type = tile.Type;

					var chain = new List<TileObject>();

					if (tile != null)
					{
						_checkChain(type, tile, chain);

						if (chain.Count > 2)
						{
							foreach (var item in chain)
							{
								item.State = TileState.Test;
							}

							mainChain.AddRange(chain);
						}
					}
				}
			}

			return mainChain.Distinct(new TileComparator())
							.ToList();
		}

		public Boolean IsMoveComplete()
		{
			foreach (var tile in _tiles)
			{
				if (!tile.IsMoveComplete())
					return false;
			}

			return true;
		}

		protected void _checkChain(TileTypes type, TileObject tile, List<TileObject> chain)
		{
			foreach (var elm in tile.Neighbors.GetAll())
			{
				if (elm.Type == type)
				{
					var item = chain.FirstOrDefault(o => o.Grid.X == elm.Grid.X && o.Grid.Y == elm.Grid.Y);

					if (item == null)
					{
						chain.Add(elm);
						_checkChain(type, elm, chain);
					}
				}
			}
		}

		protected void _setNeighbors(int x, int y, TileObject obj)
		{
			obj.Grid.X = x;
			obj.Grid.Y = y;

			var neighbors = obj.Neighbors
								.Erase();

			var max = _tiles.Count;
			if (x > 0)
			{
				neighbors.T = _tiles[x - 1][y];
				if (x < max - 1)
				{
					neighbors.B = _tiles[x + 1][y];
				}
			}
			else
			{
				neighbors.B = _tiles[x + 1][y];
			}

			if (y > 0)
			{
				neighbors.L = _tiles[x][y - 1];
				if (y < max - 1)
				{
					neighbors.R = _tiles[x][y + 1];
				}
			}
			else
			{
				neighbors.R = _tiles[x][y + 1];
			}
		}
	}
}
