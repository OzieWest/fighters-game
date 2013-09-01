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

		public void Init(Texture2D texture, int count)
		{
			_texture = texture;

			_tiles = new List<TileObject>();
			for (var i = 0; i < count; i++)
			{
				var tile = new TileObject(_texture, TileTypes.Default, _texture.Width, 0);
				this.Release(tile);

				_tiles.Add(tile);
			}
		}

		public void Update(GameTime gameTime)
		{
			foreach (var tile in _tiles)
			{
				if (tile.IsMoveComplete())
					this.Release(tile);
				else
					tile.Update(gameTime);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (var tile in _tiles)
			{
				if (!tile.IsMoveComplete())
					tile.Draw(spriteBatch);
			}
		}

		protected TileObject GetFreeman()
		{
			return _tiles.FirstOrDefault(o => o.Position.X == -100 && o.Position.Y == -100);
		}

		public TileObject LaunchTile(float x, float y, float destinationX, float destinationY)
		{
			var tile = GetFreeman();

			if (tile != null)
			{
				tile.Position.SpeedConst = 8;
				tile.SetPosition(x, y);
				tile.MoveTo(destinationX, destinationY);

				return tile;
			}

			return null;
		}

		protected void Release(TileObject tile)
		{
			tile.SetPosition(-100, -100);
			tile.MoveTo(-100, -100);
		}
	}
}
