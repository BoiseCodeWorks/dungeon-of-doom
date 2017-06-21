using BCW.ConsoleGame.Models;
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
        MapPosition StartPosition { get; set; }
        List<IScene> Scenes { get; set; }

        void SaveGameState();
    }
}
