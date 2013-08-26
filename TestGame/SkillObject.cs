using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class SkillObject : TileObject
	{
		public ScoreObject Score { get; set; }
		public String Name { get; set; }

		private Boolean disposed = false;

		public SkillObject(Texture2D texture, TileTypes type, int frameInterval, int frameOffset)
			: base(texture, type, frameInterval, frameOffset)
		{

			State = TileState.Normal;
		}

		protected override void _animate(GameTime gameTime)
		{
			switch (State)
			{
				case TileState.Focused:
					_frame.Animate(gameTime, 1, 4, 4);
					break;
				case TileState.Selected:
					_frame.Animate(gameTime, 3, 4, 1);
					break;
				default:
					_frame.ResetCurrent();
					break;
			}
		}

		public void Animate(GameTime gameTime)
		{
			_animate(gameTime);
		}

		public override void SetPosition(float x, float y)
		{
			base.SetPosition(x, y);
			Score.SetPosition((int)x + 10, (int)y + 60);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			Score.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			Score.Draw(spriteBatch);
		}
	}
}
