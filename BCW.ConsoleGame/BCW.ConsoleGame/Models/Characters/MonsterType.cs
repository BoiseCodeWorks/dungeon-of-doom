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
        public int DamageMin { get; set; }
        public int DamageMax { get; set; }
        public IDictionary<int, Odds> LevelOdds { get; set; }

        public MonsterType()
        {
            LevelOdds = new Dictionary<int, Odds>();
        }

        public MonsterType(string name, int healthMin, int healthMax, int damageMin, int damageMax, IDictionary<int, Odds> levelOdds)
        {
            Name = name;
            HealthMin = healthMin;
            HealthMax = healthMax;
            DamageMin = damageMin;
            DamageMax = damageMax;
            LevelOdds = levelOdds;
        }
    }
}
