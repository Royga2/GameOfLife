using System.Windows.Forms;
using GameOfLife.Model;
using GameOfLife.View;


namespace GameOfLife.controller
{
    public class GameController
    {
        private readonly Colony colony;
        private readonly IGameOfLifeView view;

        public GameController(Colony colony, IGameOfLifeView view)
        {
            this.colony = colony;
            this.view = view;
            //listen to the view
            this.view.CellClicked += OnCellClicked;
            //listen to the model
            this.colony.BoardChanged += UpdateView; 
            this.colony.SteadyStateReached += SimulationOver;
        }

        private void SimulationOver()
        {
            throw new System.NotImplementedException();
        }

        private void OnCellClicked(int row, int col)
        {
            bool currentState = colony.GetCellState(row, col);
            colony.SetCellState(row, col, !currentState);
            view.UpdateCell(row, col, !currentState);
        }


        private void UpdateView()
        {
            bool[,] cellStates = new bool[colony.Rows, colony.Cols];
            for (int i = 0; i < colony.Rows; i++)
            {
                for (int j = 0; j < colony.Cols; j++)
                {
                    cellStates[i, j] = colony.GetCellState(i, j);
                }
            }
            view.UpdateColony(cellStates);
        }
    }
}
