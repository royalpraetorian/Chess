using Chess.Model.Ranks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model
{
    public class Space
    {
        public byte passantTimer { get; set; } = 1;

        public Pawn PhantomPawn { get; set; }

        public Piece OccupyingPiece { get; set; }

        public void PhantomCheck()
        {
            if (this.PhantomPawn != null)
            {
                passantTimer++;
                if (passantTimer == 3)
                {
                    PhantomPawn = null;
                    passantTimer = 1;
                }
            }
        }
    }
}
