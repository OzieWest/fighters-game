using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Controllers;

namespace TestGame.Domain
{
	public class FontObject
	{
		#region Values
		protected SpriteFont _font;
		protected Vector2 _position;
		public String Text { get; set; }
		#endregion

		public Color Color { get; set; }

		public FontObject(SpriteFont font)
		{
			_font = font;
			_position = new Vector2();

			Color = Color.Black;

			Text = String.Empty;
		}

		public virtual FontObject SetPosition(int x, int y)
		{
			_position.X = x;
			_position.Y = y;

			return this;
		}

		public virtual float X
		{
			get
			{
				return _position.X;
			}
			set
			{
				_position.X = value;
			}
		}

		public virtual float Y
		{
			get
			{
				return _position.Y;
			}
			set
			{
				_position.Y = value;
			}
		}

		public virtual void Update(GameTime gameTime)
		{
			//
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(_font, Text, _position, Color);
		}
	}
}
