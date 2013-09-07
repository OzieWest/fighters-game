using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class SparkPool
	{
		protected List<SparkTile> _sparks;
		protected int _liveTime;

		public void Init(Texture2D texture, int count, int sparksLiveTime)
		{
			var font = Loader.Instance.GetFont("font_12");
			_liveTime = sparksLiveTime;

			_sparks = new List<SparkTile>();
			for (var i = 0; i < count; i++)
			{
				var tile = new SparkTile(texture, font, 128, false);
				this.Release(tile);

				_sparks.Add(tile);
			}
		}

		public void Update(GameTime gameTime)
		{
			foreach (var tile in _sparks)
			{
				if(tile.IsAlive())
					tile.Update(gameTime);
				else
					this.Release(tile);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (var tile in _sparks)
			{
				if (tile.IsAlive())
					tile.Draw(spriteBatch);
			}
		}

		protected SparkTile GetFreeman()
		{
			return _sparks.FirstOrDefault(o => o.Position.X == -100 && o.Position.Y == -100);
		}

		public SparkTile Start(float x, float y, int value)
		{
			var tile = GetFreeman();

			if (tile != null)
			{
				tile.LiveTime = _liveTime;
				tile.SetInfo(value);
				tile.SetPosition(x, y);

				return tile;
			}

			return null;
		}

		protected void Release(SparkTile tile)
		{
			tile.LiveTime = 0;
			tile.SetPosition(-100, -100);
		}
	}
}
