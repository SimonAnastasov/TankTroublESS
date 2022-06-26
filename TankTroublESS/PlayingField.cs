using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankTroublESS
{
    public class PlayingField
    {
        public Background Background { get; set; }
        public Rectangle ClientScreen { get; set; }

        public PlayingField(Rectangle ClientScreen)
        {
            this.ClientScreen = ClientScreen;
            Background = new Background(ClientScreen);
        }

        public void ChangeDimensions(Rectangle ClientScreen)
        {
            this.ClientScreen = ClientScreen;

            Background.ChangeDimensions(ClientScreen);
        }

        public void DrawBackground(Graphics G)
        {
            Background.Draw(G);
        }
    }
}
