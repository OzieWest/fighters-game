using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame.Controllers
{
	public class ColorController : IColorController
	{
		protected Color _colorCurrent;
		protected Color _colorDefault;
		protected Color _colorOpposite;

		/// <summary>
		/// Устанавливаем цвета
		/// </summary>
		/// <param name="defaultColor">Цвет по умолчанию</param>
		/// <param name="oppositeColor">Цвет противоположный/второй</param>
		public void SetColors(Color defaultColor, Color oppositeColor)
		{
			_colorOpposite = oppositeColor;
			_colorCurrent = defaultColor;
			_colorDefault = defaultColor;
		}

		/// <summary>
		/// Возвращаем текущее значение
		/// </summary>
		/// <returns></returns>
		public Color GetCurrent()
		{
			return _colorCurrent;
		}

		/// <summary>
		/// Переключаем текущий цвет
		/// </summary>
		public void Toggle()
		{
			if (_colorCurrent == _colorDefault)
			{
				_colorCurrent = _colorOpposite;
			}
			else
			{
				_colorCurrent = _colorDefault;
			}
		}
	}
}
