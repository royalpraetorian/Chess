using Chess.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model
{
	public abstract class Piece
	{
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
				throw new NotImplementedException();
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
				throw new NotImplementedException();
			}
		}
		public int PlayerNumber { get; set; }

		public Piece(int playerNumber)
		{
			PlayerNumber = playerNumber;
		}
	}
}
