using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame.Controllers
{
	public class LevelController
	{
		Boolean isGameStart = false;

		public BaseObject background;
		public BaseObject background_up;

		public TileController TileController { get; set; }
		public BattleController BattleController { get; set; }

		public void Init(String name)
		{
			background = new BaseObject(GameRoot.Textures["background1"], 550);
			background_up = new BaseObject(GameRoot.Textures["background_up"], 550);

			//get
			BattleController = new BattleController();
			TileController = new TileController();
			TileController.Battle = BattleController;
			
			//init
			BattleController.Init();
			TileController.Init(8);
		}

		public void Update(GameTime gameTime)
		{
			var wasMouseDown = UpdateControl();
			var mousePosition = Inputs.MouseObject.Position;

			if (GameRoot.State == GameStates.Play)
			{
				background.Update(gameTime);
				background_up.Update(gameTime);

				TileController.Update(gameTime, mousePosition, wasMouseDown);

				BattleController.Update(gameTime);
			}

			Inputs.End();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			background.Draw(spriteBatch);

			GameRoot.ParticleManager.Draw(spriteBatch);

			TileController.Draw(spriteBatch);

			background_up.Draw(spriteBatch);
			
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
				if (GameRoot.State != GameStates.Stop)
					GameRoot.State = GameStates.Play;

				if (!isGameStart)
				{
					MediaPlayer.Play(Sound.Background);
					isGameStart = true;
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
