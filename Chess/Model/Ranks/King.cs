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

		public override List<List<Coordinate>> Threat
		{
			get
			{
				List<List<Coordinate>> threatRange = new List<List<Coordinate>>()
				{
					{ Control.GameBoard.gameGrid.Where(space => space.Key - CurrentPosition == new Coordinate(-1, 1)).Select(space => space.Key).ToList() },
					{ Control.GameBoard.gameGrid.Where(space => space.Key - CurrentPosition == new Coordinate(-1, -1)).Select(space => space.Key).ToList() },
					{ Control.GameBoard.gameGrid.Where(space => space.Key - CurrentPosition == new Coordinate(1, 1)).Select(space => space.Key).ToList() },
					{ Control.GameBoard.gameGrid.Where(space => space.Key - CurrentPosition == new Coordinate(1, -1)).Select(space => space.Key).ToList() },
					{ Control.GameBoard.gameGrid.Where(space => space.Key - CurrentPosition == new Coordinate(-1, 0)).Select(space => space.Key).ToList() },
					{ Control.GameBoard.gameGrid.Where(space => space.Key - CurrentPosition == new Coordinate(1, 0)).Select(space => space.Key).ToList() },
					{ Control.GameBoard.gameGrid.Where(space => space.Key - CurrentPosition == new Coordinate(0, 1)).Select(space => space.Key).ToList() },
					{ Control.GameBoard.gameGrid.Where(space => space.Key - CurrentPosition == new Coordinate(0, -1)).Select(space => space.Key).ToList() },
				};
				return threatRange;
			}
		}

		public override List<List<Coordinate>> RangeOfMotion
		{
			get
			{
				return Threat;
			}
		}
	}
}
