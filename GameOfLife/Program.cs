using System;
using System.Windows.Forms;
using GameOfLife.Model;
using GameOfLife.View;
using GameOfLife.controller;

namespace GameOfLife
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Model.Colony colony = new Colony(20, 20);
            View.GameForm gameForm = new GameForm();
            GameController controller = new GameController(colony, gameForm);

            Application.Run(gameForm);
        }
    }
}
