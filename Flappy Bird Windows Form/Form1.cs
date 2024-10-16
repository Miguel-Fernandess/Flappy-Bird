using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flappy_Bird_Windows_Form
{
    public partial class Form1 : Form
    {
        int pipeSpeed = 8;
        int gravity = 15;
        int score = 0;

        bool gameOver = false;
        Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();

            ground.Controls.Add(scoreText);
            scoreText.Left = 20;
            scoreText.Top = 25; 

            RestartGame();
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            flappyBird.Top += gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            scoreText.Text = "Score:" + score;
            

            if(pipeBottom.Left < -150)
            {
                pipeBottom.Left = rnd.Next(600, 1100);
                score++;
            }
            if(pipeTop.Left < -180)
            {
                pipeTop.Left = rnd.Next(750, 1300);
                score++;
            }

            if(flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
               flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
               flappyBird.Bounds.IntersectsWith(ground.Bounds)
               )
            {
                endGame();
            }

            if (score > 5)
            {
                pipeSpeed = 15;
            }

            if (score > 10)
            {
                pipeSpeed = 20;
            }

            if (flappyBird.Top <  -25 )
            {
                endGame();
            }
        }

        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = -15;
            }
        }

        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 15;
            }
            if(e.KeyCode == Keys.R && gameOver)
            {
                RestartGame();
            }
        }

        private void endGame()
        {
            gameTimer.Stop();
            scoreText.Text += " Game Over!!! Press R to restart";
            gameOver = true;
            restartImage.Enabled = true;
            restartImage.Visible = true;
        }

        private void RestartGame()
        {
            gameOver = false;
            
            flappyBird.Location = new Point(46, 79);
            pipeTop.Left = 800;
            pipeBottom.Left = 1200;

            score = 0;
            pipeSpeed = 8;
            scoreText.Text = "Score: 0";
            restartImage.Enabled = false;
            restartImage.Visible = false;
            gameTimer.Start();
        }

        private void RestartClickEvent(object sender, EventArgs e)
        {
            RestartGame();
        }
    }
}
