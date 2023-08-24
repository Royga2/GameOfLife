using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel
{
    class Board
    {
        public Cell[,] m_Cells { get; private set; }
        private readonly int r_Rows;
        private readonly int r_Cols;

        public Board(int i_Rows, int i_Cols)
        {
            this.r_Rows = i_Rows;
            this.r_Cols = i_Cols;

            m_Cells = new Cell[i_Rows, i_Cols];

            for(int i = 0; i < i_Rows; i++)
            {
                for(int j = 0; j < i_Cols; j++)
                {
                    m_Cells[i, j] = new Cell(i, j);
                }
            }

        }
    }
}
