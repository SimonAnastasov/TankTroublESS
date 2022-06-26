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
        public Tank TankGreen { get; set; }
        public Tank TankRed { get; set; }
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

            TankGreen = new Tank(100, 100, 0, Properties.Resources.TG);
            TankRed = new Tank(500, 500, 0, Properties.Resources.TR);
        }

        private void TankTroublESS_Paint(object sender, PaintEventArgs e)
        {
            PlayingField.DrawBackground(e.Graphics);

            PlayingField.DrawWalls(e.Graphics);

            Graphics G = e.Graphics;

            TankGreen.Draw(G, ClientScreen, 0, 0);
            TankRed.Draw(G, ClientScreen, TankGreen.X, TankGreen.Y);
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
            if (Key == "D") { TankGreen.RotateRight = true; }
            if (Key == "A") { TankGreen.RotateLeft = true; }
            if (Key == "W") { TankGreen.MoveForward = true; }
            if (Key == "S") { TankGreen.MoveBackward = true; }

            // Red Tank
            if (Key == "Right") TankRed.RotateRight = true;
            if (Key == "Left") TankRed.RotateLeft = true;
            if (Key == "Up") TankRed.MoveForward = true;
            if (Key == "Down") TankRed.MoveBackward = true;
        }

        private void TankTroublESS_KeyUp(object sender, KeyEventArgs e)
        {
            // Green Tank
            String Key = e.KeyCode.ToString();
            if (Key == "D") { TankGreen.RotateRight = false; }
            if (Key == "A") { TankGreen.RotateLeft = false; }
            if (Key == "W") { TankGreen.MoveForward = false; }
            if (Key == "S") { TankGreen.MoveBackward = false; }

            // Red Tank
            if (Key == "Right") TankRed.RotateRight = false;
            if (Key == "Left") TankRed.RotateLeft = false;
            if (Key == "Up") TankRed.MoveForward = false;
            if (Key == "Down") TankRed.MoveBackward = false;
        }
    }
}