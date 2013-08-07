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

		public void Start(Keys key, Action del)
		{
			_currentState = Keyboard.GetState();

			if (_currentState.IsKeyDown(key) && !_previousState.IsKeyDown(key))
			{
				del.Invoke();
			}
		}

		public void End()
		{
			_previousState = Keyboard.GetState();
		}
	}
}
