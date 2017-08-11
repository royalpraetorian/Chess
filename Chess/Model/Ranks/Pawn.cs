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
            get {
                int direction = (PlayerNumber == 0) ? 1 : -1;
                return new Coordinate[] {
                    new Coordinate((char) (CurrentPosition.Column + 1), CurrentPosition.Row + direction),
                    new Coordinate((char) (CurrentPosition.Column - 1), CurrentPosition.Row + direction)
                };
            }
        }

        public override Coordinate[] RangeOfMotion {
            get {
                int direction = (PlayerNumber == 0) ? 1 : -1;
                List<Coordinate> motion = new List<Coordinate> {
                    new Coordinate(CurrentPosition.Column, CurrentPosition.Row + direction)
                };
                if (HasMoved) motion.Add(new Coordinate(CurrentPosition.Column, CurrentPosition.Row + (2 * direction)));

                foreach (Coordinate threatMotion in Threat.Where(space => GameBoard.gameGrid[space] != null)) {
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
