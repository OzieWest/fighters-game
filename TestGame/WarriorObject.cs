using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class WarriorObject : BaseObject
	{
		public Score Gold { get; set; }
		public Score Health { get; set; }
		public Score Power { get; set; }
		public Boolean IsReady { get; set; }
		public WarriorActions Action { get; set; }

		public WarriorObject(Texture2D texture, SpriteFont font, int healthValue, int frameInterval)
			: base(texture, frameInterval)
		{
			Gold = new Score(font, "Gold", 0, 0);
			Health = new Score(font, "Health", healthValue, 0);
			Power = new Score(font, "Power", 1, 1);
			IsReady = true;
			Action = WarriorActions.Stand;
		}

		public void SetPosition(float x, float y)
		{
			Position.MoveTo(x, y);
			Position.Set(x, y);
		}

		private void _correctInfoPosition()
		{
			var x = Position.X;
			var y = Position.Y;
			var corX = 20;

			Gold.SetPosition(
				x + corX,
				y + 150
			);
			Health.SetPosition(
				x + corX,
				y + 170
			);
			Power.SetPosition(
				x + corX,
				y + 190
			);
		}

		public override void Update(GameTime gameTime)
		{
			_correctInfoPosition();

			Gold.Update();
			Health.Update();
			Power.Update();

			_animation(gameTime);
			base.Update(gameTime);
		}

		void _animation(GameTime gameTime)
		{
			switch (Action)
			{
				case WarriorActions.Stand:
					Position.Frame.Animate(gameTime, 0, 3, 2);
					break;
				case WarriorActions.Strike:
					Position.Frame.Animate(gameTime, 4, 12, 5);
					break;
				default:
					Position.Frame.Reset();
					break;
			}

			if (Position.Frame.Current == 12)
				Action = WarriorActions.Stand;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Gold.Draw(spriteBatch);
			Health.Draw(spriteBatch);
			Power.Draw(spriteBatch);
			base.Draw(spriteBatch);
		}
	}
}
