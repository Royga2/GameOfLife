using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel
{
    public class Board
    {
        private readonly Cell[,] cells;
        private readonly int rows, cols;

        public event Action BoardChanged;
        public event Action SteadyStateReached;

        public Board(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            cells = new Cell[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    cells[i, j] = new Cell();
        }

        public void ComputeNextGeneration()
        {
            Dictionary<(int, int), Cell.CellState> updates = new Dictionary<(int, int), Cell.CellState>();

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    bool isAlive = cells[i, j].IsAlive;
                    int liveNeighbors = cells[i, j].LiveNeighborCount;

                    if (isAlive && (liveNeighbors < 2 || liveNeighbors > 3))
                        updates[(i, j)] = Cell.CellState.Dead;
                    else if (!isAlive && liveNeighbors == 3)
                        updates[(i, j)] = Cell.CellState.Alive;
                }

                if (updates.Count > 0)
                {
                    foreach (var update in updates)
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
        public Cell.CellState GetCellState(int row, int col)
        {
            return cells[row, col].State;
        }

        public void SetCellState(int row, int col, Cell.CellState state)
        {
            Cell cell = cells[row, col];
            Cell.CellState oldState = cell.State;

            cell.State = state;

            if (oldState != state)
            {
                int adjustment = state == Cell.CellState.Alive ? 1 : -1;
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
            return i >= 0 && i < rows && j >= 0 && j < cols;
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
