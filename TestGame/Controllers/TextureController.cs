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

		public TextureController()
		{
			_textures = new Dictionary<String, Texture2D>();
		}

		public int Init(String folder, ContentManager content)
		{
			var jpgs = Directory.GetFiles(folder, "*.jpg");
			var pngs = Directory.GetFiles(folder, "*.png");

			if (jpgs != null)
			{
				foreach (var file in jpgs)
				{
					var name = Path.GetFileNameWithoutExtension(file);
					var texture = content.Load<Texture2D>(file);

					_textures.Add(name, texture);
				}
			}

			if (pngs != null)
			{
				foreach (var file in pngs)
				{
					var name = Path.GetFileNameWithoutExtension(file);
					var texture = content.Load<Texture2D>("set1/" + name);

					_textures.Add(name, texture);
				}
			}

			return _textures.Count;
		}

		public Texture2D GetTexture(String name)
		{
			if (_textures.ContainsKey(name))
				return _textures[name];

			return null;
		}
	}
}
