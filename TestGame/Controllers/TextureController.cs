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

		public int Init(String folder, ContentManager content)
		{
			Content = content;
			Folder = folder;

			var files = new List<String>()
			{
				"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine",
				"Background1", "Bullet1", 
				"Skill1", "Skill2", "Skill3", "Skill4",
				"Santa",
				"Cursor"
			};

			foreach (var str in files)
				_textures.Add(Load(str));

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
