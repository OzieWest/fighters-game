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
		protected List<SkillObject> _skills;

		public TilePool Pool { get; set; }
		public TextureController TexController { get; set; }

		public SkillController(ContentManager content)
		{
			var startX = 145;
			var startY = 10;
			var score = 100;

			//todo: переделать
			TexController = new TextureController(content);

			Pool = new TilePool(content.Load<Texture2D>("plus"), 20);

			_skills = new List<SkillObject>();

			var skill = CreateSkill(content.Load<SpriteFont>("font1"),
									score,
									TileTypes.first);

			skill.SetPosition(startX, startY + 100);
			_skills.Add(skill);

			var skill2 = CreateSkill(content.Load<SpriteFont>("font1"),
									score,
									TileTypes.third);

			skill2.SetPosition(startX, startY + 200);
			_skills.Add(skill2);
		}

		public SkillObject CreateSkill(SpriteFont font, int score, TileTypes type)
		{
			var texture = TexController.GetTextureByType(type);

			return new SkillObject(texture, type, texture.Height, 0)
						{
							Message = new FontObject(font),
							State = TileState.Normal,
							Score = score
						};

		}

		public void Update(GameTime gameTime, Position obj, Boolean isSelect)
		{
			Pool.Update(gameTime);

			foreach (var skill in _skills)
				skill.Update(gameTime);

			_checkIntersect(gameTime, obj, isSelect);
		}

		public void Move(float x, float y, TileTypes type)
		{
			var skill = _skills.SingleOrDefault(o => o.Type == type);

			if (skill != null)
				Pool.LaunchTile(x, y, skill.Position.X, skill.Position.Y);
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
							skill.State = TileState.Selected;
					}
					else if (skill.IsIntersect(obj) && skill.State == TileState.Selected)
						skill.State = TileState.Focused;
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
			Pool.Draw(spriteBatch);

			foreach (var skill in _skills)
				skill.Draw(spriteBatch);
		}
	}
}
