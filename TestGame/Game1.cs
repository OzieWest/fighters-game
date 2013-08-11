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
		ScoreController _scoreController;

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
			_scoreController = new ScoreController(Content.Load<SpriteFont>("font1"), 100, 200);

			_backController = new BackgroundController(Content, spriteBatch);

			_tileController = new TileController(Content, spriteBatch, _scoreController);
			_tileController.Init(8);

			_infoMessage = new FontObject(Content.Load<SpriteFont>("MainFont"));
			_infoMessage.SetPosition(5, 500);

			_cursor = new MouseObject(Content.Load<Texture2D>("cursor"));

			_inputs = new InputController();
		}

		protected override void Update(GameTime gameTime)
		{
			var wasMouseDown = false;

			//keyboard================================================
			_inputs.isKeyDown(Keys.A, delegate() 
			{
				_tileController._tiles.FirstByState(TileState.Selected).State = TileState.Normal;
			});

			//mouse===================================================
			_inputs.isLeftMouseDown(delegate() 
			{
				wasMouseDown = true;
			});

			//Update==================================================
			_cursor.Update(gameTime);

			_tileController.Update(gameTime, _cursor, wasMouseDown);

			var tile = _tileController._tiles.FirstByState(TileState.Focused);

			if (tile != null)
			{
				_infoMessage.Text(tile.X + "-" + tile.Y)
							.AddText("||" + tile.Position.X + "-" + tile.Position.Y);
			}
			else
			{
				_infoMessage.Text("");
			}

			_scoreController.Update(80, 60);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
			//=====================

			_backController.Draw();
			_tileController.Draw();
			_scoreController.Draw(spriteBatch);

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
