using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Content;
using TestGame.Domain;

namespace TestGame
{
	public class SkillController
	{
		protected Dictionary<TileTypes, SkillObject> _skills;

		public TilePool Pool { get; set; }
		public TextureController TexController { get; set; }

		public SkillController(ContentManager content)
		{
			var startX = 145;
			var startY = 10;
			var score = 100;
			var step = 1;

			TexController = new TextureController(content);

			Pool = new TilePool(content.Load<Texture2D>("plus"), 20);

			_skills = new Dictionary<TileTypes, SkillObject>();
			
			var skill = CreateSkill(content.Load<SpriteFont>("font1"),
									score,
									step,
									TileTypes.first);

			skill.SetPosition(startX, startY + 100);
			_skills.Add(skill.Class.Type, skill);

			var skill2 = CreateSkill(content.Load<SpriteFont>("font1"),
									score,
									step,
									TileTypes.third);

			skill2.SetPosition(startX, startY + 200);
			_skills.Add(skill2.Class.Type, skill2);
		}

		public SkillObject CreateSkill(SpriteFont font, int score, int step, TileTypes type)
		{
			var texture = TexController.GetTextureByType(type);
			var skill = new SkillObject(texture, type, texture.Height, 0)
										{
											Message = new FontObject(font),
											State = TileState.Normal,
											Score = score,
											Step = step
										};

			return skill;
		}

		public void Update(GameTime gameTime, Position obj, Boolean isSelect)
		{
			Pool.Update(gameTime);

			foreach (var skill in _skills)
				skill.Value.Update(gameTime);

			_checkIntersect(gameTime, obj, isSelect);
		}

		public void Move(float x, float y, TileTypes type)
		{
			var skill = _skills.SingleOrDefault(o => o.Value.Class.Type == type);

			if (skill.Value != null)
			{
				Pool.Take(x, y, skill.Value.Position.X, skill.Value.Position.Y);
			}
		}

		protected void _checkIntersect(GameTime gameTime, Position obj, Boolean isSelect)
		{
			var selected = _skills.SingleOrDefault(o => o.Value.State == TileState.Selected);

			foreach (var skill in _skills)
			{
				if (isSelect)
				{
					if (skill.Value.IsIntersect(obj) && skill.Value.State != TileState.Selected)
					{
						if (selected.Value == null)
							skill.Value.State = TileState.Selected;
					}
					else if (skill.Value.IsIntersect(obj) && skill.Value.State == TileState.Selected)
						skill.Value.State = TileState.Focused;
				}
				else
				{
					if (skill.Value.IsIntersect(obj) && skill.Value.State != TileState.Selected)
						skill.Value.State = TileState.Focused;
					else if (!skill.Value.IsIntersect(obj) && skill.Value.State != TileState.Selected)
						skill.Value.State = TileState.Normal;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Pool.Draw(spriteBatch);

			foreach (var skill in _skills)
				skill.Value.Draw(spriteBatch);
		}
	}
}
