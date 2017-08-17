using Chess.Model.Ranks;
using Chess.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model
{
    public class Space
    {
        public byte PassantTimer { get; set; } = 1;

        public Pawn PhantomPawn { get; set; }

        public Piece OccupyingPiece { get; set; }

        public Space()
        {
            GameBoard.TurnStep += PhantomCheck;
        }

        public void PhantomCheck()
        {
            if (this.PhantomPawn != null)
            {
                PassantTimer++;
                if (PassantTimer == 3)
                {
                    PhantomPawn = null;
                    PassantTimer = 1;
                }
            }
        }
    }
}
