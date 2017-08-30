using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model.Ranks
{
    [Serializable]
    public class Queen : Piece
    {
        public Queen(int playerNumber, Player player) : base(playerNumber, player)
        {
        }

        public override List<List<Coordinate>> Threat
        {
            get
            {
                List<List<Coordinate>> threat = new List<List<Coordinate>>();
                List<Coordinate> vectors = new List<Coordinate>()
                {
                    new Coordinate(-1,-1),
                    new Coordinate(-1,1),
                    new Coordinate(1,-1),
                    new Coordinate(1,1),
                    new Coordinate(-1,0),
                    new Coordinate(0,-1),
                    new Coordinate(0,1),
                    new Coordinate(1,0),
                };
                foreach (Coordinate vector in vectors)
                {
                    List<Coordinate> directionalThreat = new List<Coordinate>();
                    Coordinate checkPosition = CurrentPosition + vector;
                    bool outOfBounds = false;
                    while (!outOfBounds)
                    {
                        if (OwningPlayer.Board.gameGrid.Where(space => space.Key == checkPosition).Count() > 0)
                        {
                            directionalThreat.Add(checkPosition);
                            checkPosition += vector;
                        }
                        else
                        {
                            outOfBounds = true;
                        }
                    }
                    threat.Add(directionalThreat);
                }
                return threat;
            }
        }

        public override List<List<Coordinate>> RangeOfMotion
        {
            get
            {
                return Threat;
            }
        }
    }
}
