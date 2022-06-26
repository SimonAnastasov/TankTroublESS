using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankTroublESS
{
    public class Walls
    {
        public static readonly int HORIZONTAL_DIVIDE = 12;
        public static readonly int VERTICAL_DIVIDE = 7;
        public static readonly int PEN_WIDTH = 4;
        public List<Wall> WallsList { get; set; }
        public Rectangle ClientScreen { get; set; }
        public Random Random { get; set; }

        public Walls(Rectangle ClientScreen)
        {
            this.ClientScreen = ClientScreen;

            Random = new Random();

            WallsList = new List<Wall>();
        }

        public void CreateWalls(Graphics G)
        {
            Pen P = new Pen(Color.Black, PEN_WIDTH);

            float RectWidth = (float)ClientScreen.Width / HORIZONTAL_DIVIDE;
            float RectHeight = (float)ClientScreen.Height / VERTICAL_DIVIDE;

            for (int i = 0; i < HORIZONTAL_DIVIDE; i++)
            {
                for (int j = 0; j < VERTICAL_DIVIDE; j++)
                {
                    int tmp1 = Random.Next(0, 4);

                    if (tmp1 == 0)
                    {
                        G.DrawRectangle(P, ClientScreen.X + i * RectWidth + PEN_WIDTH / 2, ClientScreen.Y + j * RectHeight, RectWidth - PEN_WIDTH, 1);
                    }
                    else if (tmp1 == 1)
                    {
                        G.DrawRectangle(P, ClientScreen.X + i * RectWidth + RectWidth, ClientScreen.Y + j * RectHeight + PEN_WIDTH / 2, 1, RectHeight - PEN_WIDTH);
                    }
                }
            }
        }
    }
}