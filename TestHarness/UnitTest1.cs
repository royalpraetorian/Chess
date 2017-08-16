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
	}
}
