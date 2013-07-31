using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TestGame.Domain
{
	public class AnimatedTile
	{
		#region Declare
		protected Vector2 _originalPosition;
		protected Texture2D _texture;
		protected Rectangle _rectangle;

		protected int _currentFrame;
		protected float _timer;
		protected float _interval;

		protected Color _colorCurrent;
		protected Color _colorDefault;
		protected Color _colorSelected;

		protected TileTypes _type;

		public Vector2 position;
		public Vector2 velocity;
		#endregion

		public AnimatedTile(ContentManager content, TileTypes type, String fileName, int interval)
		{
			_originalPosition = new Vector2(); //default
			_interval = interval;
			_colorCurrent = Color.White;
			_type = type;

			_texture = content.Load<Texture2D>(fileName);
			_rectangle = new Rectangle(0, 0, (int)_interval, _texture.Height);
			position = new Vector2();
		}

		/// <summary>
		/// Установка позиции объекта
		/// </summary>
		/// <param name="x">Координата X</param>
		/// <param name="y">Координата Y</param>
		public virtual void SetPosition(int x, int y)
		{
			position.X = x;
			position.Y = y;
		}

		/// <summary>
		/// Обновляем состояние объекта
		/// </summary>
		/// <param name="gameTime">Игровое время</param>
		/// <param name="x">Координата X</param>
		/// <param name="y">Координата Y</param>
		public void Update(GameTime gameTime, int x, int y)
		{
			//todo: create Rectabgle controller
			_rectangle.X = _currentFrame * (int)_interval;
			_rectangle.Y = 0;

			position.X = x;
			position.Y = y;
		}

		/// <summary>
		/// Обновляем состояние объекта
		/// </summary>
		/// <param name="gameTime">Игровое время</param>
		public void Update(GameTime gameTime)
		{
			//todo: create Rectabgle controller
			_rectangle.X = _currentFrame * (int)_interval;
			_rectangle.Y = 0;
		}

		public void Animate(GameTime gameTime)
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

		//todo: абстрагироваться от конкретной реализации
		public Boolean Intersects(CursorObject obj)
		{
			var mouseX = obj.position.X;
			var mouseY = obj.position.Y;

			var offset = 10;

			if (mouseX > (position.X + offset) &&
				mouseX < (position.X + (int)_interval) - offset &&
				mouseY > (position.Y + offset) &&
				mouseY < (position.Y + (int)_interval) - offset)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Отрисовываем объект
		/// </summary>
		/// <param name="spriteBatch"></param>
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, position, _rectangle, Color.White, 0f, _originalPosition, 1.0f, SpriteEffects.None, 0);
		}

		/// <summary>
		/// Устанавливаем цвета отрисовки объекта
		/// </summary>
		/// <param name="defaulf">Стандартый цвет</param>
		/// <param name="selected">"Выбранный" цвет</param>
		public void SetColors(Color defaulf, Color selected)
		{
			_colorDefault = defaulf;
			_colorSelected = selected;
		}

		/// <summary>
		/// Переключаем текущий цвет отрисовки
		/// </summary>
		public void ToggleCurrentColor()
		{
			if (_colorCurrent == _colorDefault)
			{
				_colorCurrent = _colorSelected;
			}
			else
			{
				_colorCurrent = _colorDefault;
			}
		}

		/// <summary>
		/// Устанавливаем текущий цвет значением по умолчанию
		/// </summary>
		protected void SetDefaultColor()
		{
			_colorCurrent = _colorDefault;
		}

		/// <summary>
		/// Устанавливаем текущий цвет значением по "выбранный"
		/// </summary>
		protected void SetSelectedColor()
		{
			_colorCurrent = _colorSelected;
		}
	}
}
