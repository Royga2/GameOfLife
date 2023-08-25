using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameController
{
    class GameController
    {
        private GameView view;
        private Board board;
        private Timer timer;

        public GameController()
        {
            board = new Board(20, 20);
            view = new GameOfLifeView();
            view.GameTick += UpdateGame;

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += (sender, e) => view.GameTick?.Invoke();
            timer.Start();
        }

        private void UpdateGame()
        {
            board.ComputeNextGeneration();
            view.DrawBoard(board);
        }

        public void Run()
        {
            Application.Run(view);
        }
    }
}
