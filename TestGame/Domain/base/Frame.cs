using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
	public class Frame
	{
		float _timer { get; set; }

		public int Current { get; set; }
		public float Interval { get; set; }
		public int Offset { get; set; }

		public Frame(float interval)
		{
			Interval = interval;
		}

		public void DefaultValue()
		{
			Current = 0;
			_timer = 0;
			Interval = 0;
			Offset = 0;
		}

		public virtual void Animate(GameTime gameTime, int startFrame, int endFrame, int speed)
		{
			_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
			if (_timer > Interval / speed)
			{
				Current++;
				_timer = 0;
				if (Current > endFrame)
				{
					Current = startFrame;
				}
			}
		}

		public void Reset()
		{
			Current = 0;
		}

		public int CurrentFrame()
		{
			return Current * (int)Interval;
		}
	}
}
