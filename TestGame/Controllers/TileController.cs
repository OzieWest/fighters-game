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
			for (var i = 0; i < x; i++)
			{
				var row = new List<TileObject>();

				for (var j = 0; j < x; j++)
				{
					var pos = _grid[i, j];
					var cell = _factory.CreateTile();
					cell.SetDestination(pos.X, pos.Y);

					row.Add(cell);
				}

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
			this.DeleteChains(isSelect);
			this.CheckIntersect(gameTime, obj, isSelect);

			_placeController.MoveColumns(_factory, _grid);
			_placeController.GenerateNeighbors();
		}

		protected void DeleteChains(Boolean isSelect)
		{
			var selected = _tiles.FirstByState(TileState.Selected);
			var focused = _tiles.FirstByState(TileState.Focused);

			if (selected != null && focused != null && !selected.IsSame(focused))
			{
				var neighbor = selected.GetNeighbors().SingleOrDefault(o => o == focused);

				if (isSelect && neighbor != null)
				{
					_placeController.ChangePlace(selected, focused);
					_placeController.GenerateNeighbors();
					_score.Down();
				}
			}

			if (_placeController.FindChain().Count > 0)
			{
				foreach (var tile in _placeController.FindChain())
				{
					if (_tiles.RemoveElement(tile))
					{
						_score.Up();
					}
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
