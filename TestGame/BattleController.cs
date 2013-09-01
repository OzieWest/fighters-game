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
	public class BattleController : IObject
	{
		public WarriorObject Player { get; set; }
		public WarriorObject Enemy { get; set; }
		public TilePool bullets { get; set; }
		public TilePool heals { get; set; }

		protected Random _rnd;

		public BattleController()
		{
			_rnd = new Random();
		}

		public void Init()
		{
			bullets = IoC.GetAsNew<TilePool>();
			heals = IoC.GetAsNew<TilePool>();

			var score = 100;

			Player = new WarriorObject(
				Loader.GetTexture("Player"),
				Loader.GetFont("font1"),
				score
			);
			Player.SetPosition(10, 10, 30, 100);

			Enemy = new WarriorObject(
				Loader.GetTexture("Enemy"),
				Loader.GetFont("font1"),
				score
			);
			Enemy.SetPosition(400, 10, 30, 100);

			bullets.Init(Loader.GetTexture("Bullet2"), 20);
			heals.Init(Loader.GetTexture("Bullet1"), 20);

			GenerateSkills();
		}

		protected void GenerateSkills()
		{
			Player.AddSkill(
				new ComplexObject(
					Loader.GetTexture("Skill1"),
					Loader.GetFont("font1"),
					TileTypes.One,
					100
				)
			);

			Player.AddSkill(
				new ComplexObject(
					Loader.GetTexture("Skill2"),
					Loader.GetFont("font1"),
					TileTypes.Two,
					100
				)
			);

			Enemy.AddSkill(
				new ComplexObject(
					Loader.GetTexture("Skill3"),
					Loader.GetFont("font1"),
					TileTypes.Three,
					100
				)
			);
		}

		public void Update(GameTime gameTime)
		{
			var _timer = _rnd.Next(0, 200);

			if (_timer == 1)
				Shoot(Player, Enemy);
			if (_timer == 199)
				Shoot(Enemy, Player);

			Player.Update(gameTime);
			Enemy.Update(gameTime);
			bullets.Update(gameTime);
			heals.Update(gameTime);
		}

		public void Strike(int x, int y, TileTypes type)
		{
			var skill = Player.GetByType(type);
			if (skill != null)
			{
				heals.LaunchTile(x, y, skill.centerX(), skill.centerY());
				skill.ScoreDown(1);
			}

			var skill2 = Enemy.GetByType(type);
			if (skill2 != null)
			{
				heals.LaunchTile(x, y, skill2.centerX(), skill2.centerY());
				skill2.ScoreDown(1);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			bullets.Draw(spriteBatch);
			Player.Draw(spriteBatch);
			Enemy.Draw(spriteBatch);
			heals.Draw(spriteBatch);
		}

		protected void Shoot(ComplexObject one, ComplexObject two)
		{
			bullets.LaunchTile(one.centerX(), one.centerY(), two.centerX(), two.centerY());
			two.ScoreDown(1);
		}
	}
}
