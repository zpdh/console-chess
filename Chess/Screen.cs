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
        public static bool LegendHasAppeared = false;

        public static void PrintMatch(Match match)
        {
            PrintBoard(match.Board);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Turn: " + match.Turn);
            Console.WriteLine("Awaiting move from " + match.CurrentPlayer);
        }
        public static void PrintCapturedPieces(Match match)
        {
            Console.WriteLine("Captured Pieces: ");
            Console.Write("White: ");
            PrintSet(match.CapturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Black: ");
            ConsoleColor c = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintSet(match.CapturedPieces(Color.Black));
            Console.ForegroundColor = c;
        }

        public static void PrintSet(HashSet<Piece> set)
        {
            Console.Write("[");
            foreach (Piece piece in set)
            {
                Console.Write(piece + " ");
            }
            Console.Write("]");
        }

        public static void PrintBoard(Board board)
        {
            if (!LegendHasAppeared)
            {
                string[] legendElements = new string[board.Row];
                legendElements[0] = "         Legend:";
                legendElements[1] = "         K: King";
                legendElements[2] = "         Q: Queen";
                legendElements[3] = "         R: Rook";
                legendElements[4] = "         N: Knight";
                legendElements[5] = "         B: Bishop";
                legendElements[6] = "         P: Pawn";
                legendElements[7] = "         Origin: Select Piece; Destination: Space to move piece to.";

                for (int i = 0; i < board.Row; i++)
                {
                    Console.Write(8-i + " ");
                    for (int j = 0; j < board.Column; j++)
                    {
                        PrintPiece(board.GetPiece(i, j));
                    }
                    Console.Write(legendElements[i]);
                    Console.WriteLine();
                    Screen.LegendHasAppeared = true;
                }
            }
            else
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
