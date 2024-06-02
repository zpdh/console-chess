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
                Match match = new Match();

                while (!match.IsFinished)
                {
                    Console.Clear();
                    Screen.PrintBoard(match.Board);

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = Screen.ReadPosition().ToPosition();
                    Console.Write("Destination:");
                    Position destination = Screen.ReadPosition().ToPosition();

                    match.Move(origin, destination);
                }
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}