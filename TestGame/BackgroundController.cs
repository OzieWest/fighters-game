using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Controllers;

namespace TestGame
{
	public class BackgroundController
	{
		protected BackgroundTile _tile;

		protected ContentManager _content;
		protected SpriteBatch _spriteBatch;

		public BackgroundController(ContentManager content, SpriteBatch spriteBatch)
		{
			_spriteBatch = spriteBatch;
			_content = content;

			var texture = content.Load<Texture2D>("back_2");
			var rectangle = new Rectangle(0, 0, texture.Width, texture.Height);

			var color = new ColorController();
			color.SetColors(Color.White, Color.Gray);

			_tile = new BackgroundTile(texture, rectangle, color);
		}

		public virtual void Update(GameTime gameTime)
		{
			_tile.Update(gameTime);
		}

		public virtual void Draw()
		{
			_tile.Draw(_spriteBatch);
		}
	}
}
