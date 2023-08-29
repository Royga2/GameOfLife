using System;
using System.Collections.Generic;

namespace GameOfLife.Model
{
    public class Colony
    {
        #region Properties

        public Cell[,] Cells { get; private set; }
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public int GenerationCount { get; private set; } = 0;
        public int LiveCellCount { get; private set; } = 0;
        public int DeadCellCount { get; private set; } = 0;

        #endregion


        #region Events

        public event Action ColonyChanged;
        public event Action SteadyStateReached;

        #endregion


        #region Constructors

        public Colony(int rows, int cols)
        {
            Initialize(rows, cols);
        }

        #endregion


        #region Public Methods

        public void Reset(int numberOfRows, int numberOfColumns)
        {
            Initialize(numberOfRows, numberOfColumns);
        }

        public void ComputeNextGeneration()
        {
            Dictionary<(int row, int col), bool> cellUpdates = new Dictionary<(int row, int col), bool>();

            for (int currentRow = 0; currentRow < Rows; currentRow++)
            {
                for (int currentCol = 0; currentCol < Cols; currentCol++)
                {
                    bool currentCellAliveStatus = Cells[currentRow, currentCol].IsAlive;
                    int numberOfLiveNeighbors = Cells[currentRow, currentCol].LiveNeighborCount;

                    if (currentCellAliveStatus)
                    {
                        if (numberOfLiveNeighbors < 2 || numberOfLiveNeighbors > 3)
                        {
                            cellUpdates[(currentRow, currentCol)] = false;
                        }
                    }
                    else
                    {
                        if (numberOfLiveNeighbors == 3)
                        {
                            cellUpdates[(currentRow, currentCol)] = true;
                        }
                    }
                }
            }
            if (cellUpdates.Count > 0)
            {
                GenerationCount++;
                foreach (var update in cellUpdates)
                {
                    SetCellState(update.Key.row, update.Key.col, update.Value);
                }
                OnColonyChanged();
            }
            else
            {
                OnSteadyStateReached();
            }
        }

        public bool GetCellState(int row, int col)
        {
            return Cells[row, col].IsAlive;
        }

        public void SetCellState(int row, int col, bool isAlive)
        {
            bool oldState = Cells[row, col].IsAlive;
            Cells[row, col].IsAlive = isAlive;

            if (oldState != isAlive)
            {
                UpdateLiveDeadCounts(isAlive);
                int adjustment = isAlive ? 1 : -1;
                AdjustNeighborCount(row, col, adjustment);
            }
        }

        public bool[,] ToBoolArray()
        {
            bool[,] result = new bool[Rows, Cols];

            for (int currentRow = 0; currentRow < Rows; currentRow++)
            {
                for (int currentCol = 0; currentCol < Cols; currentCol++)
                {
                    result[currentRow, currentCol] = Cells[currentRow, currentCol].IsAlive;
                }
            }
            return result;
        }

        #endregion


        #region Private Methods

        private void UpdateLiveDeadCounts(bool isBecomingAlive)
        {
            LiveCellCount += isBecomingAlive ? 1 : -1;
            DeadCellCount += isBecomingAlive ? -1 : 1;
        }

        private void AdjustNeighborCount(int currentRow, int currentCol, int adjustment)
        {
            for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (int colOffset = -1; colOffset <= 1; colOffset++)
                {
                    if ((rowOffset != 0 || colOffset != 0) && IsValidCell(currentRow + rowOffset, currentCol + colOffset))
                        Cells[currentRow + rowOffset, currentCol + colOffset].LiveNeighborCount += adjustment;
                }
            }
        }

        private bool IsValidCell(int rowIndex, int colIndex)
        {
            return rowIndex >= 0 && rowIndex < Rows && colIndex >= 0 && colIndex < Cols;
        }

        protected virtual void OnColonyChanged()
        {
            ColonyChanged?.Invoke();
        }

        private void Initialize(int numberOfRows, int numberOfColumns)
        {
            GenerationCount = 0;
            LiveCellCount = 0;
            DeadCellCount = numberOfRows * numberOfColumns;
            this.Rows = numberOfRows;
            this.Cols = numberOfColumns;
            Cells = new Cell[numberOfRows, numberOfColumns];

            for (int currentRow = 0; currentRow < numberOfRows; currentRow++)
            {
                for (int currentCol = 0; currentCol < numberOfColumns; currentCol++)
                {
                    Cells[currentRow, currentCol] = new Cell();
                }
            }
            OnColonyChanged();
        }

        protected virtual void OnSteadyStateReached()
        {
            SteadyStateReached?.Invoke();
        }

        #endregion
    }
}
