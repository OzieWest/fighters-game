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

		public void Init()
		{
			var score = 100;
			var alg = 3;
			var font = GameRoot.Fonts["font_12"];

			Player = new WarriorObject(
				GameRoot.Textures["player"],
				font,
				score,
				180
			);
			Player.SetPosition(10, 10);

			Enemy = new WarriorObject(
				GameRoot.Textures["enemy"],
				font,
				(score * alg),
				180
			);
			Enemy.SetPosition(350, 10);

			bullets = IoC.GetAsNew<TilePool>();
			bullets.Init(
				GameRoot.Textures["bullet1"],
				6
			);

			skills = IoC.GetAsNew<SparkPool>();
			skills.Init(
				GameRoot.Textures["exp_type_a"],
				10, 80
			);

		}

		public void Update(GameTime gameTime)
		{
			var attackQuery = GameRoot.RND.Next(0, 500);

			if (attackQuery == 1)
				Shoot(Enemy, Player);

			Player.Update(gameTime);
			Enemy.Update(gameTime);
			bullets.Update(gameTime);
			skills.Update(gameTime);
		}

		public void Strike(int x, int y, TileTypes type)
		{
			switch (type)
			{
				case TileTypes.one:
					Shoot(Player, Enemy);
					Enemy.Health.Minus(Player.Power.Value);
					skills.Start(Enemy.Position.X, Enemy.Position.Y, Player.Power.Value);
					break;
				case TileTypes.two:
					Player.Health.Plus(1);
					break;
				case TileTypes.three:
					Player.Power.Plus(1);
					break;
				case TileTypes.four:
					Player.Gold.Plus(1);
					break;
				case TileTypes.five:
					Player.Power.Minus(1);
					break;
				case TileTypes.six:
					Player.Health.Minus(Enemy.Power.Value);
					skills.Start(Player.Position.X, Player.Position.Y, Enemy.Power.Value);
					break;
				case TileTypes.seven:
					Player.Health.Minus(5);
					Enemy.Health.Minus(5);
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
			skills.Draw(spriteBatch);
		}

		protected void Shoot(WarriorObject one, WarriorObject two)
		{
			one.Action = WarriorActions.Strike;
			two.Health.Minus(one.Power.Value);

			bullets.LaunchTile(
				one.Position.X + 10, one.Position.Y + 60,
				two.Position.X + 10, two.Position.Y + 60
			);
		}
	}
}
