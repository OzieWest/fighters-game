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
		public GridPosition Grid { get; set; }
		public TileState State { get; set; }
		public TileTypes Type { get; set; }
		public TileNeighbor Neighbors { get; set; }

		private Boolean disposed = false;

		public TileObject(Texture2D texture, TileTypes type, int frameInterval, int frameOffset)
			: base(texture)
		{
			Type = type;

			_frame = new Frame()
			{
				Interval = frameInterval,
				Offset = frameOffset
			};

			Position.Rectangle = new Rectangle()
			{
				X = 0,
				Y = 0,
				Width = frameInterval,
				Height = texture.Height
			};

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
			if (obj.X > (this.Position.X + _frame.Offset) &&
				obj.X < (this.Position.X + _frame.Interval) - _frame.Offset &&
				obj.Y > (this.Position.Y + _frame.Offset) &&
				obj.Y < (this.Position.Y + _frame.Interval) - _frame.Offset)
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
					_frame.Animate(gameTime, 1, 1, 2);
					break;
				case TileState.Selected:
					_frame.Animate(gameTime, 2, 2, 2);
					break;
				case TileState.Test:
					_frame.Animate(gameTime, 0, 40, 10);
					break;
				default:
					_frame.Reset();
					break;
			}
		}

		public virtual void Update(GameTime gameTime)
		{
			Position.SetFrame(_frame.StrageMath());

			_animate(gameTime);

			var distanceX = Position.X - Position.toX;
			var distanceY = Position.Y - Position.toY;

			//change X speed
			if (Math.Abs(distanceX) < Position.SpeedX)
				Position.SpeedX = Position.SpeedX / 2;
			else
				Position.SpeedX += 1;

			//change Y speed
			if (Math.Abs(distanceY) < Position.SpeedY)
				Position.SpeedY = Position.SpeedY / 2;
			else
				Position.SpeedY += 1;

			//move object (Y)
			if (distanceY > 0)
				Position.Y -= Position.SpeedY;
			else if (distanceY < 0)
				Position.Y += Position.SpeedY;

			//move object (X)
			if (distanceX > 0)
				Position.X -= Position.SpeedX;
			else if (distanceX < 0)
				Position.X += Position.SpeedX;
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

			this.SetPosition(-100, -100);

			_frame.DefaultValue();
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
					_frame = null;
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