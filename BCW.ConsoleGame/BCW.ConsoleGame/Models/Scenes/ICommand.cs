using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Scenes
{
    public interface ICommand
    {
        string Keys { get; set; }
        string Description { get; set; }
        Action Action { get; set; }
    }
}
