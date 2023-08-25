using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel
{
    public class Cell
    {
        public enum CellState
        {
            Dead,
            Alive
        }

        public CellState State { get; set; }
        public int LiveNeighborCount { get; set; }

        public Cell()
        {
            State = CellState.Dead;
        }

        public bool IsAlive => State == CellState.Alive;
    }
}
