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
		public ObjectFactory Factory { get; set; }
		public SkillController Skills { get; set; }
		public InputController Inputs { get; set; }

		public LevelController()
		{
			//todo: создать объект Level, в котором все конфиги для контроллеров
			TileController = new TileController();
			BackController = new BackgroundController();
			Factory = new ObjectFactory();
			Skills = new SkillController();
			Inputs = new InputController();
		}

		public void CreateLevel(String name, ContentManager content)
		{
			Factory.Init("set1/", content);
			Skills.Init(Factory, content);
			TileController.Init(8, Factory, Skills);
			BackController.Init(Factory);
			
			Inputs.Init(
				Factory.CreateTexture("Cursor")
			);
		} 

		public void Update(GameTime gameTime)
		{
			var wasMouseDown = UpdateControl();
			var mousePosition = Inputs.MouseControl.Position;

			BackController.Update(gameTime);
			TileController.Update(gameTime, mousePosition, wasMouseDown);
			Skills.Update(gameTime, mousePosition, wasMouseDown);

			Inputs.End();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			BackController.Draw(spriteBatch);
			TileController.Draw(spriteBatch);
			Skills.Draw(spriteBatch);
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
