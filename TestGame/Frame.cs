using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
	public class Frame
	{
		public int Current { get; set; }
		public float Timer { get; set; }
		public float Interval { get; set; }
		public int Offset { get; set; }

		public void DefaultValue()
		{
			Current = 0;
			Timer = 0;
			Interval = 0;
			Offset = 0;
		}

		public virtual void Animate(GameTime gameTime, int startFrame, int endFrame, int speed)
		{
			Timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
			if (Timer > Interval / speed)
			{
				Current++;
				Timer = 0;
				if (Current > endFrame)
				{
					Current = startFrame;
				}
			}
		}

		public void ResetCurrent()
		{
			Current = 0;
		}
	}
}
