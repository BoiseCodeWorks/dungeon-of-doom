using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Characters
{
    public class Monster : Composite, IMonster
    {
        private int originalHealth;

        public int Health { get; set; }
        public int Agility { get; set; }
        public int Damage { get; set; }
        public int Defense { get; set; }
        public int Vitality { get; set; }

        public Monster()
        {
            items = new List<IComposite>();
        }

        public int Attack()
        {
            var random = new Random();

            if (random.Next(1, 100) <= Agility)
            {
                return Damage;
            }

            return 0;
        }

        public int Defend()
        {
            var random = new Random();
            decimal damageTaken = 0;

            originalHealth = originalHealth > 0 ? originalHealth : Health;

            var hitOdds = 100 - Defense;

            if (random.Next(1, 100) <= hitOdds)
            {
                damageTaken =  Convert.ToDecimal((100 - Vitality)) / 100 * originalHealth;

                Health -= (int)damageTaken;

                if (Health < 0) Health = 0;
            }

            return (int)damageTaken;
        }
    }
}
