using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestGame;
using TestGame.Domain;

public static class Loader
{
	public static Dictionary<String, Texture2D> _textures;
	public static Dictionary<String, SpriteFont> _fonts;

	public static ContentManager Content { get; set; }
	public static String Folder { get; set; }

	static Loader()
	{
		Content = IoC.GetSingleton<ContentManager>();

		_textures = new Dictionary<String, Texture2D>();
		_fonts = new Dictionary<String, SpriteFont>();

		Folder = "set1/";

		var files = new List<String>()
			{
				"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight",
				"Background1", "Bullet1", "Bullet2", 
				"Skill1", "Skill2", "Skill3",
				"Player", "Enemy",
				"Cursor"
			};

		foreach (var file in files)
			_textures.Add(Load(file));

		_fonts.Add("font1", Content.Load<SpriteFont>("font1"));
	}

	public static KeyValuePair<String, Texture2D> Load(String filename)
	{
		return new KeyValuePair<String, Texture2D>(
									filename,
									Content.Load<Texture2D>(Folder + filename.ToLower())
								);
	}

	public static Texture2D GetTexture(String name)
	{
		if (_textures.ContainsKey(name))
			return _textures[name];

		throw new Exception(String.Format("Texture {0} not found in Dictionary", name));
	}

	public static SpriteFont GetFont(String name)
	{
		if (_fonts.ContainsKey(name))
			return _fonts[name];

		throw new Exception(String.Format("FONT {0} not found in Dictionary", name));
	}
}
