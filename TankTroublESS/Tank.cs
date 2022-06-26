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
        public int Rotation { get; set; }
        public Bitmap TankImage { get; set; }

        public bool MoveForward { get; set; } = false;
        public bool MoveBackward { get; set; } = false;
        public bool RotateRight { get; set; } = false;
        public bool RotateLeft { get; set; } = false;

        public Tank(int x, int y, int rotation, Bitmap tankImage)
        {
            X = x;
            Y = y;
            Rotation = rotation;
            TankImage = tankImage;
        }

        public void Draw(Graphics G, Rectangle ClientScreen, int AdjustX, int AdjustY)
        {
            SetTankBox();

            Dictionary<String, int> Dict = calculateWidthAndHeight();
            int newWidth = Dict["width"];
            int newHeight = Dict["height"];

            Bitmap bmp = new Bitmap(newWidth, newHeight);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.TranslateTransform(newWidth / 2, newHeight / 2);
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
                Rotation += ROTATE_BY;
            }
            if (RotateLeft)
            {
                Rotation += (360 - ROTATE_BY);
            }
            if (MoveForward)
            {
                double Radians = (Math.PI / 180) * (Rotation - 270);
                X = (int)(X + SPEED * Math.Cos(Radians));
                Y = (int)(Y + SPEED * Math.Sin(Radians));
            }
            if (MoveBackward)
            {
                double Radians = (Math.PI / 180) * (Rotation - 90);
                X = (int)(X + SPEED * Math.Cos(Radians));
                Y = (int)(Y + SPEED * Math.Sin(Radians));
            }

            Rotation %= 360;
        }

        private Dictionary<String, int> calculateWidthAndHeight()
        {
            Dictionary<String, int> Dict = new Dictionary<String, int>();

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

            Dict["width"] = newWidth;
            Dict["height"] = newHeight;

            return Dict;
        }
    }
}
