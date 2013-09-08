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
			var score = 100;
			var alg = 3;
			var font = Fonts.S14;

			Player = new WarriorObject(
				GameRoot.Textures["player"],
				font,
				score,
				180
			);
			Player.SetPosition(80, 90);

			Enemy = new WarriorObject(
				GameRoot.Textures["enemy"],
				font,
				(score * alg),
				180
			);
			Enemy.SetPosition(250, 90);
			Enemy.Power.Value = 3;
		}

		public void Update(GameTime gameTime)
		{
			var attackQuery = GameRoot.RND.Next(0, 200);

			if (attackQuery == 1)
				Shoot(Enemy, Player);

			Player.Update(gameTime);
			Enemy.Update(gameTime);

			if (Player.Health.Value == 0)
				GameRoot.State = GameStates.Stop;
			else if (Enemy.Health.Value == 0)
				GameRoot.State = GameStates.Stop;
		}

		public void Strike(int x, int y, TileTypes type)
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
