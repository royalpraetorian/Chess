using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model
{
	public class Coordinate
	{
		public char Column { get; set; }
		public int Row { get; set; }

		public Coordinate(char column, int row)
		{
			Column = column;
			Row = row;
		}
	}
}
