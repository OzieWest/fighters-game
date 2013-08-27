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
		public int GridX { get; set; }
		public int GridY { get; set; }
		public TileState State { get; set; }
		public TileNeighbor Neighbors { get; set; }

		private Boolean disposed = false;

		public TileObject(Texture2D texture, TileTypes type, int frameInterval, int frameOffset)
			: base(texture)
		{
			Neighbors = new TileNeighbor();

			Class.Type = type;

			State = TileState.Normal;

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

		public virtual void MoveTo(float x, float y)
		{
			Position.toX = x;
			Position.toY = y;
			Position.SetSpeed(5, 5);
		}

		public virtual Boolean IsMoveComplete()
		{
			return Position.IsMoveComplete();
		}

		protected virtual void _animate(GameTime gameTime)
		{
			switch (State)
			{
				case TileState.Focused:
					_frame.Animate(gameTime, 0, 4, 2);
					break;
				case TileState.Selected:
					_frame.Animate(gameTime, 5, 9, 2);
					break;
				case TileState.Test:
					_frame.Animate(gameTime, 10, 10, 2);
					break;
				default:
					_frame.ResetCurrent();
					break;
			}
		}

		public virtual void Update(GameTime gameTime)
		{
			Position.rX = _frame.Current * (int)_frame.Interval;
			Position.rY = 0;

			_animate(gameTime);

			var distanceX = Position.X - Position.toX;
			var distanceY = Position.Y - Position.toY;

			if (Math.Abs(distanceX) < Position.SpeedX)
			{
				Position.SpeedX = Position.SpeedX / 2;
			}
			else
			{
				Position.SpeedX += 1; 
			}

			if (Math.Abs(distanceY) < Position.SpeedY)
			{
				Position.SpeedY = Position.SpeedY / 2;
			}
			else
			{
				Position.SpeedY += 1;
			}

			if (distanceY > 0)
			{
				Position.Y -= Position.SpeedY;
			}
			else if (distanceY < 0)
			{
				Position.Y += Position.SpeedY;
			}

			if (distanceX > 0)
			{
				Position.X -= Position.SpeedX;
			}
			else if (distanceX < 0)
			{
				Position.X += Position.SpeedX;
			}
		}

		public virtual Boolean IsSame(TileObject obj)
		{
			if (this.GridX == obj.GridX &&
				this.GridY == obj.GridY &&
				this.Position.X == obj.Position.X &&
				this.Position.Y == obj.Position.Y)
			{
				return true;
			}

			return false;
		}

		public virtual void ToDefault()
		{
			Neighbors.Erase();

			GridX = -1;
			GridY = -1;

			this.SetPosition(-100, -100);

			_frame.DefaultValue();
		}

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
					GridX = -1;
					GridY = -1;
					this.SetPosition(-100, -100);

					Neighbors = null;
					Class = null;
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