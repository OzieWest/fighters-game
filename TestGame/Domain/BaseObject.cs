using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame.Domain
{
	public class BaseObject
	{
		protected Vector2 _position;
		protected SpriteBatch _spriteBatch;
		public Color Color { get; set; }
	}
}
