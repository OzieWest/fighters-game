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
			var startX = 145;
			var startY = 10;
			var score = 100;
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
											Score = score,
										},

									Name = i.ToString()
								};

				skill.SetPosition(startX, startY);

				_skills.Add(skill);

				startY += 100;
			}

		}

		public void Update(GameTime gameTime, Position obj, Boolean isSelect)
		{
			foreach (var skill in _skills)
				skill.Update(gameTime);

			_checkIntersect(gameTime, obj, isSelect);
		}

		protected void _checkIntersect(GameTime gameTime, Position obj, Boolean isSelect)
		{
			var selected = _skills.SingleOrDefault(o => o.State == TileState.Selected);

			foreach (var skill in _skills)
			{
				if (isSelect)
				{
					if (skill.IsIntersect(obj) && skill.State != TileState.Selected)
					{
						if (selected == null)
						{
							skill.State = TileState.Selected;
						}
					}
					else if (skill.IsIntersect(obj) && skill.State == TileState.Selected)
						skill.State = TileState.Focused;
					//else
					//	skill.State = TileState.Normal;
				}
				else
				{
					if (skill.IsIntersect(obj) && skill.State != TileState.Selected)
						skill.State = TileState.Focused;
					else if (!skill.IsIntersect(obj) && skill.State != TileState.Selected)
						skill.State = TileState.Normal;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (var skill in _skills)
				skill.Draw(spriteBatch);
		}

		public void Animate(GameTime gameTime)
		{
			var skill = _skills[0];

			skill.Animate(gameTime);
		}
	}
}
