using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankTroublESS
{
    public class Background
    {
        public static readonly int HORIZONTAL_DIVIDE = 12;

        public static readonly int VERTICAL_DIVIDE = 7;
        public Rectangle ClientScreen { get; set; }

        public Background(Rectangle ClientScreen)
        {
            this.ClientScreen = ClientScreen;
        }

        public void ChangeDimensions(Rectangle ClientScreen)
        {
            this.ClientScreen = ClientScreen;
        }

        public void Draw(Graphics G)
        {
            Brush BrushWhite = new SolidBrush(Color.White);
            Brush BrushGray = new SolidBrush(Color.FromArgb(50, Color.LightGray));

            float RectWidth = (float)ClientScreen.Width / HORIZONTAL_DIVIDE;
            float RectHeight = (float)ClientScreen.Height / VERTICAL_DIVIDE;

            for (int i = 0; i < HORIZONTAL_DIVIDE; i++)
            {
                for (int j = 0; j < VERTICAL_DIVIDE; j++)
                {
                    if ((i % 2 == 0 && j % 2 == 1) || (i % 2 == 1 && j % 2 == 0))
                    {
                        G.FillRectangle(BrushWhite, ClientScreen.X + i * RectWidth, ClientScreen.Y + j * RectHeight, RectWidth, RectHeight);
                    }
                    else
                    {
                        G.FillRectangle(BrushGray, ClientScreen.X + i * RectWidth, ClientScreen.Y + j * RectHeight, RectWidth, RectHeight);
                    }
                }
            }
        }
    }
}
