using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TestGame.Controllers;

namespace TestGame.Domain
{
	public class TileObject : BaseObject, IDisposable
	{
		int timeUntilStart = 60;

		public GridPosition Grid { get; set; }
		public TileState State { get; set; }
		public TileTypes Type { get; set; }
		public TileNeighbor Neighbors { get; set; }

		private Boolean disposed = false;

		public TileObject(Texture2D texture, TileTypes type, int frameInterval)
			: base(texture, frameInterval)
		{
			Type = type;
			Color = Color.Transparent;

			_init();
		}

		protected void _init()
		{
			Grid = new GridPosition();
			Neighbors = new TileNeighbor();
			State = TileState.Normal;
		}

		public virtual Boolean IsIntersect(Position obj)
		{
			if (obj.X > (Position.X + Position.Frame.Offset) &&
				obj.X < (Position.X + Position.Frame.Interval) - Position.Frame.Offset &&
				obj.Y > (Position.Y + Position.Frame.Offset) &&
				obj.Y < (Position.Y + Position.Frame.Interval) - Position.Frame.Offset)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		protected virtual void _animate(GameTime gameTime)
		{
			switch (State)
			{
				case TileState.Focused:
					Position.Frame.Animate(gameTime, 1, 1, 2);
					break;
				case TileState.Selected:
					Position.Frame.Animate(gameTime, 2, 2, 2);
					break;
				default:
					Position.Frame.Reset();
					break;
			}
		}

		public override void Update(GameTime gameTime)
		{
			timeUntilStart--;
			Color = Color.White * (1 - timeUntilStart / 60f);

			_animate(gameTime);
			base.Update(gameTime);
		}

		public virtual Boolean Compare(TileObject obj)
		{
			if (this.Grid.X == obj.Grid.X &&
				this.Grid.Y == obj.Grid.Y &&
				this.Position.X == obj.Position.X &&
				this.Position.Y == obj.Position.Y)
			{
				return true;
			}

			return false;
		}

		public virtual void ResetToDefault()
		{
			Neighbors.Erase();

			Grid.X = -1;
			Grid.Y = -1;

			Position.Set(-100, -100);
			Position.Frame.DefaultValue();
		}

		#region Interfaces
		public virtual Boolean IsMoveComplete()
		{
			return Position.IsMoveComplete();
		}

		public virtual void MoveTo(float x, float y)
		{
			Position.MoveTo(x, y);
			Position.ResetSpeed();
		}

		public virtual void SetPosition(float x, float y)
		{
			Position.Set(x, y);
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					Grid.X = -1;
					Grid.Y = -1;
					this.SetPosition(-100, -100);

					Neighbors = null;
					Position.Frame = null;
				}
				disposed = true;
			}
		}

		~TileObject()
		{
			Dispose(false);
		}
		#endregion
	}
}