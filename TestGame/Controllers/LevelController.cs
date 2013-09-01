using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Content;

namespace TestGame.Controllers
{
	public class LevelController
	{
		public TileController TileController { get; set; }
		public BackgroundController BackController { get; set; }
		public InputController Inputs { get; set; }
		public BattleController BattleController { get; set; }
		public TextureController TexController { get; set; }
		public ObjectFactory Factory { get; set; }

		public void Init(String name)
		{
			//get
			TexController = IoC.GetSingleton<TextureController>();
			BattleController = IoC.GetSingleton<BattleController>();
			TileController = IoC.GetSingleton<TileController>();
			BackController = IoC.GetSingleton<BackgroundController>();
			Inputs = IoC.GetSingleton<InputController>();
			Factory = IoC.GetSingleton<ObjectFactory>();

			//init
			TexController.Init("set1/");
			Factory.Init();
			BattleController.Init();
			TileController.Init(8);
			BackController.Init();
			Inputs.Init(
				TexController.GetTexture("Cursor")
			);
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
			BackController.Draw(spriteBatch);
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
				//
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
