using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class ScoreController
	{
		protected FontObject _message;
		protected int _score;
		protected int _step;

		protected int _penalty;
		protected Boolean _firstLaunch;

		protected TilePool _tiles;

		public float X { get; set; }
		public float Y { get; set; }

		public ScoreController(Texture2D texture, SpriteFont font, int step, int penalty)
		{
			_tiles = new TilePool(texture, 20);

			_message = new FontObject(font);

			_firstLaunch = true;

			_score = 1000;
			_step = step;
			_penalty = penalty;
		}

		public void Up()
		{
			_score += _step;
		}

		public void Down(float x, float y)
		{
			_score -= _penalty;
			_tiles.Take(x, y, _message.X, _message.Y);
		}

		public ScoreController SetMessagePosition(int x, int y)
		{
			_message.SetPosition(x, y);

			return this;
		}

		public void Update(GameTime gameTime, float toX, float toY)
		{
			X = toX;
			Y = toY;

			_tiles.Update(gameTime);

			_message.Text = _score.ToString();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			_tiles.Draw(spriteBatch);

			_message.Draw(spriteBatch);
		}
	}
}
