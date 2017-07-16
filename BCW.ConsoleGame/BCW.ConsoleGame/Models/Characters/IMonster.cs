using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Characters
{
    public interface IMonster : IComposite
    {
        int Health { get; set; }
        int Damage { get; set; }
    }
}
