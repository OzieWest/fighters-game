using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame.Content
{
	public class TextureController
	{
		protected Dictionary<String, Texture2D> _textures;
		protected ContentManager Content { get; set; }
		public String Folder { get; set; }

		public TextureController()
		{
			_textures = new Dictionary<String, Texture2D>();
		}

		public int Init(String folder)
		{
			Content = IoC.GetSingleton<ContentManager>();

			Folder = folder;

			var files = new List<String>()
			{
				"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine",
				"Background1", "Bullet1", "Bullet2", 
				"Skill1", "Skill2", "Skill3",
				"Player", "Enemy",
				"Cursor"
			};

			foreach (var file in files)
				_textures.Add(Load(file));

			return _textures.Count;
		}

		public KeyValuePair<String, Texture2D> Load(String filename)
		{
			return new KeyValuePair<String, Texture2D>(
										filename,
										Content.Load<Texture2D>(Folder + filename.ToLower())
									);
		}

		public Texture2D GetTexture(String name)
		{
			if (_textures.ContainsKey(name))
				return _textures[name];

			throw new Exception(String.Format("Texture {0} not found in Dictionary", name));
		}
	}
}
