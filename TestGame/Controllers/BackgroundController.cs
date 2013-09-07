using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Controllers;
using TestGame.Domain;

namespace TestGame
{
	public class BackgroundController
	{
		protected BaseObject _tile;

		public void Init()
		{
			_tile = OFactory.CreateBackground("background1");
		}

		public virtual void Update(GameTime gameTime)
		{
			//_tile.Update(gameTime);
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			_tile.Draw(spriteBatch);
		}
	}
}
