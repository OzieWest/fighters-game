using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
	public class Frame
	{
		int _current;
		float _timer { get; set; }

		public float Interval { get; set; }
		public int Offset { get; set; }

		public Frame(float interval)
		{
			Interval = interval;
		}

		public void DefaultValue()
		{
			_current = 0;
			_timer = 0;
			Interval = 0;
			Offset = 0;
		}

		public virtual void Animate(GameTime gameTime, int startFrame, int endFrame, int speed)
		{
			_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
			if (_timer > Interval / speed)
			{
				_current++;
				_timer = 0;
				if (_current > endFrame)
				{
					_current = startFrame;
				}
			}
		}

		public void Reset()
		{
			_current = 0;
		}

		public int CurrentFrame()
		{
			return _current * (int)Interval;
		}
	}
}
