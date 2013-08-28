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
	public class Game1 : Game
	{
		#region Inject
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		//BackgroundController _backController;
		//TileController _tileController;
		//SkillController _skillController;

		LevelController _level;

		FontObject _infoMessage;
		MouseObject _cursor;
		InputController _inputs;

		#endregion

		public Game1()
			: base()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			this.SetResoluton(800, 550, false);
			this.IsMouseVisible = false;
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			_level = new LevelController();
			_level.CreateLevel("level1", Content);

			_infoMessage = new FontObject(Get<SpriteFont>("MainFont"));
			_infoMessage.SetPosition(5, 500);

			_cursor = new MouseObject(Get<Texture2D>("cursor"));

			_inputs = new InputController();
		}

		protected override void Update(GameTime gameTime)
		{
			var wasMouseDown = false;

			//mouse===================================================
			_inputs.isLeftMouseDown(delegate()
			{
				wasMouseDown = true;
			});

			//keyboard================================================
			_inputs.isKeyDown(Keys.A, delegate()
			{
				//
			});

			//keyboard================================================
			_inputs.isKeyDown(Keys.S, delegate()
			{
				//
			});

			//Update==================================================
			_cursor.Update(gameTime);

			_level.Update(gameTime, _cursor.Position, wasMouseDown);
			
			//движение по кругу
			//alpha += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
			//var	x = 55 + 3 * Math.Sin(alpha);
			//var y = 50 + 3 * Math.Cos(alpha);

			_inputs.End();
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
			//=====================

			_level.Draw(spriteBatch);

			_infoMessage.Draw(spriteBatch);
			_cursor.Draw(spriteBatch);

			//=====================
			spriteBatch.End();
			base.Draw(gameTime);
		}

		public T Get<T>(String path)
		{
			return Content.Load<T>(path);
		}

		#region Init
		protected override void Initialize()
		{
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
