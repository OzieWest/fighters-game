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
					{
						this.SetNeighbors(i, j, _tiles[i][j]);
					}
				}
			}
		}

		public void ChangePlace(TileObject one, TileObject two)
		{
			var onePos = one.Position;
			var twoPos = two.Position;

			two.MoveTo(onePos.X, onePos.Y);
			one.MoveTo(twoPos.X, twoPos.Y);

			_tiles[one.GridX][one.GridY] = two;
			_tiles[two.GridX][two.GridY] = one;
		}

		public void MoveColumns(TileFactory factory, GridController grid)
		{
			for (var e = 0; e < _tiles.Count; e++)
			{
				var list = _tiles.Column(e);

				for (var i = list.Count - 1; i > -1; i--)
				{
					if (list[i] == null)
					{
						var nextTile = this.GetNextNotNull(list, i);

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
							nextTile.SetPosition(grid[i, e].X, -100);
							nextTile.MoveTo(grid[i, e].X, grid[i, e].Y);
						}

						list[i] = nextTile;
						_tiles[i][e] = nextTile;
					}
				}
			}
		}

		protected TileObject GetNextNotNull(List<TileObject> list, int startIndex)
		{
			for (var i = startIndex; i > -1; i--)
			{
				if (list[i] != null)
				{
					return list[i];
				}
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
					var chain = new List<TileObject>();
					if (_tiles[i][j] != null)
					{
						this.CheckChain(_tiles[i][j].Class.Type, _tiles[i][j], chain);

						if (chain.Count > 2)
						{
							mainChain.AddRange(chain);
						}
					}
				}
			}

			return mainChain;
		}

		public Boolean IsMoveComplete()
		{
			foreach (var tile in _tiles)
			{
				if (!tile.Position.IsMoveComplete())
					return false;
			}

			return true;
		}

		public void CheckChain(TileTypes type, TileObject tile, List<TileObject> chain)
		{
			foreach (var elm in tile.Neighbors.GetAll())
			{
				if (elm.Class.Type == type)
				{
					var item = chain.FirstOrDefault(o => o.GridX == elm.GridX && o.GridY == elm.GridY);

					if (item == null)
					{
						chain.Add(elm);
						this.CheckChain(type, elm, chain);
					}
				}
			}
		}

		public void SetNeighbors(int x, int y, TileObject obj)
		{
			obj.GridX = x;
			obj.GridY = y;

			var neighbors = obj.Neighbors;
			neighbors.Null();

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
