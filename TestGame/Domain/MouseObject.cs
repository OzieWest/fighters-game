using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Controllers;

namespace TestGame.Domain
{
	public class MouseObject
	{
		protected Texture2D _texture;
		
		public Position Position { get; set; }
		public Color Color { get; set; }

		public MouseObject(Texture2D texture)
		{
			_texture = texture;

			Position = new Position()
			{
				Rectangle = new Rectangle(0, 0, 1, 1),
				Real = new Vector2(0, 0)
			};

			Color = Color.White;
		}

		public void Update(GameTime gameTime)
		{
			var state = Mouse.GetState();
			this.SetPosition(state.X, state.Y);
		}

		protected void SetPosition(int x, int y)
		{
			Position.X = x;
			Position.Y = y;

			Position.rX = x;
			Position.rY = y;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, Position.Real, Color);
		}
	}
}
