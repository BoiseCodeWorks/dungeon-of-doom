using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models
{
    public class MapPosition
    {
        public int X { get; set; }
        public int Y { get; set; }

        public MapPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
