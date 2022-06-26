using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public TankTroublESS()
        {
            InitializeComponent();

            ClientScreen = new Rectangle(this.ClientRectangle.X, 20, this.ClientRectangle.Width, this.ClientRectangle.Height - 40);

            PlayingField = new PlayingField(ClientScreen);
        }

        private void TankTroublESS_Paint(object sender, PaintEventArgs e)
        {
            PlayingField.DrawBackground(e.Graphics);
        }

        private void TankTroublESS_ResizeEnd(object sender, EventArgs e)
        {
            PlayingField.ChangeDimensions(ClientScreen);
            Invalidate();
        }
    }
}
