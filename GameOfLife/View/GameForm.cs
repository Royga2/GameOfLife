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
        
        public bool inSimulation;
        public event Action<int, int> CellClicked;

        private TableLayoutPanel mainContainer;
        private FlowLayoutPanel buttonStrip;
        private Button playPauseButton;
        private NumericUpDown numCellsControl;
        private TableLayoutPanel panel; // Grid container

        public GameForm()
        {
            InitializeUIComponents();
            InitializeComponent();
            //foreach (var button in gridButtons)
            //{
            //    button.Click += (sender, e) =>
            //        {
            //            var btn = (Button)sender;
            //            int row = (int)btn.Tag[0]; 
            //            int col = (int)btn.Tag[1];
            //            CellClicked?.Invoke(row, col);
            //        };
            //}
        }

       

        private void InitializeUIComponents()
        {
            // Initialize main container
            mainContainer = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };

            // Give 90% of space to the game grid and 10% to button strip
            mainContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            mainContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));

            // Setup game grid
            SetupGrid();
            mainContainer.Controls.Add(panel, 0, 0);

            // Initialize button strip
            buttonStrip = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill
            };

            // NumericUpDown for selecting number of cells
            numCellsControl = new NumericUpDown
            {
                Minimum = 3,
                Maximum = 100,
                Value = 12,
                DecimalPlaces = 0
            };
            buttonStrip.Controls.Add(numCellsControl);

            // Consolidated Play/Pause button
            playPauseButton = new Button { Text = "Play" };
            playPauseButton.Click += (sender, args) =>
            {
                if (playPauseButton.Text == "Play")
                {
                    playPauseButton.Text = "Pause";
                    // Start or resume game
                }
                else
                {
                    playPauseButton.Text = "Play";
                    // Pause game
                }
            };
            buttonStrip.Controls.Add(playPauseButton);

            var resetButton = new Button { Text = "Reset" };
            buttonStrip.Controls.Add(resetButton);

            mainContainer.Controls.Add(buttonStrip, 0, 1);

            this.Controls.Add(mainContainer);
        }

        private void SetupGrid(int rows = 5, int cols = 5)
        {
            panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = rows,
                ColumnCount = cols
            };

            for (int i = 0; i < rows; i++)
            {
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rows));
                for (int j = 0; j < cols; j++)
                {
                    if (i == 0)
                        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / cols));

                    Button cellButton = new Button
                    {
                        Dock = DockStyle.Fill,
                        BackColor = Color.LightGray,
                        FlatStyle = FlatStyle.Flat,
                        FlatAppearance = { BorderSize = 1, BorderColor = Color.DarkGray }
                    };

                    cellButton.MouseEnter += (s, e) => { cellButton.BackColor = Color.DarkGray; };
                    cellButton.MouseLeave += (s, e) => { cellButton.BackColor = Color.LightGray; };
                    cellButton.Click += (s, e) => { /* Handle cell click event */ };

                    panel.Controls.Add(cellButton);
                }
            }
        }

        public void UpdateColony(bool[,] colonyState)
        {
            for (int i = 0; i < colonyState.GetLength(0); i++)
            {
                for (int j = 0; j < colonyState.GetLength(1); j++)
                {
                    UpdateCell(i, j, colonyState[i, j]);
                }
            }
        }

        public void UpdateCell(int row, int col, bool isAlive)
        {
            {
                // gridButtons[row, col].BackColor = isAlive ? Color.Black : Color.White;
            }
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
