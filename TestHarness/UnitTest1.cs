using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess.Model.Ranks;
using System.Linq;
using Chess.Model;

namespace TestHarness
{
	[TestClass]
	public class Tester
	{
		[TestMethod]
		public void BoardInitTest()
		{
			Chess.Control.GameBoard.ResetBoard();
			Assert.IsTrue(Chess.Control.GameBoard.gameGrid.Where(square => square.Key == new Coordinate('a', 1)).First().Value.OccupyingPiece.GetType().Equals(typeof(Pawn)));
		}

		[TestMethod]
		public void MovementTest()
		{
			Coordinate startCoord = new Coordinate('a', 1);
			Coordinate endCoord = new Coordinate('a', 2);
			Chess.Model.Move testMove = new Move(startCoord, endCoord);
			Chess.Control.GameBoard.MovePiece(testMove);
			Assert.IsTrue(Chess.Control.GameBoard.GetSquare('a', 2).OccupyingPiece.GetType().Equals(typeof(Pawn)));
			Assert.IsFalse(Chess.Control.GameBoard.MovePiece(testMove) == null);
		}
	}
}
