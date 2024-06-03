using ChessBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    internal class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color) { }

        private bool canMove(Position position)
        {
            Piece piece = Board.GetPiece(position);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mArr = new bool[Board.Row, Board.Column];


            Position position = new Position(0, 0);

            position.DefineValues(Position.Row -1, Position.Column -2);
            if (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;
            }
            position.DefineValues(Position.Row -2, Position.Column -1);
            if (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;
            }
            position.DefineValues(Position.Row +1, Position.Column +2);
            if (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;
            }
            position.DefineValues(Position.Row +2, Position.Column +1);
            if (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;
            }
            position.DefineValues(Position.Row -1, Position.Column +2);
            if (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;
            }
            position.DefineValues(Position.Row +2, Position.Column -1);
            if (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;
            }
            position.DefineValues(Position.Row +1, Position.Column -2);
            if (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;
            }
            position.DefineValues(Position.Row -2, Position.Column +1);
            if (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;
            }

            return mArr;
                }

        public override string ToString()
        {
            return "N";
        }
    }
}
