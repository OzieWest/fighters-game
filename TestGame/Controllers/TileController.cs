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
		protected List<List<TileObject>> _tiles;

		protected SpriteBatch _spriteBatch;
		protected ContentManager _content;
		protected CursorObject _cursor;
		protected GameTime _gameTime;

		protected Color _defaultColor;
		protected Color _selectedColor;
		#endregion

		public TileController(SpriteBatch spriteBatch, ContentManager content, CursorObject cursor)
		{
			_content = content;
			_spriteBatch = spriteBatch;
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
			_tiles = new List<List<TileObject>>();

			var rnd = new Random();
			var constPosX = 135;
			var constPosY = 40;
			var step = 60 + 5;

			var posX = constPosX;
			var posY = constPosY;

			for (var i = 0; i < x; i++)
			{
				var row = new List<TileObject>();

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
		public TileObject GetRandomTile(Random rnd)
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
		public TileObject CreateTileByType(TileTypes type)
		{
			var fileName = String.Empty;

			switch (type)
			{
				case TileTypes.first:
					fileName = "tile_0";
					break;
				case TileTypes.second:
					fileName = "tile_1";
					break;
				case TileTypes.third:
					fileName = "tile_2";
					break;
				case TileTypes.foth:
					fileName = "tile_3";
					break;
				case TileTypes.fifth:
					fileName = "tile_4";
					break;
			}

			var result = new TileObject(_spriteBatch, _content, type, "set1/" + fileName);
			result.SetColors(_defaultColor, _selectedColor);

			return result;
		}

		/// <summary>
		/// Отрисовываем все объекты
		/// </summary>
		public void Draw()
		{
			foreach (var row in _tiles)
			{
				foreach (var cell in row)
				{
					cell.Draw();
				}
			}
		}

		/// <summary>
		/// Обновляем параметры тайлов
		/// </summary>
		/// <param name="gameTime">Игровое время</param>
		public void Update(GameTime gameTime)
		{
			_gameTime = gameTime;

			this.CheckIntersect();
		}

		/// <summary>
		/// Проверяем тайлы на столкновение с курсором
		/// </summary>
		protected void CheckIntersect()
		{
			foreach (var row in _tiles)
			{
				foreach (var cell in row)
				{
					if (_cursor.Intersects(cell))
					{
						cell.Focus();
					}
					else
					{
						cell.UnFocus();
					}
				}
			}
		}
	}
}
