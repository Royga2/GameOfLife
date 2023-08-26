using System;
using System.Threading;
using System.Windows.Forms;
using GameOfLife.Model;
using GameOfLife.View;


namespace GameOfLife.controller
{
    public class GameController
    {
        private Thread gameLoopThread;
        private bool isRunning = false;
        private readonly Colony colony;
        private readonly IGameOfLifeView view;

        public GameController(Colony colony, IGameOfLifeView view)
        {
            this.colony = colony;
            this.view = view;
            resetSimulation(5);
            //listen to the view
            this.view.CellClicked += OnCellClicked;
            this.view.ResetSimulation += OnView_ResetSimulation;
            //listen to the model
            this.colony.SteadyStateReached += SimulationOver;
        }


        private void resetSimulation(int cellSize)
        {
            (int, int) viewSize = view.getViewSize();
            int width = viewSize.Item2;
            int height = viewSize.Item1;
            int rows = height / cellSize;
            int cols = width / cellSize;
            colony.Reset(rows, cols);
        }
        private void OnView_ResetSimulation(int cellSize)
        {
           resetSimulation(cellSize);
        }
        

        public void StartSimulation()
        {
            isRunning = true;
            gameLoopThread = new Thread(GameLoop);
            gameLoopThread.Start();
        }

        public void StopSimulation()
        {
            isRunning = false;
            if (gameLoopThread != null && gameLoopThread.IsAlive)
            {
                gameLoopThread.Join(); // Wait for the thread to finish
            }
        }

        private void GameLoop()
        {
            while (isRunning)
            {
                colony.ComputeNextGeneration();

                // Sleep to control the speed of the simulation.
                // Adjust as needed.
                Thread.Sleep(1000);
            }
        }
        private void SimulationOver()
        {
            throw new System.NotImplementedException();
            
        }

        private void OnCellClicked(int row, int col)
        {
            try
            {
                bool currentState = colony.GetCellState(row, col);
                colony.SetCellState(row, col, !currentState);
                view.UpdateCell(row, col, !currentState, true);
            }
            catch(Exception e)
            {
                view.DisplayMessage("error while updating board");
            }
            
        }
        
    }
}
