using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame.Controllers
{
	public class TileController
	{
		#region Values
		protected List<List<TileObject>> _tiles;
		protected ContentManager _content;
		protected SpriteBatch _spriteBatch;
		#endregion

		public TileController(ContentManager content, SpriteBatch spriteBatch)
		{
			_content = content;
			_spriteBatch = spriteBatch;

			_tiles = new List<List<TileObject>>();
		}

		public void SetColors(Color defColor, Color selColor)
		{
			foreach (var row in _tiles)
			{
				foreach (var cell in row)
				{
					cell.SetColors(defColor, selColor);
				}
			}
		}

		public void Init(int x)
		{
			this.CreateGrid(x);
			this.SearchNeighbors();
			this.SetColors(Color.White, Color.Gray);
		}

		public void CreateGrid(int x)
		{
			var rnd = new Random();
			var constPosX = 135;
			var constPosY = 40;
			var step = 60 + 5;

			var posX = constPosX;
			var posY = constPosY;

			for (var i = 0; i < x; i++)
			{
				var row = new List<TileObject>();

				for (var j = 0; j < x; j++)
				{
					var cell = this.GetRandomTile(rnd);

					cell.SetPosition(posX, posY);

					row.Add(cell);

					posX += step;
				}

				_tiles.Add(row);

				posX = constPosX;
				posY += step;
			}
		}

		protected TileObject GetRandomTile(Random rnd)
		{
			int rInt = rnd.Next(1, 6); // все тайлы, кроме дефолтного

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
			}

			var tex = _content.Load<Texture2D>("set1/" + fileName);
			var result = new TileObject(tex, type, 60, 10);

			return result;
		}

		public void Draw()
		{
			foreach (var row in _tiles)
			{
				foreach (var cell in row)
				{
					cell.Draw(_spriteBatch);
				}
			}
		}

		public void Update(GameTime gameTime, IPosition obj, Boolean isSelect)
		{
			this.CheckIntersect(gameTime, obj, isSelect);
		}

		protected void CheckIntersect(GameTime gameTime, IPosition obj, Boolean isSelect)
		{
			if (obj != null)
			{
				foreach (var row in _tiles)
				{
					foreach (var cell in row)
					{
						cell.Update(gameTime);

						if (isSelect)
						{
							if (cell.IsIntersectWith(obj) && cell.State != TileState.Selected)
							{
								cell.State = TileState.Selected;
							}
							else
							{
								cell.State = TileState.Normal;
							}
						}
						else
						{
							if (cell.IsIntersectWith(obj) && cell.State != TileState.Selected)
							{
								cell.State = TileState.Focused;
								if (cell.Place.Bottom != null)
								{
									cell.Place.Bottom.State = TileState.Focused;
								}
								if (cell.Place.Top != null)
								{
									cell.Place.Top.State = TileState.Focused;
								}
								if (cell.Place.Right != null)
								{
									cell.Place.Right.State = TileState.Focused;
								}
								if (cell.Place.Left != null)
								{
									cell.Place.Left.State = TileState.Focused;
								}
							}
							else if (!cell.IsIntersectWith(obj) && cell.State != TileState.Selected)
							{
								cell.State = TileState.Normal;
							}
						}
					}
				}
			}
		}

		protected void SearchNeighbors()
		{
			for (var i = 0; i < _tiles.Count; i++)
			{
				for (var j = 0; j < _tiles[i].Count; j++)
				{
					this.SetNeighbors(i, j, _tiles[i][j]);
				}
			}
		}

		protected void SetNeighbors(int x, int y, TileObject obj)
		{
			var tile = obj.Place;
			var max = _tiles.Count;

			tile.X = x;
			tile.Y = y;

			if (x > 0)
			{
				tile.Left = _tiles[x - 1][y];

				if (x < max - 1)
				{
					tile.Right = _tiles[x + 1][y];
				}
			}
			else
			{
				tile.Right = _tiles[x + 1][y];
			}

			if (y > 0)
			{
				tile.Top = _tiles[x][y - 1];

				if (y < max - 1)
				{
					tile.Bottom = _tiles[x][y + 1];
				}
			}
			else
			{
				tile.Bottom = _tiles[x][y + 1];
			}
		}
	}
}
