using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame.Content
{
	public class TextureController
	{
		#region Values
		protected Dictionary<TileTypes, Texture2D> _textures;
		#endregion Values

		#region Injects
		protected ContentManager _contentManager;
		#endregion Injects

		public TextureController(ContentManager contentManager)
		{
			_contentManager = contentManager;

			_textures = new Dictionary<TileTypes, Texture2D>();

			this._init();
		}

		protected virtual void _init()
		{
			// todo: имена брать из конфига
			var folder = "set1";

			_textures[TileTypes.first] = _contentManager.Load<Texture2D>(folder + "/" + "ntile_0");
			_textures[TileTypes.second] = _contentManager.Load<Texture2D>(folder + "/" + "ntile_1");
			_textures[TileTypes.third] = _contentManager.Load<Texture2D>(folder + "/" + "ntile_2");
			_textures[TileTypes.foth] = _contentManager.Load<Texture2D>(folder + "/" + "ntile_3");
			_textures[TileTypes.fifth] = _contentManager.Load<Texture2D>(folder + "/" + "ntile_4");
			_textures[TileTypes.six] = _contentManager.Load<Texture2D>(folder + "/" + "ntile_5");
			_textures[TileTypes.seven] = _contentManager.Load<Texture2D>(folder + "/" + "ntile_6");
			_textures[TileTypes.eight] = _contentManager.Load<Texture2D>(folder + "/" + "ntile_7");
		}

		public Texture2D GetTextureByType(TileTypes type)
		{
			return _textures[type];
		}
	}
}
