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

        event Action<bool> SimulationState; 
        void UpdateColony(bool[,] colonyState);

        void UpdateCell(int row, int col, bool isAlive, bool render, Graphics g = null);

       // bool IsInvokeRequired();

        //void PerformInvoke(Action action);

        void DisplayMessage(string message);

        //void InitializeView();
        
        (int width, int height) getViewSize();
    }
}

