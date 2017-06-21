using BCW.ConsoleGame.Events;
using BCW.ConsoleGame.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Scenes
{
    public interface IScene
    {
        event EventHandler<GameEventArgs> GameMenuSelected;
        event EventHandler<NavigationEventArgs> Navigated;

        string Title { get; set; }
        string Description { get; set; }
        bool Visited { get; set; }
        MapPosition MapPosition { get; set; }

        List<ICommand> Commands { get; set; }

        void Enter();
    }
}
