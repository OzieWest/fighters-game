using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
	public static class Inputs
	{
		public static MouseObject MouseObject { get; set; }

		public static KeyboardState CurrentKeyboardState;
		public static KeyboardState PreviousKeyboardState;

		public static MouseState PreviousMouseState;

		public static void Load(Texture2D texture)
		{
			MouseObject = new MouseObject(texture, 10);
		}

		public static void UpdateMouse()
		{
			MouseObject.Update();
		}

		public static void DrawMouse(SpriteBatch spriteBatch)
		{
			MouseObject.Draw(spriteBatch);
		}

		public static void isKeyDown(Keys key, Action del)
		{
			CurrentKeyboardState = Keyboard.GetState();

			if (CurrentKeyboardState.IsKeyDown(key) &&
				!PreviousKeyboardState.IsKeyDown(key))
			{
				del.Invoke();
			}
		}

		public static void isLeftMouseDown(Action del)
		{
			if (PreviousMouseState.LeftButton == ButtonState.Released &&
					Mouse.GetState().LeftButton == ButtonState.Pressed)
			{
				del.Invoke();
			}

			PreviousMouseState = Mouse.GetState();
		}

		public static void End()
		{
			PreviousKeyboardState = Keyboard.GetState();
		}
	}
}
