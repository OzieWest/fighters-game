using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
	public class Position
	{
		Vector2 _realPosition;
		Vector2 _originalPosition;
		Vector2 _destinationPosition;
		Rectangle _rectangle;

		public float SpeedConst { get; set; }
		float _speedX;
		float _speedY;

		public Vector2 Original
		{
			get
			{
				return _originalPosition;
			}

			private set { }
		}
		public Vector2 Real
		{
			get
			{
				return _realPosition;
			}
			set
			{
				_realPosition = value;
			}
		}
		public Vector2 Destination
		{
			get
			{
				return _destinationPosition;
			}
			set
			{
				_destinationPosition = value;
			}
		}
		public Rectangle Rectangle
		{
			get { return _rectangle; }
			set { _rectangle = value; }
		}

		public Frame Frame { get; set; }

		public Position(int height, int frameInterval)
		{
			Rectangle = new Rectangle()
			{
				X = 0,
				Y = 0,
				Width = frameInterval,
				Height = height
			};

			Frame = new Frame(frameInterval);

			Original = new Vector2(0, 0);
			Real = new Vector2(0, 0);
			Destination = new Vector2(0, 0);

			SpeedConst = 15;

			ResetSpeed();
			_setFrame();
		}

		public void Set(float x, float y)
		{
			X = x;
			Y = y;
		}

		public void ResetSpeed()
		{
			_speedX = SpeedConst;
			_speedY = SpeedConst;
		}

		public void Update()
		{
			_setFrame();

			var distanceX = X - _destinationPosition.X;
			var distanceY = Y - _destinationPosition.Y;

			//change X speed
			if (Math.Abs(distanceX) < _speedX)
				_speedX = _speedX / 2;
			else
				_speedX += 1;

			//change Y speed
			if (Math.Abs(distanceY) < _speedY)
				_speedY = _speedY / 2;
			else
				_speedY += 1;

			//move object (Y)
			if (distanceY > 0)
				Y -= _speedY;
			else if (distanceY < 0)
				Y += _speedY;

			//move object (X)
			if (distanceX > 0)
				X -= _speedX;
			else if (distanceX < 0)
				X += _speedX;
		}

		public Boolean IsMoveComplete()
		{
			if (X != _destinationPosition.X || Y != _destinationPosition.Y)
			{
				return false;
			}

			return true;
		}

		public void MoveTo(float x, float y)
		{
			_destinationPosition.X = x;
			_destinationPosition.Y = y;
		}

		public float X
		{
			get { return _realPosition.X; }
			set { _realPosition.X = value; }
		}

		public float Y
		{
			get { return _realPosition.Y; }
			set { _realPosition.Y = value; }
		}

		void _setFrame()
		{
			_rectangle.X = Frame.CurrentFrame();
		}
	}
}
