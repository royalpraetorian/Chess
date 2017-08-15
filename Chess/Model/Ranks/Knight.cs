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

		public override List<List<Coordinate>> Threat
		{
			get
			{
				return new List<List<Coordinate>>()
				{
					{ Control.GameBoard.gameGrid.Where(space => space.Key == new Coordinate(CurrentPosition.Column-2, CurrentPosition.Row-1)).Select(space => space.Key).ToList() },
					{ Control.GameBoard.gameGrid.Where(space => space.Key == new Coordinate(CurrentPosition.Column-1, CurrentPosition.Row-2)).Select(space => space.Key).ToList() },
					{ Control.GameBoard.gameGrid.Where(space => space.Key == new Coordinate(CurrentPosition.Column+2, CurrentPosition.Row+1)).Select(space => space.Key).ToList() },
					{ Control.GameBoard.gameGrid.Where(space => space.Key == new Coordinate(CurrentPosition.Column+1, CurrentPosition.Row+2)).Select(space => space.Key).ToList() },
					{ Control.GameBoard.gameGrid.Where(space => space.Key == new Coordinate(CurrentPosition.Column+2, CurrentPosition.Row-1)).Select(space => space.Key).ToList() },
					{ Control.GameBoard.gameGrid.Where(space => space.Key == new Coordinate(CurrentPosition.Column-2, CurrentPosition.Row+1)).Select(space => space.Key).ToList() },
					{ Control.GameBoard.gameGrid.Where(space => space.Key == new Coordinate(CurrentPosition.Column+1, CurrentPosition.Row-2)).Select(space => space.Key).ToList() },
					{ Control.GameBoard.gameGrid.Where(space => space.Key == new Coordinate(CurrentPosition.Column-1, CurrentPosition.Row+2)).Select(space => space.Key).ToList() }
				};
			}
		}

		public override List<List<Coordinate>> RangeOfMotion
		{
			get { return Threat; }
		}
	}
}
