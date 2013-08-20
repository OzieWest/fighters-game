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

		public BackgroundController(Texture2D texture)
		{
			_tile = new BackgroundTile()
			{
				Texture = texture,
				Rectangle = new Rectangle()
				{
					X = 0,
					Y = 0,
					Width = texture.Width,
					Height = texture.Height
				},
				Color = Color.White
			};
		}

		public virtual void Update(GameTime gameTime)
		{
			_tile.Update(gameTime);
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			_tile.Draw(spriteBatch);
		}
	}
}
