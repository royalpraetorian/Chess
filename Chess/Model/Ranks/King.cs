using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model.Ranks
{
	public class King : Piece
	{
		public King(int playerNumber) : base(playerNumber) { }

		public override Coordinate[] Threat
		{
			get
			{
				return Control.GameBoard.gameGrid.Where(
					square => (Math.Abs(square.Key.Column - this.CurrentPosition.Column) == 1 && Math.Abs(square.Key.Row-this.CurrentPosition.Row)==1) || 
					(Math.Abs(square.Key.Column-this.CurrentPosition.Column)==1 && square.Key.Row == this.CurrentPosition.Row) ||
					(Math.Abs(square.Key.Row-this.CurrentPosition.Row)==1 && square.Key.Column==this.CurrentPosition.Column)
					).Select(square => square.Key).ToArray();
			}
		}

		public override Coordinate[] RangeOfMotion
		{
			get
			{
				return Threat;
			}
		}
	}
}
