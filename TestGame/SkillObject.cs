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
		private Boolean disposed = false;

		public int Score { get; set; }

		public FontObject Message { get; set; }

		public SkillObject(Texture2D texture,
							TileTypes type,
							int frameInterval,
							int frameOffset)

			: base(texture, type, frameInterval, frameOffset)
		{
			Grid = null;
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
					_frame.Reset();
					break;
			}
		}

		public override void SetPosition(float x, float y)
		{
			Message.SetPosition((int)x + 10, (int)y + 60);

			base.SetPosition(x, y);
			base.MoveTo(x, y);
		}

		public override void Update(GameTime gameTime)
		{
			Message.Text = Score.ToString();
			Message.Update(gameTime);

			base.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Message.Draw(spriteBatch);
			base.Draw(spriteBatch);
		}

		#region Steps
		public void Up(int step)
		{
			Score += step;
		}

		public void Down(int step)
		{
			Score -= step;
		}
		#endregion Steps
	}
}
