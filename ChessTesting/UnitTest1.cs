using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess.Control;
using Chess.Model;
using Chess.Model.Ranks;
using System.Collections.Generic;

namespace ChessTesting
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			GameBoard game = new GameBoard();
			Assert.IsTrue(game.White.King.ValidCastleTargets.Count == 0);
			Coordinate startCoordinate = new Coordinate(1, 1);
			Coordinate endCoordinate = new Coordinate(1, 2);
			Move m = new Move(startCoordinate, endCoordinate);
			Assert.IsTrue(game.MovePiece(m)==null);
			startCoordinate = new Coordinate(1, 0);
			endCoordinate = new Coordinate(0,2);
			m = new Move(startCoordinate, endCoordinate);
			Assert.IsTrue(game.MovePiece(m)==null);
			startCoordinate = new Coordinate(2, 0);
			endCoordinate = new Coordinate(1,1);
			m = new Move(startCoordinate, endCoordinate);
			Assert.IsTrue(game.MovePiece(m) == null);
			startCoordinate = new Coordinate(3, 1);
			endCoordinate = new Coordinate(3, 2);
			m = new Move(startCoordinate, endCoordinate);
			Assert.IsTrue(game.MovePiece(m) == null);
			startCoordinate = new Coordinate(3, 0);
			endCoordinate = new Coordinate(3, 1);
			m = new Move(startCoordinate, endCoordinate);
			Assert.IsTrue(game.MovePiece(m) == null);
			Assert.IsTrue(game.White.King.ValidCastleTargets.Count == 1);
		}
	}
}
