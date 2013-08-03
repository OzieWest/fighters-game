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
		#endregion

		#region Property
		public String Text { get; set; }
		#endregion

		#region Injects
		protected IColorController _color;
		#endregion

		public FontObject(SpriteFont font)
		{
			_font = font;
			_position = new Vector2();

			_color = new ColorController();
			_color.SetColors(Color.White, Color.Gray);

			Text = String.Empty;
		}

		public void SetPosition(int x, int y)
		{
			_position.X = x;
			_position.Y = y;
		}

		public void Update(GameTime gameTime, int x, int y)
		{
			this.SetPosition(x, y);
		}

		public void Draw(SpriteBatch spriteBatch, String text)
		{
			Text = text;

			this.Draw(spriteBatch);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(_font, Text, _position, _color.GetCurrent());
		}
	}
}
