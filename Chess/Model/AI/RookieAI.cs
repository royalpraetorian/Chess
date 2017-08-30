using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Chess.Model;

namespace ChessAI {

    public class RookieAI : ComputerPlayer {

        public override Move GetNextMove() {
            Random gen = new Random();
            
            Move bestMove = null;
            long bestValue = long.MinValue;
            foreach (Piece piece in Pieces) {
                foreach (List<Coordinate> direction in piece.ValidThreat) {
                    foreach (Coordinate destination in direction) {

                    }
                }
            }

            return bestMove;
        }
    }
}
