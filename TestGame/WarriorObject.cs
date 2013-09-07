using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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

		public WarriorObject(Texture2D texture, SpriteFont font, int healthValue, int frameInterval)
			: base(texture, frameInterval)
		{
			Gold = new Score(font, "Gold", 0, 0);
			Health = new Score(font, "Health", healthValue, 0);
			Power = new Score(font, "Power", 1, 1);
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
			var corX = 100;

			Gold.SetPosition(
				x + corX,
				y + 100
			);
			Health.SetPosition(
				x + corX,
				y + 130
			);
			Power.SetPosition(
				x + corX,
				y + 160
			);
		}

		public override void Update(GameTime gameTime)
		{
			_correctInfoPosition();

			Position.Frame.Animate(gameTime, 0, 3, 2);
			
			Gold.Update();
			Health.Update();
			Power.Update();

			base.Update(gameTime);
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
