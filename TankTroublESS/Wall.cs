using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankTroublESS
{
    public class Wall
    {
        public Rectangle WallRect { get; set; }

        public Wall(Rectangle WallRect)
        {
            this.WallRect = WallRect;
        }
    }
}
