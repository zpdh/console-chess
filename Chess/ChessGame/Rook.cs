using ChessBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    internal class Rook : Piece
    {
        public Rook(Board board, Color color) : base(board, color) { }

        private bool canMove(Position position)
        {
            Piece piece = Board.GetPiece(position);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mArr = new bool[Board.Row, Board.Column];


            Position position = new Position(0, 0);

            //N
            position.DefineValues(Position.Row -1, Position.Column);
            while (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;

                if (Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color) break;
                position.Row -= 1;
            }
            //S
            position.DefineValues(Position.Row +1, Position.Column);
            while (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;

                if (Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color) break;
                position.Row += 1;
            }
            //E
            position.DefineValues(Position.Row, Position.Column +1);
            while (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;

                if (Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color) break;
                position.Column += 1;
            }
            //W
            position.DefineValues(Position.Row, Position.Column -1);
            while (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;

                if (Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color) break;
                position.Column -= 1;
            }

            return mArr;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
