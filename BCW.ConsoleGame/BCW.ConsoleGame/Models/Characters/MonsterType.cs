using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Characters
{
    public class MonsterType : IMonsterType
    {
        public string Name { get; set; }
        public int HealthMin { get; set; }
        public int HealthMax { get; set; }
        public int AgilityMin { get; set; }
        public int AgilityMax { get; set; }
        public int DamageMin { get; set; }
        public int DamageMax { get; set; }
        public int DefenseMin { get; set; }
        public int DefenseMax { get; set; }
        public int VitalityMin { get; set; }
        public int VitalityMax { get; set; }
        public IDictionary<int, Odds> LevelOdds { get; set; }

        public MonsterType()
        {
            LevelOdds = new Dictionary<int, Odds>();
        }

        public MonsterType(string name, int healthMin, int healthMax, int agilityMin, int agilityMax, int damageMin, int damageMax, int defenseMin, int defenseMax, int vitalityMin, int vitalityMax, IDictionary<int, Odds> levelOdds)
        {
            Name = name;
            HealthMin = healthMin;
            HealthMax = healthMax;
            AgilityMin = agilityMin;
            AgilityMax = agilityMax;
            DamageMin = damageMin;
            DamageMax = damageMax;
            DefenseMin = defenseMin;
            DefenseMax = defenseMax;
            VitalityMin = vitalityMin;
            VitalityMax = vitalityMax;
            LevelOdds = levelOdds;
        }
    }
}
