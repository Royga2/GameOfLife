﻿using System;
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

        public CellState State { get; internal set; }
        public int LiveNeighborCount { get; internal set; }

        public Cell()
        {
            State = CellState.Dead;
            LiveNeighborCount = 0;
        }

        public bool IsAlive => State == CellState.Alive;
    }
}
