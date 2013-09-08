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
using Microsoft.Xna.Framework.Media;
#endregion

namespace TestGame
{
	public class GameRoot : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		public LevelController Level { get; set; }

		public static Random RND { get; set; }

		public static Dictionary<String, Texture2D> Textures { get; set; }
		public static TileFactory TileFactory { get; set; }
		public static TContainer TContainer { get; set; }
		public static GameStates State { get; set; }
		public static FontObject message;
		
		static GameRoot()
		{
			RND = new Random();

			Textures = new Dictionary<String, Texture2D>();
				
			TContainer = new TContainer();
			TileFactory = new TileFactory();

			State = GameStates.Pause;
		}

		public GameRoot()
			: base()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			this.SetResoluton(550, 800, false);
			this.IsMouseVisible = true;

			Window.SetPosition(new Point(200, 100));
		}

		protected override void LoadContent()
		{

			Fonts.Load(Content);

			SetSound();

			Level = new LevelController();
			Level.Init("level1");

			message = new FontObject(Fonts.S20);
			message.SetPosition(210, 10);

			Inputs.Load(Textures["cursor"]);

			spriteBatch = new SpriteBatch(GraphicsDevice);
		}

		protected override void Update(GameTime gameTime)
		{
			Level.Update(gameTime);

			if (State == GameStates.Pause)
			{
				message.Text = "Game Pause";
				message.Color = Color.Green;
			}
			else if(State == GameStates.Stop)
			{
				message.Text = "Game Over";
				message.Color = Color.Red;
				MediaPlayer.Stop();
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.White);
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
			//=====================

			Level.Draw(spriteBatch);

			if (State != GameStates.Play)
				message.Draw(spriteBatch);

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

		public void SetSound()
		{
			Sound.Load(Content);
			MediaPlayer.IsRepeating = true;
			MediaPlayer.Volume = 0.1f;
		}

		#region Init
		protected override void Initialize()
		{
			var file_textures = new List<String>()
			{
				"one", "two", "three", "four", "five", "six", "seven", "eight",
				"background1", "background_up", "bullet1",
				"player", "enemy",
				"cursor",
				"exp_type_a",
				"laser"
			};
			file_textures.ForEach(file =>
			{
				Textures.Add(file, Content.Load<Texture2D>("textures/" + file));
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
