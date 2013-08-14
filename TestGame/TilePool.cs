using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class TilePool
	{
		protected List<TileObject> _tiles;
		protected Texture2D _texture;

		public TilePool(Texture2D texture, int count)
		{
			_texture = texture;

			_tiles = new List<TileObject>();
			for (var i = 0; i < count; i++)
			{
				var tile = new TileObject(_texture, TileTypes.def, 20, 0);
				this.Release(tile);

				_tiles.Add(tile);
			}
		}

		public void Update(GameTime gameTime)
		{
			foreach (var tile in _tiles)
			{
				if (tile.IsMoveComplete())
				{
					this.Release(tile);
				}
				else
				{
					tile.Update(gameTime);
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (var tile in _tiles)
			{
				if (!tile.IsMoveComplete())
				{
					tile.Draw(spriteBatch);
				}
			}
		}

		public TileObject Take(float x, float y, float tox, float toy)
		{
			var tile = _tiles.FirstOrDefault(o => o.Position.X == -100 && o.Position.Y == -100);

			if (tile != null)
			{
				tile.SetPosition(x, y);
				tile.MoveTo(tox, toy);

				return tile;
			}

			return null;
		}

		public void Release(TileObject tile)
		{
			tile.SetPosition(-100, -100);
			tile.MoveTo(-100, -100);
		}
	}
}
