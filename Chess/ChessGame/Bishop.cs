using ChessBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    internal class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color) { }

        private bool canMove(Position position)
        {
            Piece piece = Board.GetPiece(position);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mArr = new bool[Board.Row, Board.Column];


            Position position = new Position(0, 0);

            //NW
            position.DefineValues(Position.Row -1, Position.Column -1);
            while (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;

                if (Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color) break;
                position.DefineValues(position.Row -1, position.Column -1);
            }
            //NE
            position.DefineValues(Position.Row -1, Position.Column +1);
            while (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;

                if (Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color) break;
                position.DefineValues(position.Row -1, position.Column +1);
            }
            //SW
            position.DefineValues(Position.Row +1, Position.Column -1);
            while (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;

                if (Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color) break;
                position.DefineValues(position.Row +1, position.Column -1);
            }
            //SE
            position.DefineValues(Position.Row +1, Position.Column +1);
            while (Board.ValidPosition(position) && canMove(position))
            {
                mArr[position.Row, position.Column] = true;

                if (Board.GetPiece(position) != null && Board.GetPiece(position).Color != Color) break;
                position.DefineValues(position.Row +1, position.Column +1);
            }

            return mArr;
        }

        public override string ToString()
        {
            return "B";
        }
    }
}

