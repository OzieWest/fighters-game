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

			return x.Grid.X == y.Grid.X && x.Grid.Y == y.Grid.Y;
		}

		public int GetHashCode(TileObject obj)
		{
			if (Object.ReferenceEquals(obj, null)) return 0;

			return obj.Grid.X.GetHashCode() ^ obj.Grid.Y.GetHashCode();
		}
	}
}
