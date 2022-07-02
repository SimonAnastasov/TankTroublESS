using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TankTroublESS
{
    public class Tank
    {
        public static readonly int ROTATE_BY = 8;

        public static readonly int SPEED = 8;

        public static readonly int MAX_BULLETS = 6;
        public Rectangle ClientScreen { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Rotation { get; set; }
        public Bitmap TankImage { get; set; }
        public List<Wall> Walls { get; set; }
        public List<Bullet> Bullets { get; set; }

        public bool IsAlive { get; set; } = true;

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

            CalculateWidthAndHeight();

            Walls = new List<Wall>();
            Bullets = new List<Bullet>();
        }
        public void SetWalls(List<Wall> Walls)
        {
            this.Walls = Walls;
        }

        public void RemoveBulletIfYours(Bullet Bullet)
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                if (Bullets[i] == Bullet)
                {
                    Bullets.Remove(Bullets[i]);
                }
            }
        }
        private void PlayTankFiringSound()
        {
            var MediaPlayer = new MediaPlayer();
            MediaPlayer.Open(new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"/../../audio/TankFiring.wav"));
            MediaPlayer.Play();
        }
        public void Fire()
        {
            if (Bullets.Count < MAX_BULLETS && IsAlive)
            {
                int MoveBy = 40;
                if (Rotation % 90 > 20 && Rotation % 90 < 70)
                {
                    MoveBy += 7;
                }
                if (Rotation % 90 > 30 && Rotation % 90 < 60)
                {
                    MoveBy += 7;
                }

                double Radians = (Math.PI / 180) * (Rotation - 270);
                int tmpX = (int)(X + MoveBy * Math.Cos(Radians));
                int tmpY = (int)(Y + MoveBy * Math.Sin(Radians));

                Bullets.Add(new Bullet(tmpX, tmpY, Rotation));

                PlayTankFiringSound();
            }
        }

        private void CheckIfBulletHitWall(Bullet Bullet)
        {

            foreach (Wall Wall in Walls)
            {
                Rectangle W = Wall.WallRect;
                Point WTL = new Point((int)W.X, (int)W.Y);
                Point WBR = new Point((int)(W.X + W.Width), (int)(W.Y + W.Height));

                if (Bullet.X - Bullet.RADIUS < WBR.X && Bullet.X + Bullet.RADIUS > WTL.X &&
                    Bullet.Y - Bullet.RADIUS < WBR.Y && Bullet.Y + Bullet.RADIUS > WTL.Y &&
                    Bullet.LastHitWall != Wall)
                {
                    Bullet.LastHitWall = Wall;
                    Bullet.Direction = CalculateBulletDirection(Bullet, Wall);
                }
            }
        }

        private int CalculateBulletDirection(Bullet Bullet, Wall Wall)
        {
            int NewDirection = Bullet.Direction;

            if (Wall.IsHorizontal)
            {
                if (Bullet.Direction > 90 && Bullet.Direction <= 270)
                {
                    NewDirection += 2 * (90 - Bullet.Direction);
                }
                else
                {
                    NewDirection += 2 * (270 - Bullet.Direction);
                }
            }
            else
            {
                if (Bullet.Direction >= 0 && Bullet.Direction < 180)
                {
                    NewDirection += 2 * (180 - Bullet.Direction);
                }
                else
                {
                    NewDirection += 2 * (0 - Bullet.Direction);
                }
            }

            return NewDirection % 360;
        }

        public void Draw(Graphics G, int AdjustX, int AdjustY)
        {
            // Draw Bullets
            for (int i = 0; i < Bullets.Count; i++)
            {
                Bullet Bullet = Bullets[i];

                if (Bullet.TTL <= 0)
                {
                    Bullets.Remove(Bullet);
                }

                Bullet.Draw(G, AdjustX, AdjustY);

                CheckIfBulletHitWall(Bullet);
            }

            // Draw Tank
            SetTankBox();

            CalculateWidthAndHeight();

            Bitmap bmp = new Bitmap(Width, Height);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.TranslateTransform(Width / 2, Height / 2);
            gfx.RotateTransform(Rotation);
            gfx.TranslateTransform(-TankImage.Width / 2, -TankImage.Height / 2);
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            gfx.DrawImage(TankImage, 0, 0);
            G.TranslateTransform(X - AdjustX, Y - AdjustY);
            
            if (IsAlive)
                G.DrawImage(bmp, -bmp.Width / 2, -bmp.Height / 2);
        }

        private void SetTankBox()
        {
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
            CalculateWidthAndHeight();

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

        private void CalculateWidthAndHeight()
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
