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
		public String _text;
		#endregion

		public IColorController Colors { get; set; }

		public FontObject(SpriteFont font)
		{
			_font = font;
			_position = new Vector2();

			Colors = new ColorController();
			Colors.SetColors(Color.Black, Color.Gray);

			_text = String.Empty;
		}

		public FontObject SetPosition(int x, int y)
		{
			_position.X = x;
			_position.Y = y;

			return this;
		}

		public float X
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

		public float Y
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

		public void Update(GameTime gameTime)
		{
			//
		}

		public FontObject Text(String value)
		{
			_text = value;
			return this;
		}

		public FontObject AddText(String value)
		{
			_text += value;
			return this;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(_font, _text, _position, Colors.GetCurrent());
		}
	}
}
