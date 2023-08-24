using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel
{
    class Cell
    {
        public bool IsAlive { get; set; }
        private readonly int r_Row;
        private readonly int r_Col;

        public Cell(int i_Row, int i_Col)
        {
            r_Row = i_Row;
            r_Col = i_Col;
        }

        public int Row => r_Row;
        public int Col => r_Col;
    }
}
