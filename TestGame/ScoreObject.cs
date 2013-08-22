using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class ScoreObject: FontObject
	{
		public int Score { get; set; }
		public int Step { get; set; }

		public ScoreObject(SpriteFont font)
			: base(font)
		{
			Score = 0;
		}

		public override void Update(GameTime gameTime)
		{
			Text = Score.ToString();
		}

		public void Up()
		{
			Score += Step;
		}

		public void Up(int step)
		{
			Score += step;
		}

		public void Down()
		{
			Score -= Step;
		}

		public void Down(int step)
		{
			Score -= step;
		}
	}
}
