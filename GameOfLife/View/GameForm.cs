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
        public bool inSimulation;
        public event Action<int, int> CellClicked;
        private SolidBrush cellBrush = new SolidBrush(Color.DarkOrange);
        private SolidBrush backgroundBrush = new SolidBrush(Color.Black);

        public GameForm()
        {
            InitializeComponent();
        }



        public void UpdateCell(int x, int y, bool isAlive, bool render = true)
        {
            using (Graphics g = Graphics.FromImage(pbGrid.Image))
            {
                if (isAlive)
                {
                    g.FillRectangle(cellBrush, x * cellSize, y * cellSize, cellSize, cellSize);
                }
                else
                {
                    g.FillRectangle(backgroundBrush, x * cellSize, y * cellSize, cellSize, cellSize);
                }
            }

            if(render)
            {
                pbGrid.Invalidate();
            }
        }

        public void UpdateColony(bool[,] matrix)
        {
            using (Bitmap bmp = new Bitmap(pbGrid.Width, pbGrid.Height))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Black);

                for (int y = 0; y < matrix.GetLength(0); y++)
                {
                    for (int x = 0; x < matrix.GetLength(1); x++)
                    {
                        UpdateCell(x, y, matrix[y, x], false);
                    }
                }

                if (pbGrid.Image != null)
                    pbGrid.Image.Dispose();

                pbGrid.Image = bmp;
            }

            pbGrid.Invalidate();
        }

        public void DisplayMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            
        }

       

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            inSimulation = false;
            Application.Exit();
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