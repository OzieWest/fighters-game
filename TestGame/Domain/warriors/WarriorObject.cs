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
		float infoX;
		float infoY;
		BaseObject window;

		public Score Gold { get; set; }
		public Score Health { get; set; }
		public Score Power { get; set; }
		public Boolean IsReady { get; set; }
		public WarriorActions Action { get; set; }

		public WarriorObject(Texture2D texture, Texture2D messageTexture, SpriteFont font, int healthValue, int frameInterval)
			: base(texture, frameInterval)
		{
			Gold = new Score(font, "Gold", 0, 0);
			Health = new Score(font, "Health", healthValue, 0);
			Power = new Score(font, "Power", 1, 1);

			window = new BaseObject(messageTexture, 132);

			IsReady = true;
			Action = WarriorActions.Stand;
		}

		public void SetPosition(float x, float y)
		{
			Position.MoveTo(x, y);
			Position.Set(x, y);

			SetPositionMessage(x, y, Color.Black);
		}

		public void SetPositionMessage(float x, float y, Color color)
		{
			window.Position.MoveTo(x, y);
			window.Position.Set(x, y);

			infoX = x + 10;
			infoY = y + 15;

			Gold.Color = color;
			Health.Color = color;
			Power.Color = color;
		}

		private void _correctInfoPosition()
		{
			Gold.SetPosition(
				infoX,
				infoY - 0
			);
			Health.SetPosition(
				infoX,
				infoY + 20
			);
			Power.SetPosition(
				infoX,
				infoY + 40
			);
		}

		public override void Update(GameTime gameTime)
		{
			_correctInfoPosition();

			Gold.Update();
			Health.Update();
			Power.Update();
			window.Update(gameTime);

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
			window.Draw(spriteBatch);
			Gold.Draw(spriteBatch);
			Health.Draw(spriteBatch);
			Power.Draw(spriteBatch);
			base.Draw(spriteBatch);
		}
	}
}
