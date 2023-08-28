using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife.View
{
    public partial class GameForm : Form, IGameOfLifeView
    {
        #region Fields and Properties
        public event Action<int, int> CellClicked;
        public event Action <int> ResetSimulation;
        public event Action<int> SimulationSpeed;
        public event Action<bool> SimulationState;
        public event Action AdvanceGeneration;
        public event Action ViewClosing;
        private readonly SolidBrush cellBrush = new SolidBrush(Color.DarkOrange);
        private readonly SolidBrush backgroundBrush = new SolidBrush(Color.FromArgb(45, 45, 45));
        private readonly object imageLock = new object();
        private bool isInSimulation = false;
        private bool isMousePressed = false;
        private int viewHeight;
        private int viewWidth;
        private Point mouseDownLocation;

        public int cellSize { get; private set; }
        #endregion 


        #region Constructor and Initialization

        public GameForm()
        {
            InitializeComponent();
            viewHeight = pbGrid.Height;
            viewWidth = pbGrid.Width;
            setGame();
        }

        private void setGame()
        {
            cellSize = (int)nudCellSize.Value;
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            InitializeView();
        }

        #endregion


        #region Event Handlers
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            ViewClosing?.Invoke();
            base.OnFormClosing(e);
        }

        private void buttonAdvancedGeneration_Click(object sender, EventArgs e)
        {
            AdvanceGeneration?.Invoke();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            OnButtonRestartClicked();
        }

        private void OnButtonRestartClicked()
        {
            isInSimulation = false;
            pbGrid.Enabled = true;
            buttonStartStop.Text = string.Format("Start Simulation");
            buttonStartStop.Enabled = true;
            buttonAdvancedGeneration.Enabled = true;
            setGame();
            AdjustPictureBoxSize();
            ResetSimulation?.Invoke(cellSize);
        }

        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            isInSimulation = !isInSimulation;

            if (isInSimulation)
            {
                buttonStartStop.Text = "Stop Simulation";
                nudCellSize.Enabled = false;
                buttonReset.Enabled = false;
                pbGrid.Enabled = false;
            }
            else
            {
                buttonStartStop.Text = "Start Simulation";
                nudCellSize.Enabled = true;
                buttonReset.Enabled = true;
                pbGrid.Enabled = true;
            }

            SimulationState?.Invoke(isInSimulation);
        }

        private void nudGameSpeed_ValueChanged(object sender, EventArgs e)
        {
            int gameSpeed = (int)nudGameSpeed.Value;
            SimulationSpeed?.Invoke(gameSpeed);
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        #endregion


        #region Public Methods

        public void UpdateCell(int x, int y, bool isAlive, bool render = true, Graphics providedGraphics = null)
        {
            if (providedGraphics == null)
            {
                using (Graphics g = Graphics.FromImage(pbGrid.Image))
                {
                    DrawOnGraphics(g, x, y, isAlive);
                }
            }
            else
            {
                DrawOnGraphics(providedGraphics, x, y, isAlive);
            }

            if (render)
            {
                pbGrid.Invalidate(new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize));
            }
        }

        public void UpdateColony(bool[,] matrix)
        {
            lock (imageLock)
            {
                using (Bitmap bmp = new Bitmap(pbGrid.Width, pbGrid.Height))
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    for (int y = 0; y < matrix.GetLength(0); y++)
                    {
                        for (int x = 0; x < matrix.GetLength(1); x++)
                        {
                            UpdateCell(y, x, matrix[y, x], false, g);
                        }
                    }

                    if (pbGrid.Image != null)
                    {
                        pbGrid.Image.Dispose();
                    }
                    pbGrid.Image = (Bitmap)bmp.Clone();
                }
            }
            pbGrid.Invalidate();
        }

        public void DisplayMessage(string message)
        {
            isMousePressed = false;
            DialogResult result = MessageBox.Show(message, "Game Of Life message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            switch (result)
            {
                case (DialogResult.Yes):
                    {
                        OnButtonRestartClicked();
                        break;
                    }

                case (DialogResult.No):
                    {
                        break;
                    }
            }
        }

        public void UpdateStatistics(int generationCount, int liveCellCount, int deadCellCount)
        {
            labelGenerationCount.Text = string.Format($"Generation: {generationCount}");
            labelLiveCellCount.Text = string.Format($"Live Cells: {liveCellCount}");
            labelDeadCellCount.Text = string.Format($"Dead Cells: {deadCellCount}");
        }

        public void SteadyStateReached(bool state)
        {
            if (state)
            {
                nudCellSize.Enabled = true;
                buttonReset.Enabled = true;
                buttonStartStop.Text = string.Format("Simulation Finished");
                buttonStartStop.Enabled = false;
                buttonAdvancedGeneration.Enabled = false;
            }
        }

        public (int, int) getViewSize()
        {
            return (viewWidth, viewHeight);
        }

        #endregion


        #region Private Methods

        private void DrawOnGraphics(Graphics g, int x, int y, bool isAlive)
        {
            if (isAlive)
            {
                g.FillRectangle(cellBrush, x * cellSize, y * cellSize, cellSize - 1, cellSize - 1);
            }
            else
            {
                g.FillRectangle(backgroundBrush, x * cellSize, y * cellSize, cellSize, cellSize);
            }
        }

        private void InitializeView()
        {
            if (pbGrid.Image == null)
            {
                Bitmap bmp = new Bitmap(viewWidth, viewHeight);
                pbGrid.BackColor = Color.FromArgb(45, 45, 45);
                pbGrid.Image = bmp;
                AdjustPictureBoxSize();
            }
        }

        private void ChangeCellStateFromMouse(int mouseX, int mouseY)
        {
            int x = mouseX / cellSize;
            int y = mouseY / cellSize;

            CellClicked?.Invoke(x, y);
        }

        private void AdjustPictureBoxSize()
        {
            int widthModulus = viewWidth % cellSize;
            int heightModulus = viewHeight % cellSize;

            int adjustedWidth = viewWidth - widthModulus;
            int adjustedHeight = viewHeight - heightModulus;

            pbGrid.Width = adjustedWidth;
            pbGrid.Height = adjustedHeight;

            pbGrid.Left = (this.ClientSize.Width - adjustedWidth) / 2;
            pbGrid.Top = (this.ClientSize.Height - adjustedHeight) / 2;
        }

        private void pbGrid_MouseUp(object sender, MouseEventArgs e)
        {
            isMousePressed = false;

            int distanceMoved = Math.Abs(e.X - mouseDownLocation.X) + Math.Abs(e.Y - mouseDownLocation.Y);

            if (distanceMoved <= cellSize)
            {
                ChangeCellStateFromMouse(e.X, e.Y);
            }
        }
        
        private void pbGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMousePressed)
            {
                ChangeCellStateFromMouse(e.X, e.Y);
            }
        }
        
        private void pbGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMousePressed = true;
                mouseDownLocation = e.Location;
            }
        }
        #endregion
    }
}
