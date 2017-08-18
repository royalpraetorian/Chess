using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model.Ranks
{
	public class Knight : Piece
	{
		public Knight(int playerNumber, Player player) : base(playerNumber, player)
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

		public override List<List<Coordinate>> ThreatCollide
		{
			get
			{
				return Threat;
			}
		}

		public override List<List<Coordinate>> RangeOfMotionCollide
		{
			get
			{
				//Knights are really easy. If there's a piece in the square, it can't move there. Otherwise, it can.
				List<List<Coordinate>> baseRange = RangeOfMotion;
				List<List<Coordinate>> retVal = new List<List<Coordinate>>();
				foreach(List<Coordinate> vector in baseRange)
				{
					//Make sure there's a square in the vector, then make sure there are no allied pieces there.
					if (vector.Count > 0 && (Control.GameBoard.GetSquare(vector[0]).OccupyingPiece==null || Control.GameBoard.GetSquare(vector[0]).OccupyingPiece.OwningPlayer!=OwningPlayer))
					{
						retVal.Add(vector);
					}
				}
				return retVal;
			}
		}

		public override List<List<Coordinate>> RangeOfMotion
		{
			get { return Threat; }
		}
	}
}
