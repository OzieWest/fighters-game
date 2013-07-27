using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame.Domain
{
	public class FontObject : BaseObject
	{
		protected SpriteFont _font;
		public String Text { get; set; }

		public FontObject(SpriteBatch spriteBatch, ContentManager content, String fileName)
		{
			_font = content.Load<SpriteFont>(fileName);
			_position = new Vector2(10, 10);
			Color = Color.Black;
			Text = String.Empty;
			_spriteBatch = spriteBatch;
		}

		public void Draw(String text, Vector2 position, Color color)
		{
			_position = position;
			Color = color;
			Text = text;

			this.Draw();
		}

		public void Draw(String text, Vector2 position)
		{
			_position = position;
			Text = text;

			this.Draw();
		}

		public void Draw(String text)
		{
			Text = text;

			this.Draw();
		}

		public void Draw()
		{
			_spriteBatch.DrawString(_font, Text, _position, Color);
		}
	}
}
