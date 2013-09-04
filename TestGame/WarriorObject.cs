using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class WarriorObject
	{
		protected TileObject _tile;

		public Score Gold { get; set; }
		public Score Health { get; set; }
		public Score Power { get; set; }

		public WarriorObject(Texture2D texture, SpriteFont font, int healthValue)
		{
			_tile = new TileObject(texture, TileTypes.Default, 128, 0);

			Gold = new Score(
				Loader.GetFont("font1"),
				"Gold",
				0
			);
			Health = new Score(
				Loader.GetFont("font1"),
				"Health",
				healthValue
			);
			Power = new Score(
				Loader.GetFont("font1"),
				"Power",
				1
			);
		}

		public void SetPosition(float x, float y)
		{
			_tile.Position.X = x;
			_tile.Position.Y = y;
			_tile.MoveTo(x, y);
		}

		public float X
		{
			get
			{
				var x = _tile.Position.X;
				return (int)x + _tile.Texture.Width / 2;
			}
		}

		public float Y
		{
			get
			{
				var y = _tile.Position.Y;
				return (int)y + _tile.Texture.Height / 2;
			}
		}

		private void _correctSkillPosition()
		{
			var x = _tile.Position.X;
			var y = _tile.Position.Y;
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

		public virtual void Update(GameTime gameTime)
		{
			_correctSkillPosition();

			Gold.Update();
			Health.Update();
			Power.Update();

			_tile.Update(gameTime);
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			Gold.Draw(spriteBatch);
			Health.Draw(spriteBatch);
			Power.Draw(spriteBatch);
			
			_tile.Draw(spriteBatch);
		}
	}
}
