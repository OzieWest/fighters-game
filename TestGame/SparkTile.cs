using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class SparkTile : TileObject
	{
		public int LiveTime { get; set; }

		public SparkTile(Texture2D texture, TileTypes type, int liveTime)
			:base(texture, type, 0, 0)
		{
			LiveTime = liveTime;
		}

		public Boolean IsAlive()
		{
			return LiveTime <= 0 ? true : false;
		}

		public override void Update(GameTime gameTime)
		{
			_decreaseLive();
			base.Update(gameTime);
		}

		public override void SetPosition(float x, float y)
		{
			MoveTo(x, y);
			base.SetPosition(x, y);
		}

		protected void _decreaseLive()
		{
			LiveTime -= 1;
		}
	}
}
