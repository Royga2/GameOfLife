using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife.View
{
    public partial class GameForm : Form, IGameOfLifeView
    {
        public int cellSize { get; set; }
        public event Action<int, int> CellClicked;
        public event Action <int> ResetSimulation;
        private bool isMousePressed = false;
        public event Action<bool> SimulationState;
        private readonly object imageLock = new object();
        private SolidBrush cellBrush = new SolidBrush(Color.DarkOrange);
        private SolidBrush backgroundBrush = new SolidBrush(Color.Black);

        
        public GameForm()
        {
            cellSize = 5;
            InitializeComponent();
        }

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
            MessageBox.Show(message);
        }

        public void InitializeView()
        {
            if (pbGrid.Image == null || pbGrid.Image.Width <= 0 || pbGrid.Image.Height <= 0)
            {
                Bitmap bmp = new Bitmap(pbGrid.Width, pbGrid.Height);
                pbGrid.BackColor = Color.Black;
                pbGrid.Image = bmp;
                AdjustPictureBoxSize();
            }
        }

        public (int, int) getViewSize()
        {
            return (pbGrid.Width, pbGrid.Height);
        }
        
        private void GameForm_Load(object sender, EventArgs e)
        {
            InitializeView();
        }

        //public bool IsInvokeRequired()
        //{
        //    return this.InvokeRequired;
        //}

        //public void PerformInvoke(Action action)
        //{
        //    this.Invoke(action);
        //}


        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void ChangeCellStateFromMouse(int mouseX, int mouseY)
        {
            int x = mouseX / cellSize;
            int y = mouseY / cellSize;

            CellClicked?.Invoke(x, y);
        }


        private void pbGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMousePressed = true;
                ChangeCellStateFromMouse(e.X, e.Y); 
            }
        }
        private void AdjustPictureBoxSize()
        {
            int widthModulus = pbGrid.Width % cellSize;
            int heightModulus = pbGrid.Height % cellSize;

            pbGrid.Width -= widthModulus;
            pbGrid.Height -= heightModulus;
        }

        private void pbGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMousePressed)
            {
                ChangeCellStateFromMouse(e.X, e.Y);
            }
        }

        private void pbGrid_MouseUp(object sender, MouseEventArgs e)
        {
            isMousePressed = false;
        }
        private void buttonReset_Click(object sender, EventArgs e)
        {
            cellSize = (int)nudCellSize.Value;
            AdjustPictureBoxSize();
            ResetSimulation?.Invoke(cellSize);
        }

        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            bool InSimulation = false;
            if (buttonStartStop.Text == "Start")
            {
                buttonStartStop.Text = "Stop";
                nudCellSize.Enabled = false;
                buttonReset.Enabled = false;
                pbGrid.Enabled = false;
                InSimulation = true;
            }
            else
            {
                buttonStartStop.Text = "Start";
                nudCellSize.Enabled = true;
                buttonReset.Enabled = true;
                pbGrid.Enabled = true;
            }
            SimulationState?.Invoke(InSimulation);
        }

       
    }
}

//private void InitializeUIComponents()
//{
//    // Initialize main container
//    mainContainer = new TableLayoutPanel
//    {
//        Dock = DockStyle.Fill,
//        RowCount = 2,
//        ColumnCount = 1
//    };

//    // Give 90% of space to the game grid and 10% to button strip
//    mainContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
//    mainContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));

//    // Setup game grid
//    SetupGrid();
//    mainContainer.Controls.Add(panel, 0, 0);

//    // Initialize button strip
//    buttonStrip = new FlowLayoutPanel
//    {
//        Dock = DockStyle.Fill
//    };

//    // NumericUpDown for selecting number of cells
//    numCellsControl = new NumericUpDown
//    {
//        Minimum = 3,
//        Maximum = 100,
//        Value = 12,
//        DecimalPlaces = 0
//    };
//    buttonStrip.Controls.Add(numCellsControl);

//    // Consolidated Play/Pause button
//    playPauseButton = new Button { Text = "Play" };
//    playPauseButton.Click += (sender, args) =>
//    {
//        if (playPauseButton.Text == "Play")
//        {
//            playPauseButton.Text = "Pause";
//            // Start or resume game
//        }
//        else
//        {
//            playPauseButton.Text = "Play";
//            // Pause game
//        }
//    };
//    buttonStrip.Controls.Add(playPauseButton);

//    var resetButton = new Button { Text = "Reset" };
//    buttonStrip.Controls.Add(resetButton);

//    mainContainer.Controls.Add(buttonStrip, 0, 1);

//    this.Controls.Add(mainContainer);
//}

//private void SetupGrid(int rows = 5, int cols = 5)
//{
//    panel = new TableLayoutPanel
//    {
//        Dock = DockStyle.Fill,
//        RowCount = rows,
//        ColumnCount = cols
//    };

//    for (int i = 0; i < rows; i++)
//    {
//        panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rows));
//        for (int j = 0; j < cols; j++)
//        {
//            if (i == 0)
//                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / cols));

//            Button cellButton = new Button
//            {
//                Dock = DockStyle.Fill,
//                BackColor = Color.LightGray,
//                FlatStyle = FlatStyle.Flat,
//                FlatAppearance = { BorderSize = 1, BorderColor = Color.DarkGray }
//            };

//            cellButton.MouseEnter += (s, e) => { cellButton.BackColor = Color.DarkGray; };
//            cellButton.MouseLeave += (s, e) => { cellButton.BackColor = Color.LightGray; };
//            cellButton.Click += (s, e) => { /* Handle cell click event */ };

//            panel.Controls.Add(cellButton);
//        }
//    }
//}