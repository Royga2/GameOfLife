using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.View
{
    public interface IGameOfLifeView
    {
        event Action<int, int> CellClicked;

        event Action<int> ResetSimulation;

        event Action<int> SimulationSpeed;

        event Action<bool> SimulationState;

        event Action AdvanceGeneration;
        void UpdateColony(bool[,] colonyState);

        void UpdateCell(int row, int col, bool isAlive, bool render, Graphics g = null);

        void DisplayMessage(string message);

        void UpdateStatistics(int generationCount, int liveCellCount, int deadCellCount);

        (int width, int height) getViewSize();
    }
}

