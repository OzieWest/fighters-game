using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
	public static class Extensions
	{
		public static void Add<T, U>(this Dictionary<T, U> dict, KeyValuePair<T, U> pare)
		{
			var key = pare.Key;
			var value = pare.Value;

			dict.Add(key, value);
		}
	}
}
