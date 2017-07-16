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

        private MonsterFactory()
        {
        }

        public static IMonsterFactory Instance()
        {
            if(instance == null)
            {
                instance = new MonsterFactory();
            }

            return instance;
        }

        public IList<IMonster> CreateMonsters(IScene scene)
        {
            var monsters = new List<IMonster>();
            var random = new Random();
            var countOdds = 0;
            var countMin = 0;
            var countMax = 0;
            var zombieOdds = 0;
            var orcOdds = 0;
            var trollOdds = 0;
            var dragonOdds = 0;
            var healthMin = 0;
            var healthMax = 0;
            var damageMin = 0;
            var damageMax = 0;

            switch (scene.Difficulty)
            {
                case 1:
                    countOdds = 50;
                    zombieOdds = 100;
                    countMax = 2;
                    healthMin = 5;
                    healthMax = 10;
                    damageMin = 2;
                    damageMax = 5;
                    break;
                case 2:
                    countOdds = 40;
                    zombieOdds = 50;
                    orcOdds = 100;
                    countMin = 1;
                    countMax = 4;
                    healthMin = 7;
                    healthMax = 12;
                    damageMin = 7;
                    damageMax = 15;
                    break;

                case 3:
                    countOdds = 60;
                    zombieOdds = 40;
                    orcOdds = 70;
                    trollOdds = 100;
                    countMin = 2;
                    countMax = 5;
                    healthMin = 10;
                    healthMax = 15;
                    damageMin = 9;
                    damageMax = 17;
                    break;

                case 4:
                    countOdds = 80;
                    zombieOdds = 20;
                    orcOdds = 50;
                    trollOdds = 80;
                    dragonOdds = 100;
                    countMin = 3;
                    countMax = 6;
                    healthMin = 15;
                    healthMax = 20;
                    damageMin = 12;
                    damageMax = 20;
                    break;
            }

            if(countOdds <= random.Next(1, 100))
            {
                for (var i = 0; i < random.Next(countMin, countMax); i++)
                {
                    var monsterOdds = random.Next(1, 100);
                    var health = random.Next(healthMin, healthMax);
                    var damage = random.Next(damageMin, damageMax);

                    if (monsterOdds <= zombieOdds)
                    {
                        monsters.Add(new Zombie() { Name = "Zombie", Health = health, Damage = damage });
                    }
                    else if (monsterOdds <= orcOdds)
                    {
                        monsters.Add(new Orc() { Name = "Orc", Health = health, Damage = damage });
                    }
                    else if (monsterOdds <= trollOdds)
                    {
                        monsters.Add(new Troll() { Name = "Troll", Health = health, Damage = damage });
                    }
                    else if (monsterOdds <= dragonOdds)
                    {
                        monsters.Add(new Dragon() { Name = "Dragon", Health = health, Damage = damage });
                    }
                }
            }

            return monsters;
        }
    }
}
