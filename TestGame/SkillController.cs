using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class SkillController
	{
		protected List<SkillObject> _skills;

		public SkillController(ContentManager content)
		{
			var startX = 50;
			var startY = 200;
			var score = 1000;
			var step = 1;

			_skills = new List<SkillObject>();

			for (var i = 0; i < 2; i++)
			{
				var texture = content.Load<Texture2D>("skills/skill" + i.ToString());

				var skill = new SkillObject(texture, TileTypes.def, texture.Height, 0)
								{
									Score = new ScoreObject(content.Load<SpriteFont>("font1"))
										{
											Step = step,
											Score = score
										}
								}; 

				skill.SetPosition(startX, startY);

				_skills.Add(skill);

				startY += 128;
			}

		}

		public void Update(GameTime gameTime)
		{
			foreach (var skill in _skills)
				skill.Update(gameTime);
		}

		public void Draw(SpriteBatch SpriteBatch)
		{
			foreach (var skill in _skills)
				skill.Draw(SpriteBatch);
		}

		public void Animate(GameTime gameTime)
		{
			var skill = _skills[0];

			skill.Animate(gameTime);
		}
	}
}
