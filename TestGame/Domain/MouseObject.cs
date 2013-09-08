using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class MouseObject: BaseObject
	{
		public MouseObject(Texture2D texture, int frameInterval)
			: base(texture, frameInterval)
		{

		}

		public void Update()
		{
			var state = Mouse.GetState();
			Position.Set(state.X, state.Y);
		}
	}
}
