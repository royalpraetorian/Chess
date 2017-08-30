using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Chess.Control;
using Chess.Model;

namespace ChessAI {

    public class NoobAI : ComputerPlayer {

        public override Move GetNextMove() {
            Random gen = new Random();

            Move move = null;
            List<Piece> ownPieces = Pieces;

            do {
                foreach (Piece curPiece in ownPieces) {
                    bool shouldMovePiece = gen.Next(1, 101) > 60;
                    if (!shouldMovePiece) continue;

                    Coordinate start = curPiece.CurrentPosition;

                    foreach (List<Coordinate> direction in curPiece.ValidThreat) {
                        bool moveChosen = false;

                        foreach (Coordinate destination in direction) {
                            bool destinationChosen = gen.Next(2) == 0;

                            if (destinationChosen) {
                                move = new Move(start, destination);
                                moveChosen = true;
                                break;
                            }
                        }
                        if (moveChosen) break;
                    }
                }
            } while (move == null);

            return move;
        }
    }
}
