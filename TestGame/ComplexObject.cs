using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class ComplexObject : IObject
	{
		protected TileObject _tile;
		protected FontObject _info;
		protected int _score;

		public ComplexObject(Texture2D texture, SpriteFont font, TileTypes type, int score)
		{
			_tile = new TileObject(texture, type, texture.Width, 0);
			_info = new FontObject(font);

			_score = score;
		}

		public TileTypes Type
		{
			get
			{
				return _tile.Type;
			}
		}

		public virtual int centerX()
		{
			var x = _tile.Position.X;

			return (int)x + _tile.Texture.Width / 2;
		}

		public virtual int centerY()
		{
			var y = _tile.Position.Y;

			return (int)y + _tile.Texture.Height / 2;
		}

		public virtual void SetPosition(float x, float y, int messOffsetX, int messOffsetY)
		{
			_tile.SetPosition(x, y);
			_tile.MoveTo(x, y);

			_info.SetPosition((int)x + messOffsetX, (int)y + messOffsetY);
		}

		public virtual void ScoreDown(int value)
		{
			_score -= value;
		}

		public virtual void ScoreUp(int value)
		{
			_score += value;
		}

		public virtual void Update(GameTime gameTime)
		{
			_tile.Update(gameTime);

			_info.Text = _score.ToString();
			_info.Update(gameTime);
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			_tile.Draw(spriteBatch);
			_info.Draw(spriteBatch);
		}
	}
}
