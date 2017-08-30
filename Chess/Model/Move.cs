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
		public string ErrorMessage { get; set; } = null;

		public Move(Coordinate startCoordinate, Coordinate endCoordinate)
		{
			StartCoordinate = startCoordinate;
			EndCoordinate = endCoordinate;
		}

		public override string ToString()
		{
			if (ErrorMessage != null)
				return ErrorMessage;
			StringBuilder retVal = new StringBuilder();
			if (PieceMoved!=null)
				retVal.Append(PieceMoved.ToString());
			retVal.Append($"{StartCoordinate} => ");
			if (PieceTaken!=null)
				retVal.Append(PieceTaken.ToString());
			retVal.Append($"{EndCoordinate}");
			return retVal.ToString();
		}
	}
}
