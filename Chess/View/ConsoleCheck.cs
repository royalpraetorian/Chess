using Chess.Control;
using Chess.Model;
using Chess.Model.Ranks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.View
{
    public class ConsoleCheck
    {
        public static string BoardPrint()
        {
            StringBuilder boardString = new StringBuilder();
            //boardString.Append("- - - - - - - - - -");
            for (int c = 0; c < 8; c++)
            {
                for (int r = 0; r < 8; r++)
                {
                    if (GameBoard.GetSquare(r, c).OccupyingPiece != null)
                    {
                        boardString.Append("| " + RankPrint(GameBoard.GetSquare(r, c).OccupyingPiece) + " ");
                    }
                    else if (GameBoard.GetSquare(r, c).OccupyingPiece == null)
                    {
                        boardString.Append("|   ");
                    }
                    //boardString.Append("|");
                }
                boardString.Append("\n");
            }
            return boardString.ToString();
        }
        public static char RankPrint(Piece p)
        {
            string outputRank = "";
            if (p.GetType() == typeof(Rook))
            {
                outputRank += 'R';
            }
            else if(p.GetType() == typeof(Knight))
            {
                outputRank += 'H';
            }
            else if(p.GetType() == typeof(Bishop))
            {
                outputRank += 'B';
            }
            else if(p.GetType() == typeof(Pawn))
            {
                outputRank += 'P';
            }
            else if(p.GetType() == typeof(King))
            {
                outputRank += 'K';
            }
            else if(p.GetType() == typeof(Queen))
            {
                outputRank += 'Q';
            }

            return outputRank[0];
        }
    }
}
