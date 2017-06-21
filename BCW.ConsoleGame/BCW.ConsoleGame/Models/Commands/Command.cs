using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.Models.Commands
{
    public class Command : ICommand
    {
        public string Keys { get; set; }
        public string Description { get; set; }
        public Action Action { get; set; }
    }
}
