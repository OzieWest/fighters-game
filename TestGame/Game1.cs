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

		BackgroundController _backController;
		TileController _tileController;

		FontObject _infoMessage;
		MouseObject _cursor;

		MouseState previousMouseState;
		InputController _inputs;
		#endregion

		public Game1()
			: base()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			previousMouseState = Mouse.GetState();

			this.SetResoluton(800, 600, false);
			this.IsMouseVisible = false;
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			_backController = new BackgroundController(Content, spriteBatch);

			_tileController = new TileController(Content, spriteBatch);
			_tileController.Init(8);

			_infoMessage = new FontObject(Content.Load<SpriteFont>("MainFont"));
			_infoMessage.SetPosition(5, 5);

			_cursor = new MouseObject(Content.Load<Texture2D>("cursor"));

			_inputs = new InputController();
		}

		protected override void Update(GameTime gameTime)
		{
			_cursor.Update(gameTime);

			_inputs.Start(Keys.A, delegate() 
			{

			});

			if (previousMouseState.LeftButton == ButtonState.Released
			&& Mouse.GetState().LeftButton == ButtonState.Pressed)
			{
				_tileController.Update(gameTime, _cursor, true);
			}
			else
			{
				_tileController.Update(gameTime, _cursor, false);
			}

			_infoMessage.Text = TileObject.Count.ToString();

			_infoMessage.Update(gameTime);

			previousMouseState = Mouse.GetState();
			_inputs.End();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
			//=====================

			_backController.Draw();
			_tileController.Draw();

			_infoMessage.Draw(spriteBatch);
			_cursor.Draw(spriteBatch);

			//=====================
			spriteBatch.End();
			base.Draw(gameTime);
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
