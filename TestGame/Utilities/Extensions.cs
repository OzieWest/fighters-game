using Microsoft.Xna.Framework;
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

		public static float ToAngle(this Vector2 vector)
		{
			return (float)Math.Atan2(vector.Y, vector.X);
		}

		public static Vector2 ScaleTo(this Vector2 vector, float length)
		{
			return vector * (length / vector.Length());
		}

		public static Point ToPoint(this Vector2 vector)
		{
			return new Point((int)vector.X, (int)vector.Y);
		}

		public static float NextFloat(this Random rand, float minValue, float maxValue)
		{
			return (float)rand.NextDouble() * (maxValue - minValue) + minValue;
		}

		public static Vector2 NextVector2(this Random rand, float minLength, float maxLength)
		{
			double theta = rand.NextDouble() * 2 * Math.PI;
			float length = rand.NextFloat(minLength, maxLength);
			return new Vector2(length * (float)Math.Cos(theta), length * (float)Math.Sin(theta));
		}
	}

	public static class GameWindowExtensions
	{
		public static void SetPosition(this GameWindow window, Point position)
		{
			OpenTK.GameWindow OTKWindow = GetForm(window);
			if (OTKWindow != null)
			{
				OTKWindow.X = position.X;
				OTKWindow.Y = position.Y;
			}
		}

		public static OpenTK.GameWindow GetForm(this GameWindow gameWindow)
		{
			Type type = typeof(OpenTKGameWindow);
			System.Reflection.FieldInfo field = type.GetField("window", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			if (field != null)
				return field.GetValue(gameWindow) as OpenTK.GameWindow;
			return null;
		}
	}
}
