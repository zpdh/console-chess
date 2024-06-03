using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoard
{
    abstract class Piece
    {
        public Board Board { get; set; }
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MoveQtt { get; protected set; }

        public Piece(Board board, Color color)
        {
            Position=null;
            Color=color;
            Board=board;
            MoveQtt = 0;
        }

        public void incrementMoveQtt()
        {
            MoveQtt++;
        }

        public abstract bool[,] PossibleMoves();
    }
}
