using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoard
{
    class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Position() { }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public void DefineValues(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public override string ToString()
        {
            return Row + ", " + Column;
        }
    }
}
