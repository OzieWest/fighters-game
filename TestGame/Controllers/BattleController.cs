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
	public class BattleController
	{
		public WarriorObject Player { get; set; }
		public WarriorObject Enemy { get; set; }

		public void Init()
		{
			var health = 100;
			var alg = 3;
			var font = Fonts.S12;

			Player = new WarriorObject(
				GameRoot.Textures["player"],
				GameRoot.Textures["messageWindow1"],
				font,
				health,
				180
			);
			Player.SetPosition(80, 90);
			Player.SetPositionMessage(0, 90, Color.White);
			Player.Power.Value = 1;

			Enemy = new WarriorObject(
				GameRoot.Textures["enemy"],
				GameRoot.Textures["messageWindow2"],
				font,
				(health * alg),
				180
			);
			Enemy.SetPosition(250, 90);
			Enemy.SetPositionMessage(415, 90, Color.Black);
			Enemy.Power.Value = 2;
		}

		public void Update(GameTime gameTime)
		{
			var enemyAttackChance = GameRoot.RND.Next(0, 200);

			if (enemyAttackChance == 1)
				Shoot(Enemy, Player);

			Player.Update(gameTime);
			Enemy.Update(gameTime);

			if (Player.Health.Value == 0)
			{
				GameRoot.State = GameStates.Stop;
				GameRoot.Info.Color = Color.Red;
				GameRoot.Info.Text = "Enemy WIN!";
			}
			else if (Enemy.Health.Value == 0)
			{
				GameRoot.State = GameStates.Stop;
				GameRoot.Info.Color = Color.Green;
				GameRoot.Info.Text = "Player WIN!";
			}
		}

		public void Strike(float x, float y, TileTypes type)
		{
			if (GameRoot.State != GameStates.Stop)
			{
				switch (type)
				{
					case TileTypes.one:
						Shoot(Player, Enemy);
						Enemy.Health.Minus(Player.Power.Value);
						break;
					case TileTypes.two:
						Player.Health.Plus(4);
						break;
					case TileTypes.three:
						Player.Power.Plus(1);
						break;
					case TileTypes.four:
						Player.Gold.Plus(10);
						break;
					case TileTypes.five:
						Player.Power.Minus(1);
						break;
					case TileTypes.six:
						Player.Health.Minus(Enemy.Power.Value);
						break;
					case TileTypes.seven:
						Player.Health.Minus(4);
						Enemy.Health.Minus(4);
						break;
					default:
						break;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Player.Draw(spriteBatch);
			Enemy.Draw(spriteBatch);
		}

		protected void Shoot(WarriorObject one, WarriorObject two)
		{
			one.Action = WarriorActions.Strike;
			two.Health.Minus(one.Power.Value);
		}
	}
}
