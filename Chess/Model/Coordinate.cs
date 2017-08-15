using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model
{
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

		public static Coordinate operator +(Coordinate a, Coordinate b)
		{
			return new Coordinate((char)(a.Column + (b.Column)), a.Row + b.Row);
		}

		public static Coordinate operator -(Coordinate a, Coordinate b)
		{
			return new Coordinate((char)(a.Column-(b.Column)), a.Row - b.Row);
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
			return new Coordinate((char)(a.Column * b.Column), a.Row * b.Row);
		}

		public override bool Equals(object obj)
		{
			if (obj.GetType().Equals(typeof(Coordinate)))
			{
				Coordinate comparitor = (Coordinate)obj;
				if (comparitor.Column == this.Column && comparitor.Row == this.Row)
				{
					return true;
				}
				else return false;
			}
			else return false;
		}
	}
}
