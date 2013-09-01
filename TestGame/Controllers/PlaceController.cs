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
		public TileContainer Container { get; set; }
		public GridController Grid { get; set; }

		public void Init(int x)
		{
			Container = IoC.GetSingleton<TileContainer>();
			Grid = IoC.GetSingleton<GridController>();

			Grid.Init(x);

			GenerateNeighbors();
		}

		public void GenerateNeighbors()
		{
			for (var i = 0; i < Container.Count; i++)
			{
				for (var j = 0; j < Container[i].Count; j++)
				{
					if (Container[i][j] != null)
						_setNeighbors(i, j, Container[i][j]);
				}
			}
		}

		public void ChangePlace(TileObject one, TileObject two)
		{
			var onePos = one.Position;
			var twoPos = two.Position;

			two.MoveTo(onePos.X, onePos.Y);
			one.MoveTo(twoPos.X, twoPos.Y);

			Container[one.Grid.X][one.Grid.Y] = two;
			Container[two.Grid.X][two.Grid.Y] = one;
		}

		public void MoveColumns(ObjectFactory factory)
		{
			var start = -100;
			for (var e = 0; e < Container.Count; e++)
			{
				var list = Container.Column(e);

				for (var i = list.Count - 1; i > -1; i--)
				{
					if (list[i] == null)
					{
						var nextTile = this._getNextNotNull(list, i);

						if (nextTile != null)
						{
							nextTile.MoveTo(Grid[i, e].X, Grid[i, e].Y);

							var index = list.IndexOf(nextTile);

							list[index] = null;
							Container[index][e] = null;
						}
						else
						{
							nextTile = factory.CreateRandomTile(10);
							nextTile.SetPosition(Grid[i, e].X, start);
							nextTile.MoveTo(Grid[i, e].X, Grid[i, e].Y);
							start -= 100;
						}

						list[i] = nextTile;
						Container[i][e] = nextTile;
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

			for (var i = 0; i < Container.Count; i++)
			{
				for (var j = 0; j < Container[i].Count; j++)
				{
					var tile = Container[i][j];
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
			foreach (var tile in Container)
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

			var max = Container.Count;
			if (x > 0)
			{
				neighbors.T = Container[x - 1][y];
				if (x < max - 1)
				{
					neighbors.B = Container[x + 1][y];
				}
			}
			else
			{
				neighbors.B = Container[x + 1][y];
			}

			if (y > 0)
			{
				neighbors.L = Container[x][y - 1];
				if (y < max - 1)
				{
					neighbors.R = Container[x][y + 1];
				}
			}
			else
			{
				neighbors.R = Container[x][y + 1];
			}
		}
	}
}
