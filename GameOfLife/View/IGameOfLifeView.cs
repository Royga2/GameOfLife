using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.View
{
    public interface IGameOfLifeView
    {
        event Action<int, int> CellClicked;

        event Action<int> ResetSimulation;

        void UpdateColony(bool[,] colonyState);

        void UpdateCell(int row, int col, bool isAlive, bool render);

        bool IsInvokeRequired();

        void PerformInvoke(Action action);

        void DisplayMessage(string message);

        void InitializeView();
        
        (int width, int height) getViewSize();
    }
}

