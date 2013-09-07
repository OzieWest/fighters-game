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
		public MouseObject(Texture2D texture)
			: base(texture, 10)
		{

		}

		public void Update()
		{
			var state = Mouse.GetState();
			this.SetPosition(state.X, state.Y);
		}

		public void SetPosition(float x, float y)
		{
			Position.Set(x, y);
		}
	}
}
