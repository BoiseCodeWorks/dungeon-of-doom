using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Characters
{
    public class Monster : Composite, IMonster
    {
        public int Health { get; set; }
        public int Damage { get; set; }

        public Monster()
        {
            items = new List<IComposite>();
        }
    }
}
