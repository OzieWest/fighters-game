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

			_frame.Interval = frameInterval;
			_frame.Offset = frameOffset;

			Class.Type = type;

			State = TileState.Normal;

			Position.Rectangle = new Rectangle(0, 0, frameInterval, texture.Height);
			Position.Speed = 10;
		}

		public virtual Boolean IsIntersect(IPosition obj)
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
		}

		public virtual void Update(GameTime gameTime)
		{
			Position.rX = _frame.Current * (int)_frame.Interval;
			Position.rY = 0;

			switch (State)
			{
				case TileState.Focused:
					Animate(gameTime, 0, 4, 2);
					break;
				case TileState.Selected:
					Animate(gameTime, 5, 9, 2);
					break;
				case TileState.Test:
					Animate(gameTime, 0, 4, 2);
					break;
				default:
					_frame.Current = 0;
					break;
			}

			var x = Position.X - Position.toX;
			var y = Position.Y - Position.toY;

			var slowSpeed = 1;
			var speedX = Position.Speed;
			var speedY = Position.Speed;

			if (Math.Abs(y) < Position.Speed)
			{
				speedY = slowSpeed;
			}
			if (Math.Abs(x) < Position.Speed)
			{
				speedX = slowSpeed;
			}

			if (y > 0)
			{
				Position.Y -= speedY;
			}
			else if (y < 0)
			{
				Position.Y += speedY;
			}

			if (x > 0)
			{
				Position.X -= speedX;
			}
			else if (x < 0)
			{
				Position.X += speedX;
			}
		}

		public virtual void Animate(GameTime gameTime, int startFrame, int endFrame, int speed)
		{
			_frame.Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
			if (_frame.Timer > _frame.Interval / speed)
			{
				_frame.Current++;
				_frame.Timer = 0;
				if (_frame.Current > endFrame)
				{
					_frame.Current = startFrame;
				}
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

		public virtual void Default()
		{
			Neighbors.Null();

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