using System;
using System.Threading;
using System.Windows.Forms;
using GameOfLife.Model;
using GameOfLife.View;
using Timer = System.Threading.Timer;


namespace GameOfLife.controller
{
    public class GameController
    {
        //private Thread gameLoopThread;
        //private bool isRunning = false;
        private readonly Colony colony;
        private readonly IGameOfLifeView view;
        private Timer gameTimer;
        private int timerIntervalInMiliSec = 1000;

        public GameController(Colony colony, IGameOfLifeView view)
        {
            this.colony = colony;
            this.view = view;
            initializedGame();


        }

        private void initializedGame()
        {
            resetSimulation(5);
            setTimer(1000);

            this.view.SimulationState += OnSimlationStateChanged;
            //listen to the view
            this.view.CellClicked += OnCellClicked;
            this.view.ResetSimulation += OnView_ResetSimulation;
            //listen to the model
            this.colony.SteadyStateReached += SimulationOver;
            this.colony.BoardChanged += upateView;
        }

        private void upateView()
        {
            bool[,] currentColonyState = colony.ToBoolArray();
            view.UpdateColony(currentColonyState);
        }

        private void OnSimlationStateChanged(bool isRuning)
        {
            if(isRuning)
            {
                StartTimer();
            }
            else
            {
                StopTimer();
            }
        }

        private void setTimer(int miliSecondInterval)
        {
            this.timerIntervalInMiliSec = miliSecondInterval;
            TimerCallback timerCallback = new TimerCallback(TimerTick);
            gameTimer = new Timer(timerCallback, null, Timeout.Infinite, miliSecondInterval);
        }

        public void StartTimer()
        {
            colony.ComputeNextGeneration();
            upateView();
            gameTimer.Change(0, timerIntervalInMiliSec);
        }

        public void StopTimer()
        {
            gameTimer.Change(Timeout.Infinite, 0); 
        }

        private void TimerTick(object state)
        {
            colony.ComputeNextGeneration();
            bool[,] currentColonyState = colony.ToBoolArray();
            view.UpdateColony(currentColonyState);
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

        
        //public void StartSimulation()
        //{
        //    isRunning = true;
        //    gameLoopThread = new Thread(GameLoop);
        //    gameLoopThread.Start();
        //}

        //public void StopSimulation()
        //{
        //    isRunning = false;
        //    if (gameLoopThread != null && gameLoopThread.IsAlive)
        //    {
        //        gameLoopThread.Join(); // Wait for the thread to finish
        //    }
        //}

        //private void GameLoop()
        //{
        //    while (isRunning)
        //    {
        //        colony.ComputeNextGeneration();

        //        // Sleep to control the speed of the simulation.
        //        // Adjust as needed.
        //        Thread.Sleep(1000);
        //    }
        //}
        private void SimulationOver()
        {
            StopTimer();
            view.DisplayMessage("Simulation Over");
            
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
            }
            
        }
        
    }
}
