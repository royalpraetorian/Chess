using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model
{
	public abstract class Piece
	{
		public Coordinate CurrentPosition { get; set; }
		public bool HasMoved { get; set; }

		abstract public Coordinate[] Threat { get; }
		abstract public Coordinate[] RangeOfMotion { get; }

		public int PlayerNumber { get; set; }

		public Piece(int playerNumber)
		{
			PlayerNumber = playerNumber;
		}
	}
}
