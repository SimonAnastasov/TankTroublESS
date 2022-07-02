using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankTroublESS
{
    public class Wall
    {
        public static readonly int PEN_WIDTH = 5;
        public Rectangle WallRect { get; set; }
        public bool IsHorizontal { get; set; }

        public Wall(Rectangle WallRect, bool IsHorizontal)
        {
            this.WallRect = WallRect;
            this.IsHorizontal = IsHorizontal;
        }

        public void Draw(Graphics G)
        {
            Pen P = new Pen(Color.Black, PEN_WIDTH);

            G.DrawRectangle(P, WallRect.X, WallRect.Y, WallRect.Width, WallRect.Height);
        }
    }
}