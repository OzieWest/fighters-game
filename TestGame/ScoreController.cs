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

		public ScoreController(SpriteFont font, int step, int penalty)
		{
			_message = new FontObject(font);

			_firstLaunch = true;

			_score = 0;
			_step = step;
			_penalty = penalty;
		}

		public void Up()
		{
			_score += _step;
		}

		public void Down()
		{
			_score -= _penalty;
		}

		public void Update(int x, int y)
		{
			_message.SetPosition(x, y);

			if (_firstLaunch)
			{
				_score = 0;
				_firstLaunch = false;
			}

			_message.Text(_score.ToString());
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			_message.Draw(spriteBatch);
		}
	}
}
