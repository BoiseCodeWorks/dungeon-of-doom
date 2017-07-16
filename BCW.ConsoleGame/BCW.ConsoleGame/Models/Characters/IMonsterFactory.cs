using BCW.ConsoleGame.Models.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Characters
{
    public interface IMonsterFactory
    {
        IList<IMonster> CreateMonsters(IScene scene);
    }
}
