using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame.Domain
{
	public class CursorObject : BaseObject
	{
		protected Texture2D _texture;
		public Rectangle position;

		public CursorObject(SpriteBatch spriteBatch, ContentManager content, String fileName)
		{
			_spriteBatch = spriteBatch;
			_texture = content.Load<Texture2D>(fileName);
			Color = Color.White;

			position = new Rectangle(0, 0, _texture.Width, _texture.Height);
		}

		public Boolean Intersects(TileObject obj)
		{
			return position.Intersects(obj.position);
		}

		public void Update()
		{
			var state = Mouse.GetState();
			position.X = state.X;
			position.Y = state.Y;
		}

		public void Draw()
		{
			_spriteBatch.Draw(_texture, position, Color);
		}
	}
}
