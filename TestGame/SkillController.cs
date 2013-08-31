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
		protected SkillObject _mainSkill;

		public TilePool Pool { get; set; }
		public ObjectFactory Factory { get; set; }
		public SpriteFont Font { get; set; }

		public int StartX;
		public int StartY;
		public int Score;

		public SkillController()
		{
			Pool = new TilePool();

			StartX = 145;
			StartY = 10;
			Score = 10;
		}

		public void Init(ObjectFactory factory, ContentManager content)
		{
			Factory = factory;
			Pool.Init(Factory.CreateTexture("Bullet1"), 20);
			Font = content.Load<SpriteFont>("font1");

			CreateMainSkill();

			_skills = new List<SkillObject>()
			{
				CreateSkill(Score, "Skill1", TileTypes.One),
				CreateSkill(Score, "Skill2", TileTypes.Two),
				CreateSkill(Score, "Skill3", TileTypes.Three)
			};

			foreach (var skill in _skills)
			{
				skill.SetPosition(StartX, StartY);
				StartY += 100;
			}
		}

		protected void CreateMainSkill()
		{
			_mainSkill = CreateSkill(Score * 5, "Santa", TileTypes.Default);
			_mainSkill.MessageOffsetX = 20;
			_mainSkill.MessageOffsetY = 150;
			_mainSkill.SetPosition(10, 10);
		}

		protected SkillObject CreateSkill(int score, String name, TileTypes type)
		{
			var texture = Factory.CreateTexture(name);

			return new SkillObject(texture, type, texture.Height, 0)
						{
							Message = new FontObject(Font),
							State = TileState.Normal,
							Score = score,
							MessageOffsetX = 10,
							MessageOffsetY = 60
						};

		}

		public void Update(GameTime gameTime, Position obj, Boolean isSelect)
		{
			Pool.Update(gameTime);

			foreach (var skill in _skills)
				skill.Update(gameTime);

			_mainSkill.Update(gameTime);

			_checkIntersect(gameTime, obj, isSelect);
		}

		public void Move(float x, float y, TileTypes type)
		{
			var skill = _skills.SingleOrDefault(o => o.Type == type);

			if (skill != null)
			{
				Pool.LaunchTile(x, y, skill.Position.X, skill.Position.Y);
				skill.Down(1);
			}
			else
			{
				Pool.LaunchTile(x, y, _mainSkill.Position.X, _mainSkill.Position.Y);
				_mainSkill.Down(1);
			}
		}

		protected void _checkIntersect(GameTime gameTime, Position obj, Boolean isSelect)
		{
			var selected = _skills.SingleOrDefault(o => o.State == TileState.Selected);

			//todo: если TEST то даем возможность включить скил, после чего его обнуляем
			foreach (var skill in _skills)
			{
				if (isSelect)
				{
					if (skill.IsIntersect(obj) && skill.State != TileState.Selected)
					{
						if (selected == null)
							skill.State = TileState.Selected;
					}
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

			_mainSkill.Draw(spriteBatch);

			foreach (var skill in _skills)
				skill.Draw(spriteBatch);
		}
	}
}
