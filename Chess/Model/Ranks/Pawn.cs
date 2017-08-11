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
        public override Coordinate[] Threat {
            get
			{
                int direction = (PlayerNumber == 0) ? 1 : -1;
				List<Coordinate> threat = new List<Coordinate>();
				if (this.CurrentPosition.Column + 1 < 'i' && (this.CurrentPosition.Row+direction > 0 && this.CurrentPosition.Row+direction<8))
					threat.Add(new Coordinate((char)(CurrentPosition.Column + 1), CurrentPosition.Row + direction));
				if (this.CurrentPosition.Column - 1 >= 'a' && (this.CurrentPosition.Row + direction > 0 && this.CurrentPosition.Row + direction < 8))
					threat.Add(new Coordinate((char)(CurrentPosition.Column - 1), CurrentPosition.Row + direction));
				return threat.ToArray();
            }
        }

        public override Coordinate[] RangeOfMotion {
            get {
                int direction = (PlayerNumber == 0) ? 1 : -1;
                List<Coordinate> motion = new List<Coordinate> {
                    new Coordinate(CurrentPosition.Column, CurrentPosition.Row + direction)
                };
                if (HasMoved) motion.Add(new Coordinate(CurrentPosition.Column, CurrentPosition.Row + (2 * direction)));

                foreach (Coordinate threatMotion in Threat.Where(space => GameBoard.GetSquare(space).OccupyingPiece != null && GameBoard.GetSquare(space).OccupyingPiece.PlayerNumber!=this.PlayerNumber)) {
                    motion.Add(threatMotion);
                }

                return motion.ToArray();
            }
        }

        public Pawn(int playerNumber) : base(playerNumber)
		{
		}

    }
}
