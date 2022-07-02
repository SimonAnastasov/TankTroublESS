using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankTroublESS
{
    public class Bullet
    {
        public static readonly int RADIUS = 7;

        public static readonly int SPEED = 11;
        public int X { get; set; }
        public int Y { get; set; }
        public int Direction { get; set; }
        public int TTL { get; set; } = 160;
        public Wall LastHitWall { get; set; }

        public Bullet(int x, int y, int direction)
        {
            X = x;
            Y = y;
            Direction = direction;

            LastHitWall = null;
        }

        public void Draw(Graphics G, int AdjustX, int AdjustY)
        {
            Brush B = new SolidBrush(Color.Black);

            G.FillEllipse(B, X-RADIUS-AdjustX, Y-RADIUS-AdjustY, RADIUS * 2, RADIUS * 2);

            MoveBullet();

            TTL--;
        }

        private void MoveBullet()
        {
            double Radians = (Math.PI / 180) * (Direction - 270);
            int tmpX = (int)(X + SPEED * Math.Cos(Radians));
            int tmpY = (int)(Y + SPEED * Math.Sin(Radians));

            X = tmpX;
            Y = tmpY;
        }
    }
}
