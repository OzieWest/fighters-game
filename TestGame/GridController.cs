using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
	public class GridController
	{
		List<List<Vector2>> _positions;

		public GridController(int count)
		{
			_positions = new List<List<Vector2>>();

			this.CreateGrid(count);
		}

		protected void CreateGrid(int x)
		{
			var constPosX = 250;
			var constPosY = 20;
			var step = 60 + 5;

			var posX = constPosX;
			var posY = constPosY;

			for (var i = 0; i < x; i++)
			{
				var row = new List<Vector2>();

				for (var j = 0; j < x; j++)
				{
					var cell = new Vector2(posX, posY);

					row.Add(cell);

					posX += step;
				}

				_positions.Add(row);

				posX = constPosX;
				posY += step;
			}
		}

		public Vector2 this[int indexA, int indexB]
		{
			get
			{
				if (indexA > -1 &&
					indexA < _positions.Count &&
					indexB > -1 &&
					indexB < _positions.Count)
				{
					var vector = new Vector2(_positions[indexA][indexB].X, _positions[indexA][indexB].Y);
					return vector;
				}
				else
				{
					return Vector2.Zero;
				}
			}
		}
	}
}
