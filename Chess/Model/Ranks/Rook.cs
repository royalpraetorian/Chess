using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model.Ranks
{
	public class Rook : Piece
	{
		public Rook(int playerNumber) : base(playerNumber)
		{
		}

		public override Coordinate[] Threat => throw new NotImplementedException();

		public override Coordinate[] RangeOfMotion => throw new NotImplementedException();
	}
}
