using Chess.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model.Ranks
{
	public class Pawn : Piece
	{
		public override List<List<Coordinate>> Threat
		{
			get
			{
				int direction = (PlayerNumber == 0) ? 1 : -1;
				List<List<Coordinate>> threat = new List<List<Coordinate>>();
				if (this.CurrentPosition.Column + 1 < 8 && (this.CurrentPosition.Row + direction > 0 && this.CurrentPosition.Row + direction < 8))
					threat.Add(new List<Coordinate>() { (new Coordinate((char)(CurrentPosition.Column + 1), CurrentPosition.Row + direction)) });
				if (this.CurrentPosition.Column - 1 >= 0 && (this.CurrentPosition.Row + direction > 0 && this.CurrentPosition.Row + direction < 8))
					threat.Add(new List<Coordinate>() { (new Coordinate((char)(CurrentPosition.Column - 1), CurrentPosition.Row + direction)) });
				return threat;
			}
		}
        // Add check logic for phantom pawn in RoM method
		public override List<List<Coordinate>> RangeOfMotion
		{
			get
			{
				int direction = (PlayerNumber == 0) ? 1 : -1;
				List<List<Coordinate>> motion = new List<List<Coordinate>>();

				foreach (List<Coordinate> vector in Threat)
				{
					if (vector.Where(space => GameBoard.GetSquare(space).OccupyingPiece != null && GameBoard.GetSquare(space).OccupyingPiece.PlayerNumber != PlayerNumber).Count() > 0)
						motion.Add(vector);
				}

				List<Coordinate> forwardVector = new List<Coordinate> { new Coordinate(CurrentPosition.Column, CurrentPosition.Row + direction) };
				if (!HasMoved)
					forwardVector.Add(new Coordinate(CurrentPosition.Column, CurrentPosition.Row + (2 * direction)));
				motion.Add(forwardVector);

				return motion;
			}
		}

		public Pawn(int playerNumber, Player player) : base(playerNumber, player)
		{
		}

	}
}