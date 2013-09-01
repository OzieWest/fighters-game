using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
	public interface IObject
	{
		void Update(GameTime gameTime);
		void Draw(SpriteBatch spriteBatch);
	}
}
