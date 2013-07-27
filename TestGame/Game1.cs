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
#endregion

namespace TestGame
{
	public class Game1 : Game
	{
		#region Inject
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		FontObject _mainFont;
		CursorObject _cursor;

		TileObject _tile;

		List<TileObject> _tiles;
		#endregion

		public Game1()
			: base()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			_tiles = new List<TileObject>();

			//this.SetResoluton(1200, 600, false);
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			_mainFont = new FontObject(spriteBatch, Content, "MainFont");
			_cursor = new CursorObject(spriteBatch, Content, "cursor");

			_tile = new TileObject(_tiles, spriteBatch, Content, "tile_0");

			_tile.SetColors(Color.White, Color.Gray);
		}

		protected override void Update(GameTime gameTime)
		{
			_cursor.Update();
			_tile.Update(100, 100);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin();
			//=====================

			if (_cursor.Intersects(_tile))
			{
				_tile.ColorSelected();
			}
			else
			{
				_tile.ColorDefault();
			}

			_tile.Draw();

			_mainFont.Draw("action: ", new Vector2(10, 10));
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
