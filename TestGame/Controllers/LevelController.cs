using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame.Controllers
{
	public class LevelController
	{
		Random rand;

		public TileController TileController { get; set; }
		public BackgroundController BackController { get; set; }
		public InputController Inputs { get; set; }
		public BattleController BattleController { get; set; }

		public void Init(String name)
		{
			rand = new Random();

			//get
			BattleController = IoC.GetSingleton<BattleController>();
			TileController = IoC.GetSingleton<TileController>();
			BackController = IoC.GetSingleton<BackgroundController>();
			Inputs = IoC.GetSingleton<InputController>();

			//init
			BattleController.Init();
			TileController.Init(8);
			BackController.Init();
			Inputs.Init();
		} 

		public void Update(GameTime gameTime)
		{
			var wasMouseDown = UpdateControl();
			var mousePosition = Inputs.MouseControl.Position;

			BackController.Update(gameTime);
			TileController.Update(gameTime, mousePosition, wasMouseDown);
			BattleController.Update(gameTime);

			Inputs.End();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			//BackController.Draw(spriteBatch);
			TileController.Draw(spriteBatch);
			BattleController.Draw(spriteBatch);

			Inputs.DrawMouse(spriteBatch);
		}

		protected Boolean UpdateControl()
		{
			Inputs.UpdateMouse();

			var wasMouseDown = false;

			//mouse===================================================
			Inputs.isLeftMouseDown(delegate()
			{
				wasMouseDown = true;
			});

			//keyboard================================================
			Inputs.isKeyDown(Keys.A, delegate()
			{
				var texture = Loader.Instance.GetTexture("laser");

				float hue1 = rand.NextFloat(0, 6);
				float hue2 = (hue1 + rand.NextFloat(0, 2)) % 6f;
				Color color1 = ColorUtil.HSVToColor(hue1, 0.5f, 1);
				Color color2 = ColorUtil.HSVToColor(hue2, 0.5f, 1);

				for (int i = 0; i < 120; i++)
				{
					float speed = 18f * (1f - 1 / rand.NextFloat(1f, 10f));
					var state = new ParticleState()
					{
						Velocity = rand.NextVector2(speed, speed),
						Type = ParticleType.Bullet,
						LengthMultiplier = 1f
					};

					Color color = Color.Lerp(color1, color2, rand.NextFloat(0, 1));
					Game1.ParticleManager.CreateParticle(texture, new Vector2(100, 100), color, 100, 0.7f, state);
				}
			});

			//keyboard================================================
			Inputs.isKeyDown(Keys.S, delegate()
			{
				//
			});

			return wasMouseDown;
		}
	}
}
