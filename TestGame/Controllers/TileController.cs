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
		protected ScoreController _score;
		protected ContentManager _content;
		protected TilesContainer _tiles;
		protected PlaceController _placeController;
		protected TileFactory _factory;
		protected GridController _grid;
		protected Random rnd;
		#endregion

		public TileController(ContentManager content, ScoreController score)
		{
			_content = content;
			_score = score;

			_tiles = new TilesContainer();

			_placeController = new PlaceController(_tiles);
			_factory = new TileFactory(content);

			rnd = new Random();
		}

		public TileController Init(int x)
		{
			_grid = new GridController()
						.CreateGrid(x);

			_fillGrid(x);

			_placeController.GenerateNeighbors();

			return this;
		}

		protected void _fillGrid(int x)
		{
			var startPos = -100;

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

				_tiles.Add(row);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			_tiles.Draw(spriteBatch);
		}

		public void Update(GameTime gameTime, Position obj, Boolean isSelect)
		{
			_tiles.Update(gameTime);

			_changeTwoElements(isSelect);

			if (_placeController.IsMoveComplete())
			{
				_deleteChains();
				_checkIntersect(gameTime, obj, isSelect);
				_placeController.MoveColumns(_factory, _grid);
				_placeController.GenerateNeighbors();
			}
		}

		protected void _deleteChains()
		{
			var list = _placeController.FindChain();

			if (list.Count > 0)
			{
				foreach (var tile in list)
				{
					var x = tile.Position.X;
					var y = tile.Position.Y;

					if (_tiles.RemoveElement(tile))
						_score.Down(x, y);
				}
			}
		}

		protected void _changeTwoElements(Boolean isSelect)
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

		protected void _checkIntersect(GameTime gameTime, Position obj, Boolean isSelect)
		{
			foreach (var tile in _tiles.WhichNotNull())
			{
				if (obj != null)
				{
					if (isSelect)
					{
						if (tile.IsIntersect(obj) && tile.State != TileState.Selected)
							tile.State = TileState.Selected;
						else
							tile.State = TileState.Normal;
					}
					else
					{
						if (tile.IsIntersect(obj) && tile.State != TileState.Selected)
							tile.State = TileState.Focused;
						else if (!tile.IsIntersect(obj) && tile.State != TileState.Selected)
							tile.State = TileState.Normal;
					}
				}
			}
		}
	}
}
