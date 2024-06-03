using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessBoard;
using ChessGame;

namespace Chess
{
    class Screen
    {
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Row; i++)
            {
                Console.Write(8-i + " ");
                for (int j = 0; j < board.Column; j++)
                {
                    PrintPiece(board.GetPiece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h ");
        }

        public static void PrintBoard(Board board, bool[,] possibleMoves)
        {
            ConsoleColor originalBg = Console.BackgroundColor;
            ConsoleColor PossibleMoveBg = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Row; i++)
            {
                Console.BackgroundColor = originalBg;
                Console.Write(8-i + " ");

                for (int j = 0; j < board.Column; j++)
                {
                    if (possibleMoves[i, j])
                    {
                        Console.BackgroundColor = PossibleMoveBg;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBg;
                    }
                    PrintPiece(board.GetPiece(i, j));
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = originalBg;
            Console.WriteLine("  a b c d e f g h ");
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = color;
                }
                Console.Write(" ");
            }
        }
        public static ChessPosition ReadPosition()
        {
            string s = Console.ReadLine();
            char col = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPosition(col, row);
        }
    }
}
