using ChessBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    internal class King : Piece
    {
        public King(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "K";
        }

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
            if (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;
            }
            //S
            position.DefineValues(Position.Row +1, Position.Column);
            if (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;
            }
            //W
            position.DefineValues(Position.Row, Position.Column -1);
            if (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;
            }
            //E
            position.DefineValues(Position.Row, Position.Column +1);
            if (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;
            }
            //NE
            position.DefineValues(Position.Row -1, Position.Column +1);
            if (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;
            }
            //NW
            position.DefineValues(Position.Row -1, Position.Column -1);
            if (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;
            }
            //SW
            position.DefineValues(Position.Row +1, Position.Column +1);
            if (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;
            }
            //SE
            position.DefineValues(Position.Row +1, Position.Column -1);
            if (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;
            }

            return mArr;
        }
    }
}
