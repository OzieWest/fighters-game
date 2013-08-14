using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class EnemyController
	{
		public TileObject tile;

		public EnemyController(Texture2D texture)
		{
			tile = new TileObject(texture, TileTypes.def, 128, 0);
		}
	}
}
