using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model
{
	public class Move
	{
		public Coordinate StartCoordinate { get; set; }
		public Coordinate EndCoordinate { get; set; }
		public Piece PieceTaken { get; set; }
        public Piece PieceMoved { get; set; }

		public Move(Coordinate startCoordinate, Coordinate endCoordinate)
		{
			StartCoordinate = startCoordinate;
			EndCoordinate = endCoordinate;
		}
	}
}
