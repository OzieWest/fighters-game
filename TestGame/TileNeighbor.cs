using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class TileNeighbor
	{
		/// <summary>
		/// Left
		/// </summary>
		public TileObject L { get; set; }

		/// <summary>
		/// Top
		/// </summary>
		public TileObject T { get; set; }

		/// <summary>
		/// Right
		/// </summary>
		public TileObject R { get; set; }

		/// <summary>
		/// Bottom
		/// </summary>
		public TileObject B { get; set; }

		public void Null()
		{
			L = null;
			T = null;
			R = null;
			B = null;
		}

		public List<TileObject> GetAll()
		{
			var result = new List<TileObject>();

			if (B != null)
			{
				result.Add(B);
			}
			if (T != null)
			{
				result.Add(T);
			}
			if (R != null)
			{
				result.Add(R);
			}
			if (L != null)
			{
				result.Add(L);
			}

			return result;
		}
	}
}
