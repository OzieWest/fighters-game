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
			: base(texture)
		{
			Position.SetFrame(0);

			Position.Rectangle = new Rectangle()
			{
				X = 0,
				Y = 0,
				Width = texture.Width,
				Height = texture.Height
			};

		}

		public void Update()
		{
			var state = Mouse.GetState();
			this.SetPosition(state.X, state.Y);
		}

		public override void SetPosition(float x, float y)
		{
			Position.X = x;
			Position.Y = y;

			//Position.rX = (int)x;
			//Position.rY = (int)y;
		}
	}
}
