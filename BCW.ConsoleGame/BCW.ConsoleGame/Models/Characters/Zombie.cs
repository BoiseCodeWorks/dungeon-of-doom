using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Characters
{
    public class Zombie : Monster
    {
        public Zombie()
        {
            items = new List<IComposite>();
        }
    }
}
