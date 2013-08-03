using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class PlaceController
	{
		#region Properties
		public int X { get; set; }
		public int Y { get; set; }
		#endregion

		#region Injects
		public TileObject Left { get; set; }
		public TileObject Top { get; set; }
		public TileObject Right { get; set; }
		public TileObject Bottom { get; set; }
		#endregion
	}
}
