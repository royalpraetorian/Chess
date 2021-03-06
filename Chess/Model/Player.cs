﻿using Chess.Control;
using Chess.Model.Ranks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Model
{
    [Serializable]
    public class Player
	{
		public string Name { get; set; }
		public List<Piece> Graveyard { get; set; } = new List<Piece>();
		public bool isHuman { get; set; }
		public List<Piece> Pieces { get; set; } = new List<Piece>();
		public King King { get; set; }
		public GameBoard Board { get; set; }
	}
}
