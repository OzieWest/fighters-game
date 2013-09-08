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
	public class TContainer
	{
		public List<List<TileObject>> List;

		public void Init()
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

		public void ChangeState(TileState currentState, TileState newState)
		{
			foreach (var tile in this.WhichNotNull())
			{
				if (tile.State == currentState)
				{
					tile.State = newState;
				}
			}
		}

		public TileObject FirstByPlace(int x, int y)
		{
			foreach (var tile in this.WhichNotNull())
			{
				if (tile.Grid.X == x && tile.Grid.Y == y)
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

		public List<TileObject> Column(int index)
		{
			var result = new List<TileObject>();

			if (index > -1 && index <= List.Count - 1)
			{
				foreach (var tile in List)
				{
					result.Add(tile[index]);
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

		public Boolean RemoveElement(TileObject tile)
		{
			foreach (var row in List)
			{
				var index = row.IndexOf(tile);

				if (index > -1)
				{
					row[index] = null;
					tile.Dispose();
					return true;
				}
			}

			return false;
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
