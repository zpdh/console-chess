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
        private Match Match;

        public Pawn(Board board, Color color, Match match) : base(board, color)
        {
            Match = match;
        }

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

                // #specialmove En Passant
                if (Position.Row == 3)
                {
                    Position left = new Position(Position.Row, Position.Column -1);
                    if (Board.ValidPosition(left) && IsEnemy(left) && Board.GetPiece(left) == Match.VulnToEnPassant)
                    {
                        mArr[left.Row -1, left.Column] = true;
                    }
                }

                if (Position.Row == 3)
                {
                    Position right = new Position(Position.Row, Position.Column +1);
                    if (Board.ValidPosition(right) && IsEnemy(right) && Board.GetPiece(right) == Match.VulnToEnPassant)
                    {
                        mArr[right.Row -1, right.Column] = true;
                    }
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

                // #specialmove En Passant
                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Column -1);
                    if (Board.ValidPosition(left) && IsEnemy(left) && Board.GetPiece(left) == Match.VulnToEnPassant)
                    {
                        mArr[left.Row + 1, left.Column] = true;
                    }
                }

                if (Position.Row == 4)
                {
                    Position right = new Position(Position.Row, Position.Column +1);
                    if (Board.ValidPosition(right) && IsEnemy(right) && Board.GetPiece(right) == Match.VulnToEnPassant)
                    {
                        mArr[right.Row + 1, right.Column] = true;
                    }
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
