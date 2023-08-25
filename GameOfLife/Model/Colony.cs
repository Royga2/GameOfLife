using System;
using System.Collections.Generic;

namespace GameOfLife.Model
{
    public class Colony
    {
        private readonly Cell[,] cells;
        public int Rows { get; }
        public int Cols { get; }

        public event Action BoardChanged;
        public event Action SteadyStateReached;


        public Colony(int rows, int cols)
        {
            this.Rows = rows;
            this.Cols = cols;
            cells = new Cell[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    cells[i, j] = new Cell();
        }

        public void ComputeNextGeneration()
        {
            Dictionary<(int, int), bool> updates = new Dictionary<(int, int), bool>();

            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                {
                    bool isAlive = cells[i, j].IsAlive;
                    int liveNeighbors = cells[i, j].LiveNeighborCount;

                    switch(isAlive)
                    {
                        case true when (liveNeighbors < 2 || liveNeighbors > 3):
                            updates[(i, j)] = false;
                            break;
                        case false when liveNeighbors == 3:
                            updates[(i, j)] = true;
                            break;
                    }
                }

            if (updates.Count > 0)
            {
                foreach (KeyValuePair<(int, int), bool> update in updates)
                {
                    SetCellState(update.Key.Item1, update.Key.Item2, update.Value);
                }
                OnBoardChanged();
            }
            else
            {
                OnSteadyStateReached();
            }
        }

        public bool GetCellState(int row, int col)
        {
            return cells[row, col].IsAlive;
        }

        public void SetCellState(int row, int col, bool isAlive)
        {
            bool oldState = cells[row, col].IsAlive;
            cells[row, col].IsAlive = isAlive;

            if (oldState != isAlive)
            {
                int adjustment = isAlive ? 1 : -1;
                AdjustNeighborCount(row, col, adjustment);
            }
        }

        private void AdjustNeighborCount(int i, int j, int adjustment)
        {
            for (int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++)
                if ((x != 0 || y != 0) && IsValidCell(i + x, j + y))
                    cells[i + x, j + y].LiveNeighborCount += adjustment;
        }

        private bool IsValidCell(int i, int j)
        {
            return i >= 0 && i < Rows && j >= 0 && j < Cols;
        }

        protected virtual void OnBoardChanged()
        {
            BoardChanged?.Invoke();
        }

        protected virtual void OnSteadyStateReached()
        {
            SteadyStateReached?.Invoke();
        }
    }
}
