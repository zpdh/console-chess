using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Board
{
    internal class Board
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Piece[,] Pieces;

        public Board(int row, int colum)
        {
            Row = row;
            Column = colum;
            Pieces = new Piece[row, colum];
        }
    }
}
