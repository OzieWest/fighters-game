using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
	public class InputController
	{
		public MouseObject MouseControl { get; set; }

		KeyboardState _currentKeyboardState;
		KeyboardState _previousKeyboardState;

		MouseState _previousMouseState;

		public InputController(Texture2D texture)
		{
			MouseControl = new MouseObject(texture);
		}

		public void UpdateMouse()
		{
			MouseControl.Update();
		}

		public void DrawMouse(SpriteBatch spriteBatch)
		{
			MouseControl.Draw(spriteBatch);
		}

		public void isKeyDown(Keys key, Action del)
		{
			_currentKeyboardState = Keyboard.GetState();

			if (_currentKeyboardState.IsKeyDown(key) &&
				!_previousKeyboardState.IsKeyDown(key))
			{
				del.Invoke();
			}
		}

		public void isLeftMouseDown(Action del)
		{
			if (_previousMouseState.LeftButton == ButtonState.Released &&
					Mouse.GetState().LeftButton == ButtonState.Pressed)
			{
				del.Invoke();
			}

			_previousMouseState = Mouse.GetState();
		}

		public void End()
		{
			_previousKeyboardState = Keyboard.GetState();
		}
	}
}
