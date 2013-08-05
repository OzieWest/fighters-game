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
		protected TilesContainer _tiles;

		protected List<TileObject> _blackList;

		protected ContentManager _content;
		protected SpriteBatch _spriteBatch;
		#endregion

		#region Injects
		protected PlaceController _placeController;
		#endregion

		public TileController(ContentManager content, SpriteBatch spriteBatch)
		{
			_content = content;
			_spriteBatch = spriteBatch;

			_tiles = new TilesContainer();

			_placeController = new PlaceController(_tiles);

			this.ResetBlackList();
		}

		public void ResetBlackList()
		{
			_blackList = new List<TileObject>();
		}

		public void SetColorsOnTiles(Color defColor, Color selColor)
		{
			foreach (var tile in _tiles.WhichNotNull())
			{
				tile.SetColors(defColor, selColor);
			}
		}

		public TileObject GetByState(TileState state)
		{
			foreach (var tile in _tiles.WhichNotNull())
			{
				if (tile.State == state)
				{
					return tile;
				}
			}

			return null;
		}

		public void Init(int x)
		{
			this.CreateGrid(x);

			_placeController.GenerateNeighbors();

			this.SetColorsOnTiles(Color.White, Color.Gray);
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
			int rInt = rnd.Next(1, 8); // все тайлы, кроме дефолтного

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

			var tex = _content.Load<Texture2D>("set1/" + fileName);
			var result = new TileObject(tex, type, 60, 10);

			return result;
		}

		public void Draw()
		{
			_tiles.Draw(_spriteBatch);
		}

		public void Update(GameTime gameTime, IPosition obj, Boolean isSelect)
		{
			this.OtherCheck(isSelect);

			_tiles.Update(gameTime);

			this.CheckIntersect(gameTime, obj, isSelect);

			var listDelete = _placeController.FindChain();

			foreach (var tile in listDelete)
			{
				tile.State = TileState.Test;
			}
		}

		protected void OtherCheck(Boolean isSelect)
		{
			var selected = this.GetByState(TileState.Selected);
			var focused = this.GetByState(TileState.Focused);

			if (selected != null && focused != null && !selected.IsSame(focused))
			{
				var neighbor = selected.GetNeighbors().SingleOrDefault(o => o == focused);

				if (isSelect && neighbor != null)
				{
					_placeController.ChangePlace(selected, focused);
					_placeController.GenerateNeighbors();
				}
			}
		}

		protected void CheckIntersect(GameTime gameTime, IPosition obj, Boolean isSelect)
		{
			foreach (var tile in _tiles.WhichNotNull())
			{
				if (obj != null)
				{
					if (isSelect)
					{
						if (tile.IsIntersectWith(obj) && tile.State != TileState.Selected)
						{
							tile.State = TileState.Selected;
						}
						else
						{
							tile.State = TileState.Normal;
						}
					}
					else
					{
						if (tile.IsIntersectWith(obj) && tile.State != TileState.Selected)
						{
							tile.State = TileState.Focused;
						}
						else if (!tile.IsIntersectWith(obj) && tile.State != TileState.Selected)
						{
							tile.State = TileState.Normal;
						}
					}
				}
			}
		}
	}
}
