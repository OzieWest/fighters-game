using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class TileComparator : IEqualityComparer<TileObject>
	{
		public bool Equals(TileObject x, TileObject y)
		{
			if (Object.ReferenceEquals(x, y)) return true;

			if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
				return false;

			return x.GridX == y.GridX && x.GridY == y.GridY;
		}

		public int GetHashCode(TileObject obj)
		{
			if (Object.ReferenceEquals(obj, null)) return 0;

			int GridX = obj.GridX == null ? 0 : obj.GridX.GetHashCode();

			int GridY = obj.GridY == null ? 0 : obj.GridY.GetHashCode();

			return GridY ^ GridX;
		}
	}
}
