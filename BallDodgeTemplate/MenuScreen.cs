using System;
using System.Windows.Forms;

namespace BallDodgeTemplate
{
    public partial class MenuScreen : UserControl
    {
        public static string difficulty;
        public MenuScreen()
        {
            InitializeComponent();
        }

        private void easyButton_Click(object sender, EventArgs e)
        {
            Form1.ChangeScreen(this, new GameScreen());
            difficulty = "easy";
        }

        private void mediumButton_Click(object sender, EventArgs e)
        {
            difficulty = "medium";
        }

        private void hardButton_Click(object sender, EventArgs e)
        {
            difficulty = "hard";
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
