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
		protected TileFactory _factory;
		#endregion

		public TileController(ContentManager content, SpriteBatch spriteBatch)
		{
			_content = content;
			_spriteBatch = spriteBatch;

			_tiles = new TilesContainer();

			_placeController = new PlaceController(_tiles);
			_factory = new TileFactory(content);

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

		public void Init(int x)
		{
			this.CreateGrid(x);

			_placeController.GenerateNeighbors();

			this.SetColorsOnTiles(Color.White, Color.Gray);
		}

		public void CreateGrid(int x)
		{
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
					var cell = _factory.CreateTile();
					cell.SetPosition(posX, posY);

					row.Add(cell);

					posX += step;
				}

				_tiles.Add(row);

				posX = constPosX;
				posY += step;
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

			this.CreateElement();

			this.CheckIntersect(gameTime, obj, isSelect);
		}

		protected void CreateElement()
		{
			var delTiles = _tiles.Column(0);
			if (delTiles.Count != 8)
			{

			}
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
				}
			}

			foreach (var tile in _placeController.FindChain())
			{
				_tiles.RemoveElement(tile);
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
