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
		public Position Position { get; set; }
		public Texture2D Texture { get; set; }
		public Color Color { get; set; }

		public BaseObject(Texture2D texture, int frameInterval)
		{
			Texture = texture;

			Position = new Position(Texture.Height, frameInterval);

			Color = Color.White;
		}

		public virtual void Update(GameTime gameTime)
		{
			Position.Update();
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, Position.Real, Position.Rectangle, Color, 0f, Position.Original, 1.0f, SpriteEffects.None, 0);
		}
	}
}
