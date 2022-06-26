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
        public Rectangle ClientScreen { get; set; }
        public Background Background { get; set; }
        public Walls Walls { get; set; }

        public PlayingField(Rectangle ClientScreen)
        {
            this.ClientScreen = ClientScreen;
            Background = new Background(ClientScreen);
            Walls = new Walls(ClientScreen);
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

        public void DrawWalls(Graphics G)
        {
            Walls.CreateWalls(G);
        }
    }
}