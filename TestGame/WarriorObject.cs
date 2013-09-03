using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class WarriorObject : ComplexObject
	{
		public int ScoreGold { get; private set; }
		protected FontObject _gold;

		public int ScoreHealth { get; private set; }
		protected FontObject _health;

		public int ScorePower { get; private set; }
		protected FontObject _power;

		public WarriorObject(Texture2D texture, SpriteFont font, int score)
			:base(texture, font, TileTypes.Default, score)
		{
			_gold = new FontObject(Loader.GetFont("font1"));
			_health = new FontObject(Loader.GetFont("font1"));
			_power = new FontObject(Loader.GetFont("font1"));

			ScorePower = 1;
			ScoreHealth = 100;
			ScoreGold = 0;
		}

		public void Health(int value)
		{
			ScoreHealth += value;
		}

		public void Power(int value)
		{
			ScorePower += value;
		}

		public void Gold(int value)
		{
			ScoreGold += value;
		}

		private void _initPosition()
		{
			_gold.SetPosition(centerX() + 50, centerY() + 0);
			_health.SetPosition(centerX() + 50, centerY() + 30);
			_power.SetPosition(centerX() + 50, centerY() + 60);
		}

		public override void Update(GameTime gameTime)
		{
			_initPosition();

			_gold.Text = String.Format("Gold: {0}", ScoreGold);
			_health.Text = String.Format("Health: {0}", ScoreHealth);
			_power.Text = String.Format("Power: {0}", ScorePower);

			_gold.Update(gameTime);
			_health.Update(gameTime);
			_power.Update(gameTime);

			base.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			_gold.Draw(spriteBatch);
			_health.Draw(spriteBatch);
			_power.Draw(spriteBatch);

			base.Draw(spriteBatch);
		}
	}
}
