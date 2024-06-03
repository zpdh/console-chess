using ChessBoard;
using ChessBoard.Exceptions;
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
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool IsFinished { get; private set; }
        private HashSet<Piece> Pieces;
        private HashSet<Piece> CaptedPieces;


        public Match()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            IsFinished = false;
            Pieces = new HashSet<Piece>();
            CaptedPieces = new HashSet<Piece>();
            PrintPieces();
        }

        public void Move(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.incrementMoveQtt();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.AddPiece(piece, destination);
            if (capturedPiece != null)
            {
                CaptedPieces.Add(capturedPiece);
            }
        }

        public void ValidateOrigin(Position origin)
        {
            if (Board.GetPiece(origin) == null) throw new BoardException("There is no piece in the origin position.");
            else if (CurrentPlayer != Board.GetPiece(origin).Color) throw new BoardException("This piece is not yours silly!");
            else if (!Board.GetPiece(origin).CheckForPossibleMoves()) throw new BoardException("This piece has no possible moves.");
        }

        private void ChangePlayer()
        {
            switch (CurrentPlayer)
            {
                case Color.White:
                    CurrentPlayer = Color.Black;
                    break;
                    case Color.Black:
                    CurrentPlayer = Color.White;
                    break;
            }
        }

        public void TakeTurn(Position origin, Position destination)
        {
            Move(origin, destination);
            Turn++;
            ChangePlayer();
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> pieces = new HashSet<Piece>();
            foreach (Piece piece in CaptedPieces)
            {
                if (piece.Color == color) pieces.Add(piece);
            }
            return pieces;
        }

        public HashSet<Piece> PiecesInGame (Color color)
        {
            HashSet<Piece> pieces = new HashSet<Piece>();
            foreach (Piece piece in Pieces)
            {
                if (piece.Color == color)
                {
                    pieces.Add(piece);
                }
            }
            pieces.ExceptWith(CapturedPieces(color));
            return pieces;
        }

        public void PrintNewPiece(char column, int row, Piece piece)
        {
            Board.AddPiece(piece, new ChessPosition(column, row).ToPosition());
            Pieces.Add(piece);
        }

        private void PrintPieces()
        {
            for (int i = 1; i <= 8; i++)
            {
                PrintNewPiece((char)(i+96), 7, new Rook(Board, Color.Black));
                PrintNewPiece((char)(i+96), 2, new Rook(Board, Color.White));
            }
            PrintNewPiece('c', 8, new Rook(Board, Color.Black));
            PrintNewPiece('e', 8, new Rook(Board, Color.Black));
            PrintNewPiece('d', 8, new King(Board, Color.Black));

            PrintNewPiece('c', 1, new Rook(Board, Color.White));
            PrintNewPiece('e', 1, new Rook(Board, Color.White));
            PrintNewPiece('d', 1, new Rook(Board, Color.White));
        }
    }
}
