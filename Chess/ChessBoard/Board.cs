using ChessBoard.Exceptions;
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

        public Piece GetPiece(Position position)
        {
            return Pieces[position.Row, position.Column];
        }

        public bool PieceExists(Position position)
        {
            ValidatePosition(position);
            return GetPiece(position) != null;
        }

        public void AddPiece(Piece piece, Position position)
        {
            if (PieceExists(position))
            {
                throw new BoardException("There's already a piece in this position.");
            }

            Pieces[position.Row, position.Column] = piece;
            piece.Position = position;
        }

        public bool ValidPosition(Position position)
        {
            if (position.Row < 0 || position.Row >= Row || position.Column < 0 || position.Column >= Column) return false;
            return true;
        }

        public void ValidatePosition(Position position)
        {
            if (!ValidPosition(position))
            {
                throw new BoardException("Invalid Position.");
            }
        }
    }
}
