﻿using System;
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
        public int cellSize { get; set; }
        public event Action<int, int> CellClicked;
        public event Action <int> ResetSimulation;
        public event Action<int> SimulationSpeed;
        public event Action<bool> SimulationState;
        private bool isMousePressed = false;
        private readonly object imageLock = new object();
        private SolidBrush cellBrush = new SolidBrush(Color.DarkOrange);
        private SolidBrush backgroundBrush = new SolidBrush(Color.Black);

        
        public GameForm()
        {
            InitializeComponent();
            setGame();
        }

        private void setGame()
        {
            cellSize = (int)nudCellSize.Value;
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
            setGame();
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
                //nudGameSpeed.Enabled = false;
                pbGrid.Enabled = false;
                InSimulation = true;
            }
            else
            {
                buttonStartStop.Text = "Start";
                nudCellSize.Enabled = true;
                //nudGameSpeed.Enabled = true;
                buttonReset.Enabled = true;
                pbGrid.Enabled = true;
            }
            SimulationState?.Invoke(InSimulation);
        }

        private void nudGameSpeed_ValueChanged(object sender, EventArgs e)
        {
            int gameSpeed = (int)nudGameSpeed.Value;
            SimulationSpeed?.Invoke(gameSpeed);
        }
    }
}
