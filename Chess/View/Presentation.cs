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
	public static class Presentation
	{
		public static void RunPresentation()
		{
			//This presentation does not validate input or protect from exceptions.
			//This is a very flimsy method designed to be used exclusively by people who know what they're doing.
			bool quit = false;
			while (!quit)
			{
				try
				{
					string userInput = Console.ReadLine().ToLower();
					string[] commands = userInput.Split();
					string startIndex = commands[0];
					string endIndex = commands[2];
					Coordinate endLocation = new Coordinate(endIndex[0], int.Parse(endIndex[1].ToString()));
					Coordinate startLocation = new Coordinate('a', 1);
					if (startIndex.Length > 2)
					{
						switch (startIndex)
						{
							case "pawn":
								startLocation = GameBoard.gameGrid.Where(square => square.Value.OccupyingPiece != null && square.Value.OccupyingPiece.GetType().Equals(typeof(Pawn)) &&
								square.Value.OccupyingPiece.RangeOfMotion.Contains(endLocation)).First().Key;
								break;
							case "rook":
								startLocation = GameBoard.gameGrid.Where(square => square.Value.OccupyingPiece != null && square.Value.OccupyingPiece.GetType().Equals(typeof(Rook)) &&
								square.Value.OccupyingPiece.RangeOfMotion.Contains(endLocation)).First().Key;
								break;
							case "knight":
								startLocation = GameBoard.gameGrid.Where(square => square.Value.OccupyingPiece != null && square.Value.OccupyingPiece.GetType().Equals(typeof(Knight)) &&
								square.Value.OccupyingPiece.RangeOfMotion.Contains(endLocation)).First().Key;
								break;
							case "bishop":
								startLocation = GameBoard.gameGrid.Where(square => square.Value.OccupyingPiece != null && square.Value.OccupyingPiece.GetType().Equals(typeof(Bishop)) &&
								square.Value.OccupyingPiece.RangeOfMotion.Contains(endLocation)).First().Key;
								break;
							case "king":
								startLocation = GameBoard.gameGrid.Where(square => square.Value.OccupyingPiece != null && square.Value.OccupyingPiece.GetType().Equals(typeof(King)) &&
								square.Value.OccupyingPiece.RangeOfMotion.Contains(endLocation)).First().Key;
								break;
							case "queen":
								startLocation = GameBoard.gameGrid.Where(square => square.Value.OccupyingPiece != null && square.Value.OccupyingPiece.GetType().Equals(typeof(Queen)) &&
								square.Value.OccupyingPiece.RangeOfMotion.Contains(endLocation)).First().Key;
								break;
						}
					}
					else
					{
						startLocation = new Coordinate(startIndex[0], int.Parse(startIndex[1].ToString()));
					}
					Move move = new Move(startLocation, endLocation);
					string result = GameBoard.MovePiece(move);
					if (result == null)
					{
						Console.WriteLine("Move successful!");
					}
					else
					{
						Console.WriteLine("Move failed!");
						Console.WriteLine(result);
					}
				}
				catch (InvalidOperationException e)
				{
					Console.WriteLine("There were no pieces of the specified type that could legally move to that square.");
				}
			}
		}
	}
}
