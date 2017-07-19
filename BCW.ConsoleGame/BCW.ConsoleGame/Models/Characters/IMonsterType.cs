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
        int AgilityMin { get; set; }
        int AgilityMax { get; set; }
        int DamageMin { get; set; }
        int DamageMax { get; set; }
        int DefenseMin { get; set; }
        int DefenseMax { get; set; }
        int VitalityMin { get; set; }
        int VitalityMax { get; set; }
        IDictionary<int, Odds> LevelOdds { get; set; }
    }
}
