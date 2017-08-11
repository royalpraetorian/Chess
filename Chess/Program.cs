using Chess.Control;
using Chess.Model;
using Chess.Model.Ranks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
	class Program
	{
		static void Main(string[] args)
		{
			GameBoard.ResetBoard();
			Coordinate startCoord = new Coordinate('a', 1);
			Coordinate endCoord = new Coordinate('a', 2);
			Chess.Model.Move testMove = new Move(startCoord, endCoord);
			Chess.Control.GameBoard.MovePiece(testMove);
			Console.WriteLine(Chess.Control.GameBoard.GetSquare('a', 2).OccupyingPiece.GetType().Equals(typeof(Pawn)));
		}
	}
}
