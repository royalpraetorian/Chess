using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model.Ranks
{
	public class Knight : Piece
	{
		public Knight(int playerNumber) : base(playerNumber)
		{
		}

		public override Coordinate[] Threat
		{
			get
			{
				return Control.GameBoard.gameGrid.Where(
					space => (Math.Abs(space.Key.Column - this.CurrentPosition.Column) == 1 && Math.Abs(space.Key.Row - this.CurrentPosition.Row) == 2 ||
					Math.Abs(space.Key.Column - this.CurrentPosition.Column) == 2 && Math.Abs(space.Key.Row - this.CurrentPosition.Row) == 1)).Select(space => space.Key).ToArray();
			}
		}

		public override Coordinate[] RangeOfMotion
		{
			get { return Threat; }
		}
	}
}
