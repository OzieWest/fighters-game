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
		public SparkPool skills { get; set; }

		protected Random _rnd;

		public BattleController()
		{
			_rnd = new Random();
		}

		public void Init()
		{
			bullets = IoC.GetAsNew<TilePool>();

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
				6
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
		}

		public void Strike(int x, int y, TileTypes type)
		{
			switch (type)
			{
				case TileTypes.One:
					Enemy.Health.Minus(Player.Power.Value);
					break;
				case TileTypes.Two:
					Player.Health.Plus(1);
					break;
				case TileTypes.Three:
					Player.Power.Plus(1);
					break;
				case TileTypes.Four:
					Player.Gold.Plus(1);
					break;
				case TileTypes.Five:
					Player.Power.Minus(1);
					break;
				case TileTypes.Six:
					Player.Health.Minus(Enemy.Power.Value);
					break;
				case TileTypes.Seven:
					Player.Health.Minus(10);
					Enemy.Health.Minus(10);
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
		}

		protected void Shoot(WarriorObject one, WarriorObject two)
		{
			bullets.LaunchTile(
				one.X, one.Y,
				two.X, two.Y
			);
			two.Health.Minus(one.Power.Value);
		}
	}
}
