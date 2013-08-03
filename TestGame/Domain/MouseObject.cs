using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Controllers;

namespace TestGame.Domain
{
	public class MouseObject : IPosition
	{
		#region Values
		protected Texture2D _texture;
		protected Rectangle _rectangle;
		protected Vector2 _position;
		#endregion

		#region Property
		public Rectangle Rectangle
		{
			get { return _rectangle; }
		}
		public Vector2 Position
		{
			get
			{
				return _position;
			}
		}
		#endregion

		#region Injects
		protected IColorController _color;
		#endregion

		public MouseObject(Texture2D texture)
		{
			_texture = texture;
			_color = new ColorController();
			_color.SetColors(Color.White, Color.Gray);

			_rectangle = new Rectangle(0, 0, 1, 1);

			_position = new Vector2(_rectangle.X, _rectangle.Y);
		}

		public void Update(GameTime gameTime)
		{
			var state = Mouse.GetState();
			this.SetPosition(state.X, state.Y);
		}

		public void Update(GameTime gameTime, int x, int y)
		{
			var state = Mouse.GetState();
			this.SetPosition(x, y);
		}

		protected void SetPosition(int x, int y)
		{
			_position.X = x;
			_position.Y = y;

			_rectangle.X = x;
			_rectangle.Y = y;
		}

		/// <summary>
		/// Устанавливаем цвета отрисовки объекта
		/// </summary>
		/// <param name="defaulf">Стандартый цвет</param>
		/// <param name="selected">"Выбранный" цвет</param>
		public virtual void SetColors(Color defaultColor, Color selected)
		{
			_color.SetColors(defaultColor, selected);
		}

		/// <summary>
		/// Переключаем текущий цвет отрисовки
		/// </summary>
		public virtual void ToggleCurrentColor()
		{
			_color.Toggle();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, _position, _color.GetCurrent());
		}
	}
}
