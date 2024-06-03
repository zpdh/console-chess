using ChessBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    internal class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color) { }

        private bool canMove(Position position)
        {
            Piece piece = Board.GetPiece(position);
            return piece == null || piece.Color != Color;
        }

        private bool IsEnemy(Position position)
        {
            Piece piece = Board.GetPiece(position);
            return piece != null && piece.Color != Color;
        }

        private bool isFree(Position position)
        {
            return Board.GetPiece(position) == null;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mArr = new bool[Board.Row, Board.Column];

            Position position = new Position(0, 0);

            if (this.Color == Color.White)
            {
                position.DefineValues(Position.Row -1, Position.Column);
                if (Board.ValidPosition(position) && isFree(position))
                {
                    mArr[position.Row, position.Column] = true;
                }
                position.DefineValues(Position.Row -2, Position.Column);
                if (Board.ValidPosition(position) && MoveQtt == 0)
                {
                    mArr[position.Row, position.Column] = true;
                }
                position.DefineValues(Position.Row -1, Position.Column -1);
                if (Board.ValidPosition(position) && IsEnemy(position))
                {
                    mArr[position.Row, position.Column] = true;
                }
                position.DefineValues(Position.Row -1, Position.Column +1);
                if (Board.ValidPosition(position) && IsEnemy(position))
                {
                    mArr[position.Row, position.Column] = true;
                }
            }
            else
            {
                position.DefineValues(Position.Row +1, Position.Column);
                if (Board.ValidPosition(position) && isFree(position))
                {
                    mArr[position.Row, position.Column] = true;
                }
                position.DefineValues(Position.Row +2, Position.Column);
                if (Board.ValidPosition(position) && MoveQtt == 0)
                {
                    mArr[position.Row, position.Column] = true;
                }
                position.DefineValues(Position.Row +1, Position.Column -1);
                if (Board.ValidPosition(position) && IsEnemy(position))
                {
                    mArr[position.Row, position.Column] = true;
                }
                position.DefineValues(Position.Row +1, Position.Column +1);
                if (Board.ValidPosition(position) && IsEnemy(position))
                {
                    mArr[position.Row, position.Column] = true;
                }
            }

            return mArr;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
