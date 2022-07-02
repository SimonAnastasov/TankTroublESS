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

        public TankTroublESS()
        {
            InitializeComponent();

            ClientScreen = new Rectangle(0, 25, this.ClientRectangle.Width + 6, this.ClientRectangle.Height - 50);

            PlayingField = new PlayingField(ClientScreen);

            timer.Interval = 50;

            timer.Start();

            DoubleBuffered = true;

            Random = new Random();
        }

        private void TankTroublESS_Paint(object sender, PaintEventArgs e)
        {
            PlayingField.DrawBackground(e.Graphics);

            PlayingField.DrawWalls(e.Graphics);

            PlayingField.DrawTanks(e.Graphics);

            Graphics G = e.Graphics;
        }

        private void TankTroublESS_ResizeEnd(object sender, EventArgs e)
        {
            PlayingField.ChangeDimensions(ClientScreen);
            Invalidate();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void TankTroublESS_Load(object sender, EventArgs e)
        {

        }

        private void TankTroublESS_KeyDown(object sender, KeyEventArgs e)
        {
            // Green Tank
            String Key = e.KeyCode.ToString();
            if (Key == "D") { PlayingField.TankGreen.RotateRight = true; }
            if (Key == "A") { PlayingField.TankGreen.RotateLeft = true; }
            if (Key == "W") { PlayingField.TankGreen.MoveForward = true; }
            if (Key == "S") { PlayingField.TankGreen.MoveBackward = true; }

            // Red Tank
            if (Key == "Right") PlayingField.TankRed.RotateRight = true;
            if (Key == "Left") PlayingField.TankRed.RotateLeft = true;
            if (Key == "Up") PlayingField.TankRed.MoveForward = true;
            if (Key == "Down") PlayingField.TankRed.MoveBackward = true;
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
    }
}