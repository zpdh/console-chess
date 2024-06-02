﻿using ChessBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    internal class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "P ";
        }
    }
}
