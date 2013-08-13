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
		//todo: public while testing 
		#region Injects
		public ScoreController _score;
		public SpriteBatch _spriteBatch;
		public ContentManager _content;
		#endregion Values

		#region Values
		public TilesContainer _tiles;
		public PlaceController _placeController;
		public TileFactory _factory;
		public GridController _grid;
		#endregion

		public TileController(ContentManager content, SpriteBatch spriteBatch, ScoreController score)
		{
			_content = content;
			_spriteBatch = spriteBatch;
			_score = score;

			_tiles = new TilesContainer();

			_placeController = new PlaceController(_tiles);
			_factory = new TileFactory(content);
		}

		public void SetColorsOnTiles(Color defColor, Color selColor)
		{
			foreach (var tile in _tiles.WhichNotNull())
			{
				tile.SetColors(defColor, selColor);
			}
		}

		public void Init(int x)
		{
			_grid = new GridController(x);

			this.CreateGrid(x);

			_placeController.GenerateNeighbors();

			this.SetColorsOnTiles(Color.White, Color.Gray);
		}

		public void CreateGrid(int x)
		{
			var startPos = -50;
			var stepPos = -50;

			for (var i = 0; i < x; i++)
			{
				var row = new List<TileObject>();

				for (var j = 0; j < x; j++)
				{
					var pos = _grid[i, j];
					var cell = _factory.CreateTile();
					cell.SetPosition(pos.X, startPos);
					cell.MoveTo(pos.X, pos.Y);

					row.Add(cell);
				}

				startPos += stepPos;

				_tiles.Add(row);
			}
		}

		public void Draw()
		{
			_tiles.Draw(_spriteBatch);
		}

		public void Update(GameTime gameTime, IPosition obj, Boolean isSelect)
		{
			_tiles.Update(gameTime);

			this.ChangeTwoElemnts(isSelect);

			if (_placeController.IsMoveComplete())
			{
				this.DeleteChains();
				this.CheckIntersect(gameTime, obj, isSelect);
				_placeController.MoveColumns(_factory, _grid);
				_placeController.GenerateNeighbors();
			}
		}

		public void DeleteChains()
		{
			var list = _placeController.FindChain();

			if (list.Count > 0)
			{
				foreach (var tile in list)
				{
					if (_tiles.RemoveElement(tile))
					{
						_score.Down();
					}
				}
			}
		}

		public void ChangeTwoElemnts(Boolean isSelect)
		{
			var selected = _tiles.FirstByState(TileState.Selected);
			var focused = _tiles.FirstByState(TileState.Focused);

			if (selected != null && focused != null && !selected.IsSame(focused))
			{
				var neighbor = selected.Neighbors.GetAll().SingleOrDefault(o => o == focused); //todo: возможно стоит перенести в класс Neibors

				if (isSelect && neighbor != null)
				{
					_placeController.ChangePlace(selected, focused);
					_placeController.GenerateNeighbors();
					selected.State = TileState.Normal;
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
						if (tile.IsIntersect(obj) && tile.State != TileState.Selected)
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
						if (tile.IsIntersect(obj) && tile.State != TileState.Selected)
						{
							tile.State = TileState.Focused;
						}
						else if (!tile.IsIntersect(obj) && tile.State != TileState.Selected)
						{
							tile.State = TileState.Normal;
						}
					}
				}
			}
		}
	}
}
