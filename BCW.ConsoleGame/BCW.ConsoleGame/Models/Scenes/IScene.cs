using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Scenes
{
    public interface IScene
    {
        string Title { get; set; }
        string Description { get; set; }
        bool Visited { get; set; }
        MapPosition MapPosition { get; set; }

        List<ICommand> Commands { get; set; }

        void Enter();
    }
}
