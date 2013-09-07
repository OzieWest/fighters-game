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
		public Boolean IsGood { get; set; }
		public int LiveTime { get; set; }
		public FontObject Info { get; set; }

		public SparkTile(Texture2D texture, SpriteFont font, TileTypes type, int frameInterval, int frameOffset, Boolean isGood)
			: base(texture, type, frameInterval, frameOffset)
		{
			LiveTime = 0;
			Info = new FontObject(font);
			Info.Color = isGood ? Color.Green : Color.Red;
		}

		public Boolean IsAlive()
		{
			return LiveTime > 0 ? true : false;
		}

		public override void Update(GameTime gameTime)
		{
			_decreaseLive();
			base.Update(gameTime);
		}

		public void SetInfo(int value)
		{
			if (IsGood)
				Info.Text = "+" + value.ToString();
			else
				Info.Text = "-" + value.ToString();
		}

		public override void SetPosition(float x, float y)
		{
			MoveTo(x, y);
			base.SetPosition(x, y);
			Info.SetPosition(x + 70, y + 70);
		}

		protected void _decreaseLive()
		{
			LiveTime -= 1;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Info.Draw(spriteBatch);
			base.Draw(spriteBatch);
		}
	}
}
