using System;
using System.Threading;
using GameOfLife.Model;
using GameOfLife.View;
using Timer = System.Windows.Forms.Timer;


namespace GameOfLife.controller
{
    public class GameController
    {
        //private Thread gameLoopThread;
        private bool isRunning = false;
        private readonly Colony colony;
        private readonly IGameOfLifeView view;
        private Timer gameTimer;
        private int BASE_SPEED = 1000;
        private int timerInterval = 1000;

        public GameController(Colony colony, IGameOfLifeView view)
        {
            this.colony = colony;
            this.view = view;
            initializedGame();
        }

        private void initializedGame()
        {
            resetSimulation(5);
            setTimer();
            this.view.SimulationState += OnSimulationStateChanged;
            this.view.CellClicked += OnCellClicked;
            this.view.ResetSimulation += OnView_ResetSimulation;
            this.view.SimulationSpeed += OnView_SimulationSpeed;
            this.colony.SteadyStateReached += SimulationOver;
            this.colony.BoardChanged += updateView;
        }

        private void OnView_SimulationSpeed(int gameSpeed)
        {
            timerInterval = BASE_SPEED / gameSpeed;
            if(isRunning)
            {
                StartTimer();
            }
        }

        private void updateView()
        {
            bool[,] currentColonyState = colony.ToBoolArray();
            view.UpdateColony(currentColonyState);
        }

        private void OnSimulationStateChanged(bool inSimulation)
        {
            if(inSimulation)
            {
                isRunning = true;
                StartTimer();
            }
            else
            {
                isRunning = false;
                StopTimer();
            }
        }
        private void setTimer()
        {
            gameTimer = new Timer();
            gameTimer.Interval = timerInterval;
            gameTimer.Tick += TimerTick;
        }

        public void StartTimer()
        {
            gameTimer.Interval = timerInterval;
            gameTimer.Start();
        }

        public void StopTimer()
        {
            gameTimer.Stop();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            colony.ComputeNextGeneration();
            updateView();
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
            isRunning = false;
            resetSimulation(cellSize);
        }
        
        private void SimulationOver()
        {
            StopTimer();
            isRunning = false;
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
