using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
	public interface IPosition
	{
		float Speed { get; set; }
		Vector2 Original { get; }
		Vector2 Destination { get; set; }
		Vector2 Real { get; set; }
		Rectangle Rectangle { get; set; }

		float X { get; set; }
		float Y { get; set; }
		
		float toX { get; set; }
		float toY { get; set; }
		
		int rX { get; set; }
		int rY { get; set; }
	}
}
