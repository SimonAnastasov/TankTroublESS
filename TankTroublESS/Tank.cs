using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankTroublESS
{
    public class Tank
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Rotation { get; set; }
        public Bitmap TankImage { get; set; }
        public List<Wall> Walls { get; set; }
        public Rectangle ClientScreen { get; set; }

        public bool MoveForward { get; set; } = false;
        public bool MoveBackward { get; set; } = false;
        public bool RotateRight { get; set; } = false;
        public bool RotateLeft { get; set; } = false;

        public Tank(int x, int y, int rotation, Bitmap tankImage, Rectangle clientScreen)
        {
            X = x;
            Y = y;
            Rotation = rotation;
            TankImage = tankImage;

            ClientScreen = clientScreen;

            calculateWidthAndHeight();
        }

        public void SetWalls(List<Wall> Walls)
        {
            this.Walls = Walls;
        }

        public void Draw(Graphics G, int AdjustX, int AdjustY)
        {
            SetTankBox();

            calculateWidthAndHeight();

            Bitmap bmp = new Bitmap(Width, Height);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.TranslateTransform(Width / 2, Height / 2);
            gfx.RotateTransform(Rotation);
            gfx.TranslateTransform(-TankImage.Width / 2, -TankImage.Height / 2);
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            gfx.DrawImage(TankImage, 0, 0);
            G.TranslateTransform(X - AdjustX, Y - AdjustY);
            G.DrawImage(bmp, -bmp.Width / 2, -bmp.Height / 2);
        }

        private void SetTankBox()
        {
            int ROTATE_BY = 10;
            int SPEED = 10;

            if (RotateRight)
            {
                int tmpRotation = Rotation + ROTATE_BY;

                Rotation = tmpRotation;
            }
            if (RotateLeft)
            {
                int tmpRotation = Rotation + (360 - ROTATE_BY);

                Rotation = tmpRotation;
            }
            if (MoveForward)
            {
                double Radians = (Math.PI / 180) * (Rotation - 270);
                int tmpX = (int)(X + SPEED * Math.Cos(Radians));
                int tmpY = (int)(Y + SPEED * Math.Sin(Radians));

                if (CanMove(tmpX, tmpY))
                {
                    X = tmpX;
                    Y = tmpY;
                }
            }
            if (MoveBackward)
            {
                double Radians = (Math.PI / 180) * (Rotation - 90);
                int tmpX = (int)(X + SPEED * Math.Cos(Radians));
                int tmpY = (int)(Y + SPEED * Math.Sin(Radians));

                if (CanMove(tmpX, tmpY))
                {
                    X = tmpX;
                    Y = tmpY;
                }
            }

            Rotation %= 360;
        }

        private bool CanMove(int X, int Y)
        {
            calculateWidthAndHeight();

            Point TL = new Point(X, Y);
            Point BR = new Point(X + Width, Y + Height);

            int EXTRA_TANK_LENGTH = 30;

            bool canMove = true;
            foreach(Wall Wall in Walls)
            {
                Rectangle W = Wall.WallRect;
                Point WTL = new Point((int)W.X, (int)W.Y);
                Point WBR = new Point((int)(W.X + W.Width), (int)(W.Y + W.Height));


                if (TL.X - EXTRA_TANK_LENGTH < WBR.X && BR.X - EXTRA_TANK_LENGTH > WTL.X &&
                    TL.Y - EXTRA_TANK_LENGTH < WBR.Y && BR.Y - EXTRA_TANK_LENGTH > WTL.Y)
                {
                    canMove = false;
                }
            }

            return canMove;
        }

        private void calculateWidthAndHeight()
        {
            int newWidth = 50;
            int newHeight = 50;

            Bitmap bmp = new Bitmap(TankImage.Width, TankImage.Height);
            if (Rotation <= 90)
            {
                newWidth = (int)(bmp.Width * Math.Cos(2 * Math.PI * Rotation / 360) + bmp.Height * Math.Sin(2 * Math.PI * Rotation / 360));
                newHeight = (int)(bmp.Height * Math.Cos(2 * Math.PI * Rotation / 360) + bmp.Width * Math.Sin(2 * Math.PI * Rotation / 360));
            }
            else if (Rotation > 90 && Rotation <= 180)
            {
                newWidth = (int)(bmp.Width * -Math.Cos(2 * Math.PI * Rotation / 360) + bmp.Height * Math.Sin(2 * Math.PI * Rotation / 360));
                newHeight = (int)(bmp.Height * -Math.Cos(2 * Math.PI * Rotation / 360) + bmp.Width * Math.Sin(2 * Math.PI * Rotation / 360));
            }
            else if (Rotation > 90 && Rotation <= 270)
            {
                newWidth = (int)(bmp.Width * -Math.Cos(2 * Math.PI * Rotation / 360) + bmp.Height * -Math.Sin(2 * Math.PI * Rotation / 360));
                newHeight = (int)(bmp.Height * -Math.Cos(2 * Math.PI * Rotation / 360) + bmp.Width * -Math.Sin(2 * Math.PI * Rotation / 360));
            }
            else if (Rotation > 270 && Rotation <= 360)
            {
                newWidth = (int)(bmp.Width * Math.Cos(2 * Math.PI * Rotation / 360) + bmp.Height * -Math.Sin(2 * Math.PI * Rotation / 360));
                newHeight = (int)(bmp.Height * Math.Cos(2 * Math.PI * Rotation / 360) + bmp.Width * -Math.Sin(2 * Math.PI * Rotation / 360));
            }

            Width = newWidth;
            Height = newHeight;
        }
    }
}
