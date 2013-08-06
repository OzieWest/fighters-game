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
	public class TileObject : BaseObject
	{
		#region Values
		protected int _currentFrame;
		protected float _timer;
		protected float _frameInterval;
		protected int _frameOffset;

		public TileTypes Type;

		protected Vector2 _velocity;
		#endregion

		#region Properties
		public int X { get; set; }
		public int Y { get; set; }
		public TileState State { get; set; }
		#endregion

		#region Injects
		public TileObject Left { get; set; }
		public TileObject Top { get; set; }
		public TileObject Right { get; set; }
		public TileObject Bottom { get; set; }
		#endregion

		public TileObject(Texture2D texture, TileTypes type, int frameInterval, int frameOffset)
			: base(texture)
		{
			_frameInterval = frameInterval;
			_frameOffset = frameOffset;
			Type = type;

			State = TileState.Normal;

			_rectangle = new Rectangle(0, 0, frameInterval, texture.Height);
		}

		public virtual Boolean IsIntersectWith(IPosition obj)
		{
			var objX = obj.Position.X;
			var objY = obj.Position.Y;

			var thisX = _position.X;
			var thisY = _position.Y;

			if (objX > (thisX + _frameOffset) &&
				objX < (thisX + _frameInterval) - _frameOffset &&
				objY > (thisY + _frameOffset) &&
				objY < (thisY + _frameInterval) - _frameOffset)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public void Update(GameTime gameTime)
		{
			_rectangle.X = _currentFrame * (int)_frameInterval;
			_rectangle.Y = 0;

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
					_currentFrame = 0;
					break;
			}
		}

		public void Update(GameTime gameTime, int x, int y)
		{
			this.SetPosition(x, y);
			this.Update(gameTime);
		}

		//0, 3
		public void Animate(GameTime gameTime, int startFrame, int endFrame, int speed)
		{
			_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
			if (_timer > _frameInterval / speed)
			{
				_currentFrame++;
				_timer = 0;
				if (_currentFrame > endFrame)
				{
					_currentFrame = startFrame;
				}
			}
		}

		public Boolean IsSame(TileObject obj)
		{
			if (this.X == obj.X &&
				this.Y == obj.Y &&
				this.Position.X == obj.Position.X &&
				this.Position.Y == obj.Position.Y)
			{
				return true;
			}

			return false;
		}

		public List<TileObject> GetNeighbors()
		{
			var result = new List<TileObject>();

			if (Bottom != null)
			{
				result.Add(Bottom);
			}
			if (Top != null)
			{
				result.Add(Top);
			}
			if (Right != null)
			{
				result.Add(Right);
			}
			if (Left != null)
			{
				result.Add(Left);
			}

			return result;
		}

		public void ResetPlace()
		{
			Bottom = null;
			Top = null;
			Right = null;
			Left = null;
		}

		public void Erase()
		{
			this.ResetPlace();

			X = -1;
			Y = -1;

			_currentFrame = 0;
			_timer = 0;
			_frameInterval = 0;
			_frameOffset = 0;

			_velocity = Vector2.Zero;
		}
	}
}