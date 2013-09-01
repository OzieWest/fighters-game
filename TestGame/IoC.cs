﻿using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Content;
using TestGame.Controllers;

namespace TestGame
{
	public static class IoC
	{
		static Dictionary<Type, Object> _container;

		public static void Init(ContentManager content)
		{
			_container = new Dictionary<Type, Object>();

			Register<ContentManager>(
				content
			);
			Register<TileController>(
				new TileController()
			);
			Register<BattleController>(
				new BattleController()
			);
			Register<InputController>(
				new InputController()
			);
			Register<ObjectFactory>(
				new ObjectFactory()
			);
			Register<TilePool>(
				new TilePool()
			);
			Register<TextureController>(
				new TextureController()
			);
			Register<LevelController>(
				new LevelController()
			);
			Register<PlaceController>(
				new PlaceController()
			);
			Register<GridController>(
				new GridController()
			);
			Register<TileContainer>(
				new TileContainer()
			);
			Register<BackgroundController>(
				new BackgroundController()
			);
		}

		public static T GetAsNew<T>() where T: new()
		{
			return new T();
		}

		public static T GetSingleton<T>() where T : class
		{
			return (T)_container[typeof(T)];
		}

		public static void Register<T>(T obj)
		{
			_container.Add(typeof(T), obj);
		}
	}
}
