using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Content;

namespace TestGame.Controllers
{
	public class LevelController
	{
		public TextureController TexController { get; set; }
		public TileController TileController { get; set; }
		public BackgroundController BackController { get; set; }
		public TileFactory Factory { get; set; }
		public SkillController Skills { get; set; }

		public LevelController()
		{
			//todo: создать объект Level, в котором все конфиги для контроллеров

			TexController = new TextureController();
			TileController = new TileController();
			BackController = new BackgroundController();
			Factory = new TileFactory();
			Skills = new SkillController();
		}

		public void CreateLevel(String name, ContentManager content)
		{
			TexController.Init(@"../Debug/Content/", content);

			Factory.Init(10, TexController);

			Skills.Init(TexController, content);

			TileController.Factory = Factory;
			TileController.Skills = Skills;
			TileController.Init(8);

			BackController.Init(TexController.GetTexture("background"));
		}

		public void Update(GameTime gameTime, Position obj, Boolean isSelect)
		{
			BackController.Update(gameTime);
			TileController.Update(gameTime, obj, isSelect);
			Skills.Update(gameTime, obj, isSelect);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			BackController.Draw(spriteBatch);
			TileController.Draw(spriteBatch);
			Skills.Draw(spriteBatch);
		}
	}
}
