using BCW.ConsoleGame.Models.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Characters
{
    public class MonsterFactory : IMonsterFactory
    {
        private static IMonsterFactory instance;
        private IList<IMonsterType> monsterTypes;

        private MonsterFactory(IList<IMonsterType> monsterTypes)
        {
            this.monsterTypes = monsterTypes;
        }

        public static IMonsterFactory Instance(IList<IMonsterType> monsterTypes = null)
        {
            if(instance == null)
            {
                instance = new MonsterFactory(monsterTypes);
            }

            return instance;
        }

        public IList<IMonster> CreateMonsters(IScene scene)
        {
            var monsters = new List<IMonster>();
            var random = new Random();

            if (scene.Difficulty > 0)
            {
                foreach (var monsterType in monsterTypes)
                {
                    var odds = monsterType.LevelOdds[scene.Difficulty];

                    if (random.Next(1, 100) <= odds.Exist)
                    {
                        var health = random.Next(monsterType.HealthMin, monsterType.HealthMax);
                        var damage = random.Next(monsterType.DamageMin, monsterType.DamageMax);

                        monsters.Add(new Monster() { Name = monsterType.Name, Health = health, Damage = damage });
                    }
                }
            }

            return monsters;
        }
    }
}
