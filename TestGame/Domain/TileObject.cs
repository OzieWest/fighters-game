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
		#region Declare
		protected int _currentFrame;
		protected float _timer;
		protected float _frameInterval;

		protected TileTypes _type;

		public Vector2 velocity;
		#endregion

		public void Init(Texture2D texture, TileTypes type, int frameInterval, Color defaultColor, Color opositeColor)
		{
			_color = new ColorController();
			_color.SetColors(defaultColor, opositeColor);

			_frameInterval = frameInterval;
			_type = type;

			_texture = texture;
			_rectangle = new Rectangle(0, 0, frameInterval, texture.Height);
		}

		/// <summary>
		/// Обновляем состояние объекта
		/// </summary>
		/// <param name="gameTime">Игровое время</param>
		public void Update(GameTime gameTime)
		{
			_rectangle.X = _currentFrame * (int)_frameInterval;
			_rectangle.Y = 0;
		}

		/// <summary>
		/// Обновляем состояние объекта
		/// </summary>
		/// <param name="gameTime">Игровое время</param>
		/// <param name="x">Координата X</param>
		/// <param name="y">Координата Y</param>
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

		/// <summary>
		/// Отрисовываем объект
		/// </summary>
		/// <param name="spriteBatch"></param>
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, _position, _rectangle, _color.GetCurrent(), 0f, _originalPosition, 1.0f, SpriteEffects.None, 0);
		}
	}
}
