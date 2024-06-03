using ChessBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    internal class Match
    {
        public Board Board { get; private set; }
        private int Turn;
        private Color CurrentPlayer;
        public bool IsFinished { get; private set; }

        public Match()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            IsFinished = false;
            PrintPieces();
        }

        public void Move(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.incrementMoveQtt();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.AddPiece(piece, destination);
        }

        private void PrintPieces()
        {
            for (int i = 1; i <= 8; i++)
            {
                Board.AddPiece(new Rook(Board, Color.Black), new ChessPosition((char)(i+96), 7).ToPosition());
                Board.AddPiece(new Rook(Board, Color.White), new ChessPosition((char)(i+96), 2).ToPosition());
            }

            Board.AddPiece(new King(Board, Color.Black), new ChessPosition((char)(4+96), 8).ToPosition());
            Board.AddPiece(new King(Board, Color.White), new ChessPosition((char)(4+96), 1).ToPosition());
        }
    }
}
