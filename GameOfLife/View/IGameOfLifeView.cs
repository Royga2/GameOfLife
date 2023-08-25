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
        void UpdateColony(bool[,] colonyState);
        void UpdateCell(int row, int col, bool isAlive);
        void DisplayMessage(string message);
    }
}
