using Chess.Model;
using Chess.Model.Ranks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Control
{
    public delegate void SpacePassantDelegate();
    public delegate void GameWonDelegate(Player winner);

    [Serializable]
    public class GameBoard
    {
        // -- Turn event, all Spaces subscribe to it, currently used for en passant

        [field:NonSerialized]
        public event SpacePassantDelegate TurnStep;
        [field: NonSerialized]
        public event GameWonDelegate GameWon;
		public int Turn { get; set; } = 0;
		public List<Move> MoveHistory { get; set; }
		public Player White { get; set; } = new Player();
		public Player Black { get; set; } = new Player();
		public Dictionary<Coordinate, Space> gameGrid = new Dictionary<Coordinate, Space>();
		public Space GetSquare(int column, int row)
		{
			return gameGrid.Where(square => square.Key == new Coordinate(column, row)).First().Value;
		}
		public Space GetSquare(Coordinate coords)
		{
			return gameGrid.Where(square => square.Key == coords).First().Value;
		}
		public List<Move> moveHistory = new List<Move>();
		public GameBoard()
		{
			ResetBoard();
            SubscribeEvents();
			White.Board = this;
			Black.Board = this;
		}

        public void SubscribeEvents() {
            TurnStep += IncrimentTurn;
        }

        public void ResetBoard()
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
					gameGrid.Add(coords, new Space(this));
				}
			}

			//These two methods do exactly the same thing in different places... 
			//I probably could have made it one method with a little math, but I was tired.
			PopulatePlayerOne();
			PopulatePlayerTwo();
		}

		private void PopulatePlayerOne()
		{
			//Runs through every column and populates the correct rows with pawns, or other pieces based on a switch.
			for (int column = 0; column<8; column++)
			{
				Pawn pawn = new Pawn(0, White);
				White.Pieces.Add(pawn);
				Coordinate square = gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 1).First().Key;
				gameGrid[square].OccupyingPiece = pawn;
				pawn.CurrentPosition = square;
				square = new Coordinate(column, 0);
				switch (column)
				{
					case 0:
					case 7:
						Rook rook = new Rook(0, White);
						White.Pieces.Add(rook);
						rook.CurrentPosition = square;
						gameGrid.Where(space => space.Key.Column==column && space.Key.Row==0).First().Value.OccupyingPiece = rook;
						break;
					case 1:
					case 6:
						Knight knight = new Knight(0, White);
						White.Pieces.Add(knight);
						knight.CurrentPosition = square;
						gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 0).First().Value.OccupyingPiece = knight;
						break;
					case 2:
					case 5:
						Bishop bishop = new Bishop(0, White);
						White.Pieces.Add(bishop);
						bishop.CurrentPosition = square;
						gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 0).First().Value.OccupyingPiece = bishop;
						break;
					case 4:
						King king = new King(0, White);
						White.Pieces.Add(king);
						White.King = king;
						king.CurrentPosition = square;
						gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 0).First().Value.OccupyingPiece = king;
						break;
					case 3:
						Queen queen = new Queen(0, White);
						White.Pieces.Add(queen);
						queen.CurrentPosition = square;
						gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 0).First().Value.OccupyingPiece = queen;
						break;
				}
			}
		}

		private void PopulatePlayerTwo()
		{
			for (int column = 0; column < 8; column++)
			{
				Pawn pawn = new Pawn(1, Black);
				Black.Pieces.Add(pawn);
				pawn.CurrentPosition = new Coordinate(column, 6);
				gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 6).First().Value.OccupyingPiece = pawn;
				switch (column)
				{
					case 0:
					case 7:
						Rook rook = new Rook(1, Black);
						Black.Pieces.Add(rook);
						rook.CurrentPosition = new Coordinate(column, 7);
						gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 7).First().Value.OccupyingPiece = rook;
						break;
					case 1:
					case 6:
						Knight knight = new Knight(1, Black);
						Black.Pieces.Add(knight);
						knight.CurrentPosition = new Coordinate(column, 7);
						gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 7).First().Value.OccupyingPiece = knight;
						break;
					case 2:
					case 5:
						Bishop bishop = new Bishop(1, Black);
						Black.Pieces.Add(bishop);
						bishop.CurrentPosition = new Coordinate(column, 7);
						gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 7).First().Value.OccupyingPiece = bishop;
						break;
					case 4:
						King king = new King(1, Black);
						Black.King = king;
						king.CurrentPosition = new Coordinate(column, 7);
						Black.Pieces.Add(king);
						gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 7).First().Value.OccupyingPiece = king;
						break;
					case 3:
						Queen queen = new Queen(1, Black);
						Black.Pieces.Add(queen);
						queen.CurrentPosition = new Coordinate(column, 7);
						gameGrid.Where(space => space.Key.Column == column && space.Key.Row == 7).First().Value.OccupyingPiece = queen;
						break;
				}
			}
		}

		public Move MovePiece(Move move)
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

					//Quick castling check
					if (move.PieceMoved is King k)
					{
						List<Piece> validCastleTargets = k.ValidCastleTargets;
						if (validCastleTargets != null &&
							validCastleTargets.Count > 0 &&
							validCastleTargets.Any(rook => rook.CurrentPosition == move.EndCoordinate))
						{
							//Fetch the elligable rook
							Rook r = (Rook)validCastleTargets.Where(rook => rook.CurrentPosition == move.EndCoordinate).First();

							//Get the vector from the king to the rook.
							Coordinate vector = Coordinate.GetVector(k.CurrentPosition, r.CurrentPosition);

							//King always moves two spaces
							GetSquare(k.CurrentPosition).OccupyingPiece = null;
							k.CurrentPosition += vector;
							k.CurrentPosition += vector;
							GetSquare(k.CurrentPosition).OccupyingPiece = k;

							//Place rook on the opposite side of the king.
							GetSquare(r.CurrentPosition).OccupyingPiece = null;
							r.CurrentPosition = k.CurrentPosition - vector;
							GetSquare(r.CurrentPosition).OccupyingPiece = r;

							move.PieceMoved.HasMoved = true;
							r.HasMoved = true;

							//while (GetSquare(k.CurrentPosition+vector).OccupyingPiece!=r) //Check that the king and rook are not already adjacent.
							//{
							//	//Move the king one square towards the rook.
							//	Coordinate kNextPosition = k.CurrentPosition+vector;
							//	GetSquare(k.CurrentPosition).OccupyingPiece = null;
							//	GetSquare(kNextPosition).OccupyingPiece = k;
							//	k.CurrentPosition = kNextPosition;

							//	//Perform the same adjacency check and if they are not adjacent, move the rook. 
							//	if (GetSquare(k.CurrentPosition + vector).OccupyingPiece != r)
							//	{
							//		Coordinate rNextPosition = r.CurrentPosition - vector;
							//		GetSquare(r.CurrentPosition).OccupyingPiece = null;
							//		GetSquare(rNextPosition).OccupyingPiece = r;
							//		r.CurrentPosition = rNextPosition;
							//	}
							//}
							////Store the final positions of each piece.
							//Coordinate rCurrent = r.CurrentPosition;
							//Coordinate kCurrent = k.CurrentPosition;

							////Invert the two.
							//GetSquare(rCurrent).OccupyingPiece = k;
							//GetSquare(kCurrent).OccupyingPiece = r;

							moveHistory.Add(move);
							return move;
						}
					}

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
							if (GetSquare(move.EndCoordinate).OccupyingPiece != null)
							{
								move.PieceTaken = GetSquare(move.EndCoordinate).OccupyingPiece;
								move.PieceTaken.CurrentPosition = null;
								move.PieceTaken.OwningPlayer.Graveyard.Add(move.PieceTaken);
								move.PieceTaken.OwningPlayer.Pieces.Remove(move.PieceTaken);
							}

							//If we're here, then all the checks have passed and we can move the piece.

							GetSquare(move.StartCoordinate).OccupyingPiece = null;
							GetSquare(move.EndCoordinate).OccupyingPiece = move.PieceMoved;
							move.PieceMoved.CurrentPosition = move.EndCoordinate;
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


			move.ErrorMessage = moveError;
			if (move.ErrorMessage == null)
				moveHistory.Add(move);
			return move;
		}

		public void IncrimentTurn()
		{
			Turn++;
			CheckMateValidation();
		}

        public bool KingCheck(int curPlayerToCheck) {

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

		public void CheckMateValidation()
		{
			/*
			 * If both players only have one piece left, it must be the king.
			 * In instances where the only remaining pieces are kings, the game is considered a draw.
			 * In a draw, we call the GameWon method and pass in null.
			 */
			if (White.Pieces.Count == 1 && Black.Pieces.Count == 1)
			{
				GameWon(null);
			}
			else
			{
				if (!White.Pieces.Any(piece => //If white has no pieces,
					piece.ValidRangeOfMotion.Any(vector => //Which have any valid vector,
					vector.Count > 0))) //With at least one move, then they have lost.
				{
					GameWon(Black);
				}  //Perform the same check on Black.
				else if (!White.Pieces.Any(piece => //If black has no pieces,
					piece.ValidRangeOfMotion.Any(vector => //Which have any valid vector,
					vector.Count > 0))) //With at least one move, then they have lost.
				{
					GameWon(White);
				} //Otherwise this method does nothing, as the game is not yet over.
			}
		}
	}
}
