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
        // -- Turn event, all Spaces subscribe to it, currently used for en passant

        public delegate void SpacePassantDelegate();
        public static event SpacePassantDelegate TurnStep;

		public static Player White { get; set; } = new Player();

		public static Player Black { get; set; } = new Player();

		public static Dictionary<Coordinate, Space> gameGrid = new Dictionary<Coordinate, Space>();
		public static Space GetSquare(int column, int row)
		{
			return gameGrid.Where(square => square.Key == new Coordinate(column, row)).First().Value;
		}
		public static Space GetSquare(Coordinate coords)
		{
			return gameGrid.Where(square => square.Key == coords).First().Value;
		}
		public static List<Move> moveHistory = new List<Move>();
		static GameBoard()
		{
			ResetBoard();

		}
		public static void ResetBoard()
		{
			//Clear the board of all current spaces.
			//Because pieces are contained within spaces, this will effectively clear all of our pieces as well.
			gameGrid.Clear();
			//We also need to clear the move history to begin a new game.
			moveHistory.Clear();

			//Generate a new board.
			for(int column = 0; column<8; column++)
			{
				for (int row = 0; row<8; row++)
				{
					Coordinate coords = new Coordinate(column, row);
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
			for (int column = 0; column<8; column++)
			{
				Pawn pawn = new Pawn(0, White);
				White.Pieces.Add(pawn);
				Coordinate square = gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 1).First().Key;
				gameGrid[square].OccupyingPiece = pawn;
				switch (column)
				{
					case 0:
					case 7:
						Rook rook = new Rook(0, White);
						White.Pieces.Add(rook);
						gameGrid.Where(space => space.Key.Column==column && space.Key.Row==0).First().Value.OccupyingPiece = rook;
						break;
					case 1:
					case 6:
						Knight knight = new Knight(0, White);
						White.Pieces.Add(knight);
						gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 0).First().Value.OccupyingPiece = knight;
						break;
					case 2:
					case 5:
						Bishop bishop = new Bishop(0, White);
						White.Pieces.Add(bishop);
						gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 0).First().Value.OccupyingPiece = bishop;
						break;
					case 4:
						King king = new King(0, White);
						White.Pieces.Add(king);
						White.King = king;
						gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 0).First().Value.OccupyingPiece = king;
						break;
					case 3:
						Queen queen = new Queen(0, White);
						White.Pieces.Add(queen);
						gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 0).First().Value.OccupyingPiece = queen;
						break;
				}
			}
		}

		private static void PopulatePlayerTwo()
		{
			for (int column = 0; column < 8; column++)
			{
				Pawn pawn = new Pawn(1, Black);
				Black.Pieces.Add(pawn);
				gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 6).First().Value.OccupyingPiece = pawn;
				switch (column)
				{
					case 0:
					case 7:
						Rook rook = new Rook(1, Black);
						Black.Pieces.Add(rook);
						gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 7).First().Value.OccupyingPiece = rook;
						break;
					case 1:
					case 6:
						Knight knight = new Knight(1, Black);
						Black.Pieces.Add(knight);
						gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 7).First().Value.OccupyingPiece = knight;
						break;
					case 2:
					case 5:
						Bishop bishop = new Bishop(1, Black);
						Black.Pieces.Add(bishop);
						gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 7).First().Value.OccupyingPiece = bishop;
						break;
					case 4:
						King king = new King(1, Black);
						Black.King = king;
						Black.Pieces.Add(king);
						gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 7).First().Value.OccupyingPiece = king;
						break;
					case 3:
						Queen queen = new Queen(1, Black);
						Black.Pieces.Add(queen);
						gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 7).First().Value.OccupyingPiece = queen;
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

			//First we have to check that the space exists
			if (move.StartCoordinate.Row < 8 && move.StartCoordinate.Column < 8)
			{
				//Second we check if there is even a piece in that spot.
				if (GetSquare(move.StartCoordinate).OccupyingPiece != null)
				{
					//If there is a piece in the targeted space, we add it to the move object.
					move.PieceMoved = GetSquare(move.StartCoordinate).OccupyingPiece;

					//Next we check if the space we're attempting to move the piece to is within that piece's range of motion.
					if (GetSquare(move.StartCoordinate).OccupyingPiece.RangeOfMotionCollide.Where(vector => vector.Contains(move.EndCoordinate)).Count() == 1)
					{
						//Finally, we check if the piece can move there without putting its king in check.
						if (move.PieceMoved.MoveContains(move.EndCoordinate, move.PieceMoved.ValidRangeOfMotion))
						{
							//Check for pawn double-move.
							if (move.PieceMoved.GetType().Equals(typeof(Pawn)))
							{
								//Get the current space and the end space, and make sure they are more than one away from each other
								if (Math.Abs(move.EndCoordinate.Row - move.StartCoordinate.Row) == 2)
								{
									//TODO Place the phantom pawn.
								}
							}

							//Check the destination to see if a piece gets taken.
							if (GetSquare(move.EndCoordinate) != null)
							{
								move.PieceTaken = GetSquare(move.EndCoordinate).OccupyingPiece;
							}

							//If we're here, then all the checks have passed and we can move the piece.
							GetSquare(move.StartCoordinate).OccupyingPiece = null;
							GetSquare(move.EndCoordinate).OccupyingPiece = move.PieceMoved;
							move.PieceMoved.HasMoved = true;
						}
						else
						{
							moveError = "Moving there would place the piece's king in check.";
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
			}
			else
				moveError = "That space does not exist.";

			return moveError;
		}

        public static bool KingCheck(int curPlayerToCheck) {

            // This query should only return 1 result, since there is only 1 king on the opposing player's side
            // Thus, indexing at 0 should be appropriate in this case to access the value
            Coordinate curPlayerKingPosition = gameGrid.Where(
                space => space.Value.OccupyingPiece != null 
                && space.Value.OccupyingPiece.PlayerNumber != curPlayerToCheck
                && space.Value.OccupyingPiece.GetType() == typeof(King)).ToArray()[0].Key;

            // Getting all of the spaces that contain pieces possessed by the opponent 
            List<KeyValuePair<Coordinate, Space>> opponentTerritory = gameGrid.Where(
                square => square.Value.OccupyingPiece != null && square.Value.OccupyingPiece.PlayerNumber != curPlayerToCheck).ToList();

            // Here, we're checking if any pieces in the opposing team can take this player's king
            foreach (KeyValuePair<Coordinate, Space> territory in opponentTerritory) {
                Piece threateningPiece = territory.Value.OccupyingPiece;
                foreach (List<Coordinate> threatDirection in threateningPiece.ThreatCollide) {

                    // If the opposing piece has the current player's king in it's line of threat, it will return as being in check
                    if (threatDirection.Contains(curPlayerKingPosition)) return true;
                }
            }
            return false;
        }
	}
}
