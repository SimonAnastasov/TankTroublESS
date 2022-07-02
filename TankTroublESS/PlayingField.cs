using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TankTroublESS
{
    public class PlayingField
    {
        public static readonly int HORIZONTAL_DIVIDE = 12;

        public static readonly int VERTICAL_DIVIDE = 7;
        public Rectangle ClientScreen { get; set; }
        public Background Background { get; set; }
        public Tank TankGreen { get; set; }
        public Tank TankRed { get; set; }
        public Walls Walls { get; set; }
        public bool WallsAreSetForTanks { get; set; } = false;
        public List<Bullet> Bullets { get; set; }
        public Random Random { get; set; }
        public MediaPlayer TankMovingMedia { get; set; }

        public PlayingField(Rectangle ClientScreen)
        {
            this.ClientScreen = ClientScreen;
            Background = new Background(ClientScreen);
            Walls = new Walls(ClientScreen);

            Random = new Random();

            Point TankPoint = SpawnTankPoint();
            int TankRotation = SpawnTankRotation();
            TankGreen = new Tank(TankPoint.X, TankPoint.Y, TankRotation, Properties.Resources.TG, ClientScreen);

            TankPoint = SpawnTankPoint();
            TankRotation = SpawnTankRotation();
            TankRed = new Tank(TankPoint.X, TankPoint.Y, TankRotation, Properties.Resources.TR, ClientScreen);

            PlayBackgroundSound();
        }

        public void CheckIfBothTanksDead()
        {
            if (!TankGreen.IsAlive && !TankRed.IsAlive)
            {
                TankMovingMedia.Pause();
            }
        }
        public bool CheckIfOneTankDead()
        {
            if (!TankGreen.IsAlive || !TankRed.IsAlive)
            {
                return true;
            }

            return false;
        }

        private void PlayBackgroundSound()
        {
            TankMovingMedia = new MediaPlayer();

            TankMovingMedia.Open(new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"/../../audio/TankMoving.wav"));

            TankMovingMedia.Play();
        }
        private void PlayTankExplodingSound()
        {
            var MediaPlayer = new MediaPlayer();
            MediaPlayer.Open(new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"/../../audio/TankExploding.wav"));
            MediaPlayer.Play();
        }

        public void ChangeDimensions(Rectangle ClientScreen)
        {
            this.ClientScreen = ClientScreen;

            Background.ChangeDimensions(ClientScreen);
        }

        public Point SpawnTankPoint()
        {
            int Rnd = Random.Next(0, HORIZONTAL_DIVIDE * VERTICAL_DIVIDE);

            int RectWidth = (int)ClientScreen.Width / HORIZONTAL_DIVIDE;
            int RectHeight = (int)ClientScreen.Height / VERTICAL_DIVIDE;

            int i = (int)(Rnd / HORIZONTAL_DIVIDE);
            int j = Rnd % VERTICAL_DIVIDE;
            
            return new Point((int)ClientScreen.X + i * RectWidth + (RectWidth / 2), (int)ClientScreen.Y + j * RectHeight + (RectHeight / 2));
        }

        public int SpawnTankRotation()
        {
            return Random.Next(0, 360/6) * 6;
        }

        public void CheckIfTankHit()
        {
            Bullets = new List<Bullet>();
            foreach (Bullet Bullet in TankGreen.Bullets)
            {
                Bullets.Add(Bullet);
            }
            foreach (Bullet Bullet in TankRed.Bullets)
            {
                Bullets.Add(Bullet);
            }

            int EXTRA_TANK_LENGTH = 30;

            // Green Tank
            if (TankGreen.IsAlive)
            {
                Point TL = new Point(TankGreen.X, TankGreen.Y);
                Point BR = new Point(TankGreen.X + TankGreen.Width, TankGreen.Y + TankGreen.Height);
                for (int i = 0; i < Bullets.Count; i++)
                {
                    Bullet Bullet = Bullets[i];

                    if (Bullet.X - Bullet.RADIUS < BR.X - EXTRA_TANK_LENGTH && Bullet.X + Bullet.RADIUS > TL.X - EXTRA_TANK_LENGTH &&
                        Bullet.Y - Bullet.RADIUS < BR.Y - EXTRA_TANK_LENGTH && Bullet.Y + Bullet.RADIUS > TL.Y - EXTRA_TANK_LENGTH)
                    {
                        TankGreen.IsAlive = false;
                        TankGreen.RemoveBulletIfYours(Bullet);
                        TankRed.RemoveBulletIfYours(Bullet);

                        PlayTankExplodingSound();
                    }
                }
            }

            // Red Tank
            if (TankRed.IsAlive)
            {
                Point TL = new Point(TankRed.X, TankRed.Y);
                Point BR = new Point(TankRed.X + TankRed.Width, TankRed.Y + TankRed.Height);
                for (int i = 0; i < Bullets.Count; i++)
                {
                    Bullet Bullet = Bullets[i];

                    if (Bullet.X - Bullet.RADIUS < BR.X - EXTRA_TANK_LENGTH && Bullet.X + Bullet.RADIUS > TL.X - EXTRA_TANK_LENGTH &&
                        Bullet.Y - Bullet.RADIUS < BR.Y - EXTRA_TANK_LENGTH && Bullet.Y + Bullet.RADIUS > TL.Y - EXTRA_TANK_LENGTH)
                    {
                        TankRed.IsAlive = false;
                        TankGreen.RemoveBulletIfYours(Bullet);
                        TankRed.RemoveBulletIfYours(Bullet);

                        PlayTankExplodingSound();
                    }
                }
            }
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