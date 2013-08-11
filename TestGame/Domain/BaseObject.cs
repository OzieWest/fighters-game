using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Controllers;

namespace TestGame.Domain
{
	public class BaseObject : IPosition
	{
		#region Values
		protected Rectangle _rectangle;
		protected Vector2 _position;
		protected Vector2 _originalPosition;
		protected Texture2D _texture;
		#endregion

		#region Property
		public Vector2 Position
		{
			get
			{
				return _position;
			}
		}
		public Rectangle Rectangle
		{
			get
			{
				return _rectangle;
			}
		}
		#endregion

		#region Injects
		protected IColorController _color;
		#endregion

		public BaseObject(Texture2D texture)
		{
			_color = new ColorController();

			_originalPosition = new Vector2();

			_position = new Vector2(0, 0);

			_texture = texture;

			this.SetColors(Color.White, Color.Black);
		}

		public virtual void SetColors(Color defaultColor, Color selected)
		{
			_color.SetColors(defaultColor, selected);
		}

		public virtual void ToggleCurrentColor()
		{
			_color.Toggle();
		}

		public virtual void SetPosition(float x, float y)
		{
			_position.X = x;
			_position.Y = y;
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, _position, _rectangle, _color.GetCurrent(), 0f, _originalPosition, 1.0f, SpriteEffects.None, 0);
		}
	}
}
