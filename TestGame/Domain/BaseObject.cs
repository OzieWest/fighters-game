using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Controllers;

namespace TestGame.Domain
{
	public class BaseObject : IRectangle
	{
		#region Values
		protected Rectangle _rectangle;
		protected Vector2 _position;
		protected Vector2 _originalPosition;
		protected Texture2D _texture;
		#endregion

		#region Property
		public Vector2 Position
		{
			get
			{
				return _position;
			}
			set
			{
				_position = value;
			}
		}
		public Rectangle Rectangle
		{
			get
			{
				return _rectangle;
			}
			set { _rectangle = value; }
		}
		#endregion

		#region Injects
		protected IColorController _color;
		#endregion

		public BaseObject()
		{
			_originalPosition = new Vector2();
			_position = new Vector2();
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

		/// <summary>
		/// Установка позиции объекта
		/// </summary>
		/// <param name="x">Координата X</param>
		/// <param name="y">Координата Y</param>
		public virtual void SetPosition(int x, int y)
		{
			_position.X = x;
			_position.Y = y;
		}

		//todo: передалать
		public virtual Boolean IsIntersectWith(BaseObject obj, int objInterval, int objOffset)
		{
			var mouseX = obj.Position.X;
			var mouseY = obj.Position.Y;

			if (mouseX > (_position.X + objOffset) &&
				mouseX < (_position.X + objInterval) - objOffset &&
				mouseY > (_position.Y + objOffset) &&
				mouseY < (_position.Y + objInterval) - objOffset)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
