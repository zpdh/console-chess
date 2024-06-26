﻿using ChessBoard;
using ChessBoard.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
        public bool Check { get; private set; }
        public Piece VulnToEnPassant { get; private set; }
        private HashSet<Piece> Pieces;
        private HashSet<Piece> CaptedPieces;


        public Match()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            IsFinished = false;
            Check = false;
            Pieces = new HashSet<Piece>();
            CaptedPieces = new HashSet<Piece>();
            VulnToEnPassant = null;
            PrintPieces();
        }

        private Color Opponent(Color color)
        {
            if (color == Color.White) return Color.Black;
            else return Color.White;
        }

        private Piece King(Color color)
        {
            foreach (Piece piece in PiecesInGame(color))
            {
                if (piece is King) return piece;
            }
            return null;
        }

        public bool isInCheck(Color color)
        {
            Piece K = King(color);
            if (K == null) throw new BoardException($"There's no {color} King in the board!");

            foreach(Piece piece in PiecesInGame(Opponent(color)))
            {
                bool[,] arr = piece.PossibleMoves();
                if (arr[K.Position.Row, K.Position.Column]) return true;
            }
            return false;
        }

        public bool TestCheckmate(Color color)
        {
            if (!isInCheck(color)) return false;
            foreach (Piece piece in PiecesInGame(color))
            {
                bool[,] mArr = piece.PossibleMoves();
                for (int i = 0; i < Board.Row; i++)
                {
                    for (int j = 0; j < Board.Column; j++)
                    {
                        if (mArr[i,j])
                        {
                            Position origin = piece.Position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = Move(origin, destination);
                            bool testCheck = isInCheck(color);
                            UndoMove(origin, destination, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public Piece Move(Position origin, Position destination)
        {

            bool[,] possiblePositions = Board.GetPiece(origin).PossibleMoves();
            if (possiblePositions[destination.Row, destination.Column])
            {
                Piece piece = Board.RemovePiece(origin);
                piece.incrementMoveQtt();
                Piece capturedPiece = Board.RemovePiece(destination);
                Board.AddPiece(piece, destination);
                if (capturedPiece != null)
                {
                    CaptedPieces.Add(capturedPiece);
                }

                // #specialmove Short Castle
                if (piece is King && destination.Column == origin.Column + 2)
                {
                    Position rookOrigin = new Position(origin.Row, origin.Column + 3);
                    Position rookDest = new Position(origin.Row, origin.Column + 1);
                    Piece rook = Board.RemovePiece(rookOrigin);
                    Board.AddPiece(rook, rookDest);
                    rook.incrementMoveQtt();
                }

                // #specialmove Long Castle
                if (piece is King && destination.Column == origin.Column - 2)
                {
                    Position rookOrigin = new Position(origin.Row, origin.Column - 4);
                    Position rookDest = new Position(origin.Row, origin.Column - 1);
                    Piece rook = Board.RemovePiece(rookOrigin);
                    rook.incrementMoveQtt();
                    Board.AddPiece(rook, rookDest);
                }

                // #specialmove En Passant
                if (piece is Pawn)
                {
                    if (origin.Column != destination.Column && capturedPiece == null)
                    {
                        Position capPos;
                        if (piece.Color == Color.White)
                        {
                            capPos = new Position(destination.Row +1, destination.Column);
                        }
                        else
                        {
                            capPos = new Position(destination.Row -1, destination.Column);
                        }
                        capturedPiece = Board.RemovePiece(capPos);
                        CaptedPieces.Add(capturedPiece);
                    }
                }

                return capturedPiece;
            }
            else throw new BoardException("This piece can't move there silly!");
        }

        public void UndoMove(Position origin, Position destination, Piece capturedPiece)
        {
            Piece piece = Board.RemovePiece(destination);
            piece.decrementMoveQtt();
            if (capturedPiece != null)
            {
                Board.AddPiece(capturedPiece, destination);
                CaptedPieces.Remove(capturedPiece);
            }
            Board.AddPiece(piece, origin);

            // #specialmove Short Castle
            if (piece is King && destination.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column + 3);
                Position rookDest = new Position(origin.Row, origin.Column + 1);
                Piece rook = Board.RemovePiece(rookDest);
                rook.decrementMoveQtt();
                Board.AddPiece(rook, rookOrigin);
            }

            // #specialmove Long Castle
            if (piece is King && destination.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column - 4);
                Position rookDest = new Position(origin.Row, origin.Column - 1);
                Piece rook = Board.RemovePiece(rookDest);
                rook.decrementMoveQtt();
                Board.AddPiece(rook, rookOrigin);
            }

            // #specialmove En Passant
            if (piece is Pawn)
            {
                if (origin.Column != destination.Column && capturedPiece == VulnToEnPassant)
                {
                    Piece pawn = Board.RemovePiece(destination);
                    Position capPos;
                    if (piece.Color == Color.White)
                    {
                        capPos = new Position(3, destination.Column);
                    }
                    else
                    {
                        capPos = new Position(4, destination.Column);
                    }
                    Board.AddPiece(pawn, capPos);
                }
            }
        }

        public void TakeTurn(Position origin, Position destination)
        {
            Piece capturedPiece = Move(origin, destination);

            if (isInCheck(CurrentPlayer))
            {
                UndoMove(origin, destination, capturedPiece);
                throw new BoardException("You cannot put yourself in check!");
            }
            Piece piece = Board.GetPiece(destination);

            // #specialmove Promotion
            if (piece is Pawn)
            {
                if (piece.Color == Color.White && destination.Row == 0 || piece.Color == Color.Black && destination.Row == 7)
                {
                    piece = Board.RemovePiece(destination);
                    Pieces.Remove(piece);
                    Piece queen = new Queen(Board, piece.Color);
                    Board.AddPiece(queen, destination);
                    Pieces.Add(queen);
                }
            }

            if (TestCheckmate(Opponent(CurrentPlayer)))
            {
                IsFinished = true;
            }

            if (isInCheck(Opponent(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            Turn++;
            ChangePlayer();


            // #specialmove En Passant

            if (piece is Pawn && destination.Row == origin.Row -2 || destination.Row == origin.Row + 2)
            {
                VulnToEnPassant = piece;
            }
            else
            {
                VulnToEnPassant = null;
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

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> pieces = new HashSet<Piece>();
            foreach (Piece piece in CaptedPieces)
            {
                if (piece.Color == color) pieces.Add(piece);
            }
            return pieces;
        }

        public HashSet<Piece> PiecesInGame(Color color)
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
            PrintNewPiece('a', 1, new Rook(Board, Color.White));
            PrintNewPiece('b', 1, new Knight(Board, Color.White));
            PrintNewPiece('c', 1, new Bishop(Board, Color.White));
            PrintNewPiece('d', 1, new Queen(Board, Color.White));
            PrintNewPiece('e', 1, new King(Board, Color.White, this));
            PrintNewPiece('f', 1, new Bishop(Board, Color.White));
            PrintNewPiece('g', 1, new Knight(Board, Color.White));
            PrintNewPiece('h', 1, new Rook(Board, Color.White));
            PrintNewPiece('a', 2, new Pawn(Board, Color.White, this));
            PrintNewPiece('b', 2, new Pawn(Board, Color.White, this));
            PrintNewPiece('c', 2, new Pawn(Board, Color.White, this));
            PrintNewPiece('d', 2, new Pawn(Board, Color.White, this));
            PrintNewPiece('e', 2, new Pawn(Board, Color.White, this));
            PrintNewPiece('f', 2, new Pawn(Board, Color.White, this));
            PrintNewPiece('g', 2, new Pawn(Board, Color.White, this));
            PrintNewPiece('h', 2, new Pawn(Board, Color.White, this));

            PrintNewPiece('a', 8, new Rook(Board, Color.Black));
            PrintNewPiece('b', 8, new Knight(Board, Color.Black));
            PrintNewPiece('c', 8, new Bishop(Board, Color.Black));
            PrintNewPiece('d', 8, new Queen(Board, Color.Black));
            PrintNewPiece('e', 8, new King(Board, Color.Black, this));
            PrintNewPiece('f', 8, new Bishop(Board, Color.Black));
            PrintNewPiece('g', 8, new Knight(Board, Color.Black));
            PrintNewPiece('h', 8, new Rook(Board, Color.Black));
            PrintNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            PrintNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            PrintNewPiece   ('c', 7, new Pawn(Board, Color.Black, this));
            PrintNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            PrintNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            PrintNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            PrintNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            PrintNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }
    }
}
