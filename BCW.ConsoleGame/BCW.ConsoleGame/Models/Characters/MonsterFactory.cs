using BCW.ConsoleGame.Models.Scenes;
using BCW.ConsoleGame.Models.Treasures;
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
        private IList<string> treasureNames;

        private MonsterFactory(IList<IMonsterType> monsterTypes)
        {
            this.monsterTypes = monsterTypes;

            treasureNames = new List<string>()
            {
                "Gold",
                "Silver",
                "Platinum",
                "Ruby",
                "Diamond",
                "Sapphire",
                "Emerald",
                "Garnet",
                "Amulet",
                "Ring"
            };
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
                        var countMin = monsterType.LevelOdds[scene.Difficulty].CountMin;
                        var countMax = monsterType.LevelOdds[scene.Difficulty].CountMax;
                        var count = random.Next(countMin, countMax);

                        while (count > 0)
                        {
                            var health = random.Next(monsterType.HealthMin, monsterType.HealthMax);
                            var agility = random.Next(monsterType.AgilityMin, monsterType.AgilityMax);
                            var damage = random.Next(monsterType.DamageMin, monsterType.DamageMax);
                            var defense = random.Next(monsterType.DefenseMin, monsterType.DefenseMax);
                            var vitality = random.Next(monsterType.VitalityMin, monsterType.VitalityMax);

                            var treasureIndex = random.Next(0, 9);
                            var treasureValue = random.Next(5, 25);

                            var treasure = new Treasure()
                            {
                                Name = treasureNames[treasureIndex],
                                Value = treasureValue
                            };

                            var monster = new Monster()
                            {
                                Name = monsterType.Name,
                                Health = health,
                                Agility = agility,
                                Damage = damage,
                                Defense = defense,
                                Vitality = vitality
                            };

                            monster.AddItem("Treasures", treasure);

                            monsters.Add(monster);

                            count--;
                        }
                    }
                }
            }

            return monsters;
        }
    }
}
