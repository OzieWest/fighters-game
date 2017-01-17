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
		public static FontObject Info;
		public static ParticleManager<ParticleState> ParticleManager { get; private set; }
		
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

			Info = new FontObject(Fonts.S20);
			Info.SetPosition(180, 10);

			Inputs.Load(Textures["cursor"]);

			spriteBatch = new SpriteBatch(GraphicsDevice);

			ParticleManager = new ParticleManager<ParticleState>(1024 * 20, ParticleState.UpdateParticle);
		}

		protected override void Update(GameTime gameTime)
		{
			Level.Update(gameTime);

			if (State == GameStates.Pause)
			{
				Info.Text = "Game Pause: press A";
				Info.Color = Color.Green;
			}
			else if(State == GameStates.Stop)
			{
				MediaPlayer.Stop();
			}

			ParticleManager.Update();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.White);
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
			//=====================

			Level.Draw(spriteBatch);

			if (State != GameStates.Play)
				Info.Draw(spriteBatch);

			//=====================
			spriteBatch.End();
			base.Draw(gameTime);
		}

		public void SetSound()
		{
			Sound.Load(Content);
			MediaPlayer.IsRepeating = true;
			MediaPlayer.Volume = 0.5f;
		}

		public static void Explosive(float x, float y)
		{
			for (int i = 0; i < 120; i++)
			{
				float speed = 18f * (1f - 1 / RND.NextFloat(1f, 10f));
				var state = new ParticleState()
				{
					Velocity = RND.NextVector2(speed, speed),
					Type = ParticleType.IgnoreGravity,
					LengthMultiplier = 1
				};

				ParticleManager.CreateParticle(GameRoot.Textures["laser"], new Vector2(x, y), Color.RoyalBlue, 200, 1.5f, state);
			}
		}

		#region Init
		protected override void Initialize()
		{
			var file_textures = new List<String>()
			{
				"one", "two", "three", "four", "five", "six", "seven", "eight",
				"background1", "background_up",
				"player", "enemy",
				"cursor", "messageWindow1", "messageWindow2",
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
