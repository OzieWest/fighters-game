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

		TileObject _background;

		TileController _tileController;

		FontObject _infoMessage;
		CursorObject _cursor;
		#endregion

		public Game1()
			: base()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			this.SetResoluton(800, 600, false);
			this.IsMouseVisible = false;
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			_infoMessage = new FontObject(spriteBatch, Content, "MainFont");
			_cursor = new CursorObject(spriteBatch, Content, "cursor");
			_background = new TileObject(spriteBatch, Content, TileTypes.def, "back_2");

			_tileController = new TileController(spriteBatch, Content);
			_tileController.CreateGrid(8);
		}

		protected override void Update(GameTime gameTime)
		{
			_cursor.Update();

			_tileController.IsIntersect(_cursor);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
			//=====================
			_background.Draw();

			_tileController.Draw();

			_infoMessage.Draw("output: ", new Vector2(5, 5));
			_cursor.Draw();
			//=====================
			spriteBatch.End();
			base.Draw(gameTime);
		}

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
	}
}
