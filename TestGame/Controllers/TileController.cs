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

		protected Color _defaultColor;
		protected Color _selectedColor;
		#endregion

		public TileController(SpriteBatch spriteBatch, ContentManager content)
		{
			_content = content;
			_spriteBatch = spriteBatch;

			_defaultColor = Color.Black;
			_selectedColor = Color.Gray;
		}

		/// <summary>
		/// Создаем равностороннюю сетку объектов
		/// </summary>
		/// <param name="x">Количество элементов по оси X/Y</param>
		public void CreateGrid(int x)
		{
			var posX = 50;
			var posY = 50;
			var step = 100 + 10;

			_tiles = new List<List<TileObject>>();

			for (var i = 0; i < x; i++)
			{
				var row = new List<TileObject>();

				for (var j = 0; j < x; j++)
				{
					var cell = this.GetRandomTile();
					cell.SetPosition(posX, posY);

					row.Add(cell);

					posX += step;
				}

				_tiles.Add(row);

				posX = 50;
				posY += step;
			}
		}

		/// <summary>
		/// Генерируем случайный тайл
		/// </summary>
		/// <returns></returns>
		public TileObject GetRandomTile()
		{
			int rInt = new Random().Next(1, 6); // все тайлы, кроме дефолтного

			var result = this.GetTileByType((TileTypes)rInt);

			return result;
		}

		/// <summary>
		/// Получаем тайл по его типу
		/// </summary>
		/// <param name="type">Тип тайла</param>
		/// <returns></returns>
		public TileObject GetTileByType(TileTypes type)
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

			var result = new TileObject(_spriteBatch, _content, fileName);
			result.SetColors(_defaultColor, _selectedColor);

			return result;
		}

		/// <summary>
		/// Отрисовываем все объекты
		/// </summary>
		public void Draw()
		{
			_tiles.ForEach((row) =>
			{
				row.ForEach((cell) => 
				{
					cell.Draw();
				});
			});
		}
	}
}
