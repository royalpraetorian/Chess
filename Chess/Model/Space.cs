using Chess.Model.Ranks;
using Chess.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Chess.Model
{
    [Serializable]
    public class Space : INotifyPropertyChanged
    {
		public GameBoard Board;
        public byte PassantTimer { get; set; } = 1;

        public Pawn PhantomPawn { get; set; }

		private Piece occupyingPiece;

		public event PropertyChangedEventHandler PropertyChanged;

		public Piece OccupyingPiece
		{
			get { return occupyingPiece; }
			set
			{
				occupyingPiece = value;
				if (PropertyChanged != null)
					PropertyChanged(this, null);
			}
		}


		public Space(GameBoard board)
        {
			Board = board;
            Board.TurnStep += PhantomCheck;
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
