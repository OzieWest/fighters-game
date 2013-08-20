using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
	public class Position
	{
		protected Vector2 _realPosition;
		protected Vector2 _originalPosition;
		protected Vector2 _destinationPosition;
		protected Rectangle _rectangle;

		public float Speed { get; set; }

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

		public Position()
		{
			Original = new Vector2(0, 0);
			Real = new Vector2(0, 0);
			Destination = new Vector2(0, 0);
		}

		public Boolean IsMoveComplete()
		{
			if (X != toX || Y != toY)
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Real position X
		/// </summary>
		public float X
		{
			get { return _realPosition.X; }
			set { _realPosition.X = value; }
		}

		/// <summary>
		/// Real position Y
		/// </summary>
		public float Y
		{
			get { return _realPosition.Y; }
			set { _realPosition.Y = value; }
		}

		/// <summary>
		/// Destination position X
		/// </summary>
		public float toX
		{
			get { return _destinationPosition.X; }
			set { _destinationPosition.X = value; }
		}

		/// <summary>
		/// Destination position Y
		/// </summary>
		public float toY
		{
			get { return _destinationPosition.Y; }
			set { _destinationPosition.Y = value; }
		}

		/// <summary>
		/// Rectangle position X
		/// </summary>
		public int rX
		{
			get { return _rectangle.X; }
			set { _rectangle.X = value; }
		}

		/// <summary>
		/// Rectangle position Y
		/// </summary>
		public int rY
		{
			get { return _rectangle.Y; }
			set { _rectangle.Y = value; }
		}
	}
}
