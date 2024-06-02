using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoard
{
    class Board
    {
        public int Row { get; set; }
        public int Column { get; set; }
        private Piece[,] Pieces;

        public Board(int row, int colum)
        {
            Row = row;
            Column = colum;
            Pieces = new Piece[row, colum];
        }

        public Piece GetPiece(int row, int colum)
        {
            return Pieces[row, colum];
        }
    }
}
