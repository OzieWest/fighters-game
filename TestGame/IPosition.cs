using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
	public interface IPosition
	{
		Vector2 Position { get; }
		Rectangle Rectangle { get;}
	}
}
