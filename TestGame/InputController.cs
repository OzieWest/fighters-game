using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
	public class InputController
	{
		protected KeyboardState _currentState;
		protected KeyboardState _previousState;

		protected MouseState _currentMouseState;
		protected MouseState _previousMouseState;

		public void isKeyDown(Keys key, Action del)
		{
			_currentState = Keyboard.GetState();

			if (_currentState.IsKeyDown(key) && !_previousState.IsKeyDown(key))
			{
				del.Invoke();
			}
		}

		public void isLeftMouseDown(Action del)
		{
			if (_previousMouseState.LeftButton == ButtonState.Released
			&& Mouse.GetState().LeftButton == ButtonState.Pressed)
			{
				del.Invoke();
			}

			_previousMouseState = Mouse.GetState();
		}

		public void End()
		{
			_previousState = Keyboard.GetState();
		}
	}
}
