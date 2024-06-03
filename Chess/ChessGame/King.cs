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
        private Match Match;

        public King(Board board, Color color, Match match) : base(board, color) {
            Match = match;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool canMove(Position position)
        {
            Piece piece = Board.GetPiece(position);
            return piece == null || piece.Color != Color;
        }

        private bool CanCastle(Position position)
        {
            Piece piece = Board.GetPiece(position);
            return piece != null && piece.Color == Color && piece is Rook && piece.MoveQtt == 0;
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

            // #specialmove (Castling)

            if(MoveQtt == 0 && !Match.Check)
            {
                // #specialmove Short Castle
                Position posRook1 = new Position(Position.Row, Position.Column + 3);
                if (CanCastle(posRook1))
                {
                    Position pos1 = new Position(Position.Row, Position.Column + 1);
                    Position pos2 = new Position(Position.Row, Position.Column + 2);
                   if (Board.GetPiece(pos1) == null && Board.GetPiece(pos2) == null)
                    {
                        mArr[Position.Row, Position.Column + 2] = true;
                    }
                }

                // #specialmove Long Castle
                Position posRook2 = new Position(Position.Row, Position.Column - 4);
                if (CanCastle(posRook2))
                {
                    Position pos1 = new Position(Position.Row, Position.Column - 1);
                    Position pos2 = new Position(Position.Row, Position.Column - 2);
                    Position pos3 = new Position(Position.Row, Position.Column - 3);

                    if (Board.GetPiece(pos1) == null && Board.GetPiece(pos2) == null)
                    {
                        mArr[Position.Row, Position.Column - 2] = true;
                    }
                }
            }


            return mArr;
        }
    }
}
