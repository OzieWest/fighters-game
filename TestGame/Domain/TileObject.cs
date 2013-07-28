using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame.Domain
{
	public class TileObject : BaseTileObject
	{
		public TileObject(SpriteBatch spriteBatch, ContentManager content, TileTypes type, String fileName)
			: base(spriteBatch, content, type, fileName)
		{
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
		public void SetDefaultColor()
		{
			_colorCurrent = _colorDefault;
		}

		/// <summary>
		/// Устанавливаем текущий цвет значением по "выбранный"
		/// </summary>
		public void SetSelectedColor()
		{
			_colorCurrent = _colorSelected;
		}

	}
}
