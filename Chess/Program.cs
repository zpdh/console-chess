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

                board.AddPiece(new Rook(board, Color.Black), new Position(0, 0));
                board.AddPiece(new Knight(board, Color.White), new Position(0, 1));
                board.AddPiece(new Bishop(board, Color.Black), new Position(0, 2));
                board.AddPiece(new Pawn(board, Color.Black), new Position(0, 3));
                board.AddPiece(new Queen(board, Color.Black), new Position(0, 4));
                board.AddPiece(new King(board, Color.Black), new Position(0, 5));
                Screen.PrintBoard(board);

                ChessPosition pos = new ChessPosition('c', 7);
                Console.WriteLine(pos);

                Console.WriteLine(pos.ToPosition());
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}