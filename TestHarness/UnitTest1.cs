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
	}
}
