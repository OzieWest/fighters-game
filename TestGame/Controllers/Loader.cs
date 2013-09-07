using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestGame;
using TestGame.Domain;

public class Loader
{
	public static Loader Instance { get; private set; }

	Dictionary<String, Texture2D> _textures;
	Dictionary<String, SpriteFont> _fonts;

	ContentManager Content { get; set; }

	static Loader()
	{
		Instance = new Loader();
	}

	private Loader() 
	{
		Content = IoC.GetSingleton<ContentManager>();

		_textures = new Dictionary<String, Texture2D>();
		_fonts = new Dictionary<String, SpriteFont>();

		_loadTexures("textures/",
			new List<String>()
			{
				"one", "two", "three", "four", "five", "six", "seven", "eight",
				"background1", "bullet1",
				"player", "enemy",
				"cursor",
				"exp_type_a"
			}
		);

		_loadFonts("fonts/",
			new List<String>()
			{
				"font_12", "font_14", "font_16", "font_18"
			}
		);
	}

	void _loadTexures(String folder, List<String> fileNames)
	{
		fileNames.ForEach(file_name =>
		{
			_textures.Add(file_name, Content.Load<Texture2D>(folder + file_name));
		});
	}

	void _loadFonts(String folder, List<String> fileNames)
	{
		fileNames.ForEach(file_name =>
		{
			_fonts.Add(file_name, Content.Load<SpriteFont>(folder + file_name));
		});
	}

	public Texture2D GetTexture(String name)
	{
		return _textures[name];
	}

	public SpriteFont GetFont(String name)
	{
		return _fonts[name];
	}
}
