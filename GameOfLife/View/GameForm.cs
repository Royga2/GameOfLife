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

        public event Action<int, int> CellClicked;

        public GameForm()
        {
            InitializeComponent();
            foreach (var button in gridButtons)
            {
                button.Click += (sender, e) =>
                    {
                        var btn = (Button)sender;
                        int row = (int)btn.Tag[0];  // Assuming you set the row and col as a tag to each button
                        int col = (int)btn.Tag[1];
                        CellClicked?.Invoke(row, col);
                    };
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
                gridButtons[row, col].BackColor = isAlive ? Color.Black : Color.White;
            }
        }

        public void DisplayMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }
    }
}
