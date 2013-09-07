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
		public List<List<Vector2>> Positions { get; set; }
		public TContainer Container { get; set; }

		public void Init(int x)
		{
			Container = GameRoot.TContainer;

			_initGrid(20, 250, 65, x);

			InitNeighbors();
		}

		public void InitNeighbors()
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

		public void MoveColumns()
		{
			var start = -100;
			for (var e = 0; e < Container.Count; e++)
			{
				var list = Container.Column(e);

				for (var i = list.Count - 1; i > -1; i--)
				{
					if (list[i] == null)
					{
						var nextTile = _getNextNotNull(list, i);

						var x = Positions[i][e].X;
						var y = Positions[i][e].Y;

						if (nextTile != null)
						{
							nextTile.MoveTo(x, y);

							var index = list.IndexOf(nextTile);

							list[index] = null;
							Container[index][e] = null;
						}
						else
						{
							nextTile = GameRoot.TileFactory.GetTile();
							nextTile.SetPosition(x, start);
							nextTile.MoveTo(x, y);
							start -= 100;
						}

						list[i] = nextTile;
						Container[i][e] = nextTile;
					}
				}
			}

			InitNeighbors();
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
							mainChain.AddRange(chain);
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

		void _initGrid(int constPosX, int constPosY, int step, int x)
		{
			Positions = new List<List<Vector2>>();

			var posX = constPosX;
			var posY = constPosY;

			for (var i = 0; i < x; i++)
			{
				var row = new List<Vector2>();

				for (var j = 0; j < x; j++)
				{
					var cell = new Vector2(posX, posY);

					row.Add(cell);

					posX += step;
				}

				Positions.Add(row);

				posX = constPosX;
				posY += step;
			}
		}
	}
}
