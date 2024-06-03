using System;
using System.Text.RegularExpressions;
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
                ChessGame.Match match = new ChessGame.Match();

                while (!match.IsFinished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(match);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadPosition().ToPosition();
                        match.ValidateOrigin(origin);

                        bool[,] possiblePositions = match.Board.GetPiece(origin).PossibleMoves();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, possiblePositions);
                        Console.WriteLine();

                        Console.Write("Destination:");
                        Position destination = Screen.ReadPosition().ToPosition();

                        match.TakeTurn(origin, destination);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine();
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine();
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                    }
                }
                Console.Clear();
                Screen.PrintMatch(match);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}