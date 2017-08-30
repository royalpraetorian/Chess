using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Chess.Model;
using Chess.Control;

namespace ChessAI {

    abstract public class ComputerPlayer : Player {

        public abstract Move GetNextMove();
    }
}
