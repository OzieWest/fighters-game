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
			var alg = 3;

			Player = new WarriorObject(
				Loader.GetTexture("Player"),
				Loader.GetFont("font1"),
				score
			);
			Player.SetPosition(10, 10);

			Enemy = new WarriorObject(
				Loader.GetTexture("Enemy"),
				Loader.GetFont("font1"),
				(score * alg)
			);
			Enemy.SetPosition(400, 10);

			bullets.Init(
				Loader.GetTexture("Bullet2"),
				20
			);
			heals.Init(
				Loader.GetTexture("Bullet1"),
				20
			);
		}

		public void Update(GameTime gameTime)
		{
			var _timer = _rnd.Next(0, 200);

			if (_timer == 1)
				Shoot(Player, Enemy);
			else if (_timer == 199)
				Shoot(Enemy, Player);

			Player.Update(gameTime);
			Enemy.Update(gameTime);
			bullets.Update(gameTime);
			heals.Update(gameTime);
		}

		public void Strike(int x, int y, TileTypes type)
		{
			switch (type)
			{
				case TileTypes.One:
					Enemy.Health.Down(1);
					break;
				case TileTypes.Two:
					Player.Health.Up(1);
					break;
				case TileTypes.Three:
					Player.Power.Up(1);
					break;
				case TileTypes.Four:
					Player.Gold.Up(1);
					break;
				case TileTypes.Five:
					Player.Power.Down(1);
					break;
				case TileTypes.Six:
					Player.Health.Down(1);
					break;
				case TileTypes.Seven:
					Player.Health.Down(2);
					Enemy.Health.Down(2);
					break;
				default:
					break;
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			bullets.Draw(spriteBatch);
			Player.Draw(spriteBatch);
			Enemy.Draw(spriteBatch);
			heals.Draw(spriteBatch);
		}

		protected void Shoot(WarriorObject one, WarriorObject two)
		{
			bullets.LaunchTile(
				one.X, one.Y,
				two.X, two.Y
			);
			two.Health.Down(one.Power.Value);
		}
	}
}
