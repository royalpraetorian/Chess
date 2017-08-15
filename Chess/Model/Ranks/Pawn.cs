using Chess.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model.Ranks {

    public class Pawn : Piece {

        public override List<List<Coordinate>> Threat {
            get {
                int direction = (PlayerNumber == 0) ? 1 : -1;

                List<Coordinate> leftDiagonal = new List<Coordinate>() {
                    CurrentPosition + new Coordinate(-1, direction)
                };
                List<Coordinate> rightDiagonal = new List<Coordinate>() {
                    CurrentPosition + new Coordinate(1, direction)
                };

                List<List<Coordinate>> threat = new List<List<Coordinate>>() {
                    leftDiagonal,
                    rightDiagonal
                };

                return threat;
            }
        }

        public override List<List<Coordinate>> RangeOfMotion {
            get {
                int direction = (PlayerNumber == 0) ? 1 : -1;
                List<Coordinate> forwardMovement = new List<Coordinate>() {
                    CurrentPosition + new Coordinate(0, direction)
                };
                if (!HasMoved) forwardMovement.Add(CurrentPosition + new Coordinate(0, 2 * direction));

                List<List<Coordinate>> rangeOfMotion = new List<List<Coordinate>>() {
                    forwardMovement
                };

                foreach (List<Coordinate> diagonalThreat in Threat) {
                    Coordinate curCheck = diagonalThreat.ElementAt(0);

                    if (GameBoard.GetSquare(curCheck).OccupyingPiece != null) {
                        rangeOfMotion.Add(diagonalThreat);
                    } 
                }

                return rangeOfMotion;
            }
        }

        public Pawn(int playerNumber) : base(playerNumber) {}

    }
}
