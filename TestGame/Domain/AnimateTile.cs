using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame.Domain
{
	public class AnimateTile
	{
		public Texture2D _texture;
		public Rectangle _rectangle;
		public Vector2 _originalPosition;
		public Vector2 _position;
		public Vector2 _velocity;

		public int _frameWidth;
		public int _frameHeight;
		public int _currentFrame;

		public float _timer;
		public float _interval = 60;

		public AnimateTile(Texture2D texture, Vector2 postion, int frameWidth, int frameHeight)
		{
			_texture = texture;
			_position = postion;
			_frameHeight = frameHeight;
			_frameWidth = frameWidth;
		}

		public void Update(GameTime gameTime)
		{
			_rectangle = new Rectangle(_currentFrame * _frameWidth, 0, _frameWidth, _frameHeight);
			_originalPosition = new Vector2(_rectangle.Width / 2, _rectangle.Height / 2);
			_position = _position + _velocity;

			if (Keyboard.GetState().IsKeyDown(Keys.Right))
			{
				Right(gameTime);
				_velocity.X = 3;
			}
			else
			{
				_velocity = Vector2.Zero;
			}
		}

		public void Right(GameTime gameTime)
		{
			_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
			if (_timer > _interval)
			{
				_currentFrame++;
				_timer = 0;
				if (_currentFrame > 3)
				{
					_currentFrame = 0;
				}
			}
		}

		//public void Left(GameTime gameTime)
		//{
		//	_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
		//	if (_timer > _interval)
		//	{
		//		_currentFrame++;
		//		_timer = 0;
		//		if (_currentFrame > 3)
		//		{
		//			_currentFrame = 0;
		//		}
		//	}
		//}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, _position, _rectangle, Color.White, 0f, _originalPosition, 1.0f, SpriteEffects.None, 0);
		}
	}
}
