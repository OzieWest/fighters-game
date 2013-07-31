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
		public Rectangle size;
		public Vector2 position;

		public CursorObject(SpriteBatch spriteBatch, ContentManager content, String fileName)
		{
			_spriteBatch = spriteBatch;
			_texture = content.Load<Texture2D>(fileName);
			Color = Color.White;

			size = new Rectangle(0, 0, 1, 1);

			position = new Vector2(size.X, size.Y);
		}

		public void Update()
		{
			var state = Mouse.GetState();
			position.X = state.X;
			position.Y = state.Y;

			size.X = state.X;
			size.Y = state.Y;
		}

		public void Draw()
		{
			_spriteBatch.Draw(_texture, position, Color);
		}
	}
}
