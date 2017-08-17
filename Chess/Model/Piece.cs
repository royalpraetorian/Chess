using Chess.Control;
using Chess.Model.Ranks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model
{
	public abstract class Piece
	{
		public Player OwningPlayer { get; set; }
		public Coordinate CurrentPosition
		{
			get
			{
				return GameBoard.gameGrid.Where(square => square.Value.OccupyingPiece == this).First().Key;
			}
		}
		public bool HasMoved { get; set; }

		abstract public List<List<Coordinate>> Threat { get; }
		abstract public List<List<Coordinate>> RangeOfMotion { get; }

		public List<List<Coordinate>> ThreatCollide
		{
			get
			{
				List<List<Coordinate>> threatRange = Threat;
				List<List<Coordinate>> threatWithCollision = new List<List<Coordinate>>();
				foreach(List<Coordinate> range in threatRange)
				{
					List<Coordinate> rangeCollide = new List<Coordinate>();
					Coordinate vector;
					if (range.Count > 0)
					{
						vector = (range[0] - CurrentPosition) / (new Coordinate(Math.Abs(range[0].Column - CurrentPosition.Column), Math.Abs(range[0].Row-CurrentPosition.Row)));
						Coordinate checkPosition = CurrentPosition + vector;
						bool outOfBounds = false;
						while (!outOfBounds)
						{
							if (!GameBoard.gameGrid.Keys.Contains(checkPosition))
							{
								outOfBounds = true;
							}
							if (GameBoard.GetSquare(checkPosition).OccupyingPiece == null)
							{
								rangeCollide.Add(checkPosition);
								checkPosition += vector;
							}
							else
							{
								rangeCollide.Add(checkPosition);
								outOfBounds = true;
							}
						}
					}
					threatWithCollision.Add(rangeCollide);
				}
				return threatWithCollision;
			}
		}
		public List<List<Coordinate>> RangeOfMotionCollide
		{
			get
			{
				List<List<Coordinate>> normalRange = RangeOfMotion;
				List<List<Coordinate>> collisionRange = new List<List<Coordinate>>();

				foreach(List<Coordinate> range in normalRange)
				{
					List<Coordinate> rangeCollide = new List<Coordinate>();
					Coordinate vector;
					if (range.Count > 0)
					{
						vector = (range[0] - CurrentPosition) / (new Coordinate(Math.Abs(range[0].Column - CurrentPosition.Column), Math.Abs(range[0].Row - CurrentPosition.Row)));
						Coordinate checkPosition = CurrentPosition + vector;
						bool outOfBounds = false;
						while (!outOfBounds)
						{
							if (!(GameBoard.gameGrid.Where(space => space.Key == checkPosition).Count() > 0))
							{
								outOfBounds = true;
							}
							else if (range.Where(space => space == checkPosition).Count() > 0)
							{
								if (GameBoard.GetSquare(checkPosition).OccupyingPiece == null)
								{
									rangeCollide.Add(checkPosition);
									checkPosition += vector;
								}
								else if (GameBoard.GetSquare(checkPosition).OccupyingPiece.PlayerNumber == this.PlayerNumber)
								{
									outOfBounds = true;
								}
								else
								{
									rangeCollide.Add(checkPosition);
									outOfBounds = true;
								}
							}
							else
								outOfBounds = true;
						}
					}
					collisionRange.Add(rangeCollide);
				}

				return collisionRange;
			}
		}

		public List<List<Coordinate>> ValidRangeOfMotion
		{
			get
			{
				/*
				 * Take in this piece's range of motion including threat.
				 * Treat each piece as though it were a destination.
				 * Run the following checks to determine if it's a valid destination. If yes, add it back to the list.
				 * 1. Check each enemy piece's ThreatCollide vectors to see if they contain this piece, and its king, and not the destination.
				 * 2. For all vectors that pass check 1, make sure the current piece is between the threatening piece and the king, and there are no other pieces between them.
				 *	- Find the vector between the king's position and the threatening piece's. Check from the king to the threatening piece.
				 *	- If there are any pieces other than this piece between the king and the potential threat, this piece can move to that destination.
				 *	- If this piece is not between the king and the potential threat, the move is invalid.
				 *	- If there are no other pieces between the king and the potential threat besides this piece, the move is invalid.
				 */

				//Construct a list of vectors to return.
				List<List<Coordinate>> validMoves = new List<List<Coordinate>>();

				//We should cache a list of all enemy vectors that contain both this piece and its king.
				List<Piece> potentialThreats = GameBoard.gameGrid.Values.Where(space => //Using a linq statement to return only the pieces with possibly threatening vectors.
					space.OccupyingPiece != null && //Make sure the space has a piece.
					space.OccupyingPiece.PlayerNumber != PlayerNumber && //Such that the piece in that space is an enemy piece.
					space.OccupyingPiece.Threat.Where(vector => vector.Contains(CurrentPosition) && //Such that the piece in that space has this piece in its ThreatCollide
					vector.Contains(GameBoard.gameGrid.Values.Where(square => //Such that the piece in that space has this piece's king in its ThreatCollide.
					square.OccupyingPiece.PlayerNumber == PlayerNumber && //Make sure the king is this piece's king.
					square.OccupyingPiece.GetType().Equals(typeof(King)) //Make sure the piece in the space is a king.
					).Select(coord => coord.OccupyingPiece).First().CurrentPosition)).Count() > 0 //Make sure that the list of vectors containing both pieces is greater than zero.
					).Select(space => space.OccupyingPiece).ToList(); //Return only the pieces, and convert the collection to a list.


				//This dictionary stores all valid vectors of threat, and the pieces that create them.
				Dictionary<List<Coordinate>, Piece> validThreats = new Dictionary<List<Coordinate>, Piece>();

				//We need to winnow this collection down to pieces where this piece is between the potential threat and the king.
				foreach(Piece potentialThreat in potentialThreats)
				{
					//Ensure there are no pieces between the potentialThreat and the king other than this piece.
					bool validThreat = true;

					//Iterate through the ThreatCollide of that piece and find the applicable avenues of threat.
					foreach(List<Coordinate> vector in potentialThreat.ThreatCollide)
					{
						//Get a vector that will lead us from the king to the potential threat.
						Coordinate direction = Coordinate.GetVector(OwningPlayer.King.CurrentPosition, potentialThreat.CurrentPosition);
						
						//Initialize a variable to store our current position as we incriment.
						Coordinate checkPosition = OwningPlayer.King.CurrentPosition + direction;

						//We stop when we reach the current position of the threatening peice.
						while (checkPosition!=potentialThreat.CurrentPosition)
						{
							//If we encounter another piece, we incriment 
							if (GameBoard.GetSquare(checkPosition).OccupyingPiece != null && GameBoard.GetSquare(checkPosition).OccupyingPiece != this)
							{
								//If even one piece other than this piece is between the king and the potential threat, it is not a valid threat.
								validThreat = false;
								break;
							}

							checkPosition += direction;
						}

						if (validThreat)
							validThreats.Add(vector, potentialThreat);
					}
				}

				/*
				 * We now have a dictionary of all the vectors that might potentially prevent this piece from moving.
				 * Next we need to check every spot this piece can move to. If that spot isn't contained within EVERY vector of threat, it is not a valid move.
				 */ 

				//We need to iterate through each coordinate in CollisionMoves.
				foreach(List<Coordinate> vector in RangeOfMotionCollide)
				{
					//We need a new list to store all valid moves in a given direction.
					List<Coordinate> vectorWithValidation = new List<Coordinate>();
					foreach(Coordinate potentialDestination in vector)
					{
						bool validMove = true;
						//Iterate through all the valid vectors of threat.
						foreach(KeyValuePair<List<Coordinate>, Piece> threatVector in validThreats)
						{
							//Check that the piece is trying to move outside of the line of threat, and it's not taking the piece generating that line of threat. 
							if (!threatVector.Key.Contains(potentialDestination) && potentialDestination!=threatVector.Value.CurrentPosition)
								validMove = false;
						}

						//If validMove is still true by the end of the above loop, then the potentialDestination has appeared in every threat vector, and is therefore a valid move.
						if (validMove)
							vectorWithValidation.Add(potentialDestination);
					}
					validMoves.Add(vectorWithValidation);
				}

				return validMoves;
			}
		}

		public List<List<Coordinate>> ValidThreat
		{
			get
			{
				/*
				 * Take in this piece's range of motion including threat.
				 * Treat each piece as though it were a destination.
				 * Run the following checks to determine if it's a valid destination. If yes, add it back to the list.
				 * 1. Check each enemy piece's ThreatCollide vectors to see if they contain this piece, and its king, and not the destination.
				 * 2. For all vectors that pass check 1, make sure the current piece is between the threatening piece and the king, and there are no other pieces between them.
				 *	- Find the vector between the king's position and the threatening piece's. Check from the king to the threatening piece.
				 *	- If there are any pieces other than this piece between the king and the potential threat, this piece can move to that destination.
				 *	- If this piece is not between the king and the potential threat, the move is invalid.
				 *	- If there are no other pieces between the king and the potential threat besides this piece, the move is invalid.
				 */

				//Construct a list of vectors to return.
				List<List<Coordinate>> validMoves = new List<List<Coordinate>>();

				//We should cache a list of all enemy vectors that contain both this piece and its king.
				List<Piece> potentialThreats = GameBoard.gameGrid.Values.Where(space => //Using a linq statement to return only the pieces with possibly threatening vectors.
					space.OccupyingPiece != null && //Make sure the space has a piece.
					space.OccupyingPiece.PlayerNumber != PlayerNumber && //Such that the piece in that space is an enemy piece.
					space.OccupyingPiece.Threat.Where(vector => vector.Contains(CurrentPosition) && //Such that the piece in that space has this piece in its ThreatCollide
					vector.Contains(GameBoard.gameGrid.Values.Where(square => //Such that the piece in that space has this piece's king in its ThreatCollide.
					square.OccupyingPiece.PlayerNumber == PlayerNumber && //Make sure the king is this piece's king.
					square.OccupyingPiece.GetType().Equals(typeof(King)) //Make sure the piece in the space is a king.
					).Select(coord => coord.OccupyingPiece).First().CurrentPosition)).Count() > 0 //Make sure that the list of vectors containing both pieces is greater than zero.
					).Select(space => space.OccupyingPiece).ToList(); //Return only the pieces, and convert the collection to a list.

				//This dictionary stores all valid vectors of threat, and the pieces that create them.
				Dictionary<List<Coordinate>, Piece> validThreats = new Dictionary<List<Coordinate>, Piece>();

				//We need to winnow this collection down to pieces where this piece is between the potential threat and the king.
				foreach (Piece potentialThreat in potentialThreats)
				{
					//Ensure there are no pieces between the potentialThreat and the king other than this piece.
					bool validThreat = true;

					//Iterate through the ThreatCollide of that piece and find the applicable avenues of threat.
					foreach (List<Coordinate> vector in potentialThreat.ThreatCollide)
					{
						//Get a vector that will lead us from the king to the potential threat.
						Coordinate direction = Coordinate.GetVector(OwningPlayer.King.CurrentPosition, potentialThreat.CurrentPosition);

						//Initialize a variable to store our current position as we incriment.
						Coordinate checkPosition = OwningPlayer.King.CurrentPosition + direction;

						//We stop when we reach the current position of the threatening peice.
						while (checkPosition != potentialThreat.CurrentPosition)
						{
							//If we encounter another piece, we incriment 
							if (GameBoard.GetSquare(checkPosition).OccupyingPiece != null && GameBoard.GetSquare(checkPosition).OccupyingPiece != this)
							{
								//If even one piece other than this piece is between the king and the potential threat, it is not a valid threat.
								validThreat = false;
								break;
							}

							checkPosition += direction;
						}

						if (validThreat)
							validThreats.Add(vector, potentialThreat);
					}
				}

				/*
				 * We now have a dictionary of all the vectors that might potentially prevent this piece from moving.
				 * Next we need to check every spot this piece can move to. If that spot isn't contained within EVERY vector of threat, it is not a valid move.
				 */

				//We need to iterate through each coordinate in CollisionMoves.
				foreach (List<Coordinate> vector in ThreatCollide)
				{
					//We need a new list to store all valid moves in a given direction.
					List<Coordinate> vectorWithValidation = new List<Coordinate>();
					foreach (Coordinate potentialDestination in vector)
					{
						bool validMove = true;
						//Iterate through all the valid vectors of threat.
						foreach (KeyValuePair<List<Coordinate>, Piece> threatVector in validThreats)
						{
							//Check that the piece is trying to move outside of the line of threat, and it's not taking the piece generating that line of threat.
							if (!threatVector.Key.Contains(potentialDestination) && potentialDestination != threatVector.Value.CurrentPosition)
								validMove = false;
						}

						//If validMove is still true by the end of the above loop, then the potentialDestination has appeared in every threat vector, and is therefore a valid move.
						if (validMove)
							vectorWithValidation.Add(potentialDestination);
					}
					validMoves.Add(vectorWithValidation);
				}

				return validMoves;
			}
		}
		public int PlayerNumber { get; set; }

		public Piece(int playerNumber, Player player)
		{
			PlayerNumber = playerNumber;
			OwningPlayer = player;
		}
	}
}
