using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
	public class BackgroundTile
	{
		protected Rectangle _rectangle;
		protected Texture2D _texture;

		protected IColorController _color;

		public BackgroundTile(Texture2D texture, Rectangle rectangle, IColorController color)
		{
			_rectangle = rectangle;
			_texture = texture;
			_color = color;
		}

		public virtual void Update(GameTime gameTime)
		{
			//
		}

		protected virtual void SetPosition(int x, int y)
		{
			_rectangle.X = x;
			_rectangle.Y = y;
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, _rectangle, _color.GetCurrent());
		}

		#region Colors
		public virtual void SetColors(Color defaultColor, Color selected)
		{
			_color.SetColors(defaultColor, selected);
		}

		public virtual void ToggleCurrentColor()
		{
			_color.Toggle();
		}
		#endregion
	}
}
