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
		public PlaceController Places { get; set; }

		#region Injects
		public SkillController Skills { get; set; }
		public TileContainer Container { get; set; }
		public TileFactory Factory { get; set; }
		#endregion

		public TileController()
		{
			Places = new PlaceController();
			Container = new TileContainer();
		}

		public TileController Init(int x)
		{
			Container.Init();

			Places.Init(Container, 8);
			Places.GenerateNeighbors();

			_fillGrid(x);

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
					var pos = Places.Grid[i, j];
					var cell = Factory.CreateTile();

					cell.SetPosition(pos.X, startPos);
					cell.MoveTo(pos.X, pos.Y);

					row.Add(cell);
				}

				Container.Add(row);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Container.Draw(spriteBatch);
		}

		public void Update(GameTime gameTime, Position obj, Boolean isSelect)
		{
			Container.Update(gameTime);

			_changeTwoElements(isSelect);

			if (Places.IsMoveComplete())
			{
				_checkChains();
				_checkIntersect(gameTime, obj, isSelect);
				Places.MoveColumns(Factory);
				Places.GenerateNeighbors();
			}
		}

		protected void _checkChains()
		{
			var chain = Places.FindChain();

			if (chain != null && chain.Count != 0)
			{
				foreach (var tile in chain)
				{
					var x = tile.Position.X;
					var y = tile.Position.Y;
					var type = tile.Type;

					if (Container.RemoveElement(tile))
						Skills.Move(x, y, type);
				}
			}
		}

		protected void _changeTwoElements(Boolean isSelect)
		{
			var selected = Container.FirstByState(TileState.Selected);
			var focused = Container.FirstByState(TileState.Focused);

			if (selected != null && focused != null && !selected.Compare(focused))
			{
				var neighbor = selected.Neighbors.GetAll().SingleOrDefault(o => o == focused); //todo: возможно стоит перенести в класс Neibors

				if (isSelect && neighbor != null)
				{
					Places.ChangePlace(selected, focused);
					Places.GenerateNeighbors();
					selected.State = TileState.Normal;
				}
			}
		}

		protected void _checkIntersect(GameTime gameTime, Position obj, Boolean isSelect)
		{
			foreach (var tile in Container.WhichNotNull())
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
