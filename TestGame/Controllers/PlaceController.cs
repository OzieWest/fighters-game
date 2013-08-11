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

			two.SetDestination(onePos.X, onePos.Y);
			one.SetDestination(twoPos.X, twoPos.Y);

			_tiles[one.X][one.Y] = two;
			_tiles[two.X][two.Y] = one;
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
							nextTile.SetDestination(grid[i, e].X, grid[i, e].Y);

							var index = list.IndexOf(nextTile);

							list[index] = null;
							_tiles[index, e] = null;
						}
						else
						{
							nextTile = factory.CreateTile();
							nextTile.SetPosition(-100, -100);
							nextTile.SetDestination(grid[i, e].X, grid[i, e].Y);
						}

						list[i] = nextTile;
						_tiles[i, e] = nextTile;
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
						this.CheckChain(_tiles[i][j].Type, _tiles[i][j], chain);

						if (chain.Count > 2)
						{
							mainChain.AddRange(chain);
						}
					}
				}
			}

			return mainChain;
		}

		public void CheckChain(TileTypes type, TileObject tile, List<TileObject> chain)
		{
			foreach (var neir in tile.GetNeighbors())
			{
				if (neir.Type == type)
				{
					var item = chain.FirstOrDefault(o => o.X == neir.X && o.Y == neir.Y);
					if (item == null)
					{
						chain.Add(neir);
						this.CheckChain(type, neir, chain);
					}
				}
			}
		}

		public void SetNeighbors(int x, int y, TileObject obj)
		{
			obj.ResetPlace();

			var max = _tiles.Count;

			obj.X = x;
			obj.Y = y;

			if (x > 0)
			{
				obj.Top = _tiles[x - 1][y];

				if (x < max - 1)
				{
					obj.Bottom = _tiles[x + 1][y];
				}
			}
			else
			{
				obj.Bottom = _tiles[x + 1][y];
			}

			if (y > 0)
			{
				obj.Left = _tiles[x][y - 1];

				if (y < max - 1)
				{
					obj.Right = _tiles[x][y + 1];
				}
			}
			else
			{
				obj.Right = _tiles[x][y + 1];
			}
		}
	}
}
