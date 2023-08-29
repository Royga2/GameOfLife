using System;
using System.Threading;
using GameOfLife.Model;
using GameOfLife.View;
using Timer = System.Windows.Forms.Timer;


namespace GameOfLife.controller
{
    public class GameController : IDisposable
    {
        #region Fields and Properties

        private bool isRunning = false;
        private readonly Colony colony;
        private readonly IGameOfLifeView view;
        private Timer gameTimer;
        private const int BASE_SPEED = 1000;
        private int timerInterval = 1000;

        #endregion

        #region Constructor and Initialization

        public GameController(Colony colony, IGameOfLifeView view)
        {
            this.colony = colony;
            this.view = view;
            this.view.ViewClosing += OnView_Closing;
            initializedGame();
        }

        private void initializedGame()
        {
            resetSimulation(5);
            SetTimer();
            this.view.SimulationState += OnView_SimulationStateChanged;
            this.view.CellClicked += OnView_CellClicked;
            this.view.ResetSimulation += OnView_ResetSimulation;
            this.view.SimulationSpeed += OnView_SimulationSpeedChanged;
            this.colony.SteadyStateReached += OnModel_SimulationOver;
            this.colony.ColonyChanged += OnModel_ColonyChanged;
            this.view.AdvanceGeneration += OnView_AdvanceGenerationClicked;
        }

        #endregion

        #region Event Handlers

        private void OnView_Closing()
        {
            this.Dispose();
        }

        private void OnView_AdvanceGenerationClicked()
        {
            colony.ComputeNextGeneration();
            updateView();
        }

        private void OnView_SimulationSpeedChanged(int gameSpeed)
        {
            timerInterval = BASE_SPEED / gameSpeed;
            if (isRunning)
            {
                StartTimer();
            }
        }

        private void OnView_SimulationStateChanged(bool inSimulation)
        {
            if (inSimulation)
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

        private void OnView_CellClicked(int row, int col)
        {
            if (!IsWithinBounds(row, col))
            {
                return;
            }

            try
            {
                bool currentState = colony.GetCellState(row, col);
                colony.SetCellState(row, col, !currentState);
                view.UpdateCell(row, col, !currentState, true);
                view.UpdateStatistics(colony.GenerationCount, colony.LiveCellCount, colony.DeadCellCount);
            }
            catch (Exception e)
            {
                view.DisplayMessage($"Unexpected error processing cell click: {e.Message}");
            }
        }

        private void OnView_ResetSimulation(int cellSize)
        {
            isRunning = false;
            resetSimulation(cellSize);
        }

        private void OnModel_SimulationOver()
        {
            StopTimer();
            isRunning = false;
            view.SteadyStateReached(!isRunning);
            string msg = string.Format($"Simulation Over!\n\nDo you wish to reset the grid?");
            view.DisplayMessage(msg);
        }

        private void OnModel_ColonyChanged()
        {
            updateView();
        }

        #endregion

        #region Timer Control

        private void SetTimer()
        {
            gameTimer = new Timer
            {
                Interval = timerInterval,
                Enabled = false
            };

            gameTimer.Tick += TimerTick;
        }

        private void StartTimer()
        {
            gameTimer.Interval = timerInterval;
            gameTimer.Start();
        }

        private void StopTimer()
        {
            gameTimer.Stop();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            try
            {
                colony.ComputeNextGeneration();
                updateView();
            }
            catch (Exception ex)
            {
                view.DisplayMessage($"Error updating generation: {ex.Message}");
            }
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                gameTimer?.Dispose();
                this.view.SimulationState -= OnView_SimulationStateChanged;
                this.view.CellClicked -= OnView_CellClicked;
                this.view.ResetSimulation -= OnView_ResetSimulation;
                this.view.SimulationSpeed -= OnView_SimulationSpeedChanged;
                this.colony.SteadyStateReached -= OnModel_SimulationOver;
                this.colony.ColonyChanged -= OnModel_ColonyChanged;
                this.view.AdvanceGeneration -= OnView_AdvanceGenerationClicked;
            }
        }

        #endregion

        #region Helper Methods

        private void updateView()
        {
            bool[,] currentColonyState = colony.ToBoolArray();
            view.UpdateColony(currentColonyState);
            view.UpdateStatistics(colony.GenerationCount, colony.LiveCellCount, colony.DeadCellCount);
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

        private bool IsWithinBounds(int row, int col)
        {
            return row >= 0 && row < colony.Rows && col >= 0 && col < colony.Cols;
        }

        #endregion
    }
}
