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

        public TankTroublESS()
        {
            InitializeComponent();

            ClientScreen = new Rectangle(0, 25, this.ClientRectangle.Width + 6, this.ClientRectangle.Height - 50);

            PlayingField = new PlayingField(ClientScreen);

            timer.Interval = 100;

            timer.Start();

            DoubleBuffered = true;

            TankGreen = new Tank(200, 200, 0, Properties.Resources.TG);
            TankRed = new Tank(50, 50, 0, Properties.Resources.TR);
        }

        private void TankTroublESS_Paint(object sender, PaintEventArgs e)
        {
            PlayingField.DrawBackground(e.Graphics);

            PlayingField.DrawWalls(e.Graphics);

            TankGreen.Draw(e.Graphics, ClientScreen);
            TankRed.Draw(e.Graphics, ClientScreen);
        }

        private void TankTroublESS_ResizeEnd(object sender, EventArgs e)
        {
            PlayingField.ChangeDimensions(ClientScreen);
            Invalidate();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            TankGreen.Rotation += 3;
            TankRed.Rotation += 358;
            Invalidate();
        }
    }
}