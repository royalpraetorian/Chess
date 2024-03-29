﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model
{
    [Serializable]
    public class Coordinate
	{
		public int Column { get; set; }
		public int Row { get; set; }

		public Coordinate(int column, int row)
		{
			Column = column;
			Row = row;
		}

		public static bool operator ==(Coordinate a, Coordinate b)
		{
			return (a.Column == b.Column && a.Row == b.Row);
		}

		public static bool operator !=(Coordinate a, Coordinate b)
		{
			return !(a == b);
		}

		/// <summary>
		/// This method calculates the direction in which you will have to move to get from a to b.
		/// </summary>
		/// <param name="a">Starting position</param>
		/// <param name="b">Destination</param>
		/// <returns>A coordinate which may be added to the starting position to reach the ending position.</returns>
		public static Coordinate GetVector(Coordinate a, Coordinate b)
		{
			Coordinate vector = b - a;
			vector /= new Coordinate(Math.Abs(vector.Column), Math.Abs(vector.Row));
			return vector;
		}

		public static Coordinate operator +(Coordinate a, Coordinate b)
		{
			return new Coordinate((a.Column + (b.Column)), a.Row + b.Row);
		}

		public static Coordinate operator -(Coordinate a, Coordinate b)
		{
			return new Coordinate((a.Column-(b.Column)), a.Row - b.Row);
		}

		public static Coordinate operator /(Coordinate a, Coordinate b)
		{
			try
			{
				return new Coordinate((a.Column / b.Column), a.Row / b.Row);
			}
			catch(DivideByZeroException e)
			{
				if (b.Row==0 && b.Column!=0)
				{
					return new Coordinate(a.Column / b.Column, 0);
				}
				else if (b.Column==0 && b.Row!=0)
				{
					return new Coordinate(0, a.Row / b.Row);
				}
				else
				{
					return new Coordinate(0, 0);
				}
			}
		}

		public static Coordinate operator *(Coordinate a, Coordinate b)
		{
			return new Coordinate((a.Column * b.Column), a.Row * b.Row);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Column * 297) ^ Row) * 397;
			}
		}

		public override bool Equals(object obj)
		{
			if(obj is Coordinate comparitor)
			{
				return comparitor.Column == this.Column && comparitor.Row == this.Row;
			}
			else return false;
		}

		public override string ToString()
		{
			char col = (char)('a' + Column);
			return $"{col}{Row}";
		}
	}
}
