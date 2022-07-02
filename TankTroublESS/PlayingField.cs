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
        public Tank TankGreen { get; set; }
        public Tank TankRed { get; set; }
        public Walls Walls { get; set; }
        public bool WallsAreSetForTanks { get; set; } = false;

        public PlayingField(Rectangle ClientScreen)
        {
            this.ClientScreen = ClientScreen;
            Background = new Background(ClientScreen);
            Walls = new Walls(ClientScreen);

            TankGreen = new Tank(100, 100, 0, Properties.Resources.TG, ClientScreen);
            TankRed = new Tank(500, 500, 0, Properties.Resources.TR, ClientScreen);
        }

        public void ChangeDimensions(Rectangle ClientScreen)
        {
            this.ClientScreen = ClientScreen;

            Background.ChangeDimensions(ClientScreen);
        }

        public void DrawTanks(Graphics G)
        {
            TankGreen.Draw(G, 0, 0);
            TankRed.Draw(G, TankGreen.X, TankGreen.Y);
        }

        public void DrawBackground(Graphics G)
        {
            Background.Draw(G);
        }

        public void DrawWalls(Graphics G)
        {
            Walls.DrawWalls(G);

            if (!WallsAreSetForTanks)
            {
                WallsAreSetForTanks = true;

                TankGreen.SetWalls(Walls.WallsList);
                TankRed.SetWalls(Walls.WallsList);
            }
        }
    }
}