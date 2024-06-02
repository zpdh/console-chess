using System;
using ChessBoard;
using ChessBoard.Exceptions;
using ChessGame;


namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board board = new Board(8, 8);

                board.AddPiece(new Rook(board, Color.Black), new Position(0, 1));
                board.AddPiece(new Knight(board, Color.Black), new Position(0, 2));
                board.AddPiece(new Bishop(board, Color.Black), new Position(0, 3));
                board.AddPiece(new Pawn(board, Color.Black), new Position(0, 4));
                board.AddPiece(new Queen(board, Color.Black), new Position(0, 5));
                board.AddPiece(new King(board, Color.Black), new Position(0, 6));

                board.AddPiece(new Rook(board, Color.White), new Position(7, 1));
                board.AddPiece(new Knight(board, Color.White), new Position(7, 2));
                board.AddPiece(new Bishop(board, Color.White), new Position(7, 3));
                board.AddPiece(new Pawn(board, Color.White), new Position(7, 4));
                board.AddPiece(new Queen(board, Color.White), new Position(7, 5));
                board.AddPiece(new King(board, Color.White), new Position(7, 6));

                Screen.PrintBoard(board);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}