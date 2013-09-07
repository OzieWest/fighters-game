#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using TestGame.Domain;
using TestGame.Controllers;
#endregion

namespace TestGame
{
	public class GameRoot : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		public static LevelController Level { get; set; }

		public static Random RND { get; set; }

		public static Dictionary<String, Texture2D> Textures { get; set; }
		public static Dictionary<String, SpriteFont> Fonts { get; set; }
		public static TileFactory TileFactory { get; set; }
		public static TContainer TContainer { get; set; }
		
		static GameRoot()
		{
			RND = new Random();

			Textures = new Dictionary<String, Texture2D>();

			Fonts = new Dictionary<String, SpriteFont>();
			TContainer = new TContainer();
			TileFactory = new TileFactory();
			Level = new LevelController();
		}

		public GameRoot()
			: base()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			this.SetResoluton(550, 800, false);
			this.IsMouseVisible = true;

			Window.SetPosition(new Point(200, 100));

			IoC.Init(Content);
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			Level = IoC.GetSingleton<LevelController>();
			Level.Init("level1");

		}

		protected override void Update(GameTime gameTime)
		{
			Level.Update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
			//=====================

			Level.Draw(spriteBatch);

			//=====================
			spriteBatch.End();
			base.Draw(gameTime);
		}

		protected void LoadTexures(String folder, List<String> fileNames)
		{
			fileNames.ForEach(file =>
			{
				Textures.Add(file, Content.Load<Texture2D>(folder + file));
			});
		}

		#region Init
		protected override void Initialize()
		{
			var file_textures = new List<String>()
			{
				"one", "two", "three", "four", "five", "six", "seven", "eight",
				"background1", "bullet1",
				"player", "enemy",
				"cursor",
				"exp_type_a",
				"laser"
			};
			file_textures.ForEach(file =>
			{
				Textures.Add(file, Content.Load<Texture2D>("textures/" + file));
			});

			var file_fonts = new List<String>() 
			{
				"font_12", "font_14", "font_16", "font_18"
			};
			file_fonts.ForEach(file_name =>
			{
				Fonts.Add(file_name, Content.Load<SpriteFont>("fonts/" + file_name));
			});

			base.Initialize();
		}

		protected override void UnloadContent()
		{
			//
		}

		protected void SetResoluton(int w, int h, Boolean isFullScreen)
		{
			graphics.PreferredBackBufferWidth = w;
			graphics.PreferredBackBufferHeight = h;

			if (graphics.IsFullScreen != isFullScreen)
			{
				graphics.ToggleFullScreen();
			}

			graphics.ApplyChanges();
		}
		#endregion
	}
}
