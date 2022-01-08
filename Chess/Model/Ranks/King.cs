using Chess.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model.Ranks
{
    [Serializable]
    public class King : Piece
	{
		public King(int playerNumber, Player player) : base(playerNumber, player)
		{
		}

		/// <summary>
		/// A list of rooks the king can castle with.
		/// </summary>
		public List<Piece> ValidCastleTargets
		{
			get
			{
				//A king can only castle if it has not moved.
				if (!HasMoved && !Threatened) //A king can only castle if it is not threatened.
				{
					//Get the player's castles.
					List<Piece> rooks = OwningPlayer.Pieces.Where(piece => piece.GetType().Equals(typeof(Rook))).ToList();

					//Make sure the player still has castles left.
					if (rooks.Count > 0)
					{
						//A king can only castle if the rook it is attempting to castle with has not been moved.
						//Remove all castles from the list if they aren't valid.
						foreach(Piece rook in rooks)
						{
							if (rook.HasMoved)
								rooks.Remove(rook);
						}

						List<Piece> validRooks = new List<Piece>();

						//Check if any are left
						if (rooks.Count > 0)
						{
							//This loop makes sure that the path is clear between the king and the rook.
							foreach(Piece rook in rooks)
							{
								bool validRook = true;
								//Get the vector from the king to the rook.
								Coordinate vector = Coordinate.GetVector(CurrentPosition, rook.CurrentPosition);

								//Move in that direction and check every space to see if it is threatened.
								Coordinate checkPosition = CurrentPosition + vector;
								while(checkPosition!=rook.CurrentPosition)
								{
									//Find any pieces which threaten this space.
									List<Piece> threats = new List<Piece>();
									if (OwningPlayer == OwningPlayer.Board.White)
									{
										threats = OwningPlayer.Board.Black.Pieces.Where(piece => //Get the opposing player's pieces.
											piece.ThreatCollide.Where(v => v.Contains(checkPosition) //Get each vector of each piece's threat.
											).Count() > 0
										).ToList();
									}
									else
									{
										threats = OwningPlayer.Board.White.Pieces.Where(piece => //Get the opposing player's pieces.
											piece.ThreatCollide.Where(v => v.Contains(checkPosition) //Get each vector of each piece's threat.
											).Count() > 0
										).ToList();
									}
									//If there are any threats, the rook is not valid.
									if (threats.Count!=0)
									{
										validRook = false;
										break;
									}

									//Incriment the position.
									checkPosition += vector;
								}
								//Check that the king is in the rook's ThreatCollide.
								if (rook.ThreatCollide.Where(v => v.Contains(CurrentPosition)).Count() == 0)
									validRook = false;

								//Remove the rook if it's invalid.
								if (validRook)
									validRooks.Add(rook);
							}
							return validRooks;
						}
						else return null;
					}
					else return null;
				}
				else return null;
			}
		}

		public override List<List<Coordinate>> Threat
		{
			get
			{
				List<List<Coordinate>> threatRange = new List<List<Coordinate>>()
				{
					{ OwningPlayer.Board.gameGrid.Where(space => space.Key - CurrentPosition == new Coordinate(-1, 1)).Select(space => space.Key).ToList() },
					{ OwningPlayer.Board.gameGrid.Where(space => space.Key - CurrentPosition == new Coordinate(-1, -1)).Select(space => space.Key).ToList() },
					{ OwningPlayer.Board.gameGrid.Where(space => space.Key - CurrentPosition == new Coordinate(1, 1)).Select(space => space.Key).ToList() },
					{ OwningPlayer.Board.gameGrid.Where(space => space.Key - CurrentPosition == new Coordinate(1, -1)).Select(space => space.Key).ToList() },
					{ OwningPlayer.Board.gameGrid.Where(space => space.Key - CurrentPosition == new Coordinate(-1, 0)).Select(space => space.Key).ToList() },
					{ OwningPlayer.Board.gameGrid.Where(space => space.Key - CurrentPosition == new Coordinate(1, 0)).Select(space => space.Key).ToList() },
					{ OwningPlayer.Board.gameGrid.Where(space => space.Key - CurrentPosition == new Coordinate(0, 1)).Select(space => space.Key).ToList() },
					{ OwningPlayer.Board.gameGrid.Where(space => space.Key - CurrentPosition == new Coordinate(0, -1)).Select(space => space.Key).ToList() },
				};
				return threatRange;
			}
		}

		public override List<List<Coordinate>> RangeOfMotion
		{
			get
			{
				return Threat;
			}
		}

		//King's range of motion only has to check that the square he's moving to isn't in check.
		public override List<List<Coordinate>> ValidRangeOfMotion
		{
			get
			{
				//Create a new return value.
				List<List<Coordinate>> validVectors = new List<List<Coordinate>>();
				foreach(List<Coordinate> vector in RangeOfMotionCollide)
				{
					List<Coordinate> validMoves = new List<Coordinate>();
					foreach(Coordinate move in vector)
					{
						//Check which player this piece belongs to.
						if (OwningPlayer == OwningPlayer.Board.White)
						{
							//Check each of the opposing player's pieces to see if any of them have the space in their ThreatCollide.
							List<Piece> blockingPieces = OwningPlayer.Board.Black.Pieces.Where(piece =>
								piece.ThreatCollide.Where(lineOfSight =>
									lineOfSight.Contains(move)
								).Count() > 0
							).ToList();
							if (blockingPieces.Count == 0)
								validMoves.Add(move);
						}
						else //If not white, must be black.
						{
							//Check each of the opposing player's pieces to see if any of them have the space in their ThreatCollide.
							List<Piece> blockingPieces = OwningPlayer.Board.White.Pieces.Where(piece =>
								piece.ThreatCollide.Where(lineOfSight =>
									lineOfSight.Contains(move)
								).Count() > 0
							).ToList();
							if (blockingPieces.Count == 0)
								validMoves.Add(move);
						}
					}

					validVectors.Add(validMoves);
				}

				//Check if we can castle.
				if (ValidCastleTargets != null && ValidCastleTargets.Count > 0)
				{
					foreach (Piece p in ValidCastleTargets)
					{
						validVectors.Add(new List<Coordinate>() { p.CurrentPosition });
					}
				}


				return validVectors;
			}
		}

		public override List<List<Coordinate>> ValidThreat
		{
			get
			{
				//Create a new return value.
				List<List<Coordinate>> validVectors = new List<List<Coordinate>>();
				foreach (List<Coordinate> vector in ThreatCollide)
				{
					List<Coordinate> validMoves = new List<Coordinate>();
					foreach (Coordinate move in vector)
					{
						//Check which player this piece belongs to.
						if (OwningPlayer == OwningPlayer.Board.White)
						{
							//Check each of the opposing player's pieces to see if any of them have the space in their ThreatCollide.
							List<Piece> blockingPieces = OwningPlayer.Board.Black.Pieces.Where(piece =>
								piece.ThreatCollide.Where(lineOfSight =>
									lineOfSight.Contains(move)
								).Count() > 0
							).ToList();
							if (blockingPieces.Count == 0)
								validMoves.Add(move);
						}
						else //If not white, must be black.
						{
							//Check each of the opposing player's pieces to see if any of them have the space in their ThreatCollide.
							List<Piece> blockingPieces = OwningPlayer.Board.White.Pieces.Where(piece =>
								piece.ThreatCollide.Where(lineOfSight =>
									lineOfSight.Contains(move)
								).Count() > 0
							).ToList();
							if (blockingPieces.Count == 0)
								validMoves.Add(move);
						}
					}
					validVectors.Add(validMoves);
				}
				return validVectors;
			}
		}
	}
}
