using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			Register<LevelController>(
				new LevelController()
			);
			Register<PlaceController>(
				new PlaceController()
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
