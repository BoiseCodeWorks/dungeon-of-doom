using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCW.ConsoleGame.User
{
    public interface IUserInterface
    {
        void Display(string text);
        string GetInput(string prompt);
        void Clear();
    }
}
