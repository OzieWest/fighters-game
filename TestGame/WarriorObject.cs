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
		protected List<ComplexObject> _skills;

		public WarriorObject(Texture2D texture, SpriteFont font, int score)
			:base(texture, font, TileTypes.Default, score)
		{
			_skills = new List<ComplexObject>();
		}

		public ComplexObject GetByType(TileTypes type)
		{
			return _skills.SingleOrDefault(o => o.Type == type);
		}

		public override void Update(GameTime gameTime)
		{
			var stepX = 0;

			foreach (var skill in _skills)
			{
				skill.SetPosition(
					_tile.Position.X + stepX,
					_tile.Position.Y + 150,
					10, 30
				);
				
				skill.Update(gameTime);

				stepX += 60;
			}

			base.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			foreach (var skill in _skills)
				skill.Draw(spriteBatch);

			base.Draw(spriteBatch);
		}

		public void AddSkill(ComplexObject skill)
		{
			_skills.Add(skill);
		}

		public void ResetSkills()
		{
			_skills.Clear();
		}
	}
}
