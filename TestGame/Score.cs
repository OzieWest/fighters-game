using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Domain;

namespace TestGame
{
	public class Score : FontObject
	{
		protected int _minValue;
		public int Value { get; private set; }
		public String Prefix { get; set; }

		public Score(SpriteFont font, String prefix, int value, int minValue)
			: base(font)
		{
			_minValue = minValue;
			Value = value;
			Prefix = prefix;
		}

		public void Update()
		{
			_checkValue();
			Text = String.Format("{0}: {1}", Prefix, Value); 
		}

		public void Plus(int value)
		{
			Value += value;
		}

		public void Minus(int value)
		{
			if (Value > _minValue)
				Value -= value;
		}

		protected void _checkValue()
		{
			if (Value < _minValue)
				Value = _minValue;
		}
	}
}
