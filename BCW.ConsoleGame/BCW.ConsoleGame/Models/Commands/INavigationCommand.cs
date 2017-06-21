using BCW.ConsoleGame.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Commands
{
    public interface INavigationCommand
    {
        Direction Direction { get; set; }
    }
}
