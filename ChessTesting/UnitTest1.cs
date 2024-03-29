﻿using System;
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
			Assert.IsTrue(game.MovePiece(m).ErrorMessage==null);
			startCoordinate = new Coordinate(1, 0);
			endCoordinate = new Coordinate(0,2);
			m = new Move(startCoordinate, endCoordinate);
			Assert.IsTrue(game.MovePiece(m).ErrorMessage == null);
			startCoordinate = new Coordinate(2, 0);
			endCoordinate = new Coordinate(1,1);
			m = new Move(startCoordinate, endCoordinate);
			Assert.IsTrue(game.MovePiece(m).ErrorMessage == null);
			startCoordinate = new Coordinate(3, 1);
			endCoordinate = new Coordinate(3, 2);
			m = new Move(startCoordinate, endCoordinate);
			Assert.IsTrue(game.MovePiece(m).ErrorMessage == null);
			startCoordinate = new Coordinate(3, 0);
			endCoordinate = new Coordinate(3, 1);
			m = new Move(startCoordinate, endCoordinate);
			Assert.IsTrue(game.MovePiece(m).ErrorMessage == null);
			Assert.IsTrue(game.White.King.ValidCastleTargets.Count == 1);
			startCoordinate = game.White.King.CurrentPosition;
			endCoordinate = game.White.King.ValidCastleTargets[0].CurrentPosition;
			m = new Move(startCoordinate, endCoordinate);
			Assert.IsTrue(game.MovePiece(m).ErrorMessage == null);
			Console.WriteLine(m);
		}

		[TestMethod]
		public void PostCastleDebug()
		{
			GameBoard game = new GameBoard();
			Move m = new Move(new Coordinate(4, 1), new Coordinate(4, 2));
			game.MovePiece(m);
			game.IncrimentTurn();
			m = new Move(new Coordinate(4, 6), new Coordinate(4, 5));
			game.MovePiece(m);
			game.IncrimentTurn();
			m = new Move(new Coordinate(5, 0), new Coordinate(4, 1));
			game.MovePiece(m);
			game.IncrimentTurn();
			m = new Move(new Coordinate(5, 6), new Coordinate(5, 5));
			game.MovePiece(m);
			game.IncrimentTurn();
			m = new Move(new Coordinate(6, 0), new Coordinate(5, 2));
			game.MovePiece(m);
			game.IncrimentTurn();
			m = new Move(new Coordinate(6, 6), new Coordinate(6, 5));
			game.MovePiece(m);
			game.IncrimentTurn();
			m = new Move(new Coordinate(4, 0), new Coordinate(7, 0));
			game.MovePiece(m);
			game.IncrimentTurn();
			m = new Move(new Coordinate(7, 6), new Coordinate(7, 5));
			game.MovePiece(m);
			game.IncrimentTurn();
			m = new Move(new Coordinate(6, 0), new Coordinate(7, 0));
			game.MovePiece(m);
			game.IncrimentTurn();
		}
	}
}
