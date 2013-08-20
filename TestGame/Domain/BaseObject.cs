using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Controllers;

namespace TestGame.Domain
{
	public class BaseObject
	{
		#region Injects
		public Position Position { get; set; }
		public TileClass Class { get; set; }
		public Color Color { get; set; }
		protected Frame _frame;
		#endregion

		public BaseObject(Texture2D texture)
		{
			Position = new Position()
				{
					Real = new Vector2(0, 0)
				};

			Class = new TileClass()
				{
					Texture = texture
				};

			_frame = new Frame();

			Color = Color.White;
		}

		public virtual void SetPosition(float x, float y)
		{
			Position.X = x;
			Position.Y = y;
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Class.Texture, Position.Real, Position.Rectangle, Color, 0f, Position.Original, 1.0f, SpriteEffects.None, 0);
		}
	}
}
