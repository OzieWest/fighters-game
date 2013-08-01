using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
	public interface IColorController
	{
		Color GetCurrent();
		void SetColors(Color defaultColor, Color oppositeColor);
		void Toggle();
	}
}
