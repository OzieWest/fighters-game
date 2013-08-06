using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class TilesContainer
	{
		public List<List<TileObject>> List;

		public TilesContainer()
		{
			List = new List<List<TileObject>>();
		}

		public int Count
		{
			get { return List.Count; }
		}

		public TileObject FirstByState(TileState state)
		{
			foreach (var tile in this.WhichNotNull())
			{
				if (tile.State == state)
				{
					return tile;
				}
			}

			return null;
		}

		public TileObject FirstByPlace(int x, int y)
		{
			foreach (var tile in this.WhichNotNull())
			{
				if (tile.X == x && tile.Y == y)
				{
					return tile;
				}
			}

			return null;
		}

		public IEnumerable<TileObject> AllByState(TileState state)
		{
			foreach (var tile in this.WhichNotNull())
			{
				if (tile.State == state)
				{
					yield return tile;
				}
			}
		}

		public List<TileObject> Column(int x)
		{
			var result = new List<TileObject>();

			if (x > -1 && x < List.Count - 1)
			{
				for (var i = 0; i < List.Count; i++)
				{
					result.Add(List[x][i]);
				}
			}
			else
			{
				return null;
			}

			return result;
		}

		public List<TileObject> this[int index]
		{
			set
			{
				if (index > -1 && index < List.Count)
				{
					List[index] = value;
				}
			}
			get
			{
				if (index > -1 && index < List.Count)
				{
					return List[index];
				}
				else
				{
					return null;
				}
			}
		}

		public TileObject this[int indexA, int indexB]
		{
			set
			{
				if (indexA > -1 &&
					indexA < List.Count &&
					indexB > -1 &&
					indexB < List.Count)
				{
					List[indexA][indexB] = value;
				}
			}
			get
			{
				if (indexA > -1 &&
					indexA < List.Count &&
					indexB > -1 &&
					indexB < List.Count)
				{
					return List[indexA][indexB];
				}
				else
				{
					return null;
				}
			}
		}

		public IEnumerator<TileObject> GetEnumerator()
		{
			foreach (var row in List)
			{
				foreach (var cell in row)
				{
					yield return (TileObject)cell;
				}
			}
		}

		public IEnumerable<TileObject> WhichNotNull()
		{
			foreach (var row in List)
			{
				foreach (var cell in row)
				{
					if (cell != null)
					{
						yield return (TileObject)cell;
					}
				}
			}
		}

		public IEnumerable<TileObject> WhichNull()
		{
			foreach (var row in List)
			{
				foreach (var cell in row)
				{
					if (cell == null)
					{
						yield return (TileObject)cell;
					}
				}
			}
		}

		public void Add(List<TileObject> row)
		{
			if (row != null)
			{
				List.Add(row);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (var tile in this.WhichNotNull())
				tile.Draw(spriteBatch);
		}

		public void Update(GameTime gameTime)
		{
			foreach (var tile in this.WhichNotNull())
				tile.Update(gameTime);
		}
	}
}
