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
			List<Piece> vct = game.White.King.ValidCastleTargets;
		}
	}
}
