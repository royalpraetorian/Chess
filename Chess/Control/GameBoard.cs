using Chess.Model;
using Chess.Model.Ranks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Control
{
	public static class GameBoard
	{
		public static Dictionary<Coordinate, Space> gameGrid = new Dictionary<Coordinate, Space>();
		public static List<Move> moveHistory = new List<Move>();
		public static void ResetBoard()
		{
			//Clear the board of all current spaces.
			//Because pieces are contained within spaces, this will effectively clear all of our pieces as well.
			gameGrid.Clear();
			//We also need to clear the move history to begin a new game.
			moveHistory.Clear();

			//Generate a new board. I used char-math for the column to make conversion a little easier.
			for(int column = 'a'; column<'i'; column++)
			{
				for (int row = 0; row<8; row++)
				{
					Coordinate coords = new Coordinate((char)column, row);
					gameGrid.Add(coords, new Space());
				}
			}

			//These two methods do exactly the same thing in different places... 
			//I probably could have made it one method with a little math, but I was tired.
			PopulatePlayerOne();
			PopulatePlayerTwo();
		}

		private static void PopulatePlayerOne()
		{
			//Runs through every column and populates the correct rows with pawns, or other pieces based on a switch.
			for (int column = 'a'; column<'i'; column++)
			{
				gameGrid[new Coordinate((char)column, 2)].OccupyingPiece = new Pawn(1);
				switch((char)column)
				{
					case 'a':
					case 'h':
						gameGrid[new Coordinate((char)column, 1)].OccupyingPiece = new Rook(1);
						break;
					case 'b':
					case 'g':
						gameGrid[new Coordinate((char)column, 1)].OccupyingPiece = new Knight(1);
						break;
					case 'c':
					case 'f':
					gameGrid[new Coordinate((char)column, 1)].OccupyingPiece = new Bishop(1);
						break;
					case 'd':
						gameGrid[new Coordinate((char)column, 1)].OccupyingPiece = new King(1);
						break;
					case 'e':
						gameGrid[new Coordinate((char)column, 1)].OccupyingPiece = new Queen(1);
						break;
				}
			}
		}

		private static void PopulatePlayerTwo()
		{
			for (int column = 'a'; column < 'i'; column++)
			{
				gameGrid[new Coordinate((char)column, 6)].OccupyingPiece = new Pawn(1);
				switch ((char)column)
				{
					case 'a':
					case 'h':
						gameGrid[new Coordinate((char)column, 7)].OccupyingPiece = new Rook(1);
						break;
					case 'b':
					case 'g':
						gameGrid[new Coordinate((char)column, 7)].OccupyingPiece = new Knight(1);
						break;
					case 'c':
					case 'f':
						gameGrid[new Coordinate((char)column, 7)].OccupyingPiece = new Bishop(1);
						break;
					case 'd':
						gameGrid[new Coordinate((char)column, 7)].OccupyingPiece = new King(1);
						break;
					case 'e':
						gameGrid[new Coordinate((char)column, 7)].OccupyingPiece = new Queen(1);
						break;
				}
			}
		}

		public static string MovePiece(Move move)
		{
			/*
			 * This method returns a string. If the string it returns is null, then the move command succeeded.
			 * If the string is not null, then the move command failed, and the string will contain the reason why.
			 */
			string moveError = null;

			//First we check if there is even a piece in that spot.
			if (gameGrid[move.StartCoordinate].OccupyingPiece != null)
			{
				//If there is a piece in the targeted space, we add it to the move object.
				move.PieceMoved = gameGrid[move.StartCoordinate].OccupyingPiece;

				//Next we check if the space we're attempting to move the piece to is within that piece's range of motion.
				if (gameGrid[move.StartCoordinate].OccupyingPiece.RangeOfMotion.Contains(move.EndCoordinate))
				{
					//Finally, we check to see if moving the piece would result in putting the player in check.
					//This is skipped for now, as we don't have that check written.
					if (true) //Placeholder for the check validation
					{
						//Finally, we check to see if the piece we moved took another piece.
						if (gameGrid[move.EndCoordinate].OccupyingPiece != null)
						{
							move.PieceTaken = true;
						}

						//Now that the checks are complete, and we're certain the move was successful,
						//We need to execute the move command, and then archive it.
						gameGrid[move.StartCoordinate].OccupyingPiece = null;
						gameGrid[move.EndCoordinate].OccupyingPiece = move.PieceMoved;
						moveHistory.Add(move);
					}
					else
					{
						moveError = "Moving the selected piece to the target space would put the player in check, and is therefore illegal.";
					}
				}
				else
				{
					moveError = "The target space was not within the selected piece's range of motion.";
				}
			}
			else
			{
				moveError = "There was no piece at the selected location.";
			}

			return moveError;
		}
	}
}
