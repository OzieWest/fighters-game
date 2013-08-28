using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Controllers;

namespace TestGame
{
	public class BackgroundTile
	{
		//Todo rectangle переделать на Postition
		protected Rectangle _rectangle;
		public Rectangle Rectangle { get { return _rectangle; } set { _rectangle = value; } }
		public Texture2D Texture { get; set; }

		public Color Color { get; set; }

		public virtual void Update(GameTime gameTime)
		{
			Color = Color.White;
		}

		protected virtual BackgroundTile SetPosition(int x, int y)
		{
			_rectangle.X = x;
			_rectangle.Y = y;

			return this;
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, Rectangle, Color);
		}
	}
}
