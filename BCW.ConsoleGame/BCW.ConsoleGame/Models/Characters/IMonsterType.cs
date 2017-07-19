using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Characters
{
    public interface IMonsterType
    {
        string Name { get; set; }
        int HealthMin { get; set; }
        int HealthMax { get; set; }
        int DamageMin { get; set; }
        int DamageMax { get; set; }
        IDictionary<int, Odds> LevelOdds { get; set; }
    }
}
