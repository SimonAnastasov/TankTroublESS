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

        public static readonly int PEN_WIDTH = 5;
        public List<Wall> WallsList { get; set; }
        public Rectangle ClientScreen { get; set; }
        public Random Random { get; set; }

        public Walls(Rectangle ClientScreen)
        {
            this.ClientScreen = ClientScreen;

            Random = new Random();

            WallsList = new List<Wall>();
        }

        public void DrawWalls(Graphics G)
        {
            if (WallsList.Count == 0) CreateWalls();

            foreach (Wall Wall in WallsList)
            {
                Wall.Draw(G);
            }
        }

        public void CreateWalls()
        {
            float RectWidth = (float)ClientScreen.Width / HORIZONTAL_DIVIDE;
            float RectHeight = (float)ClientScreen.Height / VERTICAL_DIVIDE;

            for (int i = 0; i < HORIZONTAL_DIVIDE; i++)
            {
                for (int j = 0; j < VERTICAL_DIVIDE; j++)
                {
                    int tmp1 = Random.Next(0, 4);

                    if (tmp1 == 0)
                    {
                        WallsList.Add(new Wall(new Rectangle(ClientScreen.X + i * RectWidth + PEN_WIDTH / 2, ClientScreen.Y + j * RectHeight, RectWidth - PEN_WIDTH, 1), true));
                    }
                    else if (tmp1 == 1)
                    {
                        WallsList.Add(new Wall(new Rectangle(ClientScreen.X + i * RectWidth + RectWidth, ClientScreen.Y + j * RectHeight + PEN_WIDTH / 2, 1, RectHeight - PEN_WIDTH), false));
                    }
                }
            }

            // Walls on the sides
            WallsList.Add(new Wall(new Rectangle(ClientScreen.X, ClientScreen.Y, ClientScreen.X + ClientScreen.Width, PEN_WIDTH), true));
            WallsList.Add(new Wall(new Rectangle(ClientScreen.X, ClientScreen.Y + ClientScreen.Height - (PEN_WIDTH-1), ClientScreen.X + ClientScreen.Width, PEN_WIDTH), true));
            WallsList.Add(new Wall(new Rectangle(ClientScreen.X, ClientScreen.Y, PEN_WIDTH, ClientScreen.Y + ClientScreen.Height), false));
            WallsList.Add(new Wall(new Rectangle(ClientScreen.X + ClientScreen.Width - (PEN_WIDTH*2+2), ClientScreen.Y, PEN_WIDTH, ClientScreen.Y + ClientScreen.Height), false));
        }
    }
}