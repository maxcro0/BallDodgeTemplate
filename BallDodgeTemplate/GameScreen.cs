using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BallDodgeTemplate
{
    public partial class GameScreen : UserControl
    {
        public static int screenWidth;
        public static int screenHeight;

        public static int lives = 3;
        public static int points = 0;
        public static int chaserCounter = 2;

        bool leftArrowDown, rightArrowDown, upArrowDown, downArrowDown;

        Ball chaseBall;
        List<Ball> chaserBalls = new List<Ball>();
        List<Ball> balls = new List<Ball>();
        Player hero;

        Random randGen = new Random();
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush redBrush = new SolidBrush(Color.Red);

        public GameScreen()
        {
            InitializeComponent();

            screenWidth = this.Width;
            screenHeight = this.Height;

            InitializeGame();
        }

        public void CreateBall(string type)
        {
            int x = randGen.Next(20, this.Width - 50);
            int y = randGen.Next(20, this.Height - 50);
            if (type == "enemy")
            {
                Ball b = new Ball(x, y, 8, 8);
                balls.Add(b);
            }

            if (type == "friend")
            {
                Ball b = new Ball(x, y, 8, 8);
                chaserBalls.Add(b);

            }
        }

        public void InitializeGame()
        {
            hero = new Player();

            int x = randGen.Next(20, this.Width - 50);
            int y = randGen.Next(20, this.Height - 50);

            chaseBall = new Ball(x, y, 8, 8);
            chaserBalls.Add(chaseBall);

            for (int i = 0; i < 5; i++)
            {
                CreateBall("enemy");
            }
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (lives <= 0)
            {
                gameTimer.Stop();
            }

            if (chaserCounter >= 3)
            {
                CreateBall("friend");
                chaserCounter = 0;
            }
            #region game enders
            if (points == 5 && MenuScreen.difficulty == "easy")
            {
                gameTimer.Stop();
            }

            else if (points == 10 && MenuScreen.difficulty == "medium")
            {
                gameTimer.Stop();
            }

            else if (points == 15 && MenuScreen.difficulty == "hard")
            {
                gameTimer.Stop();
            }
            #endregion
            #region Move code
            foreach (Ball b in balls)
            {                 
                if (hero.Collision(b))
                {
                    lives--;
                }
            }
            
            foreach (Ball b in chaserBalls)
            {
                if (hero.Collision(b))
                {
                    lives++;
                    points++;
                    chaserCounter++;
                }
            }



            if (rightArrowDown == true)
            {
                hero.Move("right");
            }

            else if (leftArrowDown == true)
            {
                hero.Move("left");
            }

            else if (upArrowDown == true)
            {
                hero.Move("up");
            }

            else if (downArrowDown == true)
            {
                hero.Move("down");
            }

            foreach (Ball b in balls)
            {
                b.Move();
            }

            foreach (Ball b in chaserBalls)
            {
                b.Move();
            }

            Refresh();
            #endregion

            Rectangle heroRec = new Rectangle(hero.x, hero.y, hero.width, hero.height);

            
            Rectangle chaseRec = new Rectangle(chaseBall.x, chaseBall.y, chaseBall.size, chaseBall.size);


        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {

            liveLabel.Text = $"Lives: {lives}";
            pointLabel.Text = $"Points: {points}";

            //balls to avoid
            foreach (Ball b in balls)
            {
                e.Graphics.FillEllipse(redBrush, b.x, b.y, b.size, b.size);
            }

            //balls to hit
            foreach (Ball b in chaserBalls)
            {
                e.Graphics.FillEllipse(greenBrush,b.x,b.y, b.size, b.size);
            }

            //hero
            e.Graphics.FillRectangle(greenBrush, hero.x, hero.y, hero.width, hero.height);


        }
    }
}
