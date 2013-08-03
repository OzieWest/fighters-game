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

		protected TileTypes _type;

		protected Vector2 _velocity;
		#endregion

		public TileObject(Texture2D texture, TileTypes type, int frameInterval, int frameOffset) : base(texture)
		{
			_frameInterval = frameInterval;
			_frameOffset = frameOffset;
			_type = type;

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
		}

		public void Update(GameTime gameTime, int x, int y)
		{
			this.SetPosition(x, y);
			this.Update(gameTime);
		}

		public void Animate(GameTime gameTime)
		{
			_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
			if (_timer > _frameInterval)
			{
				_currentFrame++;
				_timer = 0;
				if (_currentFrame > 3)
				{
					_currentFrame = 0;
				}
			}
		}
	}
}
