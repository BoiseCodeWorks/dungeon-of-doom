using BCW.ConsoleGame.Models.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Data
{
    public interface IDataProvider
    {
        List<IScene> LoadScenes();
    }
}
