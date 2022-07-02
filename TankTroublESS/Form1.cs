using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankTroublESS
{
    public partial class TankTroublESS : Form
    {
        public PlayingField PlayingField { get; set; }
        public Rectangle ClientScreen { get; set; }
        public Random Random { get; set; }
        public int TimeToNewGame { get; set; } = 120;
        public int GreenTankPoints { get; set; } = 0;
        public int RedTankPoints { get; set; } = 0;

        public TankTroublESS()
        {
            InitializeComponent();

            ClientScreen = new Rectangle(0, 25, this.ClientRectangle.Width + 6, this.ClientRectangle.Height - 50);

            PlayingField = new PlayingField(ClientScreen);

            timer.Interval = 30;

            timer.Start();

            DoubleBuffered = true;

            Random = new Random();
        }

        private void TankTroublESS_Paint(object sender, PaintEventArgs e)
        {
            PlayingField.DrawBackground(e.Graphics);

            PlayingField.DrawWalls(e.Graphics);

            PlayingField.DrawTanks(e.Graphics);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            PlayingField.CheckIfTankHit();

            if (PlayingField.TankMovingMedia.Position.TotalSeconds >= 100)
            {
                PlayingField.TankMovingMedia.Position = new TimeSpan(0, 0, 0);
            }

            if (PlayingField.CheckIfOneTankDead())
            {
                TimeToNewGame--;
            }

            if (TimeToNewGame == 0)
            {
                TimeToNewGame = 120;

                if (!PlayingField.TankRed.IsAlive && PlayingField.TankGreen.IsAlive) GreenTankPoints++;
                if (PlayingField.TankRed.IsAlive && !PlayingField.TankGreen.IsAlive) RedTankPoints++;

                tssGreenTank.Text = "Green Tank: " + GreenTankPoints;
                tssRedTank.Text = "Red Tank: " + RedTankPoints;

                PlayingField.TankMovingMedia.Pause();

                PlayingField = new PlayingField(ClientScreen);
            }

            PlayingField.CheckIfBothTanksDead();

            Invalidate();
        }

        private void TankTroublESS_KeyDown(object sender, KeyEventArgs e)
        {
            // Green Tank
            String Key = e.KeyCode.ToString();
            if (Key == "D") { PlayingField.TankGreen.RotateRight = true; }
            if (Key == "A") { PlayingField.TankGreen.RotateLeft = true; }
            if (Key == "W") { PlayingField.TankGreen.MoveForward = true; }
            if (Key == "S") { PlayingField.TankGreen.MoveBackward = true; }
            if (Key == "Q") { PlayingField.TankGreen.Fire(); }

            // Red Tank
            if (Key == "Right") PlayingField.TankRed.RotateRight = true;
            if (Key == "Left") PlayingField.TankRed.RotateLeft = true;
            if (Key == "Up") PlayingField.TankRed.MoveForward = true;
            if (Key == "Down") PlayingField.TankRed.MoveBackward = true;
            if (Key == "Space") { PlayingField.TankRed.Fire(); }
        }

        private void TankTroublESS_KeyUp(object sender, KeyEventArgs e)
        {
            // Green Tank
            String Key = e.KeyCode.ToString();
            if (Key == "D") { PlayingField.TankGreen.RotateRight = false; }
            if (Key == "A") { PlayingField.TankGreen.RotateLeft = false; }
            if (Key == "W") { PlayingField.TankGreen.MoveForward = false; }
            if (Key == "S") { PlayingField.TankGreen.MoveBackward = false; }

            // Red Tank
            if (Key == "Right") PlayingField.TankRed.RotateRight = false;
            if (Key == "Left") PlayingField.TankRed.RotateLeft = false;
            if (Key == "Up") PlayingField.TankRed.MoveForward = false;
            if (Key == "Down") PlayingField.TankRed.MoveBackward = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayingField = new PlayingField(ClientScreen);
        }

        private void TankTroublESS_Resize(object sender, EventArgs e)
        {
            ClientScreen = new Rectangle(0, 25, this.ClientRectangle.Width + 6, this.ClientRectangle.Height - 50);
            PlayingField = new PlayingField(ClientScreen);
        }

        private void TankTroublESS_ResizeEnd(object sender, EventArgs e)
        {
            ClientScreen = new Rectangle(0, 25, this.ClientRectangle.Width + 6, this.ClientRectangle.Height - 50);
            PlayingField = new PlayingField(ClientScreen);
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HowToPlayForm HowToPlayForm = new HowToPlayForm();
            HowToPlayForm.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutUsForm AboutUsForm = new AboutUsForm();
            AboutUsForm.ShowDialog();
        }
    }
}