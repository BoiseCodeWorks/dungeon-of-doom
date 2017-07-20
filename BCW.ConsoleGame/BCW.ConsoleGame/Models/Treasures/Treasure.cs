using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Treasures
{
    public class Treasure : Composite, ITreasure
    {
        public int Value { get; set; }

        public Treasure()
        {
            items = new List<IComposite>();
        }
    }
}
