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
			for (int column = 'a'; column<'i'; column++)
			{
				Assert.IsTrue(Chess.Control.GameBoard.GetSquare((char)column, 1).OccupyingPiece.GetType() == typeof(Pawn));
			}
		}

		//[TestMethod]
		//public void MovementTest()
		//{
		//	Coordinate startCoord = new Coordinate('a', 1);
		//	Coordinate endCoord = new Coordinate('a', 2);
		//	Chess.Model.Move testMove = new Move(startCoord, endCoord);
		//	Chess.Control.GameBoard.MovePiece(testMove);
		//	Assert.IsTrue(Chess.Control.GameBoard.GetSquare('a', 2).OccupyingPiece.GetType().Equals(typeof(Pawn)));
		//	Assert.IsFalse(Chess.Control.GameBoard.MovePiece(testMove) == null);
		//}

        [TestMethod]
        public void RookMovementTestVertical()
        {
            Coordinate startCoord = new Coordinate('h', 1);
            Coordinate endCoord = new Coordinate('h', 7);
            Chess.Model.Move testMove = new Move(startCoord, endCoord);
            GameBoard.MovePiece(testMove);
            Assert.IsTrue(GameBoard.GetSquare('h', 7).OccupyingPiece.GetType().Equals(typeof(Rook)));
            Assert.IsFalse(GameBoard.MovePiece(testMove) == null);
        }

        [TestMethod]
        public void RookMovementTestHorizontal()
        {
            Coordinate startCoord = new Coordinate('h', 0);
            Coordinate endCoord = new Coordinate('e', 0);
            Chess.Model.Move testMove = new Move(startCoord, endCoord);
            GameBoard.MovePiece(testMove);
            Assert.IsTrue(GameBoard.GetSquare('e', 0).OccupyingPiece.GetType().Equals(typeof(Rook)));
            Assert.IsFalse(GameBoard.MovePiece(testMove) == null);
        }
    }
}
