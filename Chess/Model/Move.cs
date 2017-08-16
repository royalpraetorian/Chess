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
		public bool PieceTaken { get; set; }
        public Piece PieceTook { get; set; }
        public Piece PieceMoved { get; set; }

		public Move(Coordinate startCoordinate, Coordinate endCoordinate)
		{
			StartCoordinate = startCoordinate;
			EndCoordinate = endCoordinate;
		}
	}
}
