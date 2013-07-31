using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame.Controllers
{
	public class TileController
	{
		#region Injects
		protected List<List<AnimatedTile>> _tiles;

		protected ContentManager _content;
		protected CursorObject _cursor;

		protected Color _defaultColor;
		protected Color _selectedColor;
		#endregion

		public TileController(ContentManager content, CursorObject cursor)
		{
			_content = content;
			_cursor = cursor;

			_defaultColor = Color.White;
			_selectedColor = Color.Wheat;
		}

		/// <summary>
		/// Установливаем цвет отрисовки для всех тайлов
		/// </summary>
		/// <param name="def">Цвет по умолчанию, рекомендуется "White"</param>
		/// <param name="selected">Цвет "выбранного" тайла, рекомендуется "Gray"</param>
		protected void SetColors(Color def, Color selected)
		{
			_defaultColor = def;
			_selectedColor = selected;
		}

		/// <summary>
		/// Создаем равностороннюю сетку объектов
		/// </summary>
		/// <param name="x">Количество элементов по оси X/Y</param>
		public void CreateGrid(int x)
		{
			_tiles = new List<List<AnimatedTile>>();

			var rnd = new Random();
			var constPosX = 135;
			var constPosY = 40;
			var step = 60 + 5;

			var posX = constPosX;
			var posY = constPosY;

			for (var i = 0; i < x; i++)
			{
				var row = new List<AnimatedTile>();

				for (var j = 0; j < x; j++)
				{
					var cell = this.GetRandomTile(rnd);
					cell.SetPosition(posX, posY);

					row.Add(cell);

					posX += step;
				}

				_tiles.Add(row);

				posX = constPosX;
				posY += step;
			}
		}

		/// <summary>
		/// Генерируем случайный тайл
		/// </summary>
		/// <returns></returns>
		public AnimatedTile GetRandomTile(Random rnd)
		{
			int rInt = rnd.Next(1, 6); // все тайлы, кроме дефолтного

			var result = this.CreateTileByType((TileTypes)rInt);

			return result;
		}

		/// <summary>
		/// Получаем тайл по его типу
		/// </summary>
		/// <param name="type">Тип тайла</param>
		/// <returns></returns>
		public AnimatedTile CreateTileByType(TileTypes type)
		{
			var fileName = String.Empty;

			switch (type)
			{
				case TileTypes.first:
					fileName = "ntile_0";
					break;
				case TileTypes.second:
					fileName = "ntile_1";
					break;
				case TileTypes.third:
					fileName = "ntile_2";
					break;
				case TileTypes.foth:
					fileName = "ntile_3";
					break;
				case TileTypes.fifth:
					fileName = "ntile_4";
					break;
			}

			var result = new AnimatedTile(_content, type, "set1/" + fileName, 60);
			result.SetColors(_defaultColor, _selectedColor);

			return result;
		}

		/// <summary>
		/// Отрисовываем все объекты
		/// </summary>
		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (var row in _tiles)
			{
				foreach (var cell in row)
				{
					cell.Draw(spriteBatch);
				}
			}
		}

		/// <summary>
		/// Обновляем параметры тайлов
		/// </summary>
		/// <param name="gameTime">Игровое время</param>
		public void Update(GameTime gameTime)
		{
			this.CheckIntersect(gameTime);
		}

		/// <summary>
		/// Проверяем тайлы на столкновение с курсором
		/// </summary>
		protected void CheckIntersect(GameTime gameTime)
		{
			foreach (var row in _tiles)
			{
				foreach (var cell in row)
				{
					cell.Update(gameTime);

					if (cell.Intersects(_cursor))
					{
						cell.Animate(gameTime);
					}
				}
			}
		}
	}
}
