using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess.Model.Ranks;
using System.Linq;
using Chess.Model;
using Chess.Control;

namespace TestHarness
{
	[TestClass]
	public class Tester
	{
		[TestMethod]
		public void BoardInitTest()
		{
			Chess.Control.GameBoard.ResetBoard();
			for (int column = 0; column<8; column++)
			{
				Assert.IsTrue(Chess.Control.GameBoard.GetSquare((char)column, 1).OccupyingPiece.GetType() == typeof(Pawn));
			}
		}

		[TestMethod]
		public void MovementTest()
		{
			Assert.IsTrue(GameBoard.MovePiece(new Move(new Coordinate(3,1), new Coordinate(3, 3))) == null);
			Assert.IsTrue(GameBoard.MovePiece(new Move(new Coordinate(2,0), new Coordinate(4,2))) == null);
		}

        [TestMethod]
        public void CheckTest() {
            GameBoard.ResetBoard();

        }

		[TestMethod]
		public void MoveValidationTest()
		{
			GameBoard.ResetBoard();
			GameBoard.gameGrid = new System.Collections.Generic.Dictionary<Coordinate, Space>();
			for (int column = 0; column < 7; column++)
			{
				for (int row = 0; row<7; row++)
				{
					GameBoard.gameGrid.Add(new Coordinate(column, row), new Space());
				}
			}
			GameBoard.White.Pieces.Clear();
			GameBoard.Black.Pieces.Clear();

			King whiteKing = new King(0, GameBoard.White);
			GameBoard.White.Pieces.Add(whiteKing);
			GameBoard.White.King = whiteKing;
			GameBoard.GetSquare(0, 0).OccupyingPiece = whiteKing;

			Rook whiteRook = new Rook(0, GameBoard.White);
			GameBoard.White.Pieces.Add(whiteRook);
			GameBoard.GetSquare(2, 0).OccupyingPiece = whiteRook;

			Queen blackQueen = new Queen(1, GameBoard.Black);
			GameBoard.Black.Pieces.Add(blackQueen);
			GameBoard.GetSquare(4, 0).OccupyingPiece = blackQueen;

			Assert.IsFalse(whiteRook.ValidRangeOfMotion.Where(vector => vector.Where(space => space == new Coordinate(2, 2)).Count() > 0).Count() > 0);
		}
	}
}
