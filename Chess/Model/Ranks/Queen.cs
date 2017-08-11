using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model.Ranks
{
	public class Queen : Piece
	{
		public Queen(int playerNumber) : base(playerNumber) { }

		public override Coordinate[] Threat
		{
			get
			{
				return Control.GameBoard.gameGrid.Where(
					space => (space.Key.Row-space.Key.Column == this.CurrentPosition.Row-this.CurrentPosition.Column) ||
					(space.Key.Column==this.CurrentPosition.Column) ||
					(space.Key.Row==this.CurrentPosition.Row)).Select(space => space.Key).ToArray();
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
