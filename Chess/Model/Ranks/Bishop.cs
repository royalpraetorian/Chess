using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model.Ranks
{
	public class Bishop : Piece
	{
		public Bishop(int playerNumber) : base(playerNumber)
		{
		}

		public override List<List<Coordinate>> Threat
		{
			get
			{
				return new List<List<Coordinate>>()
				{
					{Control.GameBoard.gameGrid.Where(space => (space.Key - CurrentPosition)/(new Coordinate(Math.Abs(space.Key.Column-CurrentPosition.Column), Math.Abs(space.Key.Row-CurrentPosition.Row) )) == new Coordinate(-1, -1) ).Select(space => space.Key).ToList() },
					{Control.GameBoard.gameGrid.Where(space => (space.Key - CurrentPosition)/(new Coordinate(Math.Abs(space.Key.Column-CurrentPosition.Column), Math.Abs(space.Key.Row-CurrentPosition.Row) )) == new Coordinate(-1, 1) ).Select(space => space.Key).ToList() },
					{Control.GameBoard.gameGrid.Where(space => (space.Key - CurrentPosition)/(new Coordinate(Math.Abs(space.Key.Column-CurrentPosition.Column), Math.Abs(space.Key.Row-CurrentPosition.Row) )) == new Coordinate(1, -1) ).Select(space => space.Key).ToList() },
					{Control.GameBoard.gameGrid.Where(space => (space.Key - CurrentPosition)/(new Coordinate(Math.Abs(space.Key.Column-CurrentPosition.Column), Math.Abs(space.Key.Row-CurrentPosition.Row) )) == new Coordinate(1, 1) ).Select(space => space.Key).ToList() },

				};
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
