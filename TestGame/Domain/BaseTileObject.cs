using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame.Domain
{
	public class BaseTileObject
	{
		#region Injects
		protected SpriteBatch _spriteBatch;
		protected Texture2D _texture;
		public Rectangle position;

		protected Color _colorCurrent;
		protected Color _colorDefault;
		protected Color _colorSelected;

		protected TileTypes _type;
		#endregion

		public BaseTileObject(SpriteBatch spriteBatch, ContentManager content, TileTypes type, String fileName)
		{
			_spriteBatch = spriteBatch;
			_texture = content.Load<Texture2D>(fileName);
			_colorCurrent = Color.White;
			_type = type;

			position = new Rectangle(0, 0, _texture.Width, _texture.Height);
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
		/// Обновляем позицию объекта
		/// </summary>
		/// <param name="x">Координата X</param>
		/// <param name="y">Координата Y</param>
		public virtual void Update(int x, int y)
		{
			position.X = x;
			position.Y = y;
		}

		/// <summary>
		/// Отрисовываем объект
		/// </summary>
		public virtual void Draw()
		{
			_spriteBatch.Draw(_texture, position, _colorCurrent);
		}
	}
}
