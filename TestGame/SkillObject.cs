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

		private Boolean disposed = false;

		public SkillObject(Texture2D texture, TileTypes type, int frameInterval, int frameOffset)
			: base(texture, type, frameInterval, frameOffset)
		{

			State = TileState.Selected;
		}

		protected override void _animate(GameTime gameTime)
		{
			switch (State)
			{
				case TileState.Focused:
					_frame.Animate(gameTime, 0, 4, 2);
					break;
				case TileState.Selected:
					_frame.Animate(gameTime, 5, 9, 2);
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
	}
}
